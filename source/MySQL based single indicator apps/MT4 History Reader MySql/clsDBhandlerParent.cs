using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace MT4_History_Reader_MySql
{
    class clsDBhandlerParent
    {
        /*********************** static class vars *************************/
        static public string ServerName = "";
        static public string InstanceName = "";
        static public string DBname = "";
        static public string TableName = "";
        static public ToolStripStatusLabel StatusLabel = null;
        static public char CSVsplitterChar = ',';
        protected static StringCollection PrimaryCurrencyList, SecondaryCurrencyList;


        /************************** instance properties *********************/
        protected string m_LastError = "";
        public string LastError
        { get { return m_LastError; } }


        /************************ instance global vars **********************/
        protected DateTime dtNewestTableDate = new DateTime();

        /*********************** base class constructor ********************/
        public clsDBhandlerParent()
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

        virtual public DateTime GetMostRecentDate()
        { return DateTime.Now; }

        virtual public int UpdateTableData(List<string> NewData, bool TruncateTable)
        { return 0; }


        /************************** common private methods **********************/

        protected DateTime FindDateFromDataLine(string DataLine)
        {
            char sSplitter = CSVsplitterChar;
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


    } // end class
} // end namespace
