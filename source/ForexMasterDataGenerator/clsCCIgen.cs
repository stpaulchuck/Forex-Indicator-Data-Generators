using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    /************************* enums  ***************************/
    enum TrendColorType { gray, red, green, gold };


    /*********************** main class *************************/
    class clsCCIgen:clsGeneratorParent
    {

        /****************** Properties ********************/
        int iTrendCCIperiod = 14;
        public int TrendCCIperiod
        { get { return iTrendCCIperiod; } set { iTrendCCIperiod = value; } }

        int iEntryCCIperiod = 6;
        public int EntryCCIperiod
        { get { return iEntryCCIperiod; } set { iEntryCCIperiod = value; } }


        /****************** global vars ********************/
        SortedList<DateTime, double> EntryCCIValues = new SortedList<DateTime, double>();
        SortedList<DateTime, double> TrendCCIValues = new SortedList<DateTime, double>();
        SortedList<DateTime, int> GoldBarValues = new SortedList<DateTime, int>();
        SortedList<DateTime, DirectionType> EntryDirectionList = new SortedList<DateTime, DirectionType>();
        SortedList<DateTime, TrendColorType> TrendColorList = new SortedList<DateTime, TrendColorType>();


        /****************** constructor/destructor ********************/


        /****************** methods ********************/

        public override bool GenerateData()
        {
            //---------- do the deed
            if (!CreateValues())
            {
                MessageBox.Show("Error creating CCI data!");
                StatusLabel.Text = "Error creating CCI data!";
                return false;
            }
            //---------- save the results
            if (!SaveData())
            {
                MessageBox.Show("Error saving CCI data!");
                StatusLabel.Text = "Error saving CCI data!";
                return false;
            }
            //---------- good run so save properties
            Properties.Settings.Default.TrendCCIperiod = iTrendCCIperiod;
            Properties.Settings.Default.EntryCCIperiod = iEntryCCIperiod;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "CCI Completed " + iDays.ToString("0") + " rows.";
            //---------- now exit
            return true; // if all goes well
        }

        private bool SaveData()
        {
            List<string> CommandTextList = new List<string>();

            DateTime BarDate = DateTime.Now;
            string cmd2 = "";
            double trendvar = 0.0, entryvar = 0.0;
            int goldvar = 0;

            TimeSpan ospan = new TimeSpan(), ospan2 = new TimeSpan();
            // get the minutes of the bars, look out for weekend spans
            int iStop = TrendCCIValues.Count - 2;
            for (int indexer = 0; indexer < iStop; indexer++)
            {
                ospan = TrendCCIValues.Keys[indexer] - TrendCCIValues.Keys[indexer + 1];
                ospan2 = TrendCCIValues.Keys[indexer + 1] - TrendCCIValues.Keys[indexer + 2];
                if (ospan2 == ospan) break;
            }
            int barperiod = (int)Math.Abs(ospan.TotalMinutes);
            string sBarPeriod = barperiod.ToString("0");

            // first clear the old data out
            CommandTextList.Add("delete from CCIData where pairname = '" + PairName + "' and period = " + sBarPeriod);
            // now create the new entries
            string cmdString = "insert into CCIData (PairName, BarTime, Period, EntryCCI, TrendCCI, GoldBar, TrendCCIcolor, EntryCCIdirection"
                + ") values ('" + PairName + "', '{0:G}', " + sBarPeriod + ", {1:F4}, {2:F4}, {3}, '{4}', '{5}')";

            for (int i = 0; i < iDays; i++)
            {
                Application.DoEvents();
                if (Abort) break;
                if (i % 17 == 0) StatusLabel.Text = "Saving CCI Item " + i.ToString();
                Application.DoEvents();
                BarDate = EntryCCIValues.Keys[i];
                // set up local vars from array data
                entryvar = EntryCCIValues[BarDate];
                trendvar = TrendCCIValues[BarDate];
                if (GoldBarValues.Count >= iDays)
                    goldvar = GoldBarValues[BarDate];
                else
                    goldvar = 0;
                //           TimeSpan ts1 = EntryCCIValues.Keys[1] - EntryCCIValues.Keys[0];
                //           int iSpanMinutes = (int)ts1.TotalMinutes;
                string SqlBarTime = BarDate.ToString("yyyy-MM-dd HH:mm:ss");
                try
                {
                    cmd2 = String.Format(cmdString, SqlBarTime, entryvar, trendvar, goldvar, TrendColorList.Values[i], EntryDirectionList.Values[i]);
                    CommandTextList.Add(cmd2);
                }
                catch (Exception o)
                {
                    Debug.WriteLine("CCI data save error: " + o.Message);
                    return false;
                }
            } // end for (i = ...
            try
            {
                DBmasterHandler.SendNonQueries(CommandTextList);
            }
            catch (Exception e)
            {
                Debug.WriteLine("CCI data save error " + e.Message);
                StatusLabel.Text = "CCI data save error!";
                return false;
            }
            if (Abort) return false;
            return true;
        }

        private bool CreateValues()
        {
            EntryCCIValues.Clear();
            TrendCCIValues.Clear();
            GoldBarValues.Clear();
            TrendColorList.Clear();
            EntryDirectionList.Clear();
            bool bDoneGood = true;
            StatusLabel.Text = "Creating Trend CCI data...";
            bDoneGood &= FillDataArray(TrendCCIValues, iTrendCCIperiod);
            if (bDoneGood)
            {
                StatusLabel.Text = "Creating Entry CCI data...";
                bDoneGood &= FillDataArray(EntryCCIValues, iEntryCCIperiod);
                if (bDoneGood)
                {
                    StatusLabel.Text = "Creating CCI Gold Bar data...";
                    bDoneGood &= FindGoldBars();
                    if (bDoneGood)
                    {
                        StatusLabel.Text = "Creating Entry CCI direction values...";
                        bDoneGood &= FindEntryDirectionValues();
                    }
                }
            }
            if (!bDoneGood)
            {
                MessageBox.Show("There was an error creating the CCI data.", "Data Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool FindGoldBars()
        {
            //----------- vars
            int iUpBars = 20, iDownBars = 20, iLastGoldBarIndex = 0;
            TrendColorType ctTrendCCIcolor = TrendColorType.gray;

            //---------- data scanner
            int iCount = TrendCCIValues.Count;
            if (TrendCCIValues.Values[0] < 0) iLastGoldBarIndex = -1;
            else iLastGoldBarIndex = 1;
            //---------- insert two dummy entries
            GoldBarValues.Add(TrendCCIValues.Keys[0], 0);
            GoldBarValues.Add(TrendCCIValues.Keys[1], 0);

            TrendColorList.Add(TrendCCIValues.Keys[0], TrendColorType.gray);
            TrendColorList.Add(TrendCCIValues.Keys[1], TrendColorType.gray);

            try
            {
                for (int indexer = 2; indexer < iCount; indexer++) // starts from oldest to newest
                {
                    if (Abort) break;
                    ctTrendCCIcolor = TrendColorType.gray;
                    if (TrendCCIValues.Values[indexer] < 0 && TrendCCIValues.Values[indexer - 1] >= 0) // cross over down
                    {
                        if (Math.Sign(iLastGoldBarIndex) > 0)
                            iDownBars = 1;
                    }
                    else if (TrendCCIValues.Values[indexer] > 0 && TrendCCIValues.Values[indexer - 1] <= 0) // cross over up
                    {
                        if (Math.Sign(iLastGoldBarIndex) < 0)
                            iUpBars = 1;
                    }
                    else
                    {
                        if (TrendCCIValues.Values[indexer] > 0)
                            iUpBars++;
                        if (TrendCCIValues.Values[indexer] < 0)
                            iDownBars++;
                    }

                    if (iDownBars == 5)
                    {
                        iLastGoldBarIndex = indexer * -1;
                        
                    }
                    if (iUpBars == 5)
                    {
                        iLastGoldBarIndex = indexer;
                    }
                    if (iDownBars == 5 || iUpBars == 5)
                    {
                        GoldBarValues.Add(TrendCCIValues.Keys[indexer], 1);
                        iDownBars = 20;
                        iUpBars = 20;
                        ctTrendCCIcolor = TrendColorType.gold;
                    }
                    else
                    {
                        GoldBarValues.Add(TrendCCIValues.Keys[indexer], 0);
                    }
                    if (ctTrendCCIcolor != TrendColorType.gold)
                    {
                        if (iLastGoldBarIndex < 0)
                        {
                            if (TrendCCIValues.Values[indexer] <= 0)
                            {
                                ctTrendCCIcolor = TrendColorType.red;
                            }
                        }
                        else
                        {
                            if (TrendCCIValues.Values[indexer] > 0)
                            {
                                ctTrendCCIcolor = TrendColorType.green;
                            }
                        }
                    }
                    TrendColorList.Add(TrendCCIValues.Keys[indexer], ctTrendCCIcolor);
                } // for (indexer...)
            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1);
            }
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            //--------- all is well so exit 'true'
            return true;
        }

        /***************************************************************************
           CCI = (Typical Price  -  CCI period SMA of TP) / (.015 x Mean Deviation)

           Typical Price (TP) = (High + Low + Close)/3

           Constant = .015

           There are four steps to calculating the Mean Deviation. First, subtract 
           today's CCI period average of the typical price from each period's 
           typical price. Second, take the absolute values of these numbers. Third, 
           sum the absolute values. Fourth, divide by the total number of periods (20). 
        ******************************************************************************/

        /// <summary>
        /// takes the price data and creates the CCI Trend or Entry data
        /// </summary>
        /// <param name="OutputArray">a ref to the output data array</param>
        /// <param name="CCIPeriod">int value of averaging period</param>
        /// <returns>true if no problems, otherwise false</returns>
        private bool FillDataArray(SortedList<DateTime, double> OutputArray, int CCIPeriod)
        {
            int iTotalRows = oRows.Count;
            double[] daSMA = new double[iTotalRows]; // array of the Simple Moving Averages
            double[] daMD = new double[iTotalRows]; // array of the Mean Deviations
            double[] daTP = new double[iTotalRows]; // array of the Typical Prices
            double[] daDev = new double[iTotalRows]; // array of the deviations

            DataRow oRow;
            try
            {
                /** remember - row zero is the most recent date **/
                // first Typical Price array
                for (int tpindexer = 0; tpindexer < iTotalRows; tpindexer++)
                {
                    oRow = oRows[tpindexer];
                    double fTP = (oRow.Field<double>("High") + oRow.Field<double>("Low") + oRow.Field<double>("Close")) / 3;
                    daTP[tpindexer] = fTP;
                    Application.DoEvents();
                    if (Abort) break;
                }
                if (Abort) return false;

                // second, the SMA and Deviation
                for (int tpindexer = 0; tpindexer < iDays; tpindexer++)
                {
                    double fAvg = 0;
                    for (int subindexer = 0; subindexer < CCIPeriod; subindexer++)
                    {
                        fAvg += daTP[tpindexer + subindexer];
                    }
                    fAvg = fAvg / CCIPeriod;
                    daSMA[tpindexer] = fAvg;
                    daDev[tpindexer] = daTP[tpindexer] - fAvg;
                    Application.DoEvents();
                    if (Abort) break;
                }
                if (Abort) return false;

                // now get the Mean Deviation from the current TP average value
                for (int tpindexer = 0; tpindexer < iDays; tpindexer++)
                {
                    double fDev = 0.0;
                    double fTPAvg = daSMA[tpindexer];
                    for (int subindexer = 0; subindexer < CCIPeriod; subindexer++)
                    {
                        fDev += Math.Abs(daTP[tpindexer + subindexer] - fTPAvg);
                    } // end for()
                    daMD[tpindexer] = fDev / CCIPeriod;
                    Application.DoEvents();
                    if (Abort) break;
                } // end for()
                if (Abort) return false;

                // finally we can derive the CCI value
                double fCCIvalue = 0.0;
                for (int tpindexer = 0; tpindexer < iDays; tpindexer++)
                {
                    // CCI = (Typical Price  -  20-period SMA of TP) / (.015 x Mean Deviation)
                    fCCIvalue = daDev[tpindexer] / (0.015 * daMD[tpindexer]);
                    OutputArray.Add(oRows[tpindexer].Field<DateTime>("BarTime"), fCCIvalue);
                    Application.DoEvents();
                    if (Abort) return false;
                }
                if (Abort) return false;
            } // end try{}
            catch
            {
                return false;
            }
            // everything went okay
            return true;
        }

        private bool FindEntryDirectionValues()
        {
            DirectionType DirectionVal = DirectionType.up;
            EntryDirectionList.Clear();
            EntryDirectionList.Add(EntryCCIValues.Keys[0], DirectionVal); // arbitrary start value
            // generated data lists are oldest to newest date order
            int iEntryCount = EntryCCIValues.Count - 1; // the last bar we can't sense a pivot so we'll just do color
            double fBefore = 0.0, fDuring = 0.0, fAfter = 0.0;
            for (int indexer = 1; indexer < iEntryCount; indexer++)
            {
                if (Abort) break;
                fBefore = EntryCCIValues.Values[indexer - 1];
                fDuring = EntryCCIValues.Values[indexer];
                fAfter = EntryCCIValues.Values[indexer + 1];
                if (fAfter < fDuring) // either down or down pivot
                {
                    if (fBefore < fDuring || fBefore == fDuring)
                    {
                        DirectionVal = DirectionType.pivotdown;
                    }
                    else
                    {
                        DirectionVal = DirectionType.down;
                    }
                }
                else if (fAfter > fDuring) // either up or up pivot
                {
                    if (fBefore > fDuring || fBefore == fDuring)
                    {
                        DirectionVal = DirectionType.pivotup;
                    }
                    else
                    {
                        DirectionVal = DirectionType.up;
                    }
                }
                else // next same as this one, so not a pivot point
                {
                    if (fBefore < fDuring)
                    {
                        DirectionVal = DirectionType.up;
                    }
                    else if (fBefore > fDuring)
                    {
                        DirectionVal = DirectionType.down;
                    }
                    else // same value previously to this bar
                    {
                        DirectionVal = EntryDirectionList.Values[indexer - 1]; // so we're the same color as before
                    }
                }
                EntryDirectionList.Add(EntryCCIValues.Keys[indexer], DirectionVal);
            }
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            fBefore = EntryCCIValues.Values[iEntryCount - 1];
            fDuring = EntryCCIValues.Values[iEntryCount];
            if (fBefore < fDuring)
            {
                DirectionVal = DirectionType.up;
            }
            else if (fBefore > fDuring)
            {
                DirectionVal = DirectionType.down;
            }
            else // same value previously to this bar
            {
                DirectionVal = EntryDirectionList.Values[iEntryCount - 1]; // so we're the same color as before
            }
            EntryDirectionList.Add(EntryCCIValues.Keys[iEntryCount], DirectionVal);
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            return true;
        }

    } // end class
} // end namespace
