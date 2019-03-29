using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace MT4_History_Reader_MySql
{
    /// <summary>
    /// this is the steering class to send commands to the correct DB handler class
    /// </summary>
    class clsDBmasterHandler
    {
        /**************************** properties *******************************/
        public string ServerName
        { set { clsDBhandlerParent.ServerName = value; } }

        public string InstanceName
        { set { clsDBhandlerParent.InstanceName = value; } }
        
        public string DBname
        { set { clsDBhandlerParent.DBname = value; } }
        
        public string TableName
        { set { clsDBhandlerParent.TableName = value; } }
        
        private bool m_IsMySqlDB = false;
        public bool IsMySqlDB
        { set { m_IsMySqlDB = value; } }

        public ToolStripStatusLabel StatusLabel
        { set { clsDBhandlerParent.StatusLabel = value; } }


        /***************************** global vars ******************************/
        clsDBmssql MSsqlHandler = new clsDBmssql();
        clsDBmysql MySqlHandler = new clsDBmysql();

        /***************************** constructor *******************************/

        /**************************** public methods ********************************/
        public bool FindServers(ComboBox DisplayBox, ComboBox InstancesBox)
        {
            bool retVal = false;
            if (m_IsMySqlDB)
            {
                retVal = MySqlHandler.FindServers(DisplayBox, InstancesBox);
            }
            else
                retVal = MSsqlHandler.FindServers(DisplayBox, InstancesBox);
            Application.DoEvents();
            return retVal;
        }

        public bool FindDatabases(ComboBox DisplayBox)
        {
            bool retVal = false;
            if (m_IsMySqlDB)
                retVal = MySqlHandler.FindDatabases(DisplayBox);
            else
                retVal = MSsqlHandler.FindDatabases(DisplayBox);
            Application.DoEvents();
            return retVal;
        }

        public bool FindTables(ComboBox DisplayBox)
        {
            bool retVal = false;
            if (m_IsMySqlDB)
                retVal = MySqlHandler.FindTables(DisplayBox);
            else
                retVal = MSsqlHandler.FindTables(DisplayBox);
            Application.DoEvents();
            return retVal;
        }

        public List<string> GetInstances(string Servername)
        {
            List<string> retVal = new List<string>();
            if (m_IsMySqlDB)
                retVal= MySqlHandler.GetInstances(Servername);
            else
                retVal = MSsqlHandler.GetInstances(Servername);
            Application.DoEvents();
            return retVal;
        }

        public DateTime GetMostRecentDate()
        {
            DateTime retVal = new DateTime();
            if (m_IsMySqlDB)
            { retVal = MySqlHandler.GetMostRecentDate(); }
            else
            { retVal = MSsqlHandler.GetMostRecentDate(); }
            Application.DoEvents();
            return retVal;
        }

        public int UpdateTableData(List<string> NewData, bool TruncateTable)
        {
            int retVal = 0;
            if (m_IsMySqlDB)
            { retVal = MySqlHandler.UpdateTableData(NewData, TruncateTable); }
            else
            { retVal = MSsqlHandler.UpdateTableData(NewData, TruncateTable); }
            Application.DoEvents();
            return retVal;
        }

        /********************************** private methods *********************************/

    } // end class
} // end namespace
