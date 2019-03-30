using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsMySqlDatabaseHandler:clsDatabaseHandlerParent,IDisposable
    {
        /*************************** global vars *************************/
        List<string> ServerList = new List<string>();
        string sUID = "", sPWD = "";

        MySqlConnection oConn = null;
        MySqlCommand oCmd = null;
        MySqlDataAdapter oDA = null;


        public clsMySqlDatabaseHandler():base()
        {
            // mysql has not server browser so we have to wire in the names to try
            foreach (string s in Properties.Settings.Default.MySqlServerList)
            {
                ServerList.Add(s);
            }
            // no built in SSPI security context so DB user and pwd
            sUID = Properties.Settings.Default.MySqlUID;
            sPWD = Properties.Settings.Default.MySqlPWD;
        }

        public override bool FindDatabases(ComboBox DisplayBox)
        {
            DisplayBox.Items.Clear();
            DisplayBox.Text = "";
            if (ServerName == "") return false;

            try
            {
                StatusLabel.Text = "Getting DB list from Server " + ServerName;
				if (oConn != null)
				{
					if (oConn.DataSource != ServerName)
					{
						if (oConn.State != ConnectionState.Closed)
							oConn.Close();
						oConn = null;
					}
				}
				if (oConn == null)
				{
					string sConn = "server=" + ServerName + ";uid=" + sUID + ";pwd=" + sPWD;
					if (PortNumber != "") sConn += ";Port=" + PortNumber;
					oConn = new MySqlConnection(sConn);
				}
				if (oConn.State != ConnectionState.Open)
	                oConn.Open();
                oCmd = oConn.CreateCommand();
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = "select SCHEMA_NAME from information_schema.SCHEMATA";
                oDA = new MySqlDataAdapter(oCmd);
                DataTable oTable = new DataTable();
                oDA.Fill(oTable);
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
					if (Properties.Settings.Default.LastMySqlDBname.Length > 3)
						DisplayBox.Text = Properties.Settings.Default.LastMySqlDBname;
					else
						DisplayBox.Text = DisplayBox.Items[0].ToString();
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
            DisplayBox.Items.Clear();
            DisplayBox.Text = "";
            StatusLabel.Text = "Getting Table list from DB " + DBname;
			if (oConn == null)
			{
				//oConn = new MySqlConnection("server=" + ServerName + ";database=" + DBname + ";uid=" + sUID + ";pwd=" + sPWD);
				string sConn = "server=" + ServerName + ";uid=" + sUID + ";pwd=" + sPWD + ";database=" + DBname;
				if (PortNumber != "") sConn += ";Port=" + PortNumber;
				oConn = new MySqlConnection(sConn);
			}

			if (oConn.State != ConnectionState.Open)
				oConn.Open();
            oCmd = oConn.CreateCommand();
			oCmd.CommandText = "USE " + DBname;
			oCmd.ExecuteNonQuery();

			DataTable oTable = new DataTable();
            oDA = new MySqlDataAdapter(oCmd);
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = "show tables";
            oDA.Fill(oTable);
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
            return true;
        }

        public override int GetTableRowCount()
        {
            int retVal = 0;
            try
            {
				if (oConn == null)
				{
					string sConn = "server=" + ServerName + ";uid=" + sUID + ";pwd=" + sPWD + ";database=" + DBname;
					if (PortNumber != "") sConn += ";Port=" + PortNumber;
					oConn = new MySqlConnection(sConn);
				}
				if (oConn.State != ConnectionState.Open)
					oConn.Open();
				oCmd = oConn.CreateCommand();
				oCmd.CommandType = CommandType.Text;
				oCmd.CommandText = "USE " + DBname;
				oCmd.ExecuteNonQuery();

                oCmd.CommandText = "select count(*) as count from " + TableName;
                oDA = new MySqlDataAdapter(oCmd);
                DataTable oTable = new DataTable();
                oDA.Fill(oTable);
                retVal = (int)oTable.Rows[0].Field<Int64>("count");
                oConn.Close();
            }
            catch (Exception i)
            {
                StatusLabel.Text = "Unable to fetch row count.";
                MessageBox.Show("Unable to fetch row count");
                m_LastError = i.Message;
            }
                return retVal;
        }

        public override DataTable GetTableData()
        {
			if (oConn == null)
			{
				oConn = new MySqlConnection("server = " + ServerName + "; database = " + DBname
					+ ";pwd=" + sPWD + "; uid=" + sUID);
			}
			if (oConn.State != ConnectionState.Open)
				oConn.Open();

			oCmd = oConn.CreateCommand();
            oCmd.CommandText = "select * from " + TableName + " order by bartime desc";
            oCmd.CommandType = CommandType.Text;
            oDA = new MySqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            oDA.Fill(oTable);
            oConn.Close();
            return oTable;
        }

        public override DataTable GetTableData(int RowsToGet)
        {
			if (oConn == null)
			{
				oConn = new MySqlConnection("server = " + ServerName + "; database = " + DBname
					+ ";pwd=" + sPWD + "; uid=" + sUID);
			}
			if (oConn.State != ConnectionState.Open)
				oConn.Open();
			oCmd = oConn.CreateCommand();
            oCmd.CommandText = "select * from " + TableName + " order by bartime desc limit " + RowsToGet.ToString();
            oCmd.CommandType = CommandType.Text;
            oDA = new MySqlDataAdapter(oCmd);
            DataTable oTable = new DataTable();
            oDA.Fill(oTable);
            oConn.Close();
            return oTable;
        }

        public override bool SendNonQueries(List<string> CommandText)
        {
			if (oConn == null)
			{
				oConn = new MySqlConnection("server = " + ServerName + "; database = " + DBname
					+ ";UID=" + sUID + ";PWD=" + sPWD);
			}
			if (oConn.State != ConnectionState.Open)
				oConn.Open();
			oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            try
            {
                foreach (string s in CommandText)
                {
                    // fixup for "[Open]", etc.
                    string sTemp = s.Replace("[", "");
                    sTemp = sTemp.Replace("]", "");
                    oCmd.CommandText = sTemp;
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


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (oDA != null) oDA.Dispose();
				if (oCmd != null) oCmd.Dispose();
				if (oConn != null) oConn.Dispose();
				oDA = null;
				oCmd = null;
				oConn = null;
			} // end if disposing
		} // end virtual Dispose()


	} // end class
} // end namespace
