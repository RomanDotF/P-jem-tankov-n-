using System.Windows.Forms;

namespace P��jem_tankov�n�
{
    public partial class NapovedaForm : Form
    {
        public NapovedaForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "N�pov�da - N�stroje AutoPlan";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(700, 500);

            var textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new System.Drawing.Font("Consolas", 10);
            textBox.Text =
@"Pou�it� p��kazov� ��dky aplikace N�stroje AutoPlan:

P�evod UniPOS:
  prevod-unipos <vstup.csv> <vystup.csv> <misto_tankovani>
    <vstup.csv>         cesta ke vstupn�mu UniPOS CSV souboru
    <vystup.csv>        cesta k v�stupn�mu CSV souboru
    <misto_tankovani>   n�zev m�sta tankov�n� (nap�. ""L�pa"", ""B�nov"")

P�evod SelfServiceSystem:
  prevod-selfservicesystem <vstup.csv> <vystup.csv> <misto_tankovani>
    <vstup.csv>         cesta ke vstupn�mu SelfServiceSystem CSV souboru
    <vystup.csv>        cesta k v�stupn�mu CSV souboru
    <misto_tankovani>   n�zev m�sta tankov�n� (nap�. ""Vala�sk� Mezi����"")

Odebr�n� m�ta z datov�ho souboru:
  odebrat-myto <vstup.dat> <vystup.dat>
    <vstup.dat>         cesta ke vstupn�mu souboru (dat/csv/txt)
    <vystup.dat>        cesta k v�stupn�mu souboru bez m�ta

P��klad:
  P��jem_tankov�n�.exe prevod-unipos data.csv vystup.csv L�pa
  P��jem_tankov�n�.exe prevod-selfservicesystem sss.csv vystup.csv ""Vala�sk� Mezi����""
  P��jem_tankov�n�.exe odebrat-myto omv.dat omv_bezmyta.dat

Pro spu�t�n� s grafick�m rozhran�m spus�te aplikaci bez argument�.";

            this.Controls.Add(textBox);
        }
    }
}