namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    partial class MasterGenMain
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
				MasterDBhandler.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterGenMain));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabpageMaster = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chkStochastics = new System.Windows.Forms.CheckBox();
			this.chkHeikenAshiGen = new System.Windows.Forms.CheckBox();
			this.chkIchimokuGen = new System.Windows.Forms.CheckBox();
			this.chkTrixGen = new System.Windows.Forms.CheckBox();
			this.chkRVI3gen = new System.Windows.Forms.CheckBox();
			this.chkCCIgen = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.txtDays = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.lblDaysAvailable = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbTableNames = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbDatabaseNames = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbServerName = new System.Windows.Forms.ComboBox();
			this.tabpageParameters = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtPerCentDperiod = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtSlowKperiod = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtStochLookback = new System.Windows.Forms.TextBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label19 = new System.Windows.Forms.Label();
			this.txtRVIspan = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label17 = new System.Windows.Forms.Label();
			this.txtSpanBperiod = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.txtKijunPeriod = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.txtTenkanPeriod = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtFastTrixSpan = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.txtSlowTrixSpan = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtEntryCCIperiod = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTrendCCIperiod = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1.SuspendLayout();
			this.tabpageMaster.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabpageParameters.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabpageMaster);
			this.tabControl1.Controls.Add(this.tabpageParameters);
			this.tabControl1.ItemSize = new System.Drawing.Size(100, 18);
			this.tabControl1.Location = new System.Drawing.Point(0, 1);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(670, 459);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabpageMaster
			// 
			this.tabpageMaster.Controls.Add(this.panel1);
			this.tabpageMaster.Controls.Add(this.label12);
			this.tabpageMaster.Controls.Add(this.btnExit);
			this.tabpageMaster.Controls.Add(this.btnAbort);
			this.tabpageMaster.Controls.Add(this.label13);
			this.tabpageMaster.Controls.Add(this.txtDays);
			this.tabpageMaster.Controls.Add(this.btnGenerate);
			this.tabpageMaster.Controls.Add(this.lblDaysAvailable);
			this.tabpageMaster.Controls.Add(this.label8);
			this.tabpageMaster.Controls.Add(this.cmbTableNames);
			this.tabpageMaster.Controls.Add(this.label1);
			this.tabpageMaster.Controls.Add(this.cmbDatabaseNames);
			this.tabpageMaster.Controls.Add(this.label2);
			this.tabpageMaster.Controls.Add(this.cmbServerName);
			this.tabpageMaster.Location = new System.Drawing.Point(4, 22);
			this.tabpageMaster.Name = "tabpageMaster";
			this.tabpageMaster.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageMaster.Size = new System.Drawing.Size(662, 433);
			this.tabpageMaster.TabIndex = 0;
			this.tabpageMaster.Text = "Master";
			this.tabpageMaster.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.chkStochastics);
			this.panel1.Controls.Add(this.chkHeikenAshiGen);
			this.panel1.Controls.Add(this.chkIchimokuGen);
			this.panel1.Controls.Add(this.chkTrixGen);
			this.panel1.Controls.Add(this.chkRVI3gen);
			this.panel1.Controls.Add(this.chkCCIgen);
			this.panel1.Location = new System.Drawing.Point(42, 188);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(578, 113);
			this.panel1.TabIndex = 111;
			// 
			// chkStochastics
			// 
			this.chkStochastics.AutoSize = true;
			this.chkStochastics.Checked = true;
			this.chkStochastics.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStochastics.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkStochastics.Location = new System.Drawing.Point(402, 64);
			this.chkStochastics.Name = "chkStochastics";
			this.chkStochastics.Size = new System.Drawing.Size(163, 21);
			this.chkStochastics.TabIndex = 5;
			this.chkStochastics.Text = "Generate Stochastics";
			this.chkStochastics.UseVisualStyleBackColor = true;
			// 
			// chkHeikenAshiGen
			// 
			this.chkHeikenAshiGen.AutoSize = true;
			this.chkHeikenAshiGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkHeikenAshiGen.Location = new System.Drawing.Point(194, 22);
			this.chkHeikenAshiGen.Name = "chkHeikenAshiGen";
			this.chkHeikenAshiGen.Size = new System.Drawing.Size(166, 21);
			this.chkHeikenAshiGen.TabIndex = 4;
			this.chkHeikenAshiGen.Text = "Generate Heiken Ashi";
			this.chkHeikenAshiGen.UseVisualStyleBackColor = true;
			// 
			// chkIchimokuGen
			// 
			this.chkIchimokuGen.AutoSize = true;
			this.chkIchimokuGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkIchimokuGen.Location = new System.Drawing.Point(402, 22);
			this.chkIchimokuGen.Name = "chkIchimokuGen";
			this.chkIchimokuGen.Size = new System.Drawing.Size(146, 21);
			this.chkIchimokuGen.TabIndex = 3;
			this.chkIchimokuGen.Text = "Generate Ichimoku";
			this.chkIchimokuGen.UseVisualStyleBackColor = true;
			// 
			// chkTrixGen
			// 
			this.chkTrixGen.AutoSize = true;
			this.chkTrixGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkTrixGen.Location = new System.Drawing.Point(194, 64);
			this.chkTrixGen.Name = "chkTrixGen";
			this.chkTrixGen.Size = new System.Drawing.Size(114, 21);
			this.chkTrixGen.TabIndex = 2;
			this.chkTrixGen.Text = "Generate Trix";
			this.chkTrixGen.UseVisualStyleBackColor = true;
			// 
			// chkRVI3gen
			// 
			this.chkRVI3gen.AutoSize = true;
			this.chkRVI3gen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkRVI3gen.Location = new System.Drawing.Point(20, 64);
			this.chkRVI3gen.Name = "chkRVI3gen";
			this.chkRVI3gen.Size = new System.Drawing.Size(121, 21);
			this.chkRVI3gen.TabIndex = 1;
			this.chkRVI3gen.Text = "Generate RVI3";
			this.chkRVI3gen.UseVisualStyleBackColor = true;
			// 
			// chkCCIgen
			// 
			this.chkCCIgen.AutoSize = true;
			this.chkCCIgen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkCCIgen.Location = new System.Drawing.Point(20, 22);
			this.chkCCIgen.Name = "chkCCIgen";
			this.chkCCIgen.Size = new System.Drawing.Size(112, 21);
			this.chkCCIgen.TabIndex = 0;
			this.chkCCIgen.Text = "Generate CCI";
			this.chkCCIgen.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(341, 99);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(101, 17);
			this.label12.TabIndex = 110;
			this.label12.Text = "Days Available";
			// 
			// btnExit
			// 
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(429, 353);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(87, 31);
			this.btnExit.TabIndex = 108;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAbort.Location = new System.Drawing.Point(268, 353);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(87, 31);
			this.btnAbort.TabIndex = 107;
			this.btnAbort.Text = "Abort!";
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(456, 99);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(121, 17);
			this.label13.TabIndex = 106;
			this.label13.Text = "Days to generate.";
			// 
			// txtDays
			// 
			this.txtDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDays.Location = new System.Drawing.Point(473, 124);
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(79, 23);
			this.txtDays.TabIndex = 105;
			this.txtDays.Text = "100";
			this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDays.WordWrap = false;
			// 
			// btnGenerate
			// 
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(99, 353);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(95, 31);
			this.btnGenerate.TabIndex = 104;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// lblDaysAvailable
			// 
			this.lblDaysAvailable.BackColor = System.Drawing.SystemColors.Window;
			this.lblDaysAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDaysAvailable.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lblDaysAvailable.Location = new System.Drawing.Point(351, 124);
			this.lblDaysAvailable.Name = "lblDaysAvailable";
			this.lblDaysAvailable.Size = new System.Drawing.Size(79, 23);
			this.lblDaysAvailable.TabIndex = 109;
			this.lblDaysAvailable.Text = "{not set]";
			this.lblDaysAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lblDaysAvailable.WordWrap = false;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(82, 95);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(138, 17);
			this.label8.TabIndex = 101;
			this.label8.Text = "Pick The Input Table";
			// 
			// cmbTableNames
			// 
			this.cmbTableNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTableNames.Location = new System.Drawing.Point(42, 124);
			this.cmbTableNames.Name = "cmbTableNames";
			this.cmbTableNames.Size = new System.Drawing.Size(254, 24);
			this.cmbTableNames.TabIndex = 100;
			this.cmbTableNames.Text = "{not set}";
			this.cmbTableNames.SelectedIndexChanged += new System.EventHandler(this.cmbTableNames_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(354, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 17);
			this.label1.TabIndex = 99;
			this.label1.Text = "Pick Data Input Database";
			// 
			// cmbDatabaseNames
			// 
			this.cmbDatabaseNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDatabaseNames.Location = new System.Drawing.Point(319, 49);
			this.cmbDatabaseNames.Name = "cmbDatabaseNames";
			this.cmbDatabaseNames.Size = new System.Drawing.Size(254, 24);
			this.cmbDatabaseNames.TabIndex = 98;
			this.cmbDatabaseNames.Text = "{not set}";
			this.cmbDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseNames_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(86, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 17);
			this.label2.TabIndex = 97;
			this.label2.Text = "Pick a server.";
			// 
			// cmbServerName
			// 
			this.cmbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbServerName.Location = new System.Drawing.Point(42, 49);
			this.cmbServerName.Name = "cmbServerName";
			this.cmbServerName.Size = new System.Drawing.Size(191, 24);
			this.cmbServerName.TabIndex = 96;
			this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbServerName_SelectedIndexChanged);
			// 
			// tabpageParameters
			// 
			this.tabpageParameters.Controls.Add(this.groupBox6);
			this.tabpageParameters.Controls.Add(this.groupBox4);
			this.tabpageParameters.Controls.Add(this.groupBox3);
			this.tabpageParameters.Controls.Add(this.groupBox2);
			this.tabpageParameters.Controls.Add(this.groupBox1);
			this.tabpageParameters.Location = new System.Drawing.Point(4, 22);
			this.tabpageParameters.Name = "tabpageParameters";
			this.tabpageParameters.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageParameters.Size = new System.Drawing.Size(662, 433);
			this.tabpageParameters.TabIndex = 1;
			this.tabpageParameters.Text = "Parameter Setups";
			this.tabpageParameters.UseVisualStyleBackColor = true;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label5);
			this.groupBox6.Controls.Add(this.txtPerCentDperiod);
			this.groupBox6.Controls.Add(this.label6);
			this.groupBox6.Controls.Add(this.txtSlowKperiod);
			this.groupBox6.Controls.Add(this.label7);
			this.groupBox6.Controls.Add(this.txtStochLookback);
			this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox6.Location = new System.Drawing.Point(50, 239);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(237, 177);
			this.groupBox6.TabIndex = 35;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Stochastic Parameters";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(77, 117);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 17);
			this.label5.TabIndex = 80;
			this.label5.Text = "%D Period";
			// 
			// txtPerCentDperiod
			// 
			this.txtPerCentDperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPerCentDperiod.Location = new System.Drawing.Point(75, 137);
			this.txtPerCentDperiod.Name = "txtPerCentDperiod";
			this.txtPerCentDperiod.Size = new System.Drawing.Size(79, 23);
			this.txtPerCentDperiod.TabIndex = 79;
			this.txtPerCentDperiod.Text = "3";
			this.txtPerCentDperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtPerCentDperiod.WordWrap = false;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(69, 68);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(95, 17);
			this.label6.TabIndex = 78;
			this.label6.Text = "Slow K Period";
			// 
			// txtSlowKperiod
			// 
			this.txtSlowKperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSlowKperiod.Location = new System.Drawing.Point(75, 88);
			this.txtSlowKperiod.Name = "txtSlowKperiod";
			this.txtSlowKperiod.Size = new System.Drawing.Size(79, 23);
			this.txtSlowKperiod.TabIndex = 77;
			this.txtSlowKperiod.Text = "3";
			this.txtSlowKperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSlowKperiod.WordWrap = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(65, 23);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(114, 17);
			this.label7.TabIndex = 76;
			this.label7.Text = "Lookback Period";
			// 
			// txtStochLookback
			// 
			this.txtStochLookback.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtStochLookback.Location = new System.Drawing.Point(75, 43);
			this.txtStochLookback.Name = "txtStochLookback";
			this.txtStochLookback.Size = new System.Drawing.Size(79, 23);
			this.txtStochLookback.TabIndex = 75;
			this.txtStochLookback.Text = "14";
			this.txtStochLookback.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtStochLookback.WordWrap = false;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label19);
			this.groupBox4.Controls.Add(this.txtRVIspan);
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox4.Location = new System.Drawing.Point(50, 145);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(237, 83);
			this.groupBox4.TabIndex = 34;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "RVI3 Parameters";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.Location = new System.Drawing.Point(84, 18);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(67, 17);
			this.label19.TabIndex = 86;
			this.label19.Text = "RVI Span";
			// 
			// txtRVIspan
			// 
			this.txtRVIspan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRVIspan.Location = new System.Drawing.Point(75, 43);
			this.txtRVIspan.Name = "txtRVIspan";
			this.txtRVIspan.Size = new System.Drawing.Size(79, 23);
			this.txtRVIspan.TabIndex = 85;
			this.txtRVIspan.Text = "10";
			this.txtRVIspan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtRVIspan.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label17);
			this.groupBox3.Controls.Add(this.txtSpanBperiod);
			this.groupBox3.Controls.Add(this.label18);
			this.groupBox3.Controls.Add(this.txtKijunPeriod);
			this.groupBox3.Controls.Add(this.label16);
			this.groupBox3.Controls.Add(this.txtTenkanPeriod);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(364, 24);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(237, 182);
			this.groupBox3.TabIndex = 34;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Ichimoku Parameters";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label17.Location = new System.Drawing.Point(67, 117);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(99, 17);
			this.label17.TabIndex = 80;
			this.label17.Text = "Span B Period";
			// 
			// txtSpanBperiod
			// 
			this.txtSpanBperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSpanBperiod.Location = new System.Drawing.Point(75, 137);
			this.txtSpanBperiod.Name = "txtSpanBperiod";
			this.txtSpanBperiod.Size = new System.Drawing.Size(79, 23);
			this.txtSpanBperiod.TabIndex = 79;
			this.txtSpanBperiod.Text = "8";
			this.txtSpanBperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSpanBperiod.WordWrap = false;
			this.txtSpanBperiod.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label18.Location = new System.Drawing.Point(73, 68);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(84, 17);
			this.label18.TabIndex = 78;
			this.label18.Text = "Kijun Period";
			// 
			// txtKijunPeriod
			// 
			this.txtKijunPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtKijunPeriod.Location = new System.Drawing.Point(75, 88);
			this.txtKijunPeriod.Name = "txtKijunPeriod";
			this.txtKijunPeriod.Size = new System.Drawing.Size(79, 23);
			this.txtKijunPeriod.TabIndex = 77;
			this.txtKijunPeriod.Text = "5";
			this.txtKijunPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtKijunPeriod.WordWrap = false;
			this.txtKijunPeriod.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(65, 23);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(101, 17);
			this.label16.TabIndex = 76;
			this.label16.Text = "Tenkan Period";
			// 
			// txtTenkanPeriod
			// 
			this.txtTenkanPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTenkanPeriod.Location = new System.Drawing.Point(75, 43);
			this.txtTenkanPeriod.Name = "txtTenkanPeriod";
			this.txtTenkanPeriod.Size = new System.Drawing.Size(79, 23);
			this.txtTenkanPeriod.TabIndex = 75;
			this.txtTenkanPeriod.Text = "2";
			this.txtTenkanPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtTenkanPeriod.WordWrap = false;
			this.txtTenkanPeriod.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtFastTrixSpan);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.txtSlowTrixSpan);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(364, 229);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(237, 172);
			this.groupBox2.TabIndex = 34;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Trix Parameters";
			// 
			// txtFastTrixSpan
			// 
			this.txtFastTrixSpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFastTrixSpan.Location = new System.Drawing.Point(79, 127);
			this.txtFastTrixSpan.Name = "txtFastTrixSpan";
			this.txtFastTrixSpan.Size = new System.Drawing.Size(79, 23);
			this.txtFastTrixSpan.TabIndex = 38;
			this.txtFastTrixSpan.Text = "20";
			this.txtFastTrixSpan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtFastTrixSpan.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(71, 100);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(99, 17);
			this.label14.TabIndex = 37;
			this.label14.Text = "Fast Trix Span";
			// 
			// txtSlowTrixSpan
			// 
			this.txtSlowTrixSpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSlowTrixSpan.Location = new System.Drawing.Point(79, 61);
			this.txtSlowTrixSpan.Name = "txtSlowTrixSpan";
			this.txtSlowTrixSpan.Size = new System.Drawing.Size(79, 23);
			this.txtSlowTrixSpan.TabIndex = 35;
			this.txtSlowTrixSpan.Text = "35";
			this.txtSlowTrixSpan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSlowTrixSpan.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(71, 31);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(101, 17);
			this.label15.TabIndex = 36;
			this.label15.Text = "Slow Trix Span";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtEntryCCIperiod);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtTrendCCIperiod);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(50, 9);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(237, 130);
			this.groupBox1.TabIndex = 32;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "CCI Parameters";
			// 
			// txtEntryCCIperiod
			// 
			this.txtEntryCCIperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEntryCCIperiod.Location = new System.Drawing.Point(79, 91);
			this.txtEntryCCIperiod.Name = "txtEntryCCIperiod";
			this.txtEntryCCIperiod.Size = new System.Drawing.Size(79, 23);
			this.txtEntryCCIperiod.TabIndex = 33;
			this.txtEntryCCIperiod.Text = "6";
			this.txtEntryCCIperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtEntryCCIperiod.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(72, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 17);
			this.label4.TabIndex = 32;
			this.label4.Text = "Entry CCI Period";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(66, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 17);
			this.label3.TabIndex = 31;
			this.label3.Text = "Trend CCI Period";
			// 
			// txtTrendCCIperiod
			// 
			this.txtTrendCCIperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTrendCCIperiod.Location = new System.Drawing.Point(79, 39);
			this.txtTrendCCIperiod.Name = "txtTrendCCIperiod";
			this.txtTrendCCIperiod.Size = new System.Drawing.Size(79, 23);
			this.txtTrendCCIperiod.TabIndex = 30;
			this.txtTrendCCIperiod.Text = "14";
			this.txtTrendCCIperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtTrendCCIperiod.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 460);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(671, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(525, 17);
			this.StatusLabel.Spring = true;
			this.StatusLabel.Text = "Idle...";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(131, 17);
			this.toolStripStatusLabel2.Text = "Data Generator V2.0";
			// 
			// MasterGenMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(671, 482);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MasterGenMain";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Forex Master Data Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterGenMain_FormClosing);
			this.Shown += new System.EventHandler(this.MasterGenMain_Shown);
			this.tabControl1.ResumeLayout(false);
			this.tabpageMaster.ResumeLayout(false);
			this.tabpageMaster.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tabpageParameters.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabpageMaster;
        private System.Windows.Forms.TabPage tabpageParameters;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbTableNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabaseNames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkHeikenAshiGen;
        private System.Windows.Forms.CheckBox chkIchimokuGen;
        private System.Windows.Forms.CheckBox chkTrixGen;
        private System.Windows.Forms.CheckBox chkRVI3gen;
        private System.Windows.Forms.CheckBox chkCCIgen;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox lblDaysAvailable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEntryCCIperiod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTrendCCIperiod;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtRVIspan;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtSpanBperiod;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtKijunPeriod;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTenkanPeriod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFastTrixSpan;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSlowTrixSpan;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.CheckBox chkStochastics;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPerCentDperiod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSlowKperiod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStochLookback;
    }
}

