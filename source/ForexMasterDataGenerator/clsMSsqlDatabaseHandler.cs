using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Windows.Forms;
using System.Data.SqlClient;
using System;

namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsMSsqlDatabaseHandler:clsDatabaseHandlerParent
    {
        public Dictionary<string, List<string>> ServerInstances = new Dictionary<string, List<string>>();

        SqlConnection oConn = new SqlConnection();
        SqlCommand oCmd = new SqlCommand();
        SqlDataAdapter oDA = new SqlDataAdapter();


        public override bool FindServers(ComboBox DisplayBox, ComboBox InstancesBox)
        {
            DisplayBox.Text = "";
            DisplayBox.Items.Clear();
            if (InstancesBox != null)
            {
                InstancesBox.Items.Clear();
                InstancesBox.Text = "";
            }
            StatusLabel.Text = "Seeking SQL Servers ...";
            Application.DoEvents();
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
            try
            {
                if (DisplayBox.Items.Count > 0)
                {
                    string stemp = Properties.Settings.Default.LastMSSqlServer;
                    if (DisplayBox.Items.Contains(stemp))
                    {
                        DisplayBox.Text = stemp;
                    }
                    else
                    {
                        DisplayBox.Text = (string)DisplayBox.Items[0];
                    }
                    List<string> SvrInst = ServerInstances[DisplayBox.Text];
                    if (InstancesBox != null)
                    {
                        foreach (string s in SvrInst)
                        {
                            InstancesBox.Items.Add(s);
                        }
                        if (InstancesBox.Items.Count > 0)
                        {
                            InstancesBox.Text = InstancesBox.Items[0].ToString();
                        }
                    }
                }
            }
            catch (Exception r)
            {
                StatusLabel.Text = r.Message;
                return false;
            }
            if (DisplayBox.Items.Count <= 0)
            {
                DisplayBox.Text = "{no active servers found}";
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
            oConn = new SqlConnection(ConnString);
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
            oConn = null;
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
                if (Properties.Settings.Default.LastDBname.Length > 3)
                    DisplayBox.Text = Properties.Settings.Default.LastDBname;
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
                oConn = new SqlConnection(ConnString);
                oConn.Open();
                oCmd = oConn.CreateCommand();
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "sp_tables";
                oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(dtTableList);
                oConn.Close();
                oConn = null;
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

        public override int GetTableRowCount()
        {
            int retVal = 0;
            oConn = new SqlConnection("server = " + ServerName + "; database = " + DBname
                + "; integrated security = sspi");
            oCmd = oConn.CreateCommand();
            oCmd.CommandText = "select count(*) as count from " + TableName;
            oCmd.CommandType = CommandType.Text;
            oConn.Open();
            oDA = new SqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            oDA.Fill(oTable);
            retVal = oTable.Rows[0].Field<int>("count");
            oConn.Close();
            oConn = null;
            return retVal;
        }

        public override DataTable GetTableData()
        {
            oConn = new SqlConnection("server = " + ServerName + "; database = " + DBname
                + "; integrated security = sspi");
            oCmd = oConn.CreateCommand();
            oCmd.CommandText = "select * from " + TableName + " order by bartime desc";
            oCmd.CommandType = CommandType.Text;
            oConn.Open();
            oDA = new SqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            oDA.Fill(oTable);
            oConn.Close();
            oConn = null;
            return oTable;
        }

        public override DataTable GetTableData(int RowsToGet)
        {
            SqlConnection oConn = new SqlConnection("server = " + ServerName + "; database = " + DBname
                + "; integrated security = sspi");
            SqlCommand oCmd = oConn.CreateCommand();
            oCmd.CommandText = "select top " + RowsToGet.ToString("0") + " * from " + TableName + " order by bartime desc";
            oCmd.CommandType = CommandType.Text;
            oConn.Open();
            SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            oDA.Fill(oTable);
            oConn.Close();
            return oTable;
        }

        public override bool SendNonQueries(List<string> CommandText)
        {
            oConn = new SqlConnection("server = " + ServerName + "; database = " + DBname
                + "; integrated security = sspi");
            oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            try
            {
                oConn.Open();
                foreach (string s in CommandText)
                {
                    oCmd.CommandText = s;
                    oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception j)
            {
                MessageBox.Show("Error posting updates!", "Update Fail");
                m_LastError = j.Message;
                return false;
            }
            return true;
        }

    } // end class
} // end namespace
