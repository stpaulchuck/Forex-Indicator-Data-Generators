using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;


namespace MT4_History_Reader_MySql
{
    class clsDBmysql:clsDBhandlerParent
    {
        /*************************** global vars *************************/
        List<string> ServerList = new List<string>();
        string sUID = "", sPWD = "";


        /*************************** constructor ************************/
        public clsDBmysql() : base()
        {
            // mysql has not server browser so we have to wire in the names to try
            foreach (string s in Properties.Settings.Default.ServerList)
            {
                ServerList.Add(s);
            }
            // no built in SSPI security context so DB user and pwd
            sUID = Properties.Settings.Default.MySqlUID;
            sPWD = Properties.Settings.Default.MySqlPwd;
        }

        /*************************** public methods *************************/
        public override bool FindServers(ComboBox DisplayBox, ComboBox InstancesBox)
        {
            InstancesBox.Text = "";
            InstancesBox.Items.Clear();
            DisplayBox.Text = "";
            DisplayBox.Items.Clear();
            StatusLabel.Text = "Seeking MySQL Servers ...";

            MySqlConnection oConn = null;
            foreach (string s in ServerList)
            {
				string sConnectString = "";
				if (s.Contains(":"))
				{
					// persistsecurityinfo=True;
					sConnectString = "server=" + s.Substring(0, s.IndexOf(":")) + ";Port=" + s.Substring(s.IndexOf(":") + 1) + ";uid=" + sUID + ";password=" + sPWD;
				}
				else
				{
					sConnectString = "server=" + s + ";uid=" + sUID + ";password=" + sPWD;
				}
                bool bIsGood = false;
                try
                {
                    StatusLabel.Text = "Seeking MySql Server on " + s + " ...";
                    oConn = new MySqlConnection(sConnectString);
                    oConn.Open();

                    bIsGood = true;
                }
                catch (Exception e)
                {
					Debug.WriteLine("ERROR in mysql server connect: " + e.Message + "  stacktrace = " + e.StackTrace);
					/*do nothing*/
				}
                if (bIsGood)
                {
                    DisplayBox.Items.Add(s);
                    bIsGood = false;
                    oConn.Close();
                }
            }
            string stemp = Properties.Settings.Default.LastMySqlServer;
            if (DisplayBox.Items.Count > 0)
            {
                if (DisplayBox.Items.Contains(stemp))
                {
                    DisplayBox.Text = stemp;
                }
                else
                    DisplayBox.Text = DisplayBox.Items[0].ToString();
            }
            else
            {
                DisplayBox.Text = "{ no active servers found }";
            }
            oConn = null;
            StatusLabel.Text = "Idle ...";
            return true;
        }

        public override List<string> GetInstances(string ServerName)
        {
            return new List<string>();
        }

