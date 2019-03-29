namespace StochasticsGenerator.cs
{
    partial class StochasticMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StochasticMain));
			this.cmbTableNames = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbDatabaseNames = new System.Windows.Forms.ComboBox();
			this.lblDaysAvailable = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.label6 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDays = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.txtSlowKPeriod = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtLookBack = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbServerName = new System.Windows.Forms.ComboBox();
			this.txtDperiod = new System.Windows.Forms.TextBox();
			this.lblDperiod = new System.Windows.Forms.Label();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmbTableNames
			// 
			this.cmbTableNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTableNames.FormattingEnabled = true;
			this.cmbTableNames.Location = new System.Drawing.Point(126, 128);
			this.cmbTableNames.Name = "cmbTableNames";
			this.cmbTableNames.Size = new System.Drawing.Size(254, 24);
			this.cmbTableNames.TabIndex = 57;
			this.cmbTableNames.Text = "{not set}";
			this.cmbTableNames.SelectedIndexChanged += new System.EventHandler(this.cmbTableNames_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(321, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 17);
			this.label1.TabIndex = 56;
			this.label1.Text = "Pick a Database";
			// 
			// cmbDatabaseNames
			// 
			this.cmbDatabaseNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDatabaseNames.FormattingEnabled = true;
			this.cmbDatabaseNames.Location = new System.Drawing.Point(245, 43);
			this.cmbDatabaseNames.Name = "cmbDatabaseNames";
			this.cmbDatabaseNames.Size = new System.Drawing.Size(254, 24);
			this.cmbDatabaseNames.TabIndex = 55;
			this.cmbDatabaseNames.Text = "{not set}";
			this.cmbDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
			// 
			// lblDaysAvailable
			// 
			this.lblDaysAvailable.BackColor = System.Drawing.Color.White;
			this.lblDaysAvailable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDaysAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDaysAvailable.Location = new System.Drawing.Point(277, 197);
			this.lblDaysAvailable.Name = "lblDaysAvailable";
			this.lblDaysAvailable.Size = new System.Drawing.Size(121, 26);
			this.lblDaysAvailable.TabIndex = 54;
			this.lblDaysAvailable.Text = "{not set}";
			this.lblDaysAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(95, 202);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(162, 17);
			this.label7.TabIndex = 53;
			this.label7.Text = "Days Of Data Available: ";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(141, 23);
			this.toolStripStatusLabel2.Text = "Stochastic Gen. V1.0";
			// 
			// StatusLabel
			// 
			this.StatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Padding = new System.Windows.Forms.Padding(3);
			this.StatusLabel.Size = new System.Drawing.Size(386, 23);
			this.StatusLabel.Spring = true;
			this.StatusLabel.Text = "Idle ...";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(186, 100);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(138, 17);
			this.label6.TabIndex = 58;
			this.label6.Text = "Pick The Input Table";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 435);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(542, 28);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 52;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// btnExit
			// 
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(229, 362);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(87, 31);
			this.btnExit.TabIndex = 51;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAbort.Location = new System.Drawing.Point(378, 362);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(87, 31);
			this.btnAbort.TabIndex = 50;
			this.btnAbort.Text = "Abort!";
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(404, 267);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 39);
			this.label5.TabIndex = 49;
			this.label5.Text = "Days to generate.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtDays
			// 
			this.txtDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDays.Location = new System.Drawing.Point(398, 310);
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(79, 23);
			this.txtDays.TabIndex = 48;
			this.txtDays.Text = "100";
			this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDays.WordWrap = false;
			this.txtDays.TextChanged += new System.EventHandler(this.txtDays_TextChanged);
			// 
			// btnGenerate
			// 
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(70, 362);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(95, 31);
			this.btnGenerate.TabIndex = 47;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// txtSlowKPeriod
			// 
			this.txtSlowKPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSlowKPeriod.Location = new System.Drawing.Point(174, 310);
			this.txtSlowKPeriod.Name = "txtSlowKPeriod";
			this.txtSlowKPeriod.Size = new System.Drawing.Size(79, 23);
			this.txtSlowKPeriod.TabIndex = 46;
			this.txtSlowKPeriod.Text = "3";
			this.txtSlowKPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSlowKPeriod.TextChanged += new System.EventHandler(this.txtSlowKPeriod_TextChanged);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(171, 271);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 35);
			this.label4.TabIndex = 45;
			this.label4.Text = "Slow K / %D Period";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(60, 271);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 35);
			this.label3.TabIndex = 44;
			this.label3.Text = "Lookback Period";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtLookBack
			// 
			this.txtLookBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLookBack.Location = new System.Drawing.Point(57, 309);
			this.txtLookBack.Name = "txtLookBack";
			this.txtLookBack.Size = new System.Drawing.Size(79, 23);
			this.txtLookBack.TabIndex = 43;
			this.txtLookBack.Text = "14";
			this.txtLookBack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtLookBack.TextChanged += new System.EventHandler(this.txtLookBack_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(47, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 17);
			this.label2.TabIndex = 42;
			this.label2.Text = "Pick a server.";
			// 
			// cmbServerName
			// 
			this.cmbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbServerName.FormattingEnabled = true;
			this.cmbServerName.Items.AddRange(new object[] {
            "chucksgateway:3303"});
			this.cmbServerName.Location = new System.Drawing.Point(36, 43);
			this.cmbServerName.Name = "cmbServerName";
			this.cmbServerName.Size = new System.Drawing.Size(141, 24);
			this.cmbServerName.TabIndex = 41;
			this.cmbServerName.Text = "{ not set }";
			this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
			// 
			// txtDperiod
			// 
			this.txtDperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDperiod.Location = new System.Drawing.Point(287, 310);
			this.txtDperiod.Name = "txtDperiod";
			this.txtDperiod.Size = new System.Drawing.Size(79, 23);
			this.txtDperiod.TabIndex = 60;
			this.txtDperiod.Text = "3";
			this.txtDperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDperiod.TextChanged += new System.EventHandler(this.txtDperiod_TextChanged);
			// 
			// lblDperiod
			// 
			this.lblDperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDperiod.Location = new System.Drawing.Point(293, 272);
			this.lblDperiod.Name = "lblDperiod";
			this.lblDperiod.Size = new System.Drawing.Size(63, 34);
			this.lblDperiod.TabIndex = 59;
			this.lblDperiod.Text = "Slow D Period";
			this.lblDperiod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// StochasticMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(542, 463);
			this.Controls.Add(this.txtDperiod);
			this.Controls.Add(this.lblDperiod);
			this.Controls.Add(this.cmbTableNames);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbDatabaseNames);
			this.Controls.Add(this.lblDaysAvailable);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtDays);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.txtSlowKPeriod);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtLookBack);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbServerName);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StochasticMain";
			this.Text = "Stochastic Indicator Generator";
			this.Shown += new System.EventHandler(this.StochasticMain_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTableNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabaseNames;
        private System.Windows.Forms.Label lblDaysAvailable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtSlowKPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLookBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.TextBox txtDperiod;
        private System.Windows.Forms.Label lblDperiod;
    }
}

