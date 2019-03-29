namespace TrixGenerator
{
    partial class TrixGenMain
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
				oSqlHandler.Dispose();
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
			this.label2 = new System.Windows.Forms.Label();
			this.cmbServerName = new System.Windows.Forms.ComboBox();
			this.txtSlowTrix = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtFastTrix = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.VersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rbUseTHV4Last = new System.Windows.Forms.RadioButton();
			this.rbUseMyLast = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDays = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbDatabaseNames = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbTableNames = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.lblDaysAvailable = new System.Windows.Forms.TextBox();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(55, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "Pick a server.";
			// 
			// cmbServerName
			// 
			this.cmbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbServerName.FormattingEnabled = true;
			this.cmbServerName.Location = new System.Drawing.Point(44, 49);
			this.cmbServerName.Name = "cmbServerName";
			this.cmbServerName.Size = new System.Drawing.Size(141, 24);
			this.cmbServerName.TabIndex = 3;
			this.cmbServerName.Text = "{ not set }";
			this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbServerName_SelectedIndexChanged);
			// 
			// txtSlowTrix
			// 
			this.txtSlowTrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSlowTrix.Location = new System.Drawing.Point(30, 203);
			this.txtSlowTrix.Name = "txtSlowTrix";
			this.txtSlowTrix.Size = new System.Drawing.Size(79, 23);
			this.txtSlowTrix.TabIndex = 5;
			this.txtSlowTrix.Text = "35";
			this.txtSlowTrix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(14, 176);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(101, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "Slow Trix Span";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(136, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "Fast Trix Span";
			// 
			// txtFastTrix
			// 
			this.txtFastTrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFastTrix.Location = new System.Drawing.Point(144, 203);
			this.txtFastTrix.Name = "txtFastTrix";
			this.txtFastTrix.Size = new System.Drawing.Size(79, 23);
			this.txtFastTrix.TabIndex = 8;
			this.txtFastTrix.Text = "20";
			this.txtFastTrix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.VersionLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 437);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(529, 22);
			this.statusStrip1.TabIndex = 11;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(429, 17);
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rbUseTHV4Last);
			this.groupBox1.Controls.Add(this.rbUseMyLast);
			this.groupBox1.Location = new System.Drawing.Point(42, 269);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 139);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Last Peak/Trough Method";
			// 
			// rbUseTHV4Last
			// 
			this.rbUseTHV4Last.AutoSize = true;
			this.rbUseTHV4Last.Checked = true;
			this.rbUseTHV4Last.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbUseTHV4Last.Location = new System.Drawing.Point(29, 76);
			this.rbUseTHV4Last.Name = "rbUseTHV4Last";
			this.rbUseTHV4Last.Size = new System.Drawing.Size(122, 21);
			this.rbUseTHV4Last.TabIndex = 15;
			this.rbUseTHV4Last.TabStop = true;
			this.rbUseTHV4Last.Text = "Use THV4 Last";
			this.rbUseTHV4Last.UseVisualStyleBackColor = true;
			// 
			// rbUseMyLast
			// 
			this.rbUseMyLast.AutoSize = true;
			this.rbUseMyLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbUseMyLast.Location = new System.Drawing.Point(29, 38);
			this.rbUseMyLast.Name = "rbUseMyLast";
			this.rbUseMyLast.Size = new System.Drawing.Size(85, 21);
			this.rbUseMyLast.TabIndex = 14;
			this.rbUseMyLast.Text = "Use Mine";
			this.rbUseMyLast.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(392, 178);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 17);
			this.label5.TabIndex = 17;
			this.label5.Text = "Days to generate.";
			// 
			// txtDays
			// 
			this.txtDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDays.Location = new System.Drawing.Point(409, 203);
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(79, 23);
			this.txtDays.TabIndex = 16;
			this.txtDays.Text = "100";
			this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDays.WordWrap = false;
			// 
			// btnGenerate
			// 
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(331, 258);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(95, 31);
			this.btnGenerate.TabIndex = 15;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAbort.Location = new System.Drawing.Point(339, 322);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(87, 31);
			this.btnAbort.TabIndex = 18;
			this.btnAbort.Text = "Abort!";
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// btnExit
			// 
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(339, 387);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(87, 31);
			this.btnExit.TabIndex = 19;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(315, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 17);
			this.label1.TabIndex = 74;
			this.label1.Text = "Pick a Database";
			// 
			// cmbDatabaseNames
			// 
			this.cmbDatabaseNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDatabaseNames.FormattingEnabled = true;
			this.cmbDatabaseNames.Location = new System.Drawing.Point(239, 49);
			this.cmbDatabaseNames.Name = "cmbDatabaseNames";
			this.cmbDatabaseNames.Size = new System.Drawing.Size(254, 24);
			this.cmbDatabaseNames.TabIndex = 73;
			this.cmbDatabaseNames.Text = "{not set}";
			this.cmbDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(189, 96);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(138, 17);
			this.label8.TabIndex = 76;
			this.label8.Text = "Pick The Input Table";
			// 
			// cmbTableNames
			// 
			this.cmbTableNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTableNames.FormattingEnabled = true;
			this.cmbTableNames.Location = new System.Drawing.Point(129, 124);
			this.cmbTableNames.Name = "cmbTableNames";
			this.cmbTableNames.Size = new System.Drawing.Size(254, 24);
			this.cmbTableNames.TabIndex = 75;
			this.cmbTableNames.Text = "{not set}";
			this.cmbTableNames.SelectedIndexChanged += new System.EventHandler(this.cmbTableNames_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(280, 178);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(101, 17);
			this.label6.TabIndex = 78;
			this.label6.Text = "Days Available";
			// 
			// lblDaysAvailable
			// 
			this.lblDaysAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDaysAvailable.Location = new System.Drawing.Point(290, 203);
			this.lblDaysAvailable.Name = "lblDaysAvailable";
			this.lblDaysAvailable.Size = new System.Drawing.Size(79, 23);
			this.lblDaysAvailable.TabIndex = 77;
			this.lblDaysAvailable.Text = "{not set]";
			this.lblDaysAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lblDaysAvailable.WordWrap = false;
			// 
			// TrixGenMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(529, 459);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.lblDaysAvailable);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.cmbTableNames);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbDatabaseNames);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtDays);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.txtFastTrix);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtSlowTrix);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbServerName);
			this.Name = "TrixGenMain";
			this.Text = "Trix Data Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrixGenMain_FormClosing);
			this.Shown += new System.EventHandler(this.TrixGenMain_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.TextBox txtSlowTrix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFastTrix;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel VersionLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbUseTHV4Last;
        private System.Windows.Forms.RadioButton rbUseMyLast;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabaseNames;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbTableNames;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lblDaysAvailable;
    }
}