        public override bool FindDatabases(ComboBox DisplayBox)
        {
            DisplayBox.Items.Clear();
            DisplayBox.Text = "";
            if (ServerName == "") return false;
            try
            {
                StatusLabel.Text = "Getting DB list from Server " + ServerName;
				string sConnectString = "";
				if (ServerName.Contains(":"))
				{
					sConnectString = "server=" + ServerName.Substring(0, ServerName.IndexOf(":")) + ";persistsecurityinfo=True;Port=" + ServerName.Substring(ServerName.IndexOf(":") + 1) + ";uid=" + sUID + ";password=" + sPWD;
				}
				else
				{
					sConnectString = "server=" + ServerName + ";uid=" + sUID + ";password=" + sPWD;
				}
				MySqlConnection oConn = new MySqlConnection(sConnectString);
                oConn.Open();
                MySqlCommand oCmd = oConn.CreateCommand();
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = "select SCHEMA_NAME from information_schema.SCHEMATA";
                MySqlDataAdapter oDa = new MySqlDataAdapter(oCmd);
                DataTable oTable = new DataTable();
                oDa.Fill(oTable);
                if (oConn != null)
                    if (oConn.State == ConnectionState.Open)
                        oConn.Close();
                foreach (DataRow oRow in oTable.Rows)
                {
                    if (oRow.Field<string>(0).ToLower().Contains("forex"))
                        DisplayBox.Items.Add(oRow.Field<string>(0));
                }
                if (DisplayBox.Items.Count > 0)
                {
#if DEBUG
                    DisplayBox.Text = "ForexSwing";
#else
                if (Properties.Settings.Default.LastMySqlDB.Length > 3)
                    DisplayBox.Text = Properties.Settings.Default.LastMySqlDB;
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
            }
            catch (Exception e)
            {
                m_LastError = "Error fetching database list: " + e.Message;
                StatusLabel.Text = "Error fetching database list.";
                return false;
            }
            return true;
        }

        public override bool FindTables(ComboBox DisplayBox)
        {
			string sConnectString = "";
            DisplayBox.Items.Clear();
            DisplayBox.Text = "";
            StatusLabel.Text = "Getting Table list from DB " + DBname;
			if (ServerName.Contains(":"))
			{
				sConnectString = "server=" + ServerName.Substring(0, ServerName.IndexOf(":")) + ";persistsecurityinfo=True;Port=" + ServerName.Substring(ServerName.IndexOf(":") + 1) + ";uid=" + sUID + ";password=" + sPWD + ";database=" + DBname;
			}
			else
			{
				sConnectString = "server=" + ServerName + ";uid=" + sUID + ";password=" + sPWD + ";database=" + DBname;
			}

			MySqlConnection oConn = new MySqlConnection(sConnectString);
            oConn.Open();
            MySqlCommand oCmd = oConn.CreateCommand();
            DataTable oTable = new DataTable();
            MySqlDataAdapter oDa = new MySqlDataAdapter(oCmd);
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = "show tables";
            oDa.Fill(oTable);
            if (oConn != null)
                if (oConn.State != ConnectionState.Closed)
                    oConn.Close();
            //----create filter strings
            string filter1 = "###";
            foreach (string s1 in PrimaryCurrencyList)
                filter1 += s1 + "###";
            string filter2 = "###";
            foreach (string s2 in SecondaryCurrencyList)
                filter2 += s2 + "###";
            filter1 = filter1.ToLower();
            filter2 = filter2.ToLower();
            foreach (DataRow oRow in oTable.Rows)
            {
                string sTemp = oRow.Field<string>(0).ToLower(); ;
                if (sTemp.Length < 6) continue;
                string name1 = sTemp.Substring(0, 3);
                string name2 = sTemp.Substring(3, 3);
                if (!(filter1.Contains(name1) && filter2.Contains(name2))
                    && !(filter2.Contains(name1) && filter1.Contains(name2))) continue;
                //----okay, so add it
                DisplayBox.Items.Add(sTemp);
            }
            string stemp = Properties.Settings.Default.LastMySqlTable;
            if (DisplayBox.Items.Contains(stemp)) DisplayBox.Text = stemp;
            else DisplayBox.Text = DisplayBox.Items[0].ToString();
            Application.DoEvents();
            StatusLabel.Text = "table inventory complete";
			clsDBhandlerParent.TableName = DisplayBox.Text;
            return true;
        }

        public override DateTime GetMostRecentDate()
        {
            dtNewestTableDate = new DateTime();
			string sConnectString = "";
			if (ServerName.Contains(":"))
			{
				sConnectString = "server=" + ServerName.Substring(0, ServerName.IndexOf(":")) + ";persistsecurityinfo=True;Port=" + ServerName.Substring(ServerName.IndexOf(":") + 1) + ";uid=" + sUID + ";password=" + sPWD + ";database=" + DBname;
			}
			else
			{
				sConnectString = "server=" + ServerName + ";uid=" + sUID + ";password=" + sPWD + ";database=" + DBname;
			}
			MySqlConnection oConn = new MySqlConnection(sConnectString);
            oConn.Open();
            MySqlCommand oCmd = oConn.CreateCommand();
            MySqlDataAdapter oDA = new MySqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            string sCmd = "select count(*) as count from " + TableName;
            oCmd.CommandText = sCmd;
            oDA.Fill(oTable);
            int iHowMany = (int)oTable.Rows[0].Field<Int64>("count");
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
            if (oConn != null)
                if (oConn.State != ConnectionState.Closed)
                    oConn.Clone();
            oTable.Clear();
            oTable = null;
            return dtNewestTableDate;
        }

        public override int UpdateTableData(List<string> DataList, bool TruncateTable)
        {
			string sConnectString = "";
			if (ServerName.Contains(":"))
			{
				sConnectString = "server=" + ServerName.Substring(0, ServerName.IndexOf(":")) + ";database=" + DBname + ";Port=" + ServerName.Substring(ServerName.IndexOf(":") + 1) + ";uid=" + sUID + ";password=" + sPWD;
			}
			else
			{
				sConnectString = "server=" + ServerName + ";database=" + DBname + ";uid=" + sUID + ";password=" + sPWD;
			}
			MySqlConnection oConn = new MySqlConnection(sConnectString);
            oConn.Open();
            MySqlCommand oCmd = oConn.CreateCommand();
            MySqlDataAdapter oDA = new MySqlDataAdapter(oCmd);
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
                string sOutDate = dtOldestTextDate.Year.ToString("0000") + "-" + dtOldestTextDate.Month.ToString("00")
                    + "-" + dtOldestTextDate.Day.ToString("00") + " " + dtOldestTextDate.Hour.ToString("00") + ":"
                    + dtOldestTextDate.Minute.ToString("00") + ":" + dtOldestTextDate.Second.ToString("00");
                if (dtOldestTextDate == dtNewestTableDate) // always overwrite/update the last date's values
                {
                    iHowMany++;
                    splitarray = s.Split(new char[] { CSVsplitterChar }, StringSplitOptions.None);
                    sCmd = "update " + TableName + " set open = " + splitarray[2] + ", high = " + splitarray[3]
                        + ", low = " + splitarray[4] + ", close = " + splitarray[5] + ", volume = " + splitarray[6]
                        + " where bartime = '" + sOutDate + "'";
                    oCmd.CommandText = sCmd;
                    oCmd.ExecuteNonQuery();
                }
                if (dtOldestTextDate > dtNewestTableDate)
                {
                    iHowMany++;
                    splitarray = s.Split(new char[] { CSVsplitterChar }, StringSplitOptions.None);
                    sCmd = "insert into " + TableName + " (bartime, open, high, low, close, volume) values('"
                        + sOutDate + "'," + splitarray[2] + "," + splitarray[3] + ","
                        + splitarray[4] + "," + splitarray[5] + "," + splitarray[6] + ")";
                    oCmd.CommandText = sCmd;
                    oCmd.ExecuteNonQuery();
                }
            }
            return iHowMany;
        }

     } // end class
} // end namespace
