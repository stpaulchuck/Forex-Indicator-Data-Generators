using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;


namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsRVI3gen:clsGeneratorParent
    {

        /****************** Properties ********************/
        int iRVIperiod = 0;
        public int RVIperiod
        { get { return iRVIperiod; } set { iRVIperiod = value; } }

        public const int iRVIpadding = 3; // we use today plus three back for computation of values
        public int RVIpadding
        { get { return iRVIpadding; } }

        
        /****************** global vars ********************/
        double[] RVIvalues, RVIsigvalues;
        DirectionType[] RVIdirectionValues, RVIsigdirectionValues;

        /****************** methods ********************/
        public override bool GenerateData()
        {
            //---------- do the deed
            if (!CreateValues())
            {
                MessageBox.Show("Error creating RVI3 data!");
                StatusLabel.Text = "Error creating RVI3 data!";
                return false;
            }
            //---------- save the results
            if (!SaveData())
            {
                MessageBox.Show("Error saving RVI3 data!");
                StatusLabel.Text = "Errror saving RVI3 data!";
                return false;
            }
            //---------- good run so save properties
            Properties.Settings.Default.RVI3period = iRVIperiod;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "RVI3 Completed " + iDays.ToString("0") + " rows.";
            //---------- now exit
            return true;
        }

        private bool CreateValues()
        {
            RVIvalues = new double[iDays + iRVIpadding];
            RVIsigvalues = new double[iDays + iRVIpadding];
            RVIdirectionValues = new DirectionType[iDays + iRVIpadding];
            RVIsigdirectionValues = new DirectionType[iDays + iRVIpadding];
            int iIndexerStop = iDays + iRVIpadding - 1;

            for (int indexer = iIndexerStop; indexer >= 0; indexer--)
            {
                if (Abort) break;
                if (indexer % 17 == 0) StatusLabel.Text = "Creating RVI row " + indexer.ToString("0");
                double dNum = 0.0, dDeNum = 0.0;
                try
                {
                    for (int j = indexer; j < (indexer + iRVIperiod); j++)
                    {
                        double dValueUp = ((oRows[j].Field<double>("Close") - oRows[j].Field<double>("Open")) +
                            (2 * (oRows[j + 1].Field<double>("Close") - oRows[j + 1].Field<double>("Open"))) +
                            (2 * (oRows[j + 2].Field<double>("Close") - oRows[j + 2].Field<double>("Open"))) +
                            (oRows[j + 3].Field<double>("Close") - oRows[j + 3].Field<double>("Open"))) / 6;

                        double dValueDown = ((oRows[j].Field<double>("High") - oRows[j].Field<double>("Low")) +
                            (2 * (oRows[j + 1].Field<double>("High") - oRows[j + 1].Field<double>("Low"))) +
                            (2 * (oRows[j + 2].Field<double>("High") - oRows[j + 2].Field<double>("Low"))) +
                            (oRows[j + 3].Field<double>("High") - oRows[j + 3].Field<double>("Low"))) / 6;

                        dNum += dValueUp;
                        dDeNum += dValueDown;
                    }
                }
                catch (Exception e1)
                {
                    Debug.WriteLine(e1);
                    return false;
                }
                if (dDeNum != 0.0)
                    RVIvalues[indexer] = dNum / dDeNum;
                else
                    RVIvalues[indexer] = 0.00;
            }
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            for (int indexer = 0; indexer < iDays; indexer++)
            {
                if (indexer % 17 == 0) StatusLabel.Text = "Creating RVI Signal row " + indexer.ToString("0");
                RVIsigvalues[indexer] = (RVIvalues[indexer] + 2 * RVIvalues[indexer + 1] + 2 * RVIvalues[indexer + 2] + RVIvalues[indexer + 3]) / 6;
            }
            if (FindEntryDirectionValues(RVIvalues,ref RVIdirectionValues))
                FindEntryDirectionValues(RVIsigvalues, ref RVIsigdirectionValues);
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            return true;
        }

        private bool FindEntryDirectionValues(double[] InputData, ref DirectionType[] OutputData)
        {
            DirectionType DirectionVal = DirectionType.up;
            double fBefore = 0.0, fDuring = 0.0, fAfter = 0.0;
            int iIndexerStart = InputData.Length;
            if (iIndexerStart <= 0) return false;
            iIndexerStart--; // have to stop before the end of the array;
            if (InputData[iIndexerStart] > InputData[iIndexerStart - 1]) DirectionVal = DirectionType.down;
            OutputData[iIndexerStart] = DirectionVal;

            iIndexerStart--;
            for (int indexer = iIndexerStart; indexer > 0; indexer--)
            {
                if (Abort) break;
                // array values are newest at [0] and highest at the end
                fBefore = InputData[indexer + 1];
                fDuring = InputData[indexer];
                fAfter = InputData[indexer - 1];
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
                        DirectionVal = OutputData[indexer - 1]; // so we're the same color as before
                    }
                }
                OutputData[indexer] = DirectionVal;
            }
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            fBefore = InputData[1];
            fDuring = InputData[0];
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
                DirectionVal = OutputData[1]; // so we're the same color as before
            }
            OutputData[0] = DirectionVal;
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            return true;
        }

        private bool SaveData()
        {
            List<string> CommandTextList = new List<string>();

            DateTime BarDate = DateTime.Now;
            string cmd2 = "";

            TimeSpan ospan = new TimeSpan(), ospan2 = new TimeSpan();
            // get the minutes of the bars, look out for weekend spans
            int iStop = oRows.Count - 2;
            for (int indexer = 0; indexer < iStop; indexer++)
            {
                ospan = oRows[indexer].Field<DateTime>("BarTime") - oRows[indexer + 1].Field<DateTime>("BarTime");
                ospan2 = oRows[indexer + 1].Field<DateTime>("BarTime") - oRows[indexer + 2].Field<DateTime>("BarTime");
                if (ospan2 == ospan) break;
            }
            int barperiod = (int)Math.Abs(ospan.TotalMinutes);
            string sBarPeriod = barperiod.ToString("0");

            // first clean out the old stuff
            CommandTextList.Add("Delete from RVIdata where Pairname = '" + PairName + "' and period = " + sBarPeriod);
            Application.DoEvents();
            if (Abort) return false;

            // now save the new stuff
            string cmdString = "insert into RVIdata (PairName, period, bartime, RVIvalue, RVIsigvalue, RVIdirection, RVIsigdirection) values ('"
                + PairName + "', " + sBarPeriod + ",'{0:G}', {1:N7}, {2:N7}, '{3}', '{4}')";
            for (int i = 0; i < iDays; i++)
            {
                Application.DoEvents();
                if (Abort) break;
                if (i % 17 == 0) StatusLabel.Text = "Saving RVI3 Item " + i.ToString();
                Application.DoEvents();
                BarDate = oRows[i].Field<DateTime>("BarTime");
                string SqlBarTime = BarDate.ToString("yyyy-MM-dd HH:mm:ss");
                // set up local vars from array data
                try
                {
                    cmd2 = String.Format(cmdString, SqlBarTime, RVIvalues[i], RVIsigvalues[i], RVIdirectionValues[i], RVIsigdirectionValues[i]);
                    CommandTextList.Add(cmd2);
                }
                catch (Exception s2)
                {
                    Debug.WriteLine(s2);
                    StatusLabel.Text = "RVI data save error!";
                    return false;
                }
            } // end for (i = ...
            try
            {
                DBmasterHandler.SendNonQueries(CommandTextList);
            }
            catch (Exception u)
            {
                Debug.WriteLine("RVI data save error: " + u.Message);
                StatusLabel.Text = "RVI data save error!";
                return false;
            }
            return true;
        }

    } // end class
} // end namespace
