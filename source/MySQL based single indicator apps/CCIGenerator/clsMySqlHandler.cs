using MySql.Data.MySqlClient;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;

namespace CCIGenerator
{
	class clsMySqlHandler : IDisposable
	{
		/*********** properties ***********/
		private string m_LastError = "";
		public string LastError => m_LastError;

		/*********** global vars **********/
		string sUID = Properties.Settings.Default.UID, sPWD = Properties.Settings.Default.PWD;
		StringCollection g_PrimaryCurrencyList = new StringCollection() { "USD", "CHF", "GBP", "NZD", "AUD", "EUR" };
		ComboBox m_DBlister = null, m_TableLister = null;
		string m_ServerName = "", m_DatabaseName = "";
		MySqlConnection oConn = null;
		MySqlCommand oCmd = null;

		/************ constructor/destructor ************/
		public clsMySqlHandler(ComboBox DBlister, ComboBox TableLister)
		{
			m_DBlister = DBlister;
			m_TableLister = TableLister;
		}

		~clsMySqlHandler()
		{
			Dispose(false);
		}

		/************** methods ***********/
		public bool ExecuteSQLCommand(string CommandString)
		{
			if (oConn.State != ConnectionState.Open)
			{
				oConn.Open();
			}
			if (oCmd == null)
				oCmd = oConn.CreateCommand();
			try
			{
				oCmd.CommandText = CommandString;
				oCmd.CommandType = CommandType.Text;
				oCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				m_LastError = "ExecuteSQLCommand() failed: " + e.Message + "   stacktrace = " + e.StackTrace;
				return false;
			}
			return true;
		}

		public bool ExecuteSQLCommand(string[] CommandStrings)
		{
			if (oConn.State != ConnectionState.Open)
			{
				oConn.Open();
			}
			if (oCmd == null)
				oCmd = oConn.CreateCommand();
			oCmd.CommandType = CommandType.Text;
			try
			{
				foreach (string s in CommandStrings)
				{
					oCmd.CommandText = s;
					oCmd.ExecuteNonQuery();
				}
			}
			catch (Exception e)
			{
				m_LastError = "ExecuteSQLCommand() failed: " + e.Message + "   stacktrace = " + e.StackTrace;
				return false;
			}
			return true;
		}

		public DataTable GetTableData(string QueryString)
		{
			// build the querystring outside due to too many variations controlled by main class
			if (oConn.State != ConnectionState.Open)
			{ oConn.Open(); }
			if (oCmd == null)
			{ oCmd = oConn.CreateCommand(); }
			oCmd.CommandText = QueryString;
			MySqlDataAdapter oDA = new MySqlDataAdapter(oCmd);
			DataTable oTable = new DataTable();
			oDA.Fill(oTable);
			oConn.Close();
			if (oTable.Rows.Count <= 0)
				throw new Exception("table query returned zero rows!!");
			return oTable;
		}

		public bool GetTableNames()
		{
			m_LastError = "";

			m_TableLister.Items.Clear();
			m_TableLister.Text = "";
			if (oConn == null)
			{
				m_LastError = "Connection error fetching table names. Connection object is null.";
				return false;
			}

			if (oConn.State != ConnectionState.Open)
			{
				oConn.Open();
			}
			oCmd = oConn.CreateCommand();
			oConn.ChangeDatabase(m_DBlister.Text); // it's likely we changed
			DataTable oTable = new DataTable();
			MySqlDataAdapter oDA = new MySqlDataAdapter(oCmd);
			oCmd.CommandType = CommandType.Text;
			oCmd.CommandText = "show tables";
			oDA.Fill(oTable);
			if (oConn != null)
				if (oConn.State != ConnectionState.Closed)
					oConn.Close();
			//----create filter strings
			string filter1 = "###";
			foreach (string s1 in g_PrimaryCurrencyList)
				filter1 += s1 + "###";
			foreach (DataRow oRow in oTable.Rows)
			{
				string sTemp = oRow.Field<string>(0);
				if (sTemp.Length < 6) continue;
				string name1 = sTemp.Substring(0, 3).ToUpper();
				string name2 = sTemp.Substring(3, 3).ToUpper();
				if (!(filter1.Contains(name1)) || !(filter1.Contains(name2))) continue;
				//----okay, so add it
				m_TableLister.Items.Add(sTemp);
			}
			m_TableLister.Text = "Choose Table";
			Application.DoEvents();
			return true;
		}

