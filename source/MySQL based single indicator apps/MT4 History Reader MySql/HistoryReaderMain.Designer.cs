namespace MT4_History_Reader_MySql
{
    partial class HistoryReaderMain
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.btnExit = new System.Windows.Forms.Button();
			this.btnRun = new System.Windows.Forms.Button();
			this.rbDeleteOldData = new System.Windows.Forms.RadioButton();
			this.rbAppendData = new System.Windows.Forms.RadioButton();
			this.btnGetServers = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTables = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.btnGetTables = new System.Windows.Forms.Button();
			this.cmbDatabases = new System.Windows.Forms.ComboBox();
			this.btnGetDBList = new System.Windows.Forms.Button();
			this.cmbInstances = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.cmbSqlServers = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnGetFolder = new System.Windows.Forms.Button();
			this.rbDelimitSpace = new System.Windows.Forms.RadioButton();
			this.rbDelimitTab = new System.Windows.Forms.RadioButton();
			this.rbDelimitComma = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbFileNames = new System.Windows.Forms.ComboBox();
			this.txtFolderName = new System.Windows.Forms.TextBox();
			this.rbMSSqlServer = new System.Windows.Forms.RadioButton();
			this.rbMySqlServer = new System.Windows.Forms.RadioButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.VersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnExit
			// 
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(482, 431);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 32);
			this.btnExit.TabIndex = 8;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnRun
			// 
			this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRun.Location = new System.Drawing.Point(181, 431);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(75, 32);
			this.btnRun.TabIndex = 7;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// rbDeleteOldData
			// 
			this.rbDeleteOldData.AutoSize = true;
			this.rbDeleteOldData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbDeleteOldData.Location = new System.Drawing.Point(385, 152);
			this.rbDeleteOldData.Name = "rbDeleteOldData";
			this.rbDeleteOldData.Size = new System.Drawing.Size(127, 21);
			this.rbDeleteOldData.TabIndex = 71;
			this.rbDeleteOldData.Text = "Delete Old Data";
			this.rbDeleteOldData.UseVisualStyleBackColor = true;
			// 
			// rbAppendData
			// 
			this.rbAppendData.AutoSize = true;
			this.rbAppendData.Checked = true;
			this.rbAppendData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbAppendData.Location = new System.Drawing.Point(189, 152);
			this.rbAppendData.Name = "rbAppendData";
			this.rbAppendData.Size = new System.Drawing.Size(140, 21);
			this.rbAppendData.TabIndex = 70;
			this.rbAppendData.TabStop = true;
			this.rbAppendData.Text = "Append New Data";
			this.rbAppendData.UseVisualStyleBackColor = true;
			// 
			// btnGetServers
			// 
			this.btnGetServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetServers.Location = new System.Drawing.Point(660, 31);
			this.btnGetServers.Name = "btnGetServers";
			this.btnGetServers.Size = new System.Drawing.Size(43, 30);
			this.btnGetServers.TabIndex = 69;
			this.btnGetServers.Text = "...";
			this.btnGetServers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnGetServers.UseVisualStyleBackColor = true;
			this.btnGetServers.Click += new System.EventHandler(this.btnGetServers_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rbDeleteOldData);
			this.groupBox2.Controls.Add(this.rbAppendData);
			this.groupBox2.Controls.Add(this.btnGetServers);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.cmbTables);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.btnGetTables);
			this.groupBox2.Controls.Add(this.cmbDatabases);
			this.groupBox2.Controls.Add(this.btnGetDBList);
			this.groupBox2.Controls.Add(this.cmbInstances);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.cmbSqlServers);
			this.groupBox2.Location = new System.Drawing.Point(28, 205);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(739, 208);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Output";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(442, 71);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 13);
			this.label4.TabIndex = 68;
			this.label4.Text = "Available Tables";
			// 
			// cmbTables
			// 
			this.cmbTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTables.FormattingEnabled = true;
			this.cmbTables.Location = new System.Drawing.Point(368, 89);
			this.cmbTables.MaxDropDownItems = 16;
			this.cmbTables.Name = "cmbTables";
			this.cmbTables.Size = new System.Drawing.Size(260, 24);
			this.cmbTables.Sorted = true;
			this.cmbTables.TabIndex = 66;
			this.cmbTables.TextUpdate += new System.EventHandler(this.cmbTables_TextUpdate);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(101, 69);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 13);
			this.label7.TabIndex = 63;
			this.label7.Text = "Available Databases";
			// 
			// btnGetTables
			// 
			this.btnGetTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetTables.Location = new System.Drawing.Point(634, 89);
			this.btnGetTables.Name = "btnGetTables";
			this.btnGetTables.Size = new System.Drawing.Size(90, 25);
			this.btnGetTables.TabIndex = 65;
			this.btnGetTables.Text = "Get Table List";
			this.btnGetTables.UseVisualStyleBackColor = true;
			this.btnGetTables.Click += new System.EventHandler(this.btnGetTables_Click);
			// 
			// cmbDatabases
			// 
			this.cmbDatabases.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDatabases.FormattingEnabled = true;
			this.cmbDatabases.Location = new System.Drawing.Point(32, 87);
			this.cmbDatabases.Name = "cmbDatabases";
			this.cmbDatabases.Size = new System.Drawing.Size(233, 24);
			this.cmbDatabases.Sorted = true;
			this.cmbDatabases.TabIndex = 62;
			this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
			// 
			// btnGetDBList
			// 
			this.btnGetDBList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetDBList.Location = new System.Drawing.Point(271, 87);
			this.btnGetDBList.Name = "btnGetDBList";
			this.btnGetDBList.Size = new System.Drawing.Size(75, 26);
			this.btnGetDBList.TabIndex = 61;
			this.btnGetDBList.Text = "Get DB List";
			this.btnGetDBList.UseVisualStyleBackColor = true;
			this.btnGetDBList.Click += new System.EventHandler(this.btnGetDBList_Click);
			// 
			// cmbInstances
			// 
			this.cmbInstances.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbInstances.FormattingEnabled = true;
			this.cmbInstances.Location = new System.Drawing.Point(352, 33);
			this.cmbInstances.Name = "cmbInstances";
			this.cmbInstances.Size = new System.Drawing.Size(282, 24);
			this.cmbInstances.Sorted = true;
			this.cmbInstances.TabIndex = 60;
			this.cmbInstances.SelectedIndexChanged += new System.EventHandler(this.cmbInstances_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(382, 17);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(118, 13);
			this.label9.TabIndex = 59;
			this.label9.Text = "Server Instance Names";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(120, 17);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(67, 13);
			this.label10.TabIndex = 57;
			this.label10.Text = "SQL Servers";
			// 
			// cmbSqlServers
			// 
			this.cmbSqlServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbSqlServers.FormattingEnabled = true;
			this.cmbSqlServers.Location = new System.Drawing.Point(32, 33);
			this.cmbSqlServers.Name = "cmbSqlServers";
			this.cmbSqlServers.Size = new System.Drawing.Size(280, 24);
			this.cmbSqlServers.Sorted = true;
			this.cmbSqlServers.TabIndex = 55;
			this.cmbSqlServers.SelectedIndexChanged += new System.EventHandler(this.cmbSqlServers_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnGetFolder);
			this.groupBox1.Controls.Add(this.rbDelimitSpace);
			this.groupBox1.Controls.Add(this.rbDelimitTab);
			this.groupBox1.Controls.Add(this.rbDelimitComma);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cmbFileNames);
			this.groupBox1.Controls.Add(this.txtFolderName);
			this.groupBox1.Location = new System.Drawing.Point(28, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(739, 143);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Input";
			// 
			// btnGetFolder
			// 
			this.btnGetFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetFolder.Location = new System.Drawing.Point(670, 29);
			this.btnGetFolder.Name = "btnGetFolder";
			this.btnGetFolder.Size = new System.Drawing.Size(43, 30);
			this.btnGetFolder.TabIndex = 4;
			this.btnGetFolder.Text = "...";
			this.btnGetFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnGetFolder.UseVisualStyleBackColor = true;
			this.btnGetFolder.Click += new System.EventHandler(this.btnGetFolder_Click);
			// 
			// rbDelimitSpace
			// 
			this.rbDelimitSpace.AutoSize = true;
			this.rbDelimitSpace.Location = new System.Drawing.Point(608, 92);
			this.rbDelimitSpace.Name = "rbDelimitSpace";
			this.rbDelimitSpace.Size = new System.Drawing.Size(102, 17);
			this.rbDelimitSpace.TabIndex = 6;
			this.rbDelimitSpace.Text = "Space Delimited";
			this.rbDelimitSpace.UseVisualStyleBackColor = true;
			// 
			// rbDelimitTab
			// 
			this.rbDelimitTab.AutoSize = true;
			this.rbDelimitTab.Location = new System.Drawing.Point(500, 92);
			this.rbDelimitTab.Name = "rbDelimitTab";
			this.rbDelimitTab.Size = new System.Drawing.Size(90, 17);
			this.rbDelimitTab.TabIndex = 5;
			this.rbDelimitTab.Text = "Tab Delimited";
			this.rbDelimitTab.UseVisualStyleBackColor = true;
			// 
			// rbDelimitComma
			// 
			this.rbDelimitComma.AutoSize = true;
			this.rbDelimitComma.Checked = true;
			this.rbDelimitComma.Location = new System.Drawing.Point(369, 92);
			this.rbDelimitComma.Name = "rbDelimitComma";
			this.rbDelimitComma.Size = new System.Drawing.Size(106, 17);
			this.rbDelimitComma.TabIndex = 4;
			this.rbDelimitComma.TabStop = true;
			this.rbDelimitComma.Text = "Comma Delimited";
			this.rbDelimitComma.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(164, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "File Name";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(220, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Folder Location";
			// 
			// cmbFileNames
			// 
			this.cmbFileNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbFileNames.FormattingEnabled = true;
			this.cmbFileNames.Location = new System.Drawing.Point(32, 88);
			this.cmbFileNames.Name = "cmbFileNames";
			this.cmbFileNames.Size = new System.Drawing.Size(331, 24);
			this.cmbFileNames.TabIndex = 1;
			this.cmbFileNames.SelectedIndexChanged += new System.EventHandler(this.cmbFileNames_SelectedIndexChanged);
			// 
			// txtFolderName
			// 
			this.txtFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFolderName.Location = new System.Drawing.Point(32, 33);
			this.txtFolderName.Name = "txtFolderName";
			this.txtFolderName.Size = new System.Drawing.Size(619, 23);
			this.txtFolderName.TabIndex = 0;
			this.txtFolderName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtFolderName.TextChanged += new System.EventHandler(this.txtFolderName_TextChanged);
			// 
			// rbMSSqlServer
			// 
			this.rbMSSqlServer.AutoSize = true;
			this.rbMSSqlServer.BackColor = System.Drawing.Color.Salmon;
			this.rbMSSqlServer.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.rbMSSqlServer.FlatAppearance.BorderSize = 2;
			this.rbMSSqlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMSSqlServer.Location = new System.Drawing.Point(413, 178);
			this.rbMSSqlServer.Name = "rbMSSqlServer";
			this.rbMSSqlServer.Size = new System.Drawing.Size(137, 24);
			this.rbMSSqlServer.TabIndex = 73;
			this.rbMSSqlServer.Text = "MS SQL Server";
			this.rbMSSqlServer.UseVisualStyleBackColor = false;
			// 
			// rbMySqlServer
			// 
			this.rbMySqlServer.AutoSize = true;
			this.rbMySqlServer.BackColor = System.Drawing.Color.LightGreen;
			this.rbMySqlServer.Checked = true;
			this.rbMySqlServer.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.rbMySqlServer.FlatAppearance.BorderSize = 2;
			this.rbMySqlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbMySqlServer.Location = new System.Drawing.Point(199, 178);
			this.rbMySqlServer.Name = "rbMySqlServer";
			this.rbMySqlServer.Size = new System.Drawing.Size(129, 24);
			this.rbMySqlServer.TabIndex = 72;
			this.rbMySqlServer.TabStop = true;
			this.rbMySqlServer.Text = "MySQL Server";
			this.rbMySqlServer.UseVisualStyleBackColor = false;
			this.rbMySqlServer.CheckedChanged += new System.EventHandler(this.rbMySqlServer_CheckedChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.VersionLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 511);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(802, 22);
			this.statusStrip1.TabIndex = 74;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(712, 17);
			this.StatusLabel.Spring = true;
			this.StatusLabel.Text = "Idle ...";
			this.StatusLabel.TextChanged += new System.EventHandler(this.StatusLabel_TextChanged);
			// 
			// VersionLabel
			// 
			this.VersionLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.VersionLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.VersionLabel.Size = new System.Drawing.Size(75, 17);
			this.VersionLabel.Text = "Version 2.0";
			// 
			// HistoryReaderMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(802, 533);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.rbMSSqlServer);
			this.Controls.Add(this.rbMySqlServer);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "HistoryReaderMain";
			this.Text = "MT4 History Reader";
			this.Shown += new System.EventHandler(this.HistoryReaderMain_Shown);
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

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RadioButton rbDeleteOldData;
        private System.Windows.Forms.RadioButton rbAppendData;
        private System.Windows.Forms.Button btnGetServers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cmbTables;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGetTables;
        public System.Windows.Forms.ComboBox cmbDatabases;
        private System.Windows.Forms.Button btnGetDBList;
        public System.Windows.Forms.ComboBox cmbInstances;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox cmbSqlServers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetFolder;
        private System.Windows.Forms.RadioButton rbDelimitSpace;
        private System.Windows.Forms.RadioButton rbDelimitTab;
        private System.Windows.Forms.RadioButton rbDelimitComma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFileNames;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.RadioButton rbMSSqlServer;
        private System.Windows.Forms.RadioButton rbMySqlServer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel VersionLabel;
    }
}

