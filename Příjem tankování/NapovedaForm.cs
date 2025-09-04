using System.Windows.Forms;

namespace Pøíjem_tankování
{
    public partial class NapovedaForm : Form
    {
        public NapovedaForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Nápovìda - Nástroje AutoPlan";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(700, 500);

            var textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new System.Drawing.Font("Consolas", 10);
            textBox.Text =
@"Pouití pøíkazové øádky aplikace Nástroje AutoPlan:

Pøevod UniPOS:
  prevod-unipos <vstup.csv> <vystup.csv> <misto_tankovani>
    <vstup.csv>         cesta ke vstupnímu UniPOS CSV souboru
    <vystup.csv>        cesta k vıstupnímu CSV souboru
    <misto_tankovani>   název místa tankování (napø. ""Lípa"", ""Bánov"")

Pøevod SelfServiceSystem:
  prevod-selfservicesystem <vstup.csv> <vystup.csv> <misto_tankovani>
    <vstup.csv>         cesta ke vstupnímu SelfServiceSystem CSV souboru
    <vystup.csv>        cesta k vıstupnímu CSV souboru
    <misto_tankovani>   název místa tankování (napø. ""Valašské Meziøíèí"")

Odebrání mıta z datového souboru:
  odebrat-myto <vstup.dat> <vystup.dat>
    <vstup.dat>         cesta ke vstupnímu souboru (dat/csv/txt)
    <vystup.dat>        cesta k vıstupnímu souboru bez mıta

Pøíklad:
  Pøíjem_tankování.exe prevod-unipos data.csv vystup.csv Lípa
  Pøíjem_tankování.exe prevod-selfservicesystem sss.csv vystup.csv ""Valašské Meziøíèí""
  Pøíjem_tankování.exe odebrat-myto omv.dat omv_bezmyta.dat

Pro spuštìní s grafickım rozhraním spuste aplikaci bez argumentù.";

            this.Controls.Add(textBox);
        }
    }
}