		public bool GetDatabases()
		{
			m_LastError = "";

			m_DBlister.Items.Clear();
			m_DBlister.Text = "";
			if (m_ServerName == "") return false;
			//Debug.WriteLine("datasource = " + oConn.DataSource);
			try
			{
				if (oConn == null)
				{
					m_LastError = "Error getting Databases, connection object is null.";
					throw new Exception("Error getting Databases, connection object is null.");
				}
				if (oConn.State != ConnectionState.Open)
				{
					oConn.Open();
				}
				oCmd = oConn.CreateCommand();
				oCmd.CommandType = CommandType.Text;
				oCmd.CommandText = "select SCHEMA_NAME from information_schema.SCHEMATA";
				MySqlDataAdapter oDA = new MySqlDataAdapter(oCmd);
				DataTable oTable = new DataTable();
				oDA.Fill(oTable);
				foreach (DataRow oRow in oTable.Rows)
				{
					if (oRow.Field<string>(0).ToLower().Contains("forex"))
						m_DBlister.Items.Add(oRow.Field<string>(0));
				}
				if (m_DBlister.Items.Count > 0)
				{
					m_DBlister.Text = "Choose DBase";
					if (m_DBlister.Items.Count == 1)
					{
						m_DatabaseName = m_DBlister.Items[0].ToString();
						m_DBlister.Text = m_DatabaseName;
						oCmd.CommandText = "USE " + m_DatabaseName;
						oCmd.ExecuteNonQuery();
					}
				}
				else
				{
					m_DBlister.Text = "*none found*";
					return false;
				}
			}
			catch (Exception e)
			{
				if (oConn != null)
					if (oConn.State == ConnectionState.Open)
						oConn.Close();
				m_LastError = "get databases error: " + e.Message;
				return false;
			}
			if (m_DBlister.Items.Count == 1)
				return GetTableNames();
			else
				return true;
		}

		public bool MakeSqlConnection(string ServerName)
		{
			m_LastError = "";

			m_ServerName = ServerName;
			if (oConn != null)
			{
				oConn.Close();
				oConn = null;
			}
			string sConnectString = "";
			if (ServerName.Contains(":"))
			{
				string sTemp = ServerName;
				string sServer = sTemp.Substring(0, sTemp.IndexOf(":"));
				string sPort = sTemp.Substring(sTemp.IndexOf(":") + 1);
				sConnectString = "server=" + sServer + ";Port=" + sPort + ";UID=" + sUID + ";PWD=" + sPWD;
			}
			else
			{
				sConnectString = "server=" + ServerName + ";UID=" + sUID + ";PWD=" + sPWD;
			}
			try
			{
				oConn = new MySqlConnection(sConnectString);
				oConn.Open();
				oCmd = oConn.CreateCommand();
			}
			catch (Exception eo)
			{
				oConn = null;
				m_LastError = "Server Connection Error: " + eo.Message + "   stacktrace = " + eo.StackTrace;
				return false;
			}
			return GetDatabases();
		}

		public int GetCount()
		{
			int retVal = 0;
			string sCmd = "select count(*) from " + m_TableLister.Text;
			if (oConn.State != ConnectionState.Open)
			{
				oConn.Open();
			}
			if (oCmd == null)
			{
				oCmd = oConn.CreateCommand();
			}
			oCmd.CommandText = sCmd;
			MySqlDataReader oReader = oCmd.ExecuteReader();
			oReader.Read();
			retVal = oReader.GetInt32(0);
			oReader.Close();

			return retVal;
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
				if (oCmd != null)
				{
					oCmd.Dispose();
					oCmd = null;
				}
				if (oConn != null)
				{
					if (oConn.State != ConnectionState.Closed)
					{ oConn.Close(); }
					oConn.Dispose();
					oConn = null;
				}
			} // end if disposing
		} // end virtual Dispose()

	}  //  end class

}  //  end namespace
