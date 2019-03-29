using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace MT4_History_Reader
{
    public partial class HistoryReaderMain : Form
    {
        //************** global vars ***************
        bool bInitializing = true;
        public Dictionary<string, List<string>> ServerInstances = new Dictionary<string, List<string>>();
        StringCollection PrimaryCurrencyList, SecondaryCurrencyList;

        //************* constructor **************
        public HistoryReaderMain()
        {
            InitializeComponent();
            bInitializing = false;
            Application.DoEvents();
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
            //---- set up server list
            btnGetServers_Click(null, EventArgs.Empty);
            stemp = Properties.Settings.Default.LastServer;
            if (cmbSqlServers.Items.Count > 0)
            {
                if (cmbSqlServers.Items.Contains(stemp))
                {
                    cmbSqlServers.Text = stemp;
                }
                else
                    cmbSqlServers.Text = cmbSqlServers.Items[0].ToString();
                //---- set up DB list
                bInitializing = true;
                btnGetDBList_Click(null, EventArgs.Empty);
                stemp = Properties.Settings.Default.LastDB;
                if (cmbDatabases.Items.Count > 0)
                {
                    if (cmbDatabases.Items.Contains(stemp))
                        cmbDatabases.Text = stemp;
                    else
                        cmbDatabases.Text = cmbDatabases.Items[0].ToString();
                    //---- set up table list
                    bInitializing = true;
                    btnGetTables_Click(null, EventArgs.Empty);
                    if (cmbTables.Items.Count > 0)
                    {
                        stemp = Properties.Settings.Default.LastTable;
                        if (cmbTables.Items.Contains(stemp))
                            cmbTables.Text = stemp;
                        else
                            cmbTables.Text = cmbTables.Items[0].ToString();
                    }
                }
            }
            else
                MessageBox.Show(this, "No SQL Servers were found.", "Server ERROR");
            bInitializing = false;
            StatusLabel.Text = "Idle ...";
        }

        private void btnGetServers_Click(object sender, EventArgs e)
        {
            bInitializing = true;
            cmbInstances.Items.Clear();
            cmbSqlServers.Items.Clear();
            bInitializing = false;
            StatusLabel.Text = "Seeking SQL Servers ...";
            Application.DoEvents();

            //---- now go find them
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dtServers = instance.GetDataSources();
            bInitializing = true;
            foreach (DataRow oRow in dtServers.Rows)
            {
                string sTemp = oRow[0].ToString();
                cmbSqlServers.Items.Add(sTemp);
                if (!ServerInstances.ContainsKey(sTemp))
                {
                    List<string> SvrInst = new List<string>();
                    SvrInst.Add(oRow[1].ToString());
                    ServerInstances.Add(sTemp, SvrInst);
                }
                else
                {
                    List<string> SvrInst = ServerInstances[sTemp];
                    SvrInst.Add(oRow[1].ToString());
                    ServerInstances[sTemp] = SvrInst;
                }
            }
            dtServers.Clear();
            if (cmbSqlServers.Items.Count > 0)
            {
                cmbSqlServers.Text = cmbSqlServers.Items[0].ToString();
                List<string> SvrInst = ServerInstances[cmbSqlServers.Text];
                foreach (string s in SvrInst)
                {
                    cmbInstances.Items.Add(s);
                }
                if (cmbInstances.Items.Count > 0)
                {
                    cmbInstances.Text = cmbInstances.Items[0].ToString();
                }
                else
                    MessageBox.Show(this, "No Servers Found", "Error!");
            }
            this.Cursor = Cursors.Default;
            if (Properties.Settings.Default.LastServer.Length > 3)
            {
                bInitializing = true;
                cmbSqlServers.Text = Properties.Settings.Default.LastServer;
                bInitializing = false;
            }
            else btnGetDBList_Click(null, EventArgs.Empty);
            StatusLabel.Text = "Idle ...";
            Application.DoEvents();
        }

        private void cmbSqlServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            List<string> SvrInst = ServerInstances[cmbSqlServers.Text];
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
            btnGetDBList_Click(this, EventArgs.Empty);
        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            btnGetTables_Click(this, EventArgs.Empty);
        }

        private void btnGetDBList_Click(object sender, EventArgs e)
        {
            if (cmbSqlServers.Items.Count < 1) return;
            bInitializing = true;
            cmbDatabases.Items.Clear();
            cmbDatabases.Text = "";
            this.Cursor = Cursors.WaitCursor;
            StatusLabel.Text = "Getting DB list from Server " + cmbSqlServers.Text;
            Application.DoEvents();

            string FilterString = "##master##model##msdb##tempdb##";
            string ConnString = "Data source=" + cmbSqlServers.Text + "; Integrated Security=SSPI";
            SqlConnection oConn = new SqlConnection(ConnString);
            try
            {
                oConn.Open();
            }
            catch (Exception e1)
            {
                MessageBox.Show(this, "Unable to connect to server: " + cmbSqlServers.Text
                    + "\nError message is: " + e1.Message, "Server ERROR");
                this.Cursor = Cursors.Default;
                bInitializing = false;
                StatusLabel.Text = "Error connecting to server!";
                Application.DoEvents();
                return;
            }
            DataTable dtTableList = oConn.GetSchema("Databases");
            oConn.Close();
            foreach (DataRow oRow in dtTableList.Rows)
            {
                string sTemp = oRow["database_name"].ToString();
                if (!FilterString.Contains(sTemp))
                    cmbDatabases.Items.Add(sTemp);
            }
            bInitializing = false;
            if (cmbDatabases.Items.Count > 0)
            {
#if DEBUG
                cmbDatabases.Text = "ForexSwing";
#else
                if (Properties.Settings.Default.LastDB.Length > 3)
                    cmbDatabases.Text = Properties.Settings.Default.LastDB;
                else
                    cmbDatabases.Text = cmbDatabases.Items[0].ToString();
#endif
                StatusLabel.Text = "Idle ...";
            }
            else
            {
                MessageBox.Show(this, "No Databases Found", "ERROR");
                StatusLabel.Text = "No Databases Found on server " + cmbSqlServers.Text;
            }
            Application.DoEvents();
            this.Cursor = Cursors.Default;
        }
        
        private void btnGetTables_Click(object sender, EventArgs e)
        {
            if (bInitializing) return;
            if (cmbDatabases.Items.Count < 1) return;
            /********************************/
            this.Cursor = Cursors.WaitCursor;
            cmbTables.Items.Clear();
            cmbTables.Text = "";
            StatusLabel.Text = "Getting Table list from DB " + cmbDatabases.Text;

            string ConnString = "Data source=" + cmbSqlServers.Text + "; Integrated Security=SSPI";
            DataTable dtTableList = new DataTable();
            try
            {
                SqlConnection oConn = new SqlConnection(ConnString);
                oConn.Open();
                SqlCommand oCmd = oConn.CreateCommand();
                oCmd.CommandText = "Use " + cmbDatabases.Text;
                oCmd.CommandType = CommandType.Text;
                oCmd.ExecuteNonQuery();
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "sp_tables";
                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(dtTableList);
                oConn.Close();
            }
            catch (Exception gt)
            {
                MessageBox.Show(this, "ERROR fetching tables!\n" + gt.Message, "ERROR");
                bInitializing = false;
                StatusLabel.Text = "Error fetching tables!";
                Application.DoEvents();
                this.Cursor = Cursors.Default;
                return;
            }
            //----create filter strings
            string filter1 = "###";
            foreach (string s1 in PrimaryCurrencyList)
                filter1 += s1 + "###";
            string filter2 = "###";
            foreach (string s2 in SecondaryCurrencyList)
                filter2 += s2 + "###";
            foreach (DataRow oRow in dtTableList.Rows)
            {
                if (oRow["table_type"].ToString() != "TABLE") continue;
                string sTemp = oRow["table_name"].ToString();
                if (sTemp.Length < 6) continue;
                string name1 = sTemp.Substring(0, 3);
                string name2 = sTemp.Substring(3, 3);
                if (!(filter1.Contains(name1) && filter2.Contains(name2))
                    && !(filter2.Contains(name1) && filter1.Contains(name2))) continue;
                //----okay, so add it
                cmbTables.Items.Add(sTemp);
            }
            bInitializing = false;
            if (cmbTables.Items.Count > 0)
            {
                cmbTables.Text = cmbTables.Items[0].ToString();
                StatusLabel.Text = "Idle ...";
            }
            else
            {
                MessageBox.Show(this, "No Currency Pair Tables Found", "ERROR");
                StatusLabel.Text = "No Currency Pair Tables Found!";
            }
            this.Cursor = Cursors.Default;
            Application.DoEvents();
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

            //---- first get the data file
            StreamReader oReader = new StreamReader(txtFolderName.Text + "\\" + cmbFileNames.Text);
            while (oReader.Peek() >= 0)
            {
                DataList.Add(oReader.ReadLine());
            }
            if (DataList.Count <= 0) return;

            //---- now open the destination table
            SqlConnection oConn = null;
            SqlCommand oCmd = null;
            DataTable oTable = new DataTable();
            SqlDataAdapter oDA = null;
            string sCmd = "";
            try // try to open a connection and command objects
            {
                oConn = new SqlConnection("server=" + cmbSqlServers.Text + ";database=" + cmbDatabases.Text + ";integrated security=sspi");
                oConn.Open();
                oCmd = oConn.CreateCommand();
                oDA = new SqlDataAdapter(oCmd);
            }
            catch (Exception e1) // clean up and exit
            { 
                MessageBox.Show(this, "Error attempting to connect to indicated server, DB, and table.\n" + e1.Message, "SQL ERROR");
                oDA.Dispose();
                oTable.Clear();
                oTable.Dispose();
                oCmd.Dispose();
                if (oConn != null)
                {
                    if (oConn.State == ConnectionState.Open) oConn.Close();
                    oConn.Dispose();
                }
                txtFolderName.Enabled = true;
                cmbDatabases.Enabled = true;
                cmbFileNames.Enabled = true;
                cmbInstances.Enabled = true;
                cmbSqlServers.Enabled = true;
                cmbTables.Enabled = true;
                return;
            } // catch ()
            try // try to read last date from table
            {
                sCmd = "select count(*) as count from " + cmbTables.Text;
                oCmd.CommandText = sCmd;
                oDA.Fill(oTable);
                iHowMany = oTable.Rows[0].Field<int>("count");
                if (iHowMany == 0) dtNewestTableDate = new DateTime();
                else
                {
                    oTable.Clear();
                    sCmd = "select Max(bartime) as BarTime from " + cmbTables.Text;
                    oCmd.CommandText = sCmd;
                    oCmd.CommandType = CommandType.Text;
                    oDA.Fill(oTable);
                    dtNewestTableDate = oTable.Rows[0].Field<DateTime>("BarTime");
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(this, "Error attempting to read data from table.\n" + e2.Message, "SQL ERROR");
                oDA.Dispose();
                oTable.Clear();
                oTable.Dispose();
                oCmd.Dispose();
                if (oConn != null)
                {
                    if (oConn.State == ConnectionState.Open) oConn.Close();
                    oConn.Dispose();
                }
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
            //---- always check for a date gap is "append"
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
                        oDA.Dispose();
                        oTable.Clear();
                        oTable.Dispose();
                        oCmd.Dispose();
                        if (oConn != null)
                        {
                            if (oConn.State == ConnectionState.Open) oConn.Close();
                            oConn.Dispose();
                        }
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
            if (rbDeleteOldData.Checked)
            {
                sCmd = "truncate table " + cmbTables.Text;
                oCmd.CommandText = sCmd;
                oCmd.ExecuteNonQuery();
            }
            if (rbDeleteOldData.Checked == true || dtNewestTableDate.Year < 1958)
            {
                dtNewestTableDate = new DateTime(1958, 1, 1); // dummy date that is guaranteed to be oldest
            }
            //---- now find a date that is next in the text data
            string[] splitarray = null;
            iHowMany = 0;
            int iDisplayCount = 0;
            string s = "";
            for (int indexer = 0; indexer < DataList.Count - 1; indexer++)
            {
                s = DataList[indexer];
                iDisplayCount++;
                if (iDisplayCount % 17 == 0)
                {
                    StatusLabel.Text = "Running Data Update - Checking Item# " + iDisplayCount.ToString();
                    Application.DoEvents();
                }
                dtOldestTextDate = FindDateFromDataLine(s);
                if (dtOldestTextDate == dtNewestTableDate) // always overwrite/update the last date's values
                {
                    iHowMany++;
                    splitarray = s.Split(new string[] { sSplitter }, StringSplitOptions.None);
                    sCmd = "update " + cmbTables.Text + " set [open] = " + splitarray[2] + ", high = " + splitarray[3]
                        + ", low = " + splitarray[4] + ", [close] = " + splitarray[5] 
                        + " where bartime = '" + dtOldestTextDate.ToString() + "'";
                    oCmd.CommandText = sCmd;
                    oCmd.ExecuteNonQuery();
                }
                if (dtOldestTextDate > dtNewestTableDate)
                {
                    iHowMany++;
                    splitarray = s.Split(new string[] { sSplitter }, StringSplitOptions.None);
                    sCmd = "insert into " + cmbTables.Text + " (bartime, [open], high, low, [close]) values('"
                        + dtOldestTextDate.ToString() + "'," + splitarray[2] + "," + splitarray[3] + ","
                        + splitarray[4] + "," + splitarray[5] + ")";
                    oCmd.CommandText = sCmd;
                    oCmd.ExecuteNonQuery();
                }
            }
            txtFolderName.Enabled = true;
            cmbDatabases.Enabled = true;
            cmbFileNames.Enabled = true;
            cmbInstances.Enabled = true;
            cmbSqlServers.Enabled = true;
            cmbTables.Enabled = true;

            //---- if successful, save the DB names to Properties
            Properties.Settings.Default.LastServer = cmbSqlServers.Text;
            Properties.Settings.Default.LastInstance = cmbInstances.Text;
            Properties.Settings.Default.LastDB = cmbDatabases.Text;
            Properties.Settings.Default.LastTable = cmbTables.Text;
            Properties.Settings.Default.LastFolder = txtFolderName.Text;
            Properties.Settings.Default.LastFile = cmbFileNames.Text;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "Idle ...";
            Application.DoEvents();
            MessageBox.Show(this, "Data Save Is Completed.\n" + iHowMany.ToString() + " items added.", "DONE");
        } // btnRun_Click()

    } // end class
} // end namespace
