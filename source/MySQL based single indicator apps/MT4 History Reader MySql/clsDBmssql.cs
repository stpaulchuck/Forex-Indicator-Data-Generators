using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Windows.Forms;
using System.Data.SqlClient;
using System;

namespace MT4_History_Reader_MySql
{
    class clsDBmssql:clsDBhandlerParent
    {
        public Dictionary<string, List<string>> ServerInstances = new Dictionary<string, List<string>>();


        public override bool FindServers(ComboBox DisplayBox, ComboBox InstancesBox)
        {
            DisplayBox.Text = "";
            DisplayBox.Items.Clear();
            InstancesBox.Items.Clear();
            InstancesBox.Text = "";
            StatusLabel.Text = "Seeking SQL Servers ...";
            //---- now go find them
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dtServers = instance.GetDataSources();
            foreach (DataRow oRow in dtServers.Rows)
            {
                string sTemp = oRow[0].ToString();
                DisplayBox.Items.Add(sTemp);
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
            if (DisplayBox.Items.Count > 0)
            {
                string stemp = Properties.Settings.Default.LastMSSqlServer;
                if (DisplayBox.Items.Contains(stemp))
                {
                    DisplayBox.Text = stemp;
                }
                else
                    DisplayBox.Text = DisplayBox.Items[0].ToString();
                List<string> SvrInst = ServerInstances[DisplayBox.Text];
                foreach (string s in SvrInst)
                {
                    InstancesBox.Items.Add(s);
                }
                if (InstancesBox.Items.Count > 0)
                {
                    InstancesBox.Text = InstancesBox.Items[0].ToString();
                }
            }
            StatusLabel.Text = "Idle ...";
            return true;
        }

        public override List<string> GetInstances(string ServerName)
        {
            if (ServerInstances.Keys.Contains(ServerName))
            { return ServerInstances[ServerName]; }
            else
            { return new List<string>(); }
        }

        public override bool FindDatabases(System.Windows.Forms.ComboBox DisplayBox)
        {
            DisplayBox.Text = "";
            DisplayBox.Items.Clear();
            StatusLabel.Text = "Getting DB list from Server " + ServerName;

            string FilterString = "##master##model##msdb##tempdb##";
            string ConnString = "Data source=" + ServerName + "; Integrated Security=SSPI";
            SqlConnection oConn = new SqlConnection(ConnString);
            try
            {
                oConn.Open();
            }
            catch (Exception e1)
            {
                m_LastError = "Unable to connect to server: " + ServerName
                    + "\nError message is: " + e1.Message;
                StatusLabel.Text = "Error connecting to server!";
                return false;
            }
            DataTable dtTableList = oConn.GetSchema("Databases");
            oConn.Close();
            foreach (DataRow oRow in dtTableList.Rows)
            {
                string sTemp = oRow["database_name"].ToString();
                if (!FilterString.Contains(sTemp))
                    DisplayBox.Items.Add(sTemp);
            }
            if (DisplayBox.Items.Count > 0)
            {
#if DEBUG
                DisplayBox.Text = "ForexSwing";
#else
                if (Properties.Settings.Default.LastMSSqlDB.Length > 3)
                    DisplayBox.Text = Properties.Settings.Default.LastMSSqlDB;
                else
                    DisplayBox.Text = DisplayBox.Items[0].ToString();
#endif
                StatusLabel.Text = "Idle ...";
            }
            else
            {
                m_LastError = "No Databases Found";
                StatusLabel.Text = "No Databases Found on server " + ServerName;
                return false;
            }
            return true;
        }

        public override bool FindTables(System.Windows.Forms.ComboBox DisplayBox)
        {
            DisplayBox.Items.Clear();
            DisplayBox.Text = "";
            StatusLabel.Text = "Getting Table list from DB " + DBname;

            string ConnString = "Server=" + ServerName + "; database=" + DBname + "; Integrated Security=SSPI";
            DataTable dtTableList = new DataTable();
            try
            {
                SqlConnection oConn = new SqlConnection(ConnString);
                oConn.Open();
                SqlCommand oCmd = oConn.CreateCommand();
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "sp_tables";
                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(dtTableList);
                oConn.Close();
            }
            catch (Exception gt)
            {
                m_LastError = "ERROR fetching tables!\n" + gt.Message;
                StatusLabel.Text = "Error fetching tables!";
                Application.DoEvents();
                return false;
            }
            //----create filter strings
            string sTemp = "";
            string filter1 = "###";
            foreach (string s1 in PrimaryCurrencyList)
                filter1 += s1 + "###";
            string filter2 = "###";
            foreach (string s2 in SecondaryCurrencyList)
                filter2 += s2 + "###";
            foreach (DataRow oRow in dtTableList.Rows)
            {
                if (oRow["table_type"].ToString() != "TABLE") continue;
                sTemp = oRow["table_name"].ToString();
                if (sTemp.Length < 6) continue;
                string name1 = sTemp.Substring(0, 3);
                string name2 = sTemp.Substring(3, 3);
                if (!(filter1.Contains(name1) && filter2.Contains(name2))
                    && !(filter2.Contains(name1) && filter1.Contains(name2))) continue;
                //----okay, so add it
                DisplayBox.Items.Add(sTemp);
            }
            if (DisplayBox.Items.Count > 0)
            {
                sTemp = Properties.Settings.Default.LastMSSqlTable;
                if (sTemp.Length > 3)
                {
                    if (DisplayBox.Items.Contains(sTemp))
                        DisplayBox.Text = sTemp;
                    return true;
                }
                DisplayBox.Text = DisplayBox.Items[0].ToString();
                StatusLabel.Text = "Idle ...";
                return true;
            }
            else
            {
                DisplayBox.Text = "No Tables Found";
                StatusLabel.Text = "No Currency Pair Tables Found!";
            }
            Application.DoEvents();
            return false;
        }

        public override DateTime GetMostRecentDate()
        {
            dtNewestTableDate = new DateTime();

            SqlConnection oConn = new SqlConnection("server=" + ServerName + ";database=" + DBname + "; integrated security = sspi");
            oConn.Open();
            SqlCommand oCmd = oConn.CreateCommand();
            SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            string sCmd = "select count(*) as count from " + TableName;
            oCmd.CommandText = sCmd;
            oDA.Fill(oTable);
            int iHowMany = oTable.Rows[0].Field<int>("count");
            if (iHowMany == 0) return dtNewestTableDate;
            else
            {
                oTable.Clear();
                sCmd = "select Max(bartime) as BarTime from " + TableName;
                oCmd.CommandText = sCmd;
                oCmd.CommandType = CommandType.Text;
                oDA.Fill(oTable);
                dtNewestTableDate = oTable.Rows[0].Field<DateTime>("BarTime");
            }
            return dtNewestTableDate;
        }

        public override int UpdateTableData(List<string> DataList, bool TruncateTable)
        {
            SqlConnection oConn = new SqlConnection("server=" + ServerName + ";database=" + DBname + "; integrated security = sspi");
            oConn.Open();
            SqlCommand oCmd = oConn.CreateCommand();
            SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            string sCmd = "";
            if (TruncateTable)
            {
                sCmd = "truncate table " + TableName;
                oCmd.CommandText = sCmd;
                oCmd.ExecuteNonQuery();
            }
            if (TruncateTable)
            {
                dtNewestTableDate = new DateTime(1958, 1, 1); // dummy date that is guaranteed to be oldest
            }

            //---- now find a date that is next in the text data
            string[] splitarray = null;
            int iHowMany = 0;
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
                DateTime dtOldestTextDate = FindDateFromDataLine(s);
                if (dtOldestTextDate == dtNewestTableDate) // always overwrite/update the last date's values
                {
                    iHowMany++;
                    splitarray = s.Split(new char[] { CSVsplitterChar }, StringSplitOptions.None);
                    sCmd = "update " + TableName + " set [open] = " + splitarray[2] + ", high = " + splitarray[3]
                        + ", low = " + splitarray[4] + ", [close] = " + splitarray[5]
                        + " where bartime = '" + dtOldestTextDate.ToString() + "'";
                    oCmd.CommandText = sCmd;
                    oCmd.ExecuteNonQuery();
                }
                if (dtOldestTextDate > dtNewestTableDate)
                {
                    iHowMany++;
                    splitarray = s.Split(new char[] { CSVsplitterChar }, StringSplitOptions.None);
                    sCmd = "insert into " + TableName + " (bartime, [open], high, low, [close]) values('"
                        + dtOldestTextDate.ToString() + "'," + splitarray[2] + "," + splitarray[3] + ","
                        + splitarray[4] + "," + splitarray[5] + ")";
                    oCmd.CommandText = sCmd;
                    oCmd.ExecuteNonQuery();
                }
            }
            return iHowMany;
        }

    } // end class
} // end namespace
