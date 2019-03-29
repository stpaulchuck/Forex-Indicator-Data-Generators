namespace InvestmentApps.RVIGenerator
{
    partial class RVIGenMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RVIGenMain));
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbTableNames = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbDatabaseNames = new System.Windows.Forms.ComboBox();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDays = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.lblDaysAvailable = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.VersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.txtRVIspan = new System.Windows.Forms.TextBox();
			this.cmbServerName = new System.Windows.Forms.ComboBox();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(241, 166);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(101, 17);
			this.label6.TabIndex = 97;
			this.label6.Text = "Days Available";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(355, 79);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(138, 17);
			this.label8.TabIndex = 95;
			this.label8.Text = "Pick The Input Table";
			// 
			// cmbTableNames
			// 
			this.cmbTableNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTableNames.FormattingEnabled = true;
			this.cmbTableNames.Location = new System.Drawing.Point(295, 107);
			this.cmbTableNames.Name = "cmbTableNames";
			this.cmbTableNames.Size = new System.Drawing.Size(254, 24);
			this.cmbTableNames.TabIndex = 94;
			this.cmbTableNames.Text = "{not set}";
			this.cmbTableNames.SelectedIndexChanged += new System.EventHandler(this.cmbTableNames_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(58, 79);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 17);
			this.label1.TabIndex = 93;
			this.label1.Text = "Pick Data Input Database";
			// 
			// cmbDatabaseNames
			// 
			this.cmbDatabaseNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDatabaseNames.FormattingEnabled = true;
			this.cmbDatabaseNames.Location = new System.Drawing.Point(23, 107);
			this.cmbDatabaseNames.Name = "cmbDatabaseNames";
			this.cmbDatabaseNames.Size = new System.Drawing.Size(254, 24);
			this.cmbDatabaseNames.TabIndex = 92;
			this.cmbDatabaseNames.Text = "{not set}";
			this.cmbDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
			// 
			// btnExit
			// 
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(404, 260);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(87, 31);
			this.btnExit.TabIndex = 91;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAbort.Location = new System.Drawing.Point(243, 260);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(87, 31);
			this.btnAbort.TabIndex = 90;
			this.btnAbort.Text = "Abort!";
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(387, 166);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 17);
			this.label5.TabIndex = 89;
			this.label5.Text = "Days to generate.";
			// 
			// txtDays
			// 
			this.txtDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDays.Location = new System.Drawing.Point(404, 191);
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(79, 23);
			this.txtDays.TabIndex = 88;
			this.txtDays.Text = "100";
			this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDays.WordWrap = false;
			// 
			// btnGenerate
			// 
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(74, 260);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(95, 31);
			this.btnGenerate.TabIndex = 87;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(224, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 17);
			this.label2.TabIndex = 80;
			this.label2.Text = "Pick a server.";
			// 
			// lblDaysAvailable
			// 
			this.lblDaysAvailable.BackColor = System.Drawing.SystemColors.Window;
			this.lblDaysAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDaysAvailable.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lblDaysAvailable.Location = new System.Drawing.Point(251, 191);
			this.lblDaysAvailable.Name = "lblDaysAvailable";
			this.lblDaysAvailable.Size = new System.Drawing.Size(79, 23);
			this.lblDaysAvailable.TabIndex = 96;
			this.lblDaysAvailable.Text = "{not set]";
			this.lblDaysAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lblDaysAvailable.WordWrap = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.VersionLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 335);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(572, 22);
			this.statusStrip1.TabIndex = 85;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(472, 17);
			this.StatusLabel.Spring = true;
			this.StatusLabel.Text = "Idle ...";
			// 
			// VersionLabel
			// 
			this.VersionLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.VersionLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.VersionLabel.Size = new System.Drawing.Size(85, 17);
			this.VersionLabel.Text = "Version 1.0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(83, 166);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 17);
			this.label3.TabIndex = 82;
			this.label3.Text = "RVI Span";
			// 
			// txtRVIspan
			// 
			this.txtRVIspan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRVIspan.Location = new System.Drawing.Point(74, 191);
			this.txtRVIspan.Name = "txtRVIspan";
			this.txtRVIspan.Size = new System.Drawing.Size(79, 23);
			this.txtRVIspan.TabIndex = 81;
			this.txtRVIspan.Text = "10";
			this.txtRVIspan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// cmbServerName
			// 
			this.cmbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbServerName.FormattingEnabled = true;
			this.cmbServerName.Location = new System.Drawing.Point(213, 30);
			this.cmbServerName.Name = "cmbServerName";
			this.cmbServerName.Size = new System.Drawing.Size(141, 24);
			this.cmbServerName.TabIndex = 79;
			this.cmbServerName.Text = "{ not set }";
			this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbServerName_SelectedIndexChanged);
			// 
			// RVIGenMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(572, 357);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.cmbTableNames);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbDatabaseNames);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtDays);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblDaysAvailable);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtRVIspan);
			this.Controls.Add(this.cmbServerName);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RVIGenMain";
			this.Text = "RVI3 Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RVIGenMain_FormClosing);
			this.Shown += new System.EventHandler(this.RVIGenMain_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbTableNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabaseNames;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lblDaysAvailable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel VersionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRVIspan;
        private System.Windows.Forms.ComboBox cmbServerName;
    }
}

