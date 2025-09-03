using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Příjem_tankování
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<UniPOS> ImportUniPOSFromCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath, Encoding.UTF8);

            if (lines.Length < 3)
                throw new InvalidOperationException("CSV soubor neobsahuje dostatek řádků.");

            // První řádek = metadata (můžete si jej uložit, pokud potřebujete)
            string metadata = lines[0];

            // Druhý řádek = hlavička
            var headers = lines[1].Split(';');

            // Ostatní řádky = data
            var dataLines = lines.Skip(2);

            var result = new List<UniPOS>();
            foreach (var line in dataLines)
            {
                var values = line.Split(';');
                var uniPos = new UniPOS();

                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    var prop = typeof(UniPOS).GetProperty(headers[i]);
                    if (prop != null)
                        prop.SetValue(uniPos, values[i]);
                }

                result.Add(uniPos);
            }

            return result;
        }

        public List<AutoPlan> MapUniPOSToAutoPlan(List<UniPOS> uniPosList, string fuelingLocation)
        {
            return uniPosList.Select(u => new AutoPlan
            {
                Vehicle = (u.CAR_DESIGNATION ?? "")
                    .Replace("\"", "")      // odebrat uvozovky
                    .Replace(" ", "")       // odebrat mezery
                    .ToUpperInvariant(),    // převést na velká písmena
                Ride_StartTime = "",
                Ride_StartLocation = "",
                Ride_StopTime = "",
                Ride_StopLocation = "",
                Ride_Country = "",
                Ride_Distance = "",
                Ride_Driver = "",
                Ride_Purpose = "",
                Fueling_Time = $"{u.DATE} {u.TIME}",
                Fueling_Location = fuelingLocation,
                Fueling_FuelType = u.PRODUCT,
                Fueling_Quantity = u.AMOUNT,
                Fueling_PurchaseType = "",
                Fueling_Price = "",
                Fueling_Currency = "",
                Fueling_VatRate = "",
                Fueling_PriceNetOfVat = "",
                Odometer_Time = "",
                Odometer_Value = ""
            }).ToList();
        }

        public void ExportAutoPlanListToCsv(IEnumerable<AutoPlan> data, string filePath, string metadata)
        {
            var columns = new[]
            {
                "Vehicle","Ride_StartTime","Ride_StartLocation","Ride_StopTime","Ride_StopLocation","Ride_Country","Ride_Distance","Ride_Driver","Ride_Purpose",
                "Fueling_Time","Fueling_Location","Fueling_FuelType","Fueling_Quantity","Fueling_PurchaseType","Fueling_Price","Fueling_Currency","Fueling_VatRate","Fueling_PriceNetOfVat",
                "Odometer_Time","Odometer_Value"
            };
            var csv = new StringBuilder();
            csv.AppendLine(string.Join(";", columns));
            foreach (var item in data)
            {
                var line = string.Join(";", columns.Select(col => (typeof(AutoPlan).GetProperty(col).GetValue(item) ?? "").ToString()));
                csv.AppendLine(line);
            }
            File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
        }
        private void btnPrevodUniPOS_Click(object sender, EventArgs e)
        {
            // Zobrazit dialog pro výběr místa tankování
            using var dlg = new FuelingLocationForm();
            if (dlg.ShowDialog(this) != DialogResult.OK || string.IsNullOrEmpty(dlg.SelectedLocation))
            {
                MessageBox.Show("Nebylo vybráno místo tankování.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string fuelingLocation = dlg.SelectedLocation;

            // Výběr vstupního souboru
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV soubory (*.csv)|*.csv|Všechny soubory (*.*)|*.*",
                Title = "Vyberte vstupní UniPOS CSV"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string inputPath = openFileDialog.FileName;

            // Výběr výstupního souboru
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV soubory (*.csv)|*.csv|Všechny soubory (*.*)|*.*",
                Title = "Uložit reorganizovaný CSV",
                FileName = Path.GetFileNameWithoutExtension(inputPath) + "_AutoPlan.csv"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string outputPath = saveFileDialog.FileName;

            // 1. Načtení seznamu vyloučených SPZ (všechno na velká písmena, bez uvozovek)
            string spzFilePath = Path.Combine(Application.StartupPath, "vyloucene_spz.csv");
            var excludedSpz = new HashSet<string>(
                File.Exists(spzFilePath)
                    ? File.ReadAllLines(spzFilePath, Encoding.UTF8)
                        .Select(line => line.Split(';')[0].Trim().Trim('"').Replace(" ", "").ToUpperInvariant())
                        .Where(spz => !string.IsNullOrEmpty(spz))
                    : Enumerable.Empty<string>()
            );

            // 2. Načtení osobních karet (PERSON_IDENT_NO -> SPZ)
            string osobniKartyPath = Path.Combine(Application.StartupPath, "osobni_karty.csv");
            var osobniKarty = new Dictionary<string, string>(); // karta -> SPZ
            if (File.Exists(osobniKartyPath))
            {
                foreach (var line in File.ReadAllLines(osobniKartyPath, Encoding.UTF8))
                {
                    var parts = line.Split(';');
                    if (parts.Length > 1)
                    {
                        var karta = parts[0].Trim().Trim('"');
                        var spz = parts[1].Trim();
                        if (!string.IsNullOrEmpty(karta) && !string.IsNullOrEmpty(spz))
                            osobniKarty[karta] = spz;
                    }
                }
            }

            // 3. Načtení a zpracování dat
            var uniPosListRaw = ImportUniPOSFromCsv(inputPath);
            var uniPosList = new List<UniPOS>();

            foreach (var u in uniPosListRaw)
            {
                string carDesignation = (u.CAR_DESIGNATION ?? "").Trim().Trim('"');
                if (string.IsNullOrWhiteSpace(carDesignation))
                {
                    // Pokud je CAR_DESIGNATION prázdné, zkusit PERSON_IDENT_NO jako osobní kartu
                    string karta = (u.PERSON_IDENT_NO ?? "").Trim().Trim('"');
                    if (!string.IsNullOrEmpty(karta) && osobniKarty.TryGetValue(karta, out var spzFromKarta))
                    {
                        // Použij SPZ z osobní karty
                        u.CAR_DESIGNATION = spzFromKarta;
                        carDesignation = spzFromKarta;
                    }
                }

                // Vynechat, pokud je stále prázdné nebo je ve vyloučených SPZ
                if (string.IsNullOrWhiteSpace(carDesignation) ||
                    excludedSpz.Contains(carDesignation.ToUpperInvariant()))
                {
                    continue;
                }

                uniPosList.Add(u);
            }

            var autoPlanList = MapUniPOSToAutoPlan(uniPosList, fuelingLocation);

            // Metadata z prvního řádku vstupního souboru
            var metadata = File.ReadLines(inputPath, Encoding.UTF8).FirstOrDefault() ?? "";

            // Export
            ExportAutoPlanListToCsv(autoPlanList, outputPath, metadata);

            MessageBox.Show("Převod dokončen.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnVylouceneSPZ_Click(object sender, EventArgs e)
        {
            using var dlg = new VylouceneSPZForm();
            dlg.ShowDialog(this);
        }
        private void btnOsobniKarty_Click(object sender, EventArgs e)
        {
            using var dlg = new OsobniKartyForm();
            dlg.ShowDialog(this);
        }
        private void btnPrevodSelfServiceSystem_Click(object sender, EventArgs e)
        {
            // Zobrazit dialog pro výběr místa tankování
            using var dlg = new FuelingLocationForm();
            if (dlg.ShowDialog(this) != DialogResult.OK || string.IsNullOrEmpty(dlg.SelectedLocation))
            {
                MessageBox.Show("Nebylo vybráno místo tankování.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string fuelingLocation = dlg.SelectedLocation;

            // Výběr vstupního souboru
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV soubory (*.csv)|*.csv|Všechny soubory (*.*)|*.*",
                Title = "Vyberte vstupní SelfServiceSystem CSV"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string inputPath = openFileDialog.FileName;

            // Výběr výstupního souboru
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV soubory (*.csv)|*.csv|Všechny soubory (*.*)|*.*",
                Title = "Uložit reorganizovaný CSV",
                FileName = Path.GetFileNameWithoutExtension(inputPath) + "_AutoPlan.csv"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string outputPath = saveFileDialog.FileName;

            // Načtení a převod dat
            var selfServiceList = ImportSelfServiceSystemFromCsv(inputPath);

            // Načtení seznamu vyloučených SPZ (stejná logika jako jinde)
            string spzFilePath = Path.Combine(Application.StartupPath, "vyloucene_spz.csv");
            var excludedSpz = new HashSet<string>(
                File.Exists(spzFilePath)
                    ? File.ReadAllLines(spzFilePath, Encoding.UTF8)
                        .Select(line => line.Split(';')[0].Trim().Trim('"').Replace(" ", "").ToUpperInvariant())
                        .Where(spz => !string.IsNullOrEmpty(spz))
                    : Enumerable.Empty<string>()
            );

            ExportSelfServiceSystemToAutoPlanCsv(selfServiceList, outputPath, fuelingLocation, excludedSpz);

            MessageBox.Show("Převod dokončen.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public List<SelfServiceSystem> ImportSelfServiceSystemFromCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            var result = new List<SelfServiceSystem>();
            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values.Length < 23) continue;
                result.Add(new SelfServiceSystem
                {
                    Column1 = values[0],
                    Column2 = values[1],
                    Column3 = values[2],
                    Column4 = values[3],
                    Column5 = values[4],
                    Column6 = values[5],
                    Column7 = values[6],
                    Column8 = values[7],
                    Column9 = values[8],
                    Column10 = values[9],
                    Column11 = values[10],
                    Column12 = values[11],
                    Column13 = values[12],
                    Column14 = values[13],
                    Column15 = values[14],
                    Column16 = values[15],
                    Column17 = values[16],
                    Column18 = values[17],
                    Column19 = values[18],
                    Column20 = values[19],
                    Column21 = values[20],
                    Column22 = values[21],
                    Column23 = values[22]
                });
            }
            return result;
        }

        public void ExportSelfServiceSystemToAutoPlanCsv(
            IEnumerable<SelfServiceSystem> data,
            string filePath,
            string fuelingLocation,
            HashSet<string> excludedSpz)
        {
            var columns = new[]
            {
                "Vehicle","Ride_StartTime","Ride_StartLocation","Ride_StopTime","Ride_StopLocation","Ride_Country","Ride_Distance","Ride_Driver","Ride_Purpose",
                "Fueling_Time","Fueling_Location","Fueling_FuelType","Fueling_Quantity","Fueling_PurchaseType","Fueling_Price","Fueling_Currency","Fueling_VatRate","Fueling_PriceNetOfVat",
                "Odometer_Time","Odometer_Value"
            };
            var csv = new StringBuilder();
            csv.AppendLine(string.Join(";", columns));
            foreach (var item in data)
            {
                string vehicle = (item.Column14 ?? "")
                    .Replace("\"", "")
                    .Replace(" ", "")
                    .ToUpperInvariant();

                // Kontrola na vyloučené SPZ
                if (string.IsNullOrWhiteSpace(vehicle) || excludedSpz.Contains(vehicle))
                    continue;

                string date = (item.Column11 ?? "").Replace("\"", "");
                string time = (item.Column12 ?? "").Replace("\"", "");
                string fuelingTime = $"{date} {time}".Trim();

                string fuelingQuantity = "";
                if (decimal.TryParse(item.Column17, NumberStyles.Any, CultureInfo.InvariantCulture, out var amount))
                    fuelingQuantity = (amount / 100m).ToString("0.##", CultureInfo.InvariantCulture);

                var values = new[]
                {
                    vehicle,                // Vehicle
                    "",                     // Ride_StartTime
                    "",                     // Ride_StartLocation
                    "",                     // Ride_StopTime
                    "",                     // Ride_StopLocation
                    "",                     // Ride_Country
                    "",                     // Ride_Distance
                    "",                     // Ride_Driver
                    "",                     // Ride_Purpose
                    fuelingTime,            // Fueling_Time
                    fuelingLocation,        // Fueling_Location
                    "",                     // Fueling_FuelType
                    fuelingQuantity,        // Fueling_Quantity
                    "",                     // Fueling_PurchaseType
                    "",                     // Fueling_Price
                    "",                     // Fueling_Currency
                    "",                     // Fueling_VatRate
                    "",                     // Fueling_PriceNetOfVat
                    "",                     // Odometer_Time
                    ""                      // Odometer_Value
                };
                csv.AppendLine(string.Join(";", values));
            }
            File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
        }
        private void btnOmvOdebratMyto_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Datové soubory (*.dat|*.dat",
                Title = "Vyberte vstupní soubor OMV"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string inputPath = openFileDialog.FileName;

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Datové soubory (*.dat)|*.dat",
                Title = "Uložit soubor bez mýta",
                FileName = Path.GetFileNameWithoutExtension(inputPath) + "_bezMyta" + Path.GetExtension(inputPath)
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string outputPath = saveFileDialog.FileName;

            // Filtrace řádků
            var lines = File.ReadAllLines(inputPath, Encoding.UTF8);
            var filtered = lines
                .Where(line =>
                    !line.Contains("post-pay", StringComparison.OrdinalIgnoreCase) &&
                    !line.Contains("CZMyto", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            File.WriteAllLines(outputPath, filtered, Encoding.UTF8);

            MessageBox.Show("Hotovo. Soubor byl uložen bez řádků obsahujících mýto.", "Dokončeno", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
