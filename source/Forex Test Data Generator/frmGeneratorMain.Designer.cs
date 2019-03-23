namespace Forex_test_data_generator
{
	partial class frmGeneratorMain
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage MainTab;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.TextBox txtDays;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkStoch;
		private System.Windows.Forms.CheckBox chkMACD;
		private System.Windows.Forms.CheckBox chkCCI;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFolderLocation;
		private System.Windows.Forms.Button btnFindFolder;
		private System.Windows.Forms.TabPage ParametersTab;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtOutputFolder;
		private System.Windows.Forms.Button btnFindOutput;
		private System.Windows.Forms.TextBox txtMACDSignalPeriod;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtMACDslowPeriod;
		private System.Windows.Forms.TextBox txtMACDfastPeriod;
		private System.Windows.Forms.TextBox txtStochasticSlowing;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtStocasticDperiod;
		private System.Windows.Forms.TextBox txtStochasticKperiod;
		private System.Windows.Forms.TextBox txtCCIEntryPeriod;
		private System.Windows.Forms.TextBox txtCCITrendPeriod;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.Button btnLoadFile;
		private System.Windows.Forms.CheckBox chkATR;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.TextBox txtATRperiod;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.CheckBox chkAllIndicators;
		private System.Windows.Forms.CheckBox chkClose;
		private System.Windows.Forms.CheckBox chkOpen;
		private System.Windows.Forms.CheckBox chkHigh;
		private System.Windows.Forms.CheckBox chkLow;
		private System.Windows.Forms.CheckBox chkScaleData;
		private System.Windows.Forms.CheckBox chkAllOHLC;
		private System.Windows.Forms.CheckBox chkDeltas;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.CheckBox chkIncludeTimes;
		private System.Windows.Forms.CheckBox chkIncludeDates;
		private System.Windows.Forms.CheckBox chkIncludeHeader;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.CheckBox chkMA3;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtMA3period;
		private System.Windows.Forms.RadioButton rbMA3ema;
		private System.Windows.Forms.RadioButton rbMA3sma;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.CheckBox chkMA1;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txtMA1period;
		private System.Windows.Forms.RadioButton rbMA1ema;
		private System.Windows.Forms.RadioButton rbMA1sma;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.CheckBox chkMA2;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtMA2period;
		private System.Windows.Forms.RadioButton rbMA2ema;
		private System.Windows.Forms.RadioButton rbMA2sma;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnLoadFile = new System.Windows.Forms.Button();
			this.chkATR = new System.Windows.Forms.CheckBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.txtATRperiod = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.chkAllIndicators = new System.Windows.Forms.CheckBox();
			this.chkClose = new System.Windows.Forms.CheckBox();
			this.chkOpen = new System.Windows.Forms.CheckBox();
			this.chkHigh = new System.Windows.Forms.CheckBox();
			this.chkLow = new System.Windows.Forms.CheckBox();
			this.chkScaleData = new System.Windows.Forms.CheckBox();
			this.chkAllOHLC = new System.Windows.Forms.CheckBox();
			this.chkDeltas = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.chkIncludeTimes = new System.Windows.Forms.CheckBox();
			this.chkIncludeDates = new System.Windows.Forms.CheckBox();
			this.chkIncludeHeader = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtOutputFolder = new System.Windows.Forms.TextBox();
			this.btnFindOutput = new System.Windows.Forms.Button();
			this.txtMACDSignalPeriod = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtMACDslowPeriod = new System.Windows.Forms.TextBox();
			this.txtMACDfastPeriod = new System.Windows.Forms.TextBox();
			this.txtStochasticSlowing = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtStocasticDperiod = new System.Windows.Forms.TextBox();
			this.txtStochasticKperiod = new System.Windows.Forms.TextBox();
			this.txtCCIEntryPeriod = new System.Windows.Forms.TextBox();
			this.txtCCITrendPeriod = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chkMACD = new System.Windows.Forms.CheckBox();
			this.chkCCI = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtFolderLocation = new System.Windows.Forms.TextBox();
			this.btnFindFolder = new System.Windows.Forms.Button();
			this.ParametersTab = new System.Windows.Forms.TabPage();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.rbTypeNorm = new System.Windows.Forms.RadioButton();
			this.rbTypeStd = new System.Windows.Forms.RadioButton();
			this.rbTypeScale = new System.Windows.Forms.RadioButton();
			this.txtDays = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label21 = new System.Windows.Forms.Label();
			this.txtVolatilityPeriod = new System.Windows.Forms.TextBox();
			this.chkVolatility = new System.Windows.Forms.CheckBox();
			this.chkAltEma = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.txtRoCperiod = new System.Windows.Forms.TextBox();
			this.chkRofChng = new System.Windows.Forms.CheckBox();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.chkMA1 = new System.Windows.Forms.CheckBox();
			this.label16 = new System.Windows.Forms.Label();
			this.txtMA1period = new System.Windows.Forms.TextBox();
			this.rbMA1ema = new System.Windows.Forms.RadioButton();
			this.rbMA1sma = new System.Windows.Forms.RadioButton();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.chkMA2 = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.txtMA2period = new System.Windows.Forms.TextBox();
			this.rbMA2ema = new System.Windows.Forms.RadioButton();
			this.rbMA2sma = new System.Windows.Forms.RadioButton();
			this.chkStoch = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.chkMA3 = new System.Windows.Forms.CheckBox();
			this.label17 = new System.Windows.Forms.Label();
			this.txtMA3period = new System.Windows.Forms.TextBox();
			this.rbMA3ema = new System.Windows.Forms.RadioButton();
			this.rbMA3sma = new System.Windows.Forms.RadioButton();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.MainTab = new System.Windows.Forms.TabPage();
			this.chkFixOutliers = new System.Windows.Forms.CheckBox();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.ClassificationTab = new System.Windows.Forms.TabPage();
			this.groupBox11 = new System.Windows.Forms.GroupBox();
			this.cmbClassBase2 = new System.Windows.Forms.ComboBox();
			this.cmbClassBase1 = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.txtClassLabel1 = new System.Windows.Forms.TextBox();
			this.txtClassLabel2 = new System.Windows.Forms.TextBox();
			this.chkAddUpDown = new System.Windows.Forms.CheckBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.ParametersTab.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.MainTab.SuspendLayout();
			this.ClassificationTab.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(563, 17);
			this.StatusLabel.Spring = true;
			this.StatusLabel.Text = "Idle ...";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.AutoSize = false;
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(140, 17);
			this.toolStripStatusLabel2.Text = "Python Data Gen. V2.0";
			// 
			// btnLoadFile
			// 
			this.btnLoadFile.AutoSize = true;
			this.btnLoadFile.Location = new System.Drawing.Point(552, 172);
			this.btnLoadFile.Margin = new System.Windows.Forms.Padding(4);
			this.btnLoadFile.Name = "btnLoadFile";
			this.btnLoadFile.Size = new System.Drawing.Size(125, 27);
			this.btnLoadFile.TabIndex = 15;
			this.btnLoadFile.Text = "Load Data File";
			this.btnLoadFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnLoadFile.UseVisualStyleBackColor = true;
			this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
			// 
			// chkATR
			// 
			this.chkATR.AutoSize = true;
			this.chkATR.Location = new System.Drawing.Point(25, 75);
			this.chkATR.Name = "chkATR";
			this.chkATR.Size = new System.Drawing.Size(180, 21);
			this.chkATR.TabIndex = 5;
			this.chkATR.Tag = "Indicator";
			this.chkATR.Text = "ATR, Hi-Lo, Close-Open";
			this.chkATR.UseVisualStyleBackColor = true;
			this.chkATR.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.txtATRperiod);
			this.groupBox5.Controls.Add(this.label13);
			this.groupBox5.Location = new System.Drawing.Point(494, 18);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(188, 73);
			this.groupBox5.TabIndex = 12;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "ATR param";
			// 
			// txtATRperiod
			// 
			this.txtATRperiod.Location = new System.Drawing.Point(104, 32);
			this.txtATRperiod.Name = "txtATRperiod";
			this.txtATRperiod.Size = new System.Drawing.Size(66, 23);
			this.txtATRperiod.TabIndex = 10;
			this.txtATRperiod.Text = "14";
			this.txtATRperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtATRperiod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(13, 35);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(81, 17);
			this.label13.TabIndex = 0;
			this.label13.Text = "ATR Period";
			// 
			// chkAllIndicators
			// 
			this.chkAllIndicators.AutoSize = true;
			this.chkAllIndicators.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkAllIndicators.Location = new System.Drawing.Point(109, 102);
			this.chkAllIndicators.Name = "chkAllIndicators";
			this.chkAllIndicators.Size = new System.Drawing.Size(121, 21);
			this.chkAllIndicators.TabIndex = 6;
			this.chkAllIndicators.Text = "All Indicators";
			this.chkAllIndicators.UseVisualStyleBackColor = true;
			this.chkAllIndicators.CheckedChanged += new System.EventHandler(this.chkAllIndicators_CheckedChanged);
			// 
			// chkClose
			// 
			this.chkClose.AutoSize = true;
			this.chkClose.Checked = true;
			this.chkClose.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkClose.Location = new System.Drawing.Point(123, 167);
			this.chkClose.Name = "chkClose";
			this.chkClose.Size = new System.Drawing.Size(62, 21);
			this.chkClose.TabIndex = 10;
			this.chkClose.Tag = "OHLC";
			this.chkClose.Text = "Close";
			this.chkClose.UseVisualStyleBackColor = true;
			this.chkClose.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// chkOpen
			// 
			this.chkOpen.AutoSize = true;
			this.chkOpen.Checked = true;
			this.chkOpen.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkOpen.Location = new System.Drawing.Point(25, 141);
			this.chkOpen.Name = "chkOpen";
			this.chkOpen.Size = new System.Drawing.Size(62, 21);
			this.chkOpen.TabIndex = 9;
			this.chkOpen.Tag = "OHLC";
			this.chkOpen.Text = "Open";
			this.chkOpen.UseVisualStyleBackColor = true;
			this.chkOpen.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// chkHigh
			// 
			this.chkHigh.AutoSize = true;
			this.chkHigh.Checked = true;
			this.chkHigh.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHigh.Location = new System.Drawing.Point(123, 141);
			this.chkHigh.Name = "chkHigh";
			this.chkHigh.Size = new System.Drawing.Size(56, 21);
			this.chkHigh.TabIndex = 8;
			this.chkHigh.Tag = "OHLC";
			this.chkHigh.Text = "High";
			this.chkHigh.UseVisualStyleBackColor = true;
			this.chkHigh.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// chkLow
			// 
			this.chkLow.AutoSize = true;
			this.chkLow.Checked = true;
			this.chkLow.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLow.Location = new System.Drawing.Point(25, 167);
			this.chkLow.Name = "chkLow";
			this.chkLow.Size = new System.Drawing.Size(52, 21);
			this.chkLow.TabIndex = 7;
			this.chkLow.Tag = "OHLC";
			this.chkLow.Text = "Low";
			this.chkLow.UseVisualStyleBackColor = true;
			this.chkLow.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// chkScaleData
			// 
			this.chkScaleData.AutoSize = true;
			this.chkScaleData.Checked = true;
			this.chkScaleData.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkScaleData.Location = new System.Drawing.Point(221, 73);
			this.chkScaleData.Name = "chkScaleData";
			this.chkScaleData.Size = new System.Drawing.Size(70, 21);
			this.chkScaleData.TabIndex = 16;
			this.chkScaleData.Text = "Scaled";
			this.chkScaleData.UseVisualStyleBackColor = true;
			this.chkScaleData.CheckedChanged += new System.EventHandler(this.chkScaleData_CheckedChanged);
			// 
			// chkAllOHLC
			// 
			this.chkAllOHLC.AutoSize = true;
			this.chkAllOHLC.Checked = true;
			this.chkAllOHLC.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAllOHLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkAllOHLC.Location = new System.Drawing.Point(25, 192);
			this.chkAllOHLC.Name = "chkAllOHLC";
			this.chkAllOHLC.Size = new System.Drawing.Size(83, 19);
			this.chkAllOHLC.TabIndex = 11;
			this.chkAllOHLC.Text = "All OHLC";
			this.chkAllOHLC.UseVisualStyleBackColor = true;
			this.chkAllOHLC.CheckedChanged += new System.EventHandler(this.chkAllOHLC_CheckedChanged);
			// 
			// chkDeltas
			// 
			this.chkDeltas.AutoSize = true;
			this.chkDeltas.Location = new System.Drawing.Point(123, 192);
			this.chkDeltas.Name = "chkDeltas";
			this.chkDeltas.Size = new System.Drawing.Size(107, 21);
			this.chkDeltas.TabIndex = 12;
			this.chkDeltas.Text = "OHLC deltas";
			this.chkDeltas.UseVisualStyleBackColor = true;
			this.chkDeltas.CheckedChanged += new System.EventHandler(this.chkDeltas_CheckedChanged);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.chkIncludeTimes);
			this.groupBox6.Controls.Add(this.chkIncludeDates);
			this.groupBox6.Location = new System.Drawing.Point(270, 18);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(205, 86);
			this.groupBox6.TabIndex = 13;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "OHLC settings";
			// 
			// chkIncludeTimes
			// 
			this.chkIncludeTimes.AutoSize = true;
			this.chkIncludeTimes.Checked = true;
			this.chkIncludeTimes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIncludeTimes.Location = new System.Drawing.Point(34, 52);
			this.chkIncludeTimes.Name = "chkIncludeTimes";
			this.chkIncludeTimes.Size = new System.Drawing.Size(114, 21);
			this.chkIncludeTimes.TabIndex = 1;
			this.chkIncludeTimes.Text = "Include Times";
			this.chkIncludeTimes.UseVisualStyleBackColor = true;
			this.chkIncludeTimes.CheckedChanged += new System.EventHandler(this.chkIncludeTimes_CheckedChanged);
			// 
			// chkIncludeDates
			// 
			this.chkIncludeDates.AutoSize = true;
			this.chkIncludeDates.Checked = true;
			this.chkIncludeDates.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIncludeDates.Location = new System.Drawing.Point(34, 23);
			this.chkIncludeDates.Name = "chkIncludeDates";
			this.chkIncludeDates.Size = new System.Drawing.Size(113, 21);
			this.chkIncludeDates.TabIndex = 0;
			this.chkIncludeDates.Text = "Include Dates";
			this.chkIncludeDates.UseVisualStyleBackColor = true;
			this.chkIncludeDates.CheckedChanged += new System.EventHandler(this.chkIncludeDates_CheckedChanged);
			// 
			// chkIncludeHeader
			// 
			this.chkIncludeHeader.AutoSize = true;
			this.chkIncludeHeader.Checked = true;
			this.chkIncludeHeader.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIncludeHeader.Location = new System.Drawing.Point(316, 73);
			this.chkIncludeHeader.Name = "chkIncludeHeader";
			this.chkIncludeHeader.Size = new System.Drawing.Size(123, 21);
			this.chkIncludeHeader.TabIndex = 17;
			this.chkIncludeHeader.Text = "Include Header";
			this.chkIncludeHeader.UseVisualStyleBackColor = true;
			this.chkIncludeHeader.CheckStateChanged += new System.EventHandler(this.chkIncludeHeader_CheckedChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(73, 74);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(95, 17);
			this.label9.TabIndex = 14;
			this.label9.Text = "Output Folder";
			// 
			// txtOutputFolder
			// 
			this.txtOutputFolder.Location = new System.Drawing.Point(25, 101);
			this.txtOutputFolder.Name = "txtOutputFolder";
			this.txtOutputFolder.Size = new System.Drawing.Size(510, 23);
			this.txtOutputFolder.TabIndex = 13;
			this.txtOutputFolder.Text = "{ please select output folder location }";
			this.txtOutputFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnFindOutput
			// 
			this.btnFindOutput.AutoSize = true;
			this.btnFindOutput.Location = new System.Drawing.Point(542, 100);
			this.btnFindOutput.Margin = new System.Windows.Forms.Padding(4);
			this.btnFindOutput.Name = "btnFindOutput";
			this.btnFindOutput.Size = new System.Drawing.Size(147, 27);
			this.btnFindOutput.TabIndex = 12;
			this.btnFindOutput.Text = "Find Folder Location";
			this.btnFindOutput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnFindOutput.UseVisualStyleBackColor = true;
			this.btnFindOutput.Click += new System.EventHandler(this.btnFindOutput_Click);
			// 
			// txtMACDSignalPeriod
			// 
			this.txtMACDSignalPeriod.Location = new System.Drawing.Point(156, 88);
			this.txtMACDSignalPeriod.Name = "txtMACDSignalPeriod";
			this.txtMACDSignalPeriod.Size = new System.Drawing.Size(66, 23);
			this.txtMACDSignalPeriod.TabIndex = 14;
			this.txtMACDSignalPeriod.Text = "9";
			this.txtMACDSignalPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMACDSignalPeriod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(20, 89);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(125, 17);
			this.label10.TabIndex = 13;
			this.label10.Text = "Signal EMA Period";
			// 
			// txtMACDslowPeriod
			// 
			this.txtMACDslowPeriod.Location = new System.Drawing.Point(155, 53);
			this.txtMACDslowPeriod.Name = "txtMACDslowPeriod";
			this.txtMACDslowPeriod.Size = new System.Drawing.Size(66, 23);
			this.txtMACDslowPeriod.TabIndex = 12;
			this.txtMACDslowPeriod.Text = "26";
			this.txtMACDslowPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMACDslowPeriod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// txtMACDfastPeriod
			// 
			this.txtMACDfastPeriod.Location = new System.Drawing.Point(155, 20);
			this.txtMACDfastPeriod.Name = "txtMACDfastPeriod";
			this.txtMACDfastPeriod.Size = new System.Drawing.Size(66, 23);
			this.txtMACDfastPeriod.TabIndex = 11;
			this.txtMACDfastPeriod.Text = "12";
			this.txtMACDfastPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMACDfastPeriod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// txtStochasticSlowing
			// 
			this.txtStochasticSlowing.Location = new System.Drawing.Point(120, 88);
			this.txtStochasticSlowing.Name = "txtStochasticSlowing";
			this.txtStochasticSlowing.Size = new System.Drawing.Size(66, 23);
			this.txtStochasticSlowing.TabIndex = 14;
			this.txtStochasticSlowing.Text = "3";
			this.txtStochasticSlowing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtStochasticSlowing.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(20, 89);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(56, 17);
			this.label11.TabIndex = 13;
			this.label11.Text = "Slowing";
			// 
			// txtStocasticDperiod
			// 
			this.txtStocasticDperiod.Location = new System.Drawing.Point(120, 53);
			this.txtStocasticDperiod.Name = "txtStocasticDperiod";
			this.txtStocasticDperiod.Size = new System.Drawing.Size(66, 23);
			this.txtStocasticDperiod.TabIndex = 12;
			this.txtStocasticDperiod.Text = "3";
			this.txtStocasticDperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtStocasticDperiod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// txtStochasticKperiod
			// 
			this.txtStochasticKperiod.Location = new System.Drawing.Point(120, 20);
			this.txtStochasticKperiod.Name = "txtStochasticKperiod";
			this.txtStochasticKperiod.Size = new System.Drawing.Size(66, 23);
			this.txtStochasticKperiod.TabIndex = 11;
			this.txtStochasticKperiod.Text = "5";
			this.txtStochasticKperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtStochasticKperiod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// txtCCIEntryPeriod
			// 
			this.txtCCIEntryPeriod.Location = new System.Drawing.Point(156, 50);
			this.txtCCIEntryPeriod.Name = "txtCCIEntryPeriod";
			this.txtCCIEntryPeriod.Size = new System.Drawing.Size(66, 23);
			this.txtCCIEntryPeriod.TabIndex = 11;
			this.txtCCIEntryPeriod.Text = "6";
			this.txtCCIEntryPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtCCIEntryPeriod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// txtCCITrendPeriod
			// 
			this.txtCCITrendPeriod.Location = new System.Drawing.Point(156, 21);
			this.txtCCITrendPeriod.Name = "txtCCITrendPeriod";
			this.txtCCITrendPeriod.Size = new System.Drawing.Size(66, 23);
			this.txtCCITrendPeriod.TabIndex = 10;
			this.txtCCITrendPeriod.Text = "14";
			this.txtCCITrendPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtCCITrendPeriod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtMACDSignalPeriod);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.txtMACDslowPeriod);
			this.groupBox3.Controls.Add(this.txtMACDfastPeriod);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(18, 120);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(237, 125);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "MACD params";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 54);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(115, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "Slow EMA Period";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(19, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113, 17);
			this.label4.TabIndex = 1;
			this.label4.Text = "Fast EMA Period";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.txtStochasticSlowing);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.txtStocasticDperiod);
			this.groupBox4.Controls.Add(this.txtStochasticKperiod);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Location = new System.Drawing.Point(270, 120);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(205, 125);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Stochastic Osc params";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(19, 54);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(75, 17);
			this.label7.TabIndex = 2;
			this.label7.Text = "%D Period";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(19, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(74, 17);
			this.label6.TabIndex = 1;
			this.label6.Text = "%K Period";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtCCIEntryPeriod);
			this.groupBox2.Controls.Add(this.txtCCITrendPeriod);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(18, 18);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(237, 86);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "CCI params";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "Entry CCI Period";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "Trend CCI Period";
			// 
			// chkMACD
			// 
			this.chkMACD.AutoSize = true;
			this.chkMACD.Location = new System.Drawing.Point(25, 102);
			this.chkMACD.Name = "chkMACD";
			this.chkMACD.Size = new System.Drawing.Size(66, 21);
			this.chkMACD.TabIndex = 1;
			this.chkMACD.Tag = "Indicator";
			this.chkMACD.Text = "MACD";
			this.chkMACD.UseVisualStyleBackColor = true;
			this.chkMACD.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// chkCCI
			// 
			this.chkCCI.AutoSize = true;
			this.chkCCI.Location = new System.Drawing.Point(25, 25);
			this.chkCCI.Name = "chkCCI";
			this.chkCCI.Size = new System.Drawing.Size(189, 21);
			this.chkCCI.TabIndex = 0;
			this.chkCCI.Tag = "Indicator";
			this.chkCCI.Text = "Commodity Channel Index";
			this.chkCCI.UseVisualStyleBackColor = true;
			this.chkCCI.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(64, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(129, 17);
			this.label1.TabIndex = 6;
			this.label1.Text = "History Files Folder";
			// 
			// txtFolderLocation
			// 
			this.txtFolderLocation.Location = new System.Drawing.Point(25, 34);
			this.txtFolderLocation.Name = "txtFolderLocation";
			this.txtFolderLocation.Size = new System.Drawing.Size(510, 23);
			this.txtFolderLocation.TabIndex = 5;
			this.txtFolderLocation.Text = "{ please select input folder location }";
			this.txtFolderLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnFindFolder
			// 
			this.btnFindFolder.AutoSize = true;
			this.btnFindFolder.Location = new System.Drawing.Point(542, 33);
			this.btnFindFolder.Margin = new System.Windows.Forms.Padding(4);
			this.btnFindFolder.Name = "btnFindFolder";
			this.btnFindFolder.Size = new System.Drawing.Size(147, 27);
			this.btnFindFolder.TabIndex = 4;
			this.btnFindFolder.Text = "Find Folder Location";
			this.btnFindFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnFindFolder.UseVisualStyleBackColor = true;
			this.btnFindFolder.Click += new System.EventHandler(this.btnFindFolder_Click);
			// 
			// ParametersTab
			// 
			this.ParametersTab.Controls.Add(this.groupBox6);
			this.ParametersTab.Controls.Add(this.groupBox10);
			this.ParametersTab.Controls.Add(this.groupBox5);
			this.ParametersTab.Controls.Add(this.groupBox3);
			this.ParametersTab.Controls.Add(this.groupBox2);
			this.ParametersTab.Controls.Add(this.groupBox4);
			this.ParametersTab.Location = new System.Drawing.Point(4, 25);
			this.ParametersTab.Name = "ParametersTab";
			this.ParametersTab.Padding = new System.Windows.Forms.Padding(3);
			this.ParametersTab.Size = new System.Drawing.Size(710, 479);
			this.ParametersTab.TabIndex = 1;
			this.ParametersTab.Text = "Indicator Parameters";
			this.ParametersTab.UseVisualStyleBackColor = true;
			// 
			// groupBox10
			// 
			this.groupBox10.Controls.Add(this.rbTypeNorm);
			this.groupBox10.Controls.Add(this.rbTypeStd);
			this.groupBox10.Controls.Add(this.rbTypeScale);
			this.groupBox10.Location = new System.Drawing.Point(494, 120);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(188, 125);
			this.groupBox10.TabIndex = 14;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Scaling Type";
			// 
			// rbTypeNorm
			// 
			this.rbTypeNorm.AutoSize = true;
			this.rbTypeNorm.Location = new System.Drawing.Point(30, 84);
			this.rbTypeNorm.Name = "rbTypeNorm";
			this.rbTypeNorm.Size = new System.Drawing.Size(89, 21);
			this.rbTypeNorm.TabIndex = 2;
			this.rbTypeNorm.Text = "Normalize";
			this.rbTypeNorm.UseVisualStyleBackColor = true;
			// 
			// rbTypeStd
			// 
			this.rbTypeStd.AutoSize = true;
			this.rbTypeStd.Location = new System.Drawing.Point(30, 57);
			this.rbTypeStd.Name = "rbTypeStd";
			this.rbTypeStd.Size = new System.Drawing.Size(102, 21);
			this.rbTypeStd.TabIndex = 1;
			this.rbTypeStd.Text = "Standardize";
			this.rbTypeStd.UseVisualStyleBackColor = true;
			// 
			// rbTypeScale
			// 
			this.rbTypeScale.AutoSize = true;
			this.rbTypeScale.Checked = true;
			this.rbTypeScale.Location = new System.Drawing.Point(30, 30);
			this.rbTypeScale.Name = "rbTypeScale";
			this.rbTypeScale.Size = new System.Drawing.Size(119, 21);
			this.rbTypeScale.TabIndex = 0;
			this.rbTypeScale.TabStop = true;
			this.rbTypeScale.Text = "Scale the Data";
			this.rbTypeScale.UseVisualStyleBackColor = true;
			// 
			// txtDays
			// 
			this.txtDays.Location = new System.Drawing.Point(568, 236);
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(83, 23);
			this.txtDays.TabIndex = 9;
			this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(549, 216);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(125, 17);
			this.label8.TabIndex = 8;
			this.label8.Text = "Days To Generate";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label21);
			this.groupBox1.Controls.Add(this.txtVolatilityPeriod);
			this.groupBox1.Controls.Add(this.chkVolatility);
			this.groupBox1.Controls.Add(this.chkAltEma);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.txtRoCperiod);
			this.groupBox1.Controls.Add(this.chkRofChng);
			this.groupBox1.Controls.Add(this.groupBox9);
			this.groupBox1.Controls.Add(this.groupBox8);
			this.groupBox1.Controls.Add(this.chkDeltas);
			this.groupBox1.Controls.Add(this.chkAllOHLC);
			this.groupBox1.Controls.Add(this.chkClose);
			this.groupBox1.Controls.Add(this.chkOpen);
			this.groupBox1.Controls.Add(this.chkHigh);
			this.groupBox1.Controls.Add(this.chkLow);
			this.groupBox1.Controls.Add(this.chkAllIndicators);
			this.groupBox1.Controls.Add(this.chkATR);
			this.groupBox1.Controls.Add(this.chkStoch);
			this.groupBox1.Controls.Add(this.chkMACD);
			this.groupBox1.Controls.Add(this.chkCCI);
			this.groupBox1.Controls.Add(this.groupBox7);
			this.groupBox1.Location = new System.Drawing.Point(24, 144);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(511, 313);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Indicators to generate";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(191, 268);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(49, 17);
			this.label21.TabIndex = 42;
			this.label21.Text = "Period";
			// 
			// txtVolatilityPeriod
			// 
			this.txtVolatilityPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtVolatilityPeriod.Location = new System.Drawing.Point(153, 267);
			this.txtVolatilityPeriod.Name = "txtVolatilityPeriod";
			this.txtVolatilityPeriod.Size = new System.Drawing.Size(32, 20);
			this.txtVolatilityPeriod.TabIndex = 41;
			this.txtVolatilityPeriod.Text = "20";
			this.txtVolatilityPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtVolatilityPeriod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// chkVolatility
			// 
			this.chkVolatility.AutoSize = true;
			this.chkVolatility.Location = new System.Drawing.Point(25, 267);
			this.chkVolatility.Name = "chkVolatility";
			this.chkVolatility.Size = new System.Drawing.Size(74, 21);
			this.chkVolatility.TabIndex = 40;
			this.chkVolatility.Text = "Volitility";
			this.chkVolatility.UseVisualStyleBackColor = true;
			this.chkVolatility.CheckStateChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// chkAltEma
			// 
			this.chkAltEma.AutoSize = true;
			this.chkAltEma.Checked = true;
			this.chkAltEma.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAltEma.Location = new System.Drawing.Point(295, 24);
			this.chkAltEma.Name = "chkAltEma";
			this.chkAltEma.Size = new System.Drawing.Size(146, 21);
			this.chkAltEma.TabIndex = 39;
			this.chkAltEma.Text = "Use Alternate EMA";
			this.chkAltEma.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(191, 231);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(49, 17);
			this.label14.TabIndex = 38;
			this.label14.Text = "Period";
			// 
			// txtRoCperiod
			// 
			this.txtRoCperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRoCperiod.Location = new System.Drawing.Point(153, 230);
			this.txtRoCperiod.Name = "txtRoCperiod";
			this.txtRoCperiod.Size = new System.Drawing.Size(32, 20);
			this.txtRoCperiod.TabIndex = 37;
			this.txtRoCperiod.Text = "14";
			this.txtRoCperiod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtRoCperiod.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// chkRofChng
			// 
			this.chkRofChng.AutoSize = true;
			this.chkRofChng.Location = new System.Drawing.Point(25, 230);
			this.chkRofChng.Name = "chkRofChng";
			this.chkRofChng.Size = new System.Drawing.Size(126, 21);
			this.chkRofChng.TabIndex = 36;
			this.chkRofChng.Text = "Rate of Change";
			this.chkRofChng.UseVisualStyleBackColor = true;
			this.chkRofChng.CheckStateChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.chkMA1);
			this.groupBox9.Controls.Add(this.label16);
			this.groupBox9.Controls.Add(this.txtMA1period);
			this.groupBox9.Controls.Add(this.rbMA1ema);
			this.groupBox9.Controls.Add(this.rbMA1sma);
			this.groupBox9.Location = new System.Drawing.Point(265, 63);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(215, 60);
			this.groupBox9.TabIndex = 34;
			this.groupBox9.TabStop = false;
			// 
			// chkMA1
			// 
			this.chkMA1.AutoSize = true;
			this.chkMA1.Checked = true;
			this.chkMA1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkMA1.Location = new System.Drawing.Point(36, 10);
			this.chkMA1.Name = "chkMA1";
			this.chkMA1.Size = new System.Drawing.Size(141, 21);
			this.chkMA1.TabIndex = 35;
			this.chkMA1.Text = "Moving Average 1";
			this.chkMA1.UseVisualStyleBackColor = true;
			this.chkMA1.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(156, 33);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(49, 17);
			this.label16.TabIndex = 34;
			this.label16.Text = "Period";
			// 
			// txtMA1period
			// 
			this.txtMA1period.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMA1period.Location = new System.Drawing.Point(118, 32);
			this.txtMA1period.Name = "txtMA1period";
			this.txtMA1period.Size = new System.Drawing.Size(32, 20);
			this.txtMA1period.TabIndex = 33;
			this.txtMA1period.Text = "14";
			this.txtMA1period.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMA1period.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// rbMA1ema
			// 
			this.rbMA1ema.AutoSize = true;
			this.rbMA1ema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMA1ema.Location = new System.Drawing.Point(60, 35);
			this.rbMA1ema.Name = "rbMA1ema";
			this.rbMA1ema.Size = new System.Drawing.Size(48, 17);
			this.rbMA1ema.TabIndex = 32;
			this.rbMA1ema.Text = "EMA";
			this.rbMA1ema.UseVisualStyleBackColor = true;
			// 
			// rbMA1sma
			// 
			this.rbMA1sma.AutoSize = true;
			this.rbMA1sma.Checked = true;
			this.rbMA1sma.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMA1sma.Location = new System.Drawing.Point(10, 35);
			this.rbMA1sma.Name = "rbMA1sma";
			this.rbMA1sma.Size = new System.Drawing.Size(48, 17);
			this.rbMA1sma.TabIndex = 31;
			this.rbMA1sma.TabStop = true;
			this.rbMA1sma.Text = "SMA";
			this.rbMA1sma.UseVisualStyleBackColor = true;
			this.rbMA1sma.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.chkMA2);
			this.groupBox8.Controls.Add(this.label12);
			this.groupBox8.Controls.Add(this.txtMA2period);
			this.groupBox8.Controls.Add(this.rbMA2ema);
			this.groupBox8.Controls.Add(this.rbMA2sma);
			this.groupBox8.Location = new System.Drawing.Point(265, 143);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(215, 60);
			this.groupBox8.TabIndex = 33;
			this.groupBox8.TabStop = false;
			// 
			// chkMA2
			// 
			this.chkMA2.AutoSize = true;
			this.chkMA2.Checked = true;
			this.chkMA2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkMA2.Location = new System.Drawing.Point(36, 10);
			this.chkMA2.Name = "chkMA2";
			this.chkMA2.Size = new System.Drawing.Size(141, 21);
			this.chkMA2.TabIndex = 41;
			this.chkMA2.Text = "Moving Average 2";
			this.chkMA2.UseVisualStyleBackColor = true;
			this.chkMA2.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(156, 33);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(49, 17);
			this.label12.TabIndex = 40;
			this.label12.Text = "Period";
			// 
			// txtMA2period
			// 
			this.txtMA2period.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMA2period.Location = new System.Drawing.Point(118, 32);
			this.txtMA2period.Name = "txtMA2period";
			this.txtMA2period.Size = new System.Drawing.Size(32, 20);
			this.txtMA2period.TabIndex = 39;
			this.txtMA2period.Text = "14";
			this.txtMA2period.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMA2period.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// rbMA2ema
			// 
			this.rbMA2ema.AutoSize = true;
			this.rbMA2ema.Checked = true;
			this.rbMA2ema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMA2ema.Location = new System.Drawing.Point(60, 35);
			this.rbMA2ema.Name = "rbMA2ema";
			this.rbMA2ema.Size = new System.Drawing.Size(48, 17);
			this.rbMA2ema.TabIndex = 38;
			this.rbMA2ema.TabStop = true;
			this.rbMA2ema.Text = "EMA";
			this.rbMA2ema.UseVisualStyleBackColor = true;
			// 
			// rbMA2sma
			// 
			this.rbMA2sma.AutoSize = true;
			this.rbMA2sma.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMA2sma.Location = new System.Drawing.Point(10, 35);
			this.rbMA2sma.Name = "rbMA2sma";
			this.rbMA2sma.Size = new System.Drawing.Size(48, 17);
			this.rbMA2sma.TabIndex = 37;
			this.rbMA2sma.Text = "SMA";
			this.rbMA2sma.UseVisualStyleBackColor = true;
			this.rbMA2sma.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// chkStoch
			// 
			this.chkStoch.AutoSize = true;
			this.chkStoch.Location = new System.Drawing.Point(25, 50);
			this.chkStoch.Name = "chkStoch";
			this.chkStoch.Size = new System.Drawing.Size(155, 21);
			this.chkStoch.TabIndex = 2;
			this.chkStoch.Tag = "Indicator";
			this.chkStoch.Text = "Stochastic Oscillator";
			this.chkStoch.UseVisualStyleBackColor = true;
			this.chkStoch.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.chkMA3);
			this.groupBox7.Controls.Add(this.label17);
			this.groupBox7.Controls.Add(this.txtMA3period);
			this.groupBox7.Controls.Add(this.rbMA3ema);
			this.groupBox7.Controls.Add(this.rbMA3sma);
			this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox7.Location = new System.Drawing.Point(265, 225);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(215, 60);
			this.groupBox7.TabIndex = 32;
			this.groupBox7.TabStop = false;
			// 
			// chkMA3
			// 
			this.chkMA3.AutoSize = true;
			this.chkMA3.Location = new System.Drawing.Point(36, 10);
			this.chkMA3.Name = "chkMA3";
			this.chkMA3.Size = new System.Drawing.Size(141, 21);
			this.chkMA3.TabIndex = 37;
			this.chkMA3.Text = "Moving Average 3";
			this.chkMA3.UseVisualStyleBackColor = true;
			this.chkMA3.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(156, 33);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(49, 17);
			this.label17.TabIndex = 36;
			this.label17.Text = "Period";
			// 
			// txtMA3period
			// 
			this.txtMA3period.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMA3period.Location = new System.Drawing.Point(118, 32);
			this.txtMA3period.Name = "txtMA3period";
			this.txtMA3period.Size = new System.Drawing.Size(32, 20);
			this.txtMA3period.TabIndex = 35;
			this.txtMA3period.Text = "14";
			this.txtMA3period.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMA3period.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
			// 
			// rbMA3ema
			// 
			this.rbMA3ema.AutoSize = true;
			this.rbMA3ema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMA3ema.Location = new System.Drawing.Point(60, 35);
			this.rbMA3ema.Name = "rbMA3ema";
			this.rbMA3ema.Size = new System.Drawing.Size(48, 17);
			this.rbMA3ema.TabIndex = 34;
			this.rbMA3ema.Text = "EMA";
			this.rbMA3ema.UseVisualStyleBackColor = true;
			// 
			// rbMA3sma
			// 
			this.rbMA3sma.AutoSize = true;
			this.rbMA3sma.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMA3sma.Location = new System.Drawing.Point(10, 35);
			this.rbMA3sma.Name = "rbMA3sma";
			this.rbMA3sma.Size = new System.Drawing.Size(48, 17);
			this.rbMA3sma.TabIndex = 33;
			this.rbMA3sma.Text = "SMA";
			this.rbMA3sma.UseVisualStyleBackColor = true;
			this.rbMA3sma.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.MainTab);
			this.tabControl1.Controls.Add(this.ParametersTab);
			this.tabControl1.Controls.Add(this.ClassificationTab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.ItemSize = new System.Drawing.Size(160, 21);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(718, 508);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// MainTab
			// 
			this.MainTab.Controls.Add(this.chkFixOutliers);
			this.MainTab.Controls.Add(this.chkIncludeHeader);
			this.MainTab.Controls.Add(this.chkScaleData);
			this.MainTab.Controls.Add(this.btnLoadFile);
			this.MainTab.Controls.Add(this.label9);
			this.MainTab.Controls.Add(this.txtOutputFolder);
			this.MainTab.Controls.Add(this.btnFindOutput);
			this.MainTab.Controls.Add(this.btnExit);
			this.MainTab.Controls.Add(this.btnGenerate);
			this.MainTab.Controls.Add(this.txtDays);
			this.MainTab.Controls.Add(this.label8);
			this.MainTab.Controls.Add(this.groupBox1);
			this.MainTab.Controls.Add(this.label1);
			this.MainTab.Controls.Add(this.txtFolderLocation);
			this.MainTab.Controls.Add(this.btnFindFolder);
			this.MainTab.Location = new System.Drawing.Point(4, 25);
			this.MainTab.Name = "MainTab";
			this.MainTab.Padding = new System.Windows.Forms.Padding(3);
			this.MainTab.Size = new System.Drawing.Size(710, 479);
			this.MainTab.TabIndex = 0;
			this.MainTab.Text = "Main Display";
			this.MainTab.UseVisualStyleBackColor = true;
			// 
			// chkFixOutliers
			// 
			this.chkFixOutliers.AutoSize = true;
			this.chkFixOutliers.Enabled = false;
			this.chkFixOutliers.Location = new System.Drawing.Point(282, 7);
			this.chkFixOutliers.Name = "chkFixOutliers";
			this.chkFixOutliers.Size = new System.Drawing.Size(97, 21);
			this.chkFixOutliers.TabIndex = 18;
			this.chkFixOutliers.Text = "Fix Outliers";
			this.chkFixOutliers.UseVisualStyleBackColor = true;
			this.chkFixOutliers.CheckedChanged += new System.EventHandler(this.chkFixOutliers_CheckedChanged);
			// 
			// btnExit
			// 
			this.btnExit.AutoSize = true;
			this.btnExit.Location = new System.Drawing.Point(585, 344);
			this.btnExit.Margin = new System.Windows.Forms.Padding(4);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(66, 27);
			this.btnExit.TabIndex = 11;
			this.btnExit.Text = "Exit";
			this.btnExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnGenerate
			// 
			this.btnGenerate.AutoSize = true;
			this.btnGenerate.Location = new System.Drawing.Point(552, 287);
			this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(125, 27);
			this.btnGenerate.TabIndex = 10;
			this.btnGenerate.Text = "Generate Output";
			this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// ClassificationTab
			// 
			this.ClassificationTab.Controls.Add(this.groupBox11);
			this.ClassificationTab.Location = new System.Drawing.Point(4, 25);
			this.ClassificationTab.Name = "ClassificationTab";
			this.ClassificationTab.Padding = new System.Windows.Forms.Padding(3);
			this.ClassificationTab.Size = new System.Drawing.Size(710, 479);
			this.ClassificationTab.TabIndex = 2;
			this.ClassificationTab.Text = "Classification Cols";
			this.ClassificationTab.UseVisualStyleBackColor = true;
			// 
			// groupBox11
			// 
			this.groupBox11.Controls.Add(this.cmbClassBase2);
			this.groupBox11.Controls.Add(this.cmbClassBase1);
			this.groupBox11.Controls.Add(this.label20);
			this.groupBox11.Controls.Add(this.label19);
			this.groupBox11.Controls.Add(this.label18);
			this.groupBox11.Controls.Add(this.label15);
			this.groupBox11.Controls.Add(this.txtClassLabel1);
			this.groupBox11.Controls.Add(this.txtClassLabel2);
			this.groupBox11.Controls.Add(this.chkAddUpDown);
			this.groupBox11.Location = new System.Drawing.Point(26, 22);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.Size = new System.Drawing.Size(427, 131);
			this.groupBox11.TabIndex = 0;
			this.groupBox11.TabStop = false;
			this.groupBox11.Text = "Up/Down";
			// 
			// cmbClassBase2
			// 
			this.cmbClassBase2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbClassBase2.FormattingEnabled = true;
			this.cmbClassBase2.Items.AddRange(new object[] {
            "Open",
            "High",
            "Low",
            "Close"});
			this.cmbClassBase2.Location = new System.Drawing.Point(291, 77);
			this.cmbClassBase2.Name = "cmbClassBase2";
			this.cmbClassBase2.Size = new System.Drawing.Size(75, 24);
			this.cmbClassBase2.TabIndex = 17;
			this.cmbClassBase2.Text = "Close";
			// 
			// cmbClassBase1
			// 
			this.cmbClassBase1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbClassBase1.FormattingEnabled = true;
			this.cmbClassBase1.Items.AddRange(new object[] {
            "Open",
            "High",
            "Low",
            "Close"});
			this.cmbClassBase1.Location = new System.Drawing.Point(291, 42);
			this.cmbClassBase1.Name = "cmbClassBase1";
			this.cmbClassBase1.Size = new System.Drawing.Size(75, 24);
			this.cmbClassBase1.TabIndex = 16;
			this.cmbClassBase1.Text = "Close";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(215, 80);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(71, 17);
			this.label20.TabIndex = 15;
			this.label20.Text = "Based On";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(215, 45);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(71, 17);
			this.label19.TabIndex = 14;
			this.label19.Text = "Based On";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(26, 80);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(67, 17);
			this.label18.TabIndex = 13;
			this.label18.Text = "Col Label";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(26, 45);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(67, 17);
			this.label15.TabIndex = 12;
			this.label15.Text = "Col Label";
			// 
			// txtClassLabel1
			// 
			this.txtClassLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtClassLabel1.Location = new System.Drawing.Point(114, 42);
			this.txtClassLabel1.Name = "txtClassLabel1";
			this.txtClassLabel1.Size = new System.Drawing.Size(83, 23);
			this.txtClassLabel1.TabIndex = 11;
			this.txtClassLabel1.Text = "up";
			this.txtClassLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtClassLabel2
			// 
			this.txtClassLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtClassLabel2.Location = new System.Drawing.Point(114, 77);
			this.txtClassLabel2.Name = "txtClassLabel2";
			this.txtClassLabel2.Size = new System.Drawing.Size(83, 23);
			this.txtClassLabel2.TabIndex = 10;
			this.txtClassLabel2.Text = "down";
			this.txtClassLabel2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// chkAddUpDown
			// 
			this.chkAddUpDown.AutoSize = true;
			this.chkAddUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkAddUpDown.Location = new System.Drawing.Point(149, 16);
			this.chkAddUpDown.Name = "chkAddUpDown";
			this.chkAddUpDown.Size = new System.Drawing.Size(96, 17);
			this.chkAddUpDown.TabIndex = 0;
			this.chkAddUpDown.Text = "Add To Output";
			this.chkAddUpDown.UseVisualStyleBackColor = true;
			this.chkAddUpDown.CheckedChanged += new System.EventHandler(this.Indicator_CheckedChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.AutoSize = false;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 507);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(718, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// frmGeneratorMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(718, 529);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = global::Forex_test_data_generator.Properties.Resources.dollar6;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmGeneratorMain";
			this.Text = "Forex Indicator Data Generator";
			this.Load += new System.EventHandler(this.frmTestDataMain_Load);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ParametersTab.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			this.groupBox10.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.MainTab.ResumeLayout(false);
			this.MainTab.PerformLayout();
			this.ClassificationTab.ResumeLayout(false);
			this.groupBox11.ResumeLayout(false);
			this.groupBox11.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtRoCperiod;
		private System.Windows.Forms.CheckBox chkRofChng;
		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.RadioButton rbTypeStd;
		private System.Windows.Forms.RadioButton rbTypeScale;
		private System.Windows.Forms.CheckBox chkFixOutliers;
		private System.Windows.Forms.RadioButton rbTypeNorm;
		private System.Windows.Forms.TabPage ClassificationTab;
		private System.Windows.Forms.GroupBox groupBox11;
		private System.Windows.Forms.ComboBox cmbClassBase2;
		private System.Windows.Forms.ComboBox cmbClassBase1;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtClassLabel1;
		private System.Windows.Forms.TextBox txtClassLabel2;
		private System.Windows.Forms.CheckBox chkAddUpDown;
		private System.Windows.Forms.CheckBox chkAltEma;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox txtVolatilityPeriod;
		private System.Windows.Forms.CheckBox chkVolatility;
	}
}

