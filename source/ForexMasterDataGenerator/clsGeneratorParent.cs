using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsGeneratorParent
    {
        /*************** enums available to all children ***************/
        protected enum DirectionType { up, down, pivotdown, pivotup };


        /************ static vars available to all children ***********/
        public static bool Abort = false;
        public static DataRowCollection oRows;
        public static int iDays = 0;
        public static ToolStripStatusLabel StatusLabel;
        public static string PairName = "";
        public static int BarPeriod = 0;
        public static clsDBmasterHandler DBmasterHandler = null;

        /********* common global vars used by all children *********/


        /**************** common public method *******************/
        public virtual bool GenerateData()
        {
            return false;
        }

    } // end class
} // end namespace
