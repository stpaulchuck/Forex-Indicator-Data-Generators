using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;


namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsDatabaseHandlerParent
    {
        /*********************** static class vars *************************/
        static public string ServerName = "";
		static public string PortNumber = "";
        static public string InstanceName = "";
        static public string DBname = "";
        static public string TableName = "";
        static public ToolStripStatusLabel StatusLabel = null;
        protected static StringCollection PrimaryCurrencyList, SecondaryCurrencyList;


        /************************** instance properties *********************/
        protected string m_LastError = "";
        public string LastError
        { get { return m_LastError; } }


        /*********************** base class constructor ********************/
        public clsDatabaseHandlerParent()
        {
            PrimaryCurrencyList = Properties.Settings.Default.PrimaryCurrencyList;
            SecondaryCurrencyList = Properties.Settings.Default.SecondaryCurrencyList;
        }

        /************************* virtual methods *************************/
        virtual public bool FindServers(ComboBox DisplayBox, ComboBox InstancesBox)
        { return false; }

        virtual public bool FindDatabases(ComboBox DisplayBox)
        { return false; }

        virtual public bool FindTables(ComboBox DisplayBox)
        { return false; }

        virtual public List<string> GetInstances(string ServerName)
        { return new List<string>(); }

        virtual public int GetTableRowCount()
        { return 0; }

        virtual public DataTable GetTableData()
        { return new DataTable(); }

        virtual public DataTable GetTableData(int RowsToGet)
        { return new DataTable(); }

        /// <summary>
        /// each derived data geneator creates an array of strings to send to the DB
        /// this method uses a persistent connection to post the entire list
        /// </summary>
        /// <param name="CommandText">collecton of 'table name;command text'</param>
        /// <returns>true if successful, else false</returns>
        virtual public bool SendNonQueries(List<string> CommandText)
        { return false; }

    } // end class
} // end namespace
