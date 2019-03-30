using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsStochasticIndex:clsGeneratorParent
    {

        /*
        ************ generator parent static vars available to all children ***********
        public static bool Abort = false;
        public static DataRowCollection oRows;
        public static int iDays = 0;
        public static ToolStripStatusLabel StatusLabel;
        public static string PairName = "";
        public static clsDBmasterHandler DBmasterHandler = null;
        */

        /************** global vars ****************/
        int iPercentKslowPeriod = 3, iPercentDperiod = 3, iLookBackPeriod = 14;
        double[,] tHiLowTensor = null;
        double[,] tClosePCkPCdTensor = null;

        /****************** properties ********************/
        public int LookbackPeriod { get { return iLookBackPeriod; } set => iLookBackPeriod = value; }
        public int SlowPercentKperiod { get { return iPercentKslowPeriod; } set => iPercentKslowPeriod = value; }
        public int Dperiod { get { return iPercentDperiod; } set => iPercentDperiod = value; }

        /****************** constructor ********************/

        /****************** event handlers ********************/

        /***************** private methods ********************/

        public override bool GenerateData()
        {
            //---------- do the deed
            if (!CreateValues())
            {
                MessageBox.Show("Error creating Stochastic data!");
                StatusLabel.Text = "Error creating Stochastic data!";
                return false;
            }
            //---------- save the results
            if (!SaveData())
            {
                MessageBox.Show("Error saving Stochastic data!");
                StatusLabel.Text = "Error saving Stochastic data!";
                return false;
            }
            //---------- good run so save properties
            Properties.Settings.Default.StochLookback = iLookBackPeriod;
            Properties.Settings.Default.StochSlowKperiod = iPercentKslowPeriod;
            Properties.Settings.Default.StockDperiod = iPercentDperiod;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "Stochastics Completed " + iDays.ToString("0") + " rows.";
            //---------- now exit
            return true; // if all goes well
        }

        bool CreateValues()
        {

            int iBarPeriod = BarPeriod;
            int iDaysToGet = iDays;
            int iWorkingDays = iLookBackPeriod + iPercentDperiod + iPercentKslowPeriod;
            int iTableLength = oRows.Count;

            //------ now prep the highest high, lowest low values
            StatusLabel.Text = "Starting Stoch highest-high/lowest-low creation.";
            tHiLowTensor = new double[iTableLength, 4];
            tClosePCkPCdTensor = new double[iTableLength, 4];
            int iDex = 0;
            //------ load the input data
            foreach (DataRow oDR in oRows)
            {
                tHiLowTensor[iDex, 0] = oDR.Field<double>("High");
                tHiLowTensor[iDex, 1] = oDR.Field<double>("Low");
                tClosePCkPCdTensor[iDex, 0] = oDR.Field<double>("Close");
                iDex++;
            }
            //-------- now calculate highest high and lowest low
            StatusLabel.Text = "Calculating highest-highs/lowest-lows";
            Application.DoEvents();
            int iStop = iTableLength - iLookBackPeriod;
            for (iDex = 0; iDex < iStop; iDex++)
            {
                tHiLowTensor[iDex, 2] = tHiLowTensor[iDex, 0]; // current row high value
                tHiLowTensor[iDex, 3] = tHiLowTensor[iDex, 1]; // current row low value
                for (int jDex = 1; jDex < iLookBackPeriod; jDex++)
                {
                    if (tHiLowTensor[iDex + jDex, 0] > tHiLowTensor[iDex, 2])
                    { tHiLowTensor[iDex, 2] = tHiLowTensor[iDex + jDex, 0]; }
                    if (tHiLowTensor[iDex + jDex, 1] < tHiLowTensor[iDex, 3])
                    { tHiLowTensor[iDex, 3] = tHiLowTensor[iDex + jDex, 1]; }
                }
            } // highest hi and lowest lo are in same row as high and low, synced with iTable row number

            StatusLabel.Text = "Starting %K,%D calculations.";
            Application.DoEvents();
            /*
            public DateTime BarTime;
            public float PercentK;
            public float PercentKslow;
            public float PercentD;
            public float PercentDslow;
            */
            //----- calculate %K
            iStop = iDaysToGet + iPercentDperiod + iPercentKslowPeriod;
            for (iDex = 0; iDex < iStop; iDex++)
            {
                if (Abort)
                { return false; }
                tClosePCkPCdTensor[iDex, 1] = 100 * ((tClosePCkPCdTensor[iDex, 0] - tHiLowTensor[iDex, 3]) / (tHiLowTensor[iDex, 2] - tHiLowTensor[iDex, 3]));
            }
            //------ calculate %D/slow %K
            for (iDex = 0; iDex < iDaysToGet; iDex++)
            {
                if (Abort)
                { return false; }
                double pcK = tClosePCkPCdTensor[iDex, 1];
                for (int jDex = 1; jDex < iPercentKslowPeriod; jDex++) // column 2
                {
                    pcK += tClosePCkPCdTensor[iDex + jDex, 1];
                }
                pcK /= iPercentKslowPeriod;
                tClosePCkPCdTensor[iDex, 2] = pcK;
            }

            //------ calculate slow %D
            for (iDex = 0; iDex < iDaysToGet; iDex++) // column 3
            {
                if (Abort)
                { return false; }
                double pcK = tClosePCkPCdTensor[iDex, 2];
                for (int jDex = 1; jDex < iPercentDperiod; jDex++)
                {
                    pcK += tClosePCkPCdTensor[iDex + jDex, 2];
                }
                pcK /= iPercentDperiod;
                tClosePCkPCdTensor[iDex, 3] = pcK;
            }
            StatusLabel.Text = "Computation completed, storing data...";
            return true;
        }

        bool SaveData()
        {
            List<string> CommandTextList = new List<string>();

            string sBarPeriod = BarPeriod.ToString("0");

            CommandTextList.Add("delete from stochastics where pairname='" + PairName + "' and barperiod=" + sBarPeriod);
            
            string sLookBack = iLookBackPeriod.ToString(), sKslowPeriod = iPercentKslowPeriod.ToString(), sDperiod = iPercentDperiod.ToString();
            string sInsert = "insert into stochastics (PairName, BarDateTime,BarPeriod,LookbackPeriod,K_slowmult,D_mult,"
                + "PercentK,PercentKslow,PercentD,PercentDslow) VALUES ('" + PairName + "','{0}'," + sBarPeriod
                + "," + sLookBack + "," + sKslowPeriod + "," + sDperiod + ",{1},{2},{3},{4})";

            DateTime fDT = DateTime.Today;
            string sBarTime = "";
            for (int iDex = 0; iDex < iDays; iDex++)
            {
                fDT = oRows[iDex].Field<DateTime>("BarTime");
                sBarTime = fDT.Year.ToString("0000") + "-" + fDT.Month.ToString("00") + "-" + fDT.Day.ToString("00");
                CommandTextList.Add(string.Format(sInsert, sBarTime, tClosePCkPCdTensor[iDex, 1].ToString(),
                    tClosePCkPCdTensor[iDex, 2].ToString(), tClosePCkPCdTensor[iDex, 2].ToString(), 
                    tClosePCkPCdTensor[iDex, 3].ToString()));
            }

            try
            {
                DBmasterHandler.SendNonQueries(CommandTextList);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Stochastics data save error " + e.Message);
                StatusLabel.Text = "Stochastics data save error!";
                return false;
            }
            if (Abort) return false;
            return true;

        }

    } // end class

}  // end namespace

/*
 CREATE TABLE `stochastics` (
  `PairName` varchar(8) NOT NULL,
  `BarDateTime` datetime NOT NULL,
  `BarPeriod` int(11) NOT NULL,
  `LookbackPeriod` int(11) NOT NULL,
  `K_slowmult` int(11) NOT NULL,
  `D_mult` int(11) NOT NULL,
  `PercentK` float NOT NULL,
  `PercentKslow` float NOT NULL,
  `PercentD` float NOT NULL,
  `PercentDslow` float NOT NULL,
  PRIMARY KEY (`PairName`,`BarDateTime`,`BarPeriod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
*/
/*
* %K = 100(C - L14)/(H14 - L14)

Where:
C = the most recent closing price
L14 = the low of the 14 previous trading sessions
H14 = the highest price traded during the same 14-day period
%K= the current market rate for the currency pair
%D = 3-period moving average of %K
*/
