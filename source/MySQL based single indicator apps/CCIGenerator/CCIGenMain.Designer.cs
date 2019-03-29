namespace CCIGenerator
{
    partial class CCIGenMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCIGenMain));
			this.btnExit = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDays = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.txtEntryCCI = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTrendCCI = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbServerName = new System.Windows.Forms.ComboBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblDaysAvailable = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbDatabaseNames = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbTableNames = new System.Windows.Forms.ComboBox();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnExit
			// 
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(229, 377);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(87, 31);
			this.btnExit.TabIndex = 33;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAbort.Location = new System.Drawing.Point(378, 377);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(87, 31);
			this.btnAbort.TabIndex = 32;
			this.btnAbort.Text = "Abort!";
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(371, 298);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 17);
			this.label5.TabIndex = 31;
			this.label5.Text = "Days to generate.";
			// 
			// txtDays
			// 
			this.txtDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDays.Location = new System.Drawing.Point(386, 325);
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(79, 23);
			this.txtDays.TabIndex = 30;
			this.txtDays.Text = "100";
			this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDays.WordWrap = false;
			this.txtDays.TextChanged += new System.EventHandler(this.txtDays_TextChanged);
			// 
			// btnGenerate
			// 
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(70, 377);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(95, 31);
			this.btnGenerate.TabIndex = 29;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// txtEntryCCI
			// 
			this.txtEntryCCI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEntryCCI.Location = new System.Drawing.Point(233, 324);
			this.txtEntryCCI.Name = "txtEntryCCI";
			this.txtEntryCCI.Size = new System.Drawing.Size(79, 23);
			this.txtEntryCCI.TabIndex = 27;
			this.txtEntryCCI.Text = "6";
			this.txtEntryCCI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtEntryCCI.TextChanged += new System.EventHandler(this.txtEntryCCI_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(225, 297);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 17);
			this.label4.TabIndex = 26;
			this.label4.Text = "Entry CCI Period";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(51, 297);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 17);
			this.label3.TabIndex = 25;
			this.label3.Text = "Trend CCI Period";
			// 
			// txtTrendCCI
			// 
			this.txtTrendCCI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTrendCCI.Location = new System.Drawing.Point(70, 324);
			this.txtTrendCCI.Name = "txtTrendCCI";
			this.txtTrendCCI.Size = new System.Drawing.Size(79, 23);
			this.txtTrendCCI.TabIndex = 24;
			this.txtTrendCCI.Text = "14";
			this.txtTrendCCI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtTrendCCI.TextChanged += new System.EventHandler(this.txtTrendCCI_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(47, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 17);
			this.label2.TabIndex = 23;
			this.label2.Text = "Pick a server.";
			// 
			// cmbServerName
			// 
			this.cmbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbServerName.FormattingEnabled = true;
			this.cmbServerName.Location = new System.Drawing.Point(36, 58);
			this.cmbServerName.Name = "cmbServerName";
			this.cmbServerName.Size = new System.Drawing.Size(141, 24);
			this.cmbServerName.TabIndex = 22;
			this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbServerName_SelectedIndexChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 457);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(564, 28);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 34;
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
			this.StatusLabel.Size = new System.Drawing.Size(413, 23);
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
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(136, 23);
			this.toolStripStatusLabel2.Text = "CCI Data Gen. V2.0";
			// 
			// lblDaysAvailable
			// 
			this.lblDaysAvailable.BackColor = System.Drawing.Color.White;
			this.lblDaysAvailable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDaysAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDaysAvailable.Location = new System.Drawing.Point(277, 212);
			this.lblDaysAvailable.Name = "lblDaysAvailable";
			this.lblDaysAvailable.Size = new System.Drawing.Size(121, 26);
			this.lblDaysAvailable.TabIndex = 36;
			this.lblDaysAvailable.Text = "{not set}";
			this.lblDaysAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(95, 217);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(162, 17);
			this.label7.TabIndex = 35;
			this.label7.Text = "Days Of Data Available: ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(321, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 17);
			this.label1.TabIndex = 38;
			this.label1.Text = "Pick a Database";
			// 
			// cmbDatabaseNames
			// 
			this.cmbDatabaseNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDatabaseNames.FormattingEnabled = true;
			this.cmbDatabaseNames.Location = new System.Drawing.Point(245, 58);
			this.cmbDatabaseNames.Name = "cmbDatabaseNames";
			this.cmbDatabaseNames.Size = new System.Drawing.Size(254, 24);
			this.cmbDatabaseNames.TabIndex = 37;
			this.cmbDatabaseNames.Text = "{not set}";
			this.cmbDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(186, 115);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(138, 17);
			this.label6.TabIndex = 40;
			this.label6.Text = "Pick The Input Table";
			// 
			// cmbTableNames
			// 
			this.cmbTableNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTableNames.FormattingEnabled = true;
			this.cmbTableNames.Location = new System.Drawing.Point(126, 143);
			this.cmbTableNames.Name = "cmbTableNames";
			this.cmbTableNames.Size = new System.Drawing.Size(254, 24);
			this.cmbTableNames.TabIndex = 39;
			this.cmbTableNames.Text = "{not set}";
			this.cmbTableNames.SelectedIndexChanged += new System.EventHandler(this.cmbTableNames_SelectedIndexChanged);
			// 
			// CCIGenMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 485);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cmbTableNames);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbDatabaseNames);
			this.Controls.Add(this.lblDaysAvailable);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtDays);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.txtEntryCCI);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtTrendCCI);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbServerName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "CCIGenMain";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "CCI Data Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CCIGenMain_FormClosing);
			this.Shown += new System.EventHandler(this.CCIGenMain_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtEntryCCI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTrendCCI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label lblDaysAvailable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabaseNames;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTableNames;
    }
}

