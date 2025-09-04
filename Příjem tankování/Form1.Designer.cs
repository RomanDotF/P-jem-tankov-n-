namespace Příjem_tankování
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Nástroje AutoPlan - LUKROM, spol. s r.o.";
            this.ResumeLayout(false);
            this.PerformLayout();
            // 
            // labelNastaveni
            // 
            this.labelNastaveni = new System.Windows.Forms.Label();
            this.labelNastaveni.Location = new System.Drawing.Point(20, 120);
            this.labelNastaveni.Size = new System.Drawing.Size(300, 20);
            this.labelNastaveni.Name = "labelNastaveni";
            this.labelNastaveni.Text = "Nastavení zpracování dat";
            this.labelNastaveni.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Controls.Add(this.labelNastaveni);
            // 
            // labelNastroje
            // 
            this.labelNastroje = new System.Windows.Forms.Label();
            this.labelNastroje.Location = new System.Drawing.Point(20, 40); // zleva, shora
            this.labelNastroje.Size = new System.Drawing.Size(300, 20);
            this.labelNastroje.Name = "labelNastroje";
            this.labelNastroje.Text = "Převod transakcí do AutoPlan CSV";
            this.labelNastroje.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Controls.Add(this.labelNastroje);
            // 
            // btnPrevodUniPOS
            // 
            this.btnPrevodUniPOS = new System.Windows.Forms.Button();
            this.btnPrevodUniPOS.Location = new System.Drawing.Point(20, 75); // posunuto níž
            this.btnPrevodUniPOS.Size = new System.Drawing.Size(150, 30);
            this.btnPrevodUniPOS.Name = "btnPrevodUniPOS";
            this.btnPrevodUniPOS.Text = "Převod UniPOS";
            this.btnPrevodUniPOS.UseVisualStyleBackColor = true;
            this.btnPrevodUniPOS.Click += new System.EventHandler(this.btnPrevodUniPOS_Click);
            this.Controls.Add(this.btnPrevodUniPOS);
            //
            // Formulář vyloučené SPZ
            //
            this.btnVylouceneSPZ = new System.Windows.Forms.Button();
            this.btnVylouceneSPZ.Location = new System.Drawing.Point(20, 155); // posunuto níž
            this.btnVylouceneSPZ.Size = new System.Drawing.Size(150, 30);
            this.btnVylouceneSPZ.Name = "btnVylouceneSPZ";
            this.btnVylouceneSPZ.Text = "Vyloučené SPZ";
            this.btnVylouceneSPZ.UseVisualStyleBackColor = true;
            this.btnVylouceneSPZ.Click += new System.EventHandler(this.btnVylouceneSPZ_Click);
            this.Controls.Add(this.btnVylouceneSPZ);
            //
            // btnOsobniKarty
            //
            this.btnOsobniKarty = new System.Windows.Forms.Button();
            this.btnOsobniKarty.Location = new System.Drawing.Point(200, 155); // posunuto níž
            this.btnOsobniKarty.Size = new System.Drawing.Size(150, 30);
            this.btnOsobniKarty.Name = "btnOsobniKarty";
            this.btnOsobniKarty.Text = "Osobní karty";
            this.btnOsobniKarty.UseVisualStyleBackColor = true;
            this.btnOsobniKarty.Click += new System.EventHandler(this.btnOsobniKarty_Click);
            this.Controls.Add(this.btnOsobniKarty);

            // btnPrevodSelfServiceSystem
            this.btnPrevodSelfServiceSystem = new System.Windows.Forms.Button();
            this.btnPrevodSelfServiceSystem.Location = new System.Drawing.Point(200, 75);
            this.btnPrevodSelfServiceSystem.Size = new System.Drawing.Size(200, 30);
            this.btnPrevodSelfServiceSystem.Name = "btnPrevodSelfServiceSystem";
            this.btnPrevodSelfServiceSystem.Text = "Převod SelfServiceSystem";
            this.btnPrevodSelfServiceSystem.UseVisualStyleBackColor = true;
            this.btnPrevodSelfServiceSystem.Click += new System.EventHandler(this.btnPrevodSelfServiceSystem_Click);
            this.Controls.Add(this.btnPrevodSelfServiceSystem);

            // btnOmvOdebratMyto
            this.btnOmvOdebratMyto = new System.Windows.Forms.Button();
            this.btnOmvOdebratMyto.Location = new System.Drawing.Point(420, 75);
            this.btnOmvOdebratMyto.Size = new System.Drawing.Size(180, 30);
            this.btnOmvOdebratMyto.Name = "btnOmvOdebratMyto";
            this.btnOmvOdebratMyto.Text = "OMV odebrat mýto";
            this.btnOmvOdebratMyto.UseVisualStyleBackColor = true;
            this.btnOmvOdebratMyto.Click += new System.EventHandler(this.btnOmvOdebratMyto_Click);
            this.Controls.Add(this.btnOmvOdebratMyto);

            // menuStrip1
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuSoubor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPomoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOaplikaci = new System.Windows.Forms.ToolStripMenuItem();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuSoubor,
                this.menuPomoc
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.Controls.Add(this.menuStrip1);

            // menuSoubor
            this.menuSoubor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuKonec
            });
            this.menuSoubor.Name = "menuSoubor";
            this.menuSoubor.Text = "Soubor";

            // menuKonec
            this.menuKonec.Name = "menuKonec";
            this.menuKonec.Text = "Konec programu";
            this.menuKonec.Click += new System.EventHandler(this.menuKonec_Click);

            // menuPomoc
            this.menuPomoc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuOaplikaci
            });
            this.menuPomoc.Name = "menuPomoc";
            this.menuPomoc.Text = "Pomoc";

            // menuOaplikaci
            this.menuOaplikaci.Name = "menuOaplikaci";
            this.menuOaplikaci.Text = "O aplikaci";
            this.menuOaplikaci.Click += new System.EventHandler(this.menuOaplikaci_Click);

            // menuNapoveda
            this.menuNapoveda = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNapoveda.Name = "menuNapoveda";
            this.menuNapoveda.Text = "Nápověda";
            this.menuNapoveda.Click += new System.EventHandler(this.menuNapoveda_Click);
            this.menuPomoc.DropDownItems.Add(this.menuNapoveda);
        }

        #endregion
        // Deklarace
        private System.Windows.Forms.Label labelNastroje;
        private System.Windows.Forms.Label labelNastaveni;
        private System.Windows.Forms.Button btnPrevodUniPOS;
        private System.Windows.Forms.ComboBox comboBoxFuelingLocation;
        private System.Windows.Forms.Button btnVylouceneSPZ;
        private System.Windows.Forms.Button btnOsobniKarty;
        private System.Windows.Forms.Button btnPrevodSelfServiceSystem;
        private System.Windows.Forms.Button btnOmvOdebratMyto;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSoubor;
        private System.Windows.Forms.ToolStripMenuItem menuKonec;
        private System.Windows.Forms.ToolStripMenuItem menuPomoc;
        private System.Windows.Forms.ToolStripMenuItem menuOaplikaci;
        private System.Windows.Forms.ToolStripMenuItem menuNapoveda;
    }
}
