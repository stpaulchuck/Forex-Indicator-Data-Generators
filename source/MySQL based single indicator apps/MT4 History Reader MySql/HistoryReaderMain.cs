using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MT4_History_Reader_MySql
{
    public partial class HistoryReaderMain : Form
    {

        //************** global vars ***************
        bool bInitializing = true;
        StringCollection PrimaryCurrencyList, SecondaryCurrencyList;
        clsDBmasterHandler MasterDBhandler = new clsDBmasterHandler();

        //************* constructor **************
        public HistoryReaderMain()
        {
            InitializeComponent();
            bInitializing = false;
            Application.DoEvents();
            MasterDBhandler.StatusLabel = this.StatusLabel;
        }

        //**************** methods *****************
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool VerifyFolder()
        {
            if (Directory.Exists(txtFolderName.Text) == false)
            {
                MessageBox.Show(this, "The folder in the text box does not exist\nor is not reachable. Try again.", "Path Error", MessageBoxButtons.OK);
                return false;
            }
            cmbFileNames.Items.Clear();
            string[] NameList1 = Directory.GetFiles(txtFolderName.Text, "*.txt", SearchOption.TopDirectoryOnly);
            string[] NameList2 = Directory.GetFiles(txtFolderName.Text, "*.csv", SearchOption.TopDirectoryOnly);
            if (NameList1.Length > 0)
            {
                foreach (string n1 in NameList1) cmbFileNames.Items.Add(n1.Substring(n1.LastIndexOf('\\') + 1));
            }
            if (NameList2.Length > 0)
            {
                foreach (string n1 in NameList2) cmbFileNames.Items.Add(n1.Substring(n1.LastIndexOf('\\') + 1));
            }
            if (cmbFileNames.Items.Count > 0) cmbFileNames.Text = cmbFileNames.Items[0].ToString();
            return true;
        }

        private void btnGetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.ShowNewFolderButton = false;
            diag.RootFolder = Environment.SpecialFolder.MyComputer;
            if (Directory.Exists(txtFolderName.Text)) diag.SelectedPath = txtFolderName.Text;
            else diag.SelectedPath = Directory.GetCurrentDirectory();
            if (diag.ShowDialog(this) == DialogResult.OK)
            {
                txtFolderName.Text = diag.SelectedPath;
            }
        }

        private void cmbFileNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            Properties.Settings.Default.LastFile = cmbFileNames.Text;
            Properties.Settings.Default.Save();
        }

        private void txtFolderName_TextChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            if (!VerifyFolder()) return;
            Properties.Settings.Default.LastFolder = txtFolderName.Text;
            Properties.Settings.Default.Save();
        }

        private void HistoryReaderMain_Shown(object sender, EventArgs e)
        {
            btnRun.Focus();
            StatusLabel.Text = "Loading setups and initial database.";
            //----these next two have to come early as the gettables method needs them
            PrimaryCurrencyList = Properties.Settings.Default.PrimaryCurrencyList;
            SecondaryCurrencyList = Properties.Settings.Default.SecondaryCurrencyList;
            bInitializing = true;
            //---- set up last data file
            string stemp = Properties.Settings.Default.LastFolder;
            if (stemp.Length > 3)
            {
                txtFolderName.Text = stemp;
                if (VerifyFolder())
                {
                    stemp = Properties.Settings.Default.LastFile;
                    if (stemp.Length > 3) cmbFileNames.Text = stemp;
                }
                else txtFolderName.Text = "{ folder not set }";
            }
            else txtFolderName.Text = "{ folder not set }";
            if (Properties.Settings.Default.MySqlServer) rbMySqlServer.Checked = true;
            else rbMSSqlServer.Checked = true;

            //---- set up server list which cascades into db's and tables
            btnGetServers_Click(null, EventArgs.Empty);

            StatusLabel.Text = "Idle ...";
        }

        private void btnGetServers_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            // clear all the display boxes
            cmbTables.Items.Clear();
            cmbTables.Text = "";
            cmbDatabases.Items.Clear();
            cmbDatabases.Text = "";
            cmbSqlServers.Items.Clear();
            cmbSqlServers.Text = "";
            cmbInstances.Items.Clear();
            cmbInstances.Text = "";
            // start the fetch cascade sequence
            try
            {
                bInitializing = true;
                MasterDBhandler.IsMySqlDB = rbMySqlServer.Checked;
                bool bResult = MasterDBhandler.FindServers(cmbSqlServers, cmbInstances);
                if (cmbSqlServers.Items.Count <= 0)
                    MessageBox.Show(this, "No Servers Found", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (bResult)
                    {
                        MasterDBhandler.ServerName = cmbSqlServers.Text;
                        MasterDBhandler.InstanceName = cmbInstances.Text;
                        bResult = MasterDBhandler.FindDatabases(cmbDatabases);
                        if (cmbDatabases.Items.Count <= 0)
                            MessageBox.Show(this, "No Databases Found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            if (bResult)
                            {
                                MasterDBhandler.DBname = cmbDatabases.Text;
                                bResult = MasterDBhandler.FindTables(cmbTables);
                                if (cmbTables.Items.Count <= 0)
                                    MessageBox.Show(this, "No Tables Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            } // result = true
                        } // db's > 0
                    } // result = true
                } // servers > 0
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error finding servers.\r\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
            bInitializing = false;
        }

        private void cmbSqlServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            List<string> SvrInst = MasterDBhandler.GetInstances(cmbSqlServers.Text);
            cmbInstances.Items.Clear();
            cmbInstances.Text = "";
            foreach (string s in SvrInst)
            {
                cmbInstances.Items.Add(s);
            }
            if (cmbInstances.Items.Count > 0)
            {
                cmbInstances.Text = cmbInstances.Items[0].ToString();
            }
            MasterDBhandler.ServerName = cmbSqlServers.Text;
            MasterDBhandler.InstanceName = cmbInstances.Text;
            btnGetDBList_Click(this, EventArgs.Empty);
        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            MasterDBhandler.DBname = cmbDatabases.Text;
            btnGetTables_Click(this, EventArgs.Empty);
        }

        private void btnGetDBList_Click(object sender, EventArgs e)
        {
            bInitializing = true;
            this.Cursor = Cursors.WaitCursor;
            MasterDBhandler.ServerName = cmbSqlServers.Text;
            MasterDBhandler.InstanceName = cmbInstances.Text;
            try
            {
                MasterDBhandler.FindDatabases(cmbDatabases);
            }
            catch (Exception e2)
            {
                MessageBox.Show(this, "Error finding Databases for .\r\n" + e2.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
            bInitializing = false;
        }

        private void btnGetTables_Click(object sender, EventArgs e)
        {
            bInitializing = true;
            this.Cursor = Cursors.WaitCursor;
            MasterDBhandler.DBname = cmbDatabases.Text;
            try
            {
                MasterDBhandler.FindTables(cmbTables);
            }
            catch (Exception e3)
            {
                MessageBox.Show(this, "Error finding Data Tables for Database: " + cmbDatabases.Text + " .\r\n" 
                    + e3.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
            bInitializing = false;
        }

        private DateTime FindDateFromDataLine(string DataLine)
        {
            char sSplitter = ',';
            if (rbDelimitSpace.Checked) sSplitter = ' ';
            if (rbDelimitTab.Checked) sSplitter = '\t';
            string[] firstsplit = DataLine.Split(sSplitter);

            string sTemp = firstsplit[0];
            //---- split the date - 1999.09.27
            string[] splitarray = sTemp.Split(new string[] { "." }, StringSplitOptions.None);
            int iYear = 0, iMonth = 0, iDay = 0, iHour = 0, iMinutes = 0;
            int.TryParse(splitarray[0], out iYear);
            int.TryParse(splitarray[1], out iMonth);
            int.TryParse(splitarray[2], out iDay);

            sSplitter = ':';
            splitarray = firstsplit[1].Split(sSplitter);
            int.TryParse(splitarray[0], out iHour);
            int.TryParse(splitarray[1], out iMinutes);

            return new DateTime(iYear, iMonth, iDay, iHour, iMinutes, 0);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            int iHowMany = 0;
            DateTime dtNewestTableDate;
            DateTime dtOldestTextDate;
            List<string> DataList = new List<string>();
            StatusLabel.Text = "Running Data Update ...";
            txtFolderName.Enabled = false;
            cmbDatabases.Enabled = false;
            cmbFileNames.Enabled = false;
            cmbInstances.Enabled = false;
            cmbSqlServers.Enabled = false;
            cmbTables.Enabled = false;

            // set the db names
            clsDBhandlerParent.ServerName = cmbSqlServers.Text;
            clsDBhandlerParent.InstanceName = cmbInstances.Text;
            clsDBhandlerParent.DBname = cmbDatabases.Text;
            clsDBhandlerParent.TableName = cmbTables.Text;
            if (rbDelimitComma.Checked) clsDBhandlerParent.CSVsplitterChar = ',';
            if (rbDelimitSpace.Checked) clsDBhandlerParent.CSVsplitterChar = ' ';
            if (rbDelimitTab.Checked) clsDBhandlerParent.CSVsplitterChar = '\t';

            //---- first get the data file
            StreamReader oReader = new StreamReader(txtFolderName.Text + "\\" + cmbFileNames.Text);
            while (oReader.Peek() >= 0)
            {
                DataList.Add(oReader.ReadLine());
            }
            if (DataList.Count <= 0) return;

            try // get the newest date in the table
            {
                dtNewestTableDate = MasterDBhandler.GetMostRecentDate();
            }
            catch (Exception e2)
            {
                MessageBox.Show(this, "Error attempting to read data from table.\n" + e2.Message, "SQL ERROR");
                txtFolderName.Enabled = true;
                cmbDatabases.Enabled = true;
                cmbFileNames.Enabled = true;
                cmbInstances.Enabled = true;
                cmbSqlServers.Enabled = true;
                cmbTables.Enabled = true;
                return;
                
            }
            string sSplitter = ",";
            if (rbDelimitSpace.Checked) sSplitter = " ";
            if (rbDelimitTab.Checked) sSplitter = "\t";
            //---- always check for a date gap with "append"
            if (rbAppendData.Checked && dtNewestTableDate.Year > 1958)
            {
                string sDataLine = "";
                if (DataList[0].Contains(sSplitter))
                    sDataLine = DataList[0];
                else // try the second line
                    sDataLine = DataList[1];
                dtOldestTextDate = FindDateFromDataLine(sDataLine);
                //---- if it fails, return
                if (dtNewestTableDate < dtOldestTextDate)
                {
                    if (MessageBox.Show(this, "There is a date gap between the oldest date in the new data\n"
                        + "and the newest date in the table data.\nDo you wish to proceed or abort?", "Data Gap",
                        MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        txtFolderName.Enabled = true;
                        cmbDatabases.Enabled = true;
                        cmbFileNames.Enabled = true;
                        cmbInstances.Enabled = true;
                        cmbSqlServers.Enabled = true;
                        cmbTables.Enabled = true;
                        return;
                    }
                }
            } // check date gap on 'append'
            bool bSuccess = true;
            try
            {
                iHowMany = MasterDBhandler.UpdateTableData(DataList, rbDeleteOldData.Checked);
            }
            catch (Exception k)
            {
                MessageBox.Show(this, "Error saving data: " + k.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusLabel.Text = "Error saving data!";
                bSuccess = false;
            }
            txtFolderName.Enabled = true;
            cmbDatabases.Enabled = true;
            cmbFileNames.Enabled = true;
            cmbInstances.Enabled = true;
            cmbSqlServers.Enabled = true;
            cmbTables.Enabled = true;

            //---- if successful, save the DB names to Properties
            if (rbMySqlServer.Checked)
            {
                Properties.Settings.Default.LastMySqlServer = cmbSqlServers.Text;
                Properties.Settings.Default.LastMySqlDB = cmbDatabases.Text;
                Properties.Settings.Default.LastMySqlTable = cmbTables.Text;
            }
            else
            {
                Properties.Settings.Default.LastMSSqlServer = cmbSqlServers.Text;
                Properties.Settings.Default.LastInstance = cmbInstances.Text;
                Properties.Settings.Default.LastMSSqlDB = cmbDatabases.Text;
                Properties.Settings.Default.LastMSSqlTable = cmbTables.Text;
            }
            Properties.Settings.Default.LastFolder = txtFolderName.Text;
            Properties.Settings.Default.LastFile = cmbFileNames.Text;
            Properties.Settings.Default.MySqlServer = rbMySqlServer.Checked;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "Idle ...";
            Application.DoEvents();
            if (bSuccess)
                MessageBox.Show(this, "Data Save Is Completed.\n" + iHowMany.ToString() + " items added.", "DONE");
        }

        private void rbMySqlServer_CheckedChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;

            if (rbMySqlServer.Checked)
            {
                rbMySqlServer.BackColor = Color.LightGreen;
                rbMSSqlServer.BackColor = Color.Salmon;
            }
            else
            {
                rbMSSqlServer.BackColor = Color.LightGreen;
                rbMySqlServer.BackColor = Color.Salmon;
            }
            cmbDatabases.Text = "";
            cmbInstances.Text = "";
            cmbSqlServers.Text = "";
            cmbTables.Text = "";
            Application.DoEvents();
            MasterDBhandler.IsMySqlDB = rbMySqlServer.Checked;
            btnGetServers_Click(null, EventArgs.Empty);
        }

        private void cmbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDatabases.Text = "";
            cmbTables.Text = "";
            Application.DoEvents();
            MasterDBhandler.ServerName = cmbSqlServers.Text;
            MasterDBhandler.InstanceName = cmbInstances.Text;
            MasterDBhandler.IsMySqlDB = rbMySqlServer.Checked;
            MasterDBhandler.FindDatabases(cmbDatabases);
        }

		private void cmbTables_TextUpdate(object sender, EventArgs e)
		{
			clsDBhandlerParent.TableName = cmbTables.Text;
		}

		private void StatusLabel_TextChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

    } // end class
} // end namespace