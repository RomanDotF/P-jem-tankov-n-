namespace Příjem_tankování
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // HELP
            if (args.Length == 1 && args[0].Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Použití příkazové řádku aplikace Nástroje AutoPlan:");
                Console.WriteLine();
                Console.WriteLine("Převod UniPOS:");
                Console.WriteLine("  prevod-unipos <vstup.csv> <vystup.csv> <misto_tankovani>");
                Console.WriteLine("    <vstup.csv>         cesta ke vstupnímu UniPOS CSV souboru");
                Console.WriteLine("    <vystup.csv>        cesta k výstupnímu CSV souboru");
                Console.WriteLine("    <misto_tankovani>   název místa tankování (např. \"Lípa\", \"Bánov\")");
                Console.WriteLine();
                Console.WriteLine("Převod SelfServiceSystem:");
                Console.WriteLine("  prevod-selfservicesystem <vstup.csv> <vystup.csv> <misto_tankovani>");
                Console.WriteLine("    <vstup.csv>         cesta ke vstupnímu SelfServiceSystem CSV souboru");
                Console.WriteLine("    <vystup.csv>        cesta k výstupnímu CSV souboru");
                Console.WriteLine("    <misto_tankovani>   název místa tankování (např. \"Valašské Meziříčí\")");
                Console.WriteLine();
                Console.WriteLine("Odebrání mýta z datového souboru:");
                Console.WriteLine("  odebrat-myto <vstup.dat> <vystup.dat>");
                Console.WriteLine("    <vstup.dat>         cesta ke vstupnímu souboru (dat/csv/txt)");
                Console.WriteLine("    <vystup.dat>        cesta k výstupnímu souboru bez mýta");
                Console.WriteLine();
                Console.WriteLine("Příklad:");
                Console.WriteLine("  Příjem_tankování.exe prevod-unipos data.csv vystup.csv Lípa");
                Console.WriteLine("  Příjem_tankování.exe prevod-selfservicesystem sss.csv vystup.csv \"Valašské Meziříčí\"");
                Console.WriteLine("  Příjem_tankování.exe odebrat-myto omv.dat omv_bezmyta.dat");
                Console.WriteLine();
                Console.WriteLine("Pro spuštění s grafickým rozhraním spusťte aplikaci bez argumentů.");
                return;
            }

            // Převod UniPOS
            if (args.Length == 4 && args[0].Equals("prevod-unipos", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string inputPath = args[1];
                    string outputPath = args[2];
                    string fuelingLocation = args[3];
                    Form1.RunUniPOSConversion(inputPath, outputPath, fuelingLocation);
                    Console.WriteLine("Převod UniPOS dokončen.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Chyba: " + ex.Message);
                    Environment.Exit(1);
                }
                return;
            }

            // Převod SelfServiceSystem
            if (args.Length == 4 && args[0].Equals("prevod-selfservicesystem", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string inputPath = args[1];
                    string outputPath = args[2];
                    string fuelingLocation = args[3];
                    Form1.RunSelfServiceSystemConversion(inputPath, outputPath, fuelingLocation);
                    Console.WriteLine("Převod SelfServiceSystem dokončen.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Chyba: " + ex.Message);
                    Environment.Exit(1);
                }
                return;
            }

            // Odebrání mýta
            if (args.Length == 3 && args[0].Equals("odebrat-myto", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string inputPath = args[1];
                    string outputPath = args[2];
                    Form1.RemoveMytoFromFile(inputPath, outputPath);
                    Console.WriteLine("Odebrání mýta dokončeno.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Chyba: " + ex.Message);
                    Environment.Exit(1);
                }
                return;
            }

            // Skrytí konzolového okna
            if (args.Length == 0)
            {
                ConsoleHelper.HideConsoleWindow();
            }

            // Spuštění GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}