using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Data;

namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    /// <summary>
    /// this is the steering class to send commands to the correct DB handler class
    /// </summary>
    class clsDBmasterHandler:IDisposable
    {
        /**************************** properties *******************************/
        public string ServerName
        {
			set
			{
				string sTemp = value;
				if (sTemp.Contains(":"))
				{
					clsDatabaseHandlerParent.ServerName = sTemp.Substring(0, sTemp.IndexOf(":"));
					clsDatabaseHandlerParent.PortNumber = sTemp.Substring(sTemp.IndexOf(":") + 1);
				}
				else
				{
					clsDatabaseHandlerParent.ServerName = value;
					clsDatabaseHandlerParent.PortNumber = "";
				}
			}
		}

        public string InstanceName
        { set { clsDatabaseHandlerParent.InstanceName = value; } }
        
        public string DBname
        { set { clsDatabaseHandlerParent.DBname = value; } }
        
        public string TableName
        { set { clsDatabaseHandlerParent.TableName = value; } }
        
        private bool m_IsMySqlDB = true;
        public bool IsMySqlDB
        { set { m_IsMySqlDB = value; } }

        public ToolStripStatusLabel StatusLabel
        { set { clsDatabaseHandlerParent.StatusLabel = value; } }

		public string LastError { get; set; }


        /***************************** global vars ******************************/
        //clsMSsqlDatabaseHandler MSsqlHandler = new clsMSsqlDatabaseHandler();
        clsMySqlDatabaseHandler MySqlHandler = new clsMySqlDatabaseHandler();

        /***************************** constructor *******************************/

        /**************************** public methods ********************************/

        public bool FindDatabases(ComboBox DisplayBox)
        {
            bool retVal = false;
            retVal = MySqlHandler.FindDatabases(DisplayBox);
			if (!retVal) LastError = MySqlHandler.LastError;
			else LastError = "";
            Application.DoEvents();
            return retVal;
        }

        public bool FindTables(ComboBox DisplayBox)
        {
            bool retVal = false;
            retVal = MySqlHandler.FindTables(DisplayBox);
			if (!retVal) LastError = MySqlHandler.LastError;
			else LastError = "";
			Application.DoEvents();
            return retVal;
        }

        public List<string> GetInstances(string Servername)
        {
            List<string> retVal = new List<string>();
                retVal= MySqlHandler.GetInstances(Servername);
            Application.DoEvents();
            return retVal;
        }

        public int GetRowCount()
        {
            int retVal = 0;
            retVal = MySqlHandler.GetTableRowCount();
			if (retVal <= 0) LastError = MySqlHandler.LastError;
			else LastError = "";
			Application.DoEvents();
            return retVal;
        }

        public DataTable GetTableData()
        {
            DataTable retVal = new DataTable();
            retVal = MySqlHandler.GetTableData();
			if (retVal.Rows.Count <= 0) LastError = MySqlHandler.LastError;
			else LastError = "";
			Application.DoEvents();
            return retVal;
        }

        public DataTable GetTableData(int RowsToFetch)
        {
            DataTable retVal = new DataTable();
            retVal = MySqlHandler.GetTableData(RowsToFetch);
			if (retVal.Rows.Count != RowsToFetch) LastError = MySqlHandler.LastError;
			else LastError = "";
			Application.DoEvents();
            return retVal;
        }

        public bool SendNonQueries(List<string> CommandText)
        {
                return MySqlHandler.SendNonQueries(CommandText);
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
				MySqlHandler.Dispose();
				MySqlHandler = null;
			} // end if disposing
		} // end virtual Dispose()

		/********************************** private methods *********************************/

	} // end class
} // end namespace
