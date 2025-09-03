using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pøíjem_tankování
{
    public partial class VylouceneSPZForm : Form
    {
        public VylouceneSPZForm()
        {
            InitializeComponent();
            this.Load += VylouceneSPZForm_Load;
        }

        public DataGridView DataGrid => dataGridView1;

        private DataGridView dataGridView1;
        private Button btnSave;
        private Button btnSaveAndClose;
        private Button btnCloseWithoutSave;

        private void InitializeComponent()
        {
            this.dataGridView1 = new DataGridView();
            this.btnSave = new Button();
            this.btnSaveAndClose = new Button();
            this.btnCloseWithoutSave = new Button();

            var spzColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "SPZ",
                Name = "SPZ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            var noteColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Poznámka",
                Name = "Poznamka",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { spzColumn, noteColumn });
            this.dataGridView1.Dock = DockStyle.Top;
            this.dataGridView1.AllowUserToAddRows = true;
            this.dataGridView1.AllowUserToDeleteRows = true;
            this.dataGridView1.Height = 200;

            // Tlaèítko Uložit
            this.btnSave.Text = "Uložit";
            this.btnSave.Location = new System.Drawing.Point(10, 210);
            this.btnSave.Size = new System.Drawing.Size(110, 30);
            this.btnSave.Click += BtnSave_Click;

            // Tlaèítko Uložit a Zavøít
            this.btnSaveAndClose.Text = "Uložit a Zavøít";
            this.btnSaveAndClose.Location = new System.Drawing.Point(130, 210);
            this.btnSaveAndClose.Size = new System.Drawing.Size(120, 30);
            this.btnSaveAndClose.Click += BtnSaveAndClose_Click;

            // Tlaèítko Zavøít bez Uložení
            this.btnCloseWithoutSave.Text = "Zavøít bez Uložení";
            this.btnCloseWithoutSave.Location = new System.Drawing.Point(260, 210);
            this.btnCloseWithoutSave.Size = new System.Drawing.Size(130, 30);
            this.btnCloseWithoutSave.Click += BtnCloseWithoutSave_Click;

            this.ClientSize = new System.Drawing.Size(400, 260);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnCloseWithoutSave);
            this.Text = "Vylouèené SPZ";
        }

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "vyloucene_spz.csv");
            SaveSPZList(filePath);
            MessageBox.Show("Seznam byl uložen.", "Uloženo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSaveAndClose_Click(object sender, System.EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "vyloucene_spz.csv");
            SaveSPZList(filePath);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCloseWithoutSave_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void SaveSPZList(string filePath)
        {
            var lines = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                var spz = row.Cells["SPZ"].Value?.ToString() ?? "";
                var note = row.Cells["Poznamka"].Value?.ToString() ?? "";
                lines.Add($"{spz};{note}");
            }
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        public void LoadSPZList(string filePath)
        {
            if (!File.Exists(filePath)) return;
            dataGridView1.Rows.Clear();
            foreach (var line in File.ReadAllLines(filePath, Encoding.UTF8))
            {
                var parts = line.Split(';');
                if (parts.Length > 0)
                {
                    var idx = dataGridView1.Rows.Add();
                    dataGridView1.Rows[idx].Cells["SPZ"].Value = parts[0];
                    if (parts.Length > 1)
                        dataGridView1.Rows[idx].Cells["Poznamka"].Value = parts[1];
                }
            }
        }

        private void VylouceneSPZForm_Load(object sender, System.EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "vyloucene_spz.csv");
            LoadSPZList(filePath);
        }
    }
}