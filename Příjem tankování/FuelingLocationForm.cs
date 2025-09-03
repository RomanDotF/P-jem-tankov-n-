using System.Windows.Forms;

namespace P��jem_tankov�n�
{
    public partial class FuelingLocationForm : Form
    {
        public string SelectedLocation => comboBoxFuelingLocation.SelectedItem?.ToString();

        public FuelingLocationForm()
        {
            InitializeComponent();
        }

        private ComboBox comboBoxFuelingLocation;
        private Button btnOk;

        private void InitializeComponent()
        {
            this.comboBoxFuelingLocation = new ComboBox();
            this.btnOk = new Button();

            // ComboBox
            this.comboBoxFuelingLocation.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxFuelingLocation.Items.AddRange(new object[] { "L�pa", "B�nov", "Vala�sk� Mezi����" });
            this.comboBoxFuelingLocation.Location = new System.Drawing.Point(20, 20);
            this.comboBoxFuelingLocation.Size = new System.Drawing.Size(150, 23);

            // Button
            this.btnOk.Text = "OK";
            this.btnOk.Location = new System.Drawing.Point(20, 60);
            this.btnOk.Size = new System.Drawing.Size(150, 30);
            this.btnOk.DialogResult = DialogResult.OK;

            // Form
            this.ClientSize = new System.Drawing.Size(200, 110);
            this.Controls.Add(this.comboBoxFuelingLocation);
            this.Controls.Add(this.btnOk);
            this.Text = "Vyberte m�sto tankov�n�";
            this.AcceptButton = this.btnOk;
        }
    }
}