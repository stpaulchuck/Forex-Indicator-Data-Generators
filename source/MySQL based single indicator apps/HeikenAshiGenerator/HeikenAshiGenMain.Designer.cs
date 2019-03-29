namespace HeikenAshiGenerator
{
    partial class HeikenAshiGenMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				oMySqlHandler.Dispose();
				if (components != null)
					components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeikenAshiGenMain));
			this.label7 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblDaysAvailable = new System.Windows.Forms.Label();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDays = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbServerName = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbDatabaseNames = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbTableNames = new System.Windows.Forms.ComboBox();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(89, 186);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(162, 17);
			this.label7.TabIndex = 51;
			this.label7.Text = "Days Of Data Available: ";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 352);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(582, 28);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 50;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Padding = new System.Windows.Forms.Padding(3);
			this.StatusLabel.Size = new System.Drawing.Size(435, 23);
			this.StatusLabel.Spring = true;
			this.StatusLabel.Text = "Idle ...";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(132, 23);
			this.toolStripStatusLabel2.Text = "HA Data Gen. V2.0";
			// 
			// lblDaysAvailable
			// 
			this.lblDaysAvailable.BackColor = System.Drawing.Color.White;
			this.lblDaysAvailable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDaysAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDaysAvailable.Location = new System.Drawing.Point(109, 213);
			this.lblDaysAvailable.Name = "lblDaysAvailable";
			this.lblDaysAvailable.Size = new System.Drawing.Size(121, 26);
			this.lblDaysAvailable.TabIndex = 52;
			this.lblDaysAvailable.Text = "{not set}";
			this.lblDaysAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnExit
			// 
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(238, 283);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(87, 31);
			this.btnExit.TabIndex = 49;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAbort.Location = new System.Drawing.Point(369, 283);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(87, 31);
			this.btnAbort.TabIndex = 48;
			this.btnAbort.Text = "Abort!";
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(314, 189);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 17);
			this.label5.TabIndex = 47;
			this.label5.Text = "Days to generate.";
			// 
			// txtDays
			// 
			this.txtDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDays.Location = new System.Drawing.Point(329, 216);
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(79, 23);
			this.txtDays.TabIndex = 46;
			this.txtDays.Text = "100";
			this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDays.WordWrap = false;
			// 
			// btnGenerate
			// 
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(89, 283);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(95, 31);
			this.btnGenerate.TabIndex = 45;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(69, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 17);
			this.label2.TabIndex = 40;
			this.label2.Text = "Pick a server.";
			// 
			// cmbServerName
			// 
			this.cmbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbServerName.FormattingEnabled = true;
			this.cmbServerName.Location = new System.Drawing.Point(58, 53);
			this.cmbServerName.Name = "cmbServerName";
			this.cmbServerName.Size = new System.Drawing.Size(141, 24);
			this.cmbServerName.TabIndex = 39;
			this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbServerName_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(345, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 17);
			this.label1.TabIndex = 54;
			this.label1.Text = "Pick a Database";
			// 
			// cmbDatabaseNames
			// 
			this.cmbDatabaseNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDatabaseNames.FormattingEnabled = true;
			this.cmbDatabaseNames.Location = new System.Drawing.Point(269, 53);
			this.cmbDatabaseNames.Name = "cmbDatabaseNames";
			this.cmbDatabaseNames.Size = new System.Drawing.Size(254, 24);
			this.cmbDatabaseNames.TabIndex = 53;
			this.cmbDatabaseNames.Text = "{not set}";
			this.cmbDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(187, 105);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(138, 17);
			this.label6.TabIndex = 56;
			this.label6.Text = "Pick The Input Table";
			// 
			// cmbTableNames
			// 
			this.cmbTableNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTableNames.FormattingEnabled = true;
			this.cmbTableNames.Location = new System.Drawing.Point(127, 133);
			this.cmbTableNames.Name = "cmbTableNames";
			this.cmbTableNames.Size = new System.Drawing.Size(254, 24);
			this.cmbTableNames.TabIndex = 55;
			this.cmbTableNames.Text = "{not set}";
			this.cmbTableNames.SelectedIndexChanged += new System.EventHandler(this.cmbTableName_SelectedIndexChanged);
			// 
			// HeikenAshiGenMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(582, 380);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cmbTableNames);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbDatabaseNames);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.lblDaysAvailable);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtDays);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbServerName);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "HeikenAshiGenMain";
			this.Text = "Heiken Ashi Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HeikenAshiGenMain_FormClosing);
			this.Shown += new System.EventHandler(this.HeikenAshiGenMain_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label lblDaysAvailable;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabaseNames;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTableNames;
    }
}

