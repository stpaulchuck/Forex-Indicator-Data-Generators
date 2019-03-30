using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
	class clsTrixgen:clsGeneratorParent
    {

        /*********** global vars **********/
        double[] FastTrixValues, SlowTrixValues;
        string[] FastColors, SlowColors, Arrows;


        /****************** Properties ********************/
        int iFastTrixPeriod = 0;
        public int FastTrixPeriod
        { get { return iFastTrixPeriod; } set { iFastTrixPeriod = value; } }

        int iSlowTrixPeriod = 0;
        public int SlowTrixPeriod
        { get { return iSlowTrixPeriod; } set { iSlowTrixPeriod = value; } }


        /****************** methods ********************/
        public override bool GenerateData()
        {
            //---------- do the deed
            if (!CreateValues())
            {
                MessageBox.Show("Error creating Trix data!");
                StatusLabel.Text = "Error creating Trix data!";
                return false;
            }
            //---------- save the results
            if (!SaveData())
            {
                MessageBox.Show("Error saving Trix data!");
                StatusLabel.Text = "Error saving Trix data!";
                return false;
            }
            //---------- good run so save properties
            Properties.Settings.Default.FastTrixSpan = iFastTrixPeriod;
            Properties.Settings.Default.SlowTrixSpan = iSlowTrixPeriod;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "Trix Completed " + iDays.ToString("0") + " rows.";
            //---------- now exit
            return true;
        }

        private bool CreateValues()
        {
            StatusLabel.Text = "Generating Fast Trix...";
            Application.DoEvents();
            GenerateTrix("Fast");
            Application.DoEvents();
            if (Abort) return false;
            StatusLabel.Text = "Generating Slow Trix...";
            Application.DoEvents();
            GenerateTrix("Slow");
            Application.DoEvents();
			StatusLabel.Text = "Generating Trix Arrows";
			Application.DoEvents();
			CreateArrows();
            if (Abort) return false;
            StatusLabel.Text = "Trix data generation complete";
            Application.DoEvents();
            return true;
        }


        private bool SaveData()
        {
            List<string> CommandTextList = new List<string>();

            DateTime BarDate = DateTime.Now;
			string cmd2 = "", sFastColor = "none", sSlowColor = "none"; //, sFastDirection = "", sSlowDirection = "";
            double fastvar = 0.0, slowvar = 0.0;

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

            CommandTextList.Add("delete from TrixData where pairname = '" + PairName + "' and period = " + sBarPeriod);

            string cmdString = "insert into TrixData (PairName, Period, BarTime, FastTrix, SlowTrix, FastColor, SlowColor,"
                + "RedArrow, GreenArrow) values ('" + PairName + "', " + sBarPeriod + ",'{0:G}', {1:N7}, {2:N7}, '{3}', '{4}', {5}, {6})";
            int RedArrow = 0, GreenArrow = 0;
            for (int i = 0; i <= iDays; i++)
            {
                Application.DoEvents();
                if (Abort) break;
                if (i % 17 == 0) StatusLabel.Text = "Creating Trix Item " + i.ToString();
                Application.DoEvents();
                BarDate = oRows[i].Field<DateTime>("BarTime");
                string SqlBarTime = BarDate.ToString("yyyy-MM-dd HH:mm:ss");

                // set up local vars from array data
                fastvar = FastTrixValues[i];
                slowvar = SlowTrixValues[i];
                sFastColor = FastColors[i];
                sSlowColor = SlowColors[i];
				/*
				if (sFastColor.ToLower() == "both")
                {
                    if (FastColors[i - 1].ToLower() == "red") sFastDirection = "pivotup";
                    else if (FastColors[i-1].ToLower() == "both") sFastDirection = "pivotup";
                    else sFastDirection = "pivotdown";
                }
                else
                {
                    if (sFastColor.ToLower() == "red") sFastDirection = "down";
                    else sFastDirection = "up";
                }
                if (sSlowColor.ToLower() == "both")
                {
                    if (SlowColors[i - 1].ToLower() == "red") sSlowDirection = "pivotup";
                    else if (FastColors[i - 1].ToLower() == "both") sFastDirection = "pivotup";
                    else sSlowDirection = "pivotdown";
                }
                else
                {
                    if (sSlowColor.ToLower() == "red") sSlowDirection = "down";
                    else sSlowDirection = "up";
                }
				*/

                
                if (Arrows[i] == null)
                {
                    RedArrow = 0;
                    GreenArrow = 0;
                }
                else if (Arrows[i] == "red")
                {
                    RedArrow = 1;
                    GreenArrow = 0;
                }
                else if (Arrows[i] == "green")
                {
                    RedArrow = 0;
                    GreenArrow = 1;
                }
                else
                {
                    RedArrow = 0;
                    GreenArrow = 0;
                }
                
                try
                {
                    cmd2 = String.Format(cmdString, SqlBarTime, fastvar, slowvar, sFastColor, sSlowColor, RedArrow, GreenArrow);
                    CommandTextList.Add(cmd2);
                }
                catch (Exception s2)
                {
                    Debug.WriteLine(s2);
                    StatusLabel.Text = "Trix data save error!";
                    return false;
                }
            } // end for (i = ...
            try
            {
				StatusLabel.Text = "saving Trix data to DB";
				Application.DoEvents();
                DBmasterHandler.SendNonQueries(CommandTextList);
            }
            catch (Exception o)
            {
                Debug.WriteLine("Trix data save error:" + o.Message);
                StatusLabel.Text = "Trix data save error!";
                return false;
            }
            return true;
        }

        private void CreateArrows()
        {
            Arrows = new string[iDays + 1];
            double RedTrixPrev = -99.0, GreenTrixPrev = 99.0; // nonsense value to preload the vars
            double PrevLow = -99.0, PrevHigh = 99.0; // nonsense value to preload vars
            int i1 = 0, i2 = 0, indextemp = 0;

            // create trix divergence arrow info
            for (int loopindex2 = iDays; loopindex2 > 0; loopindex2--)
            {
                Application.DoEvents();
                if (Abort) break;
                Arrows[loopindex2] = "";
                if (FastColors[loopindex2] == "both")
                {
                    // see if there is a trix-price divergence
                    if (FastColors[loopindex2 - 1] == "red")
                    {
                        if (!IsIndicatorPeak(loopindex2, FastTrixValues)) continue;
                        indextemp = GetIndicatorLastPeak(loopindex2);
                        if (indextemp == -1) continue; // can't go past the end of the array
                        i1 = Math.Sign(FastTrixValues[loopindex2] - FastTrixValues[indextemp]);
                        i2 = Math.Sign(oRows[loopindex2].Field<double>("High") - oRows[indextemp].Field<double>("High"));
                        if (i1 != i2) // divergence
                        {
                            Arrows[loopindex2] = "red";
                        }
                        PrevHigh = oRows[loopindex2].Field<double>("High");
                        RedTrixPrev = FastTrixValues[loopindex2];
                    }
                    else if (FastColors[loopindex2 - 1] == "green")
                    {
                        if (!IsIndicatorTrough(loopindex2, FastTrixValues)) continue;
                        indextemp = GetIndicatorLastTrough(loopindex2);
                        if (indextemp == -1) continue; // can't go past end of array
                        i1 = Math.Sign(FastTrixValues[loopindex2] - FastTrixValues[indextemp]);
                        i2 = Math.Sign(oRows[loopindex2].Field<double>("Low") - oRows[indextemp].Field<double>("Low"));
                        if (i1 != i2) // divergence
                        {
                            Arrows[loopindex2] = "green";
                        }
                        PrevLow = oRows[loopindex2].Field<double>("Low");
                        GreenTrixPrev = FastTrixValues[loopindex2];
                    }
                }
            } // end for(loopindex2...
        }

        int GetIndicatorLastTrough(int CurrentBarIndex)
        {
            for (int loopindex = CurrentBarIndex + 5; loopindex < iDays; loopindex++)
            {
                if (SlowTrixValues[loopindex] <= SlowTrixValues[loopindex + 1] && SlowTrixValues[loopindex] <= SlowTrixValues[loopindex + 2]
                    && SlowTrixValues[loopindex] <= SlowTrixValues[loopindex - 1] && SlowTrixValues[loopindex] <= SlowTrixValues[loopindex - 2])
                {
                    for (int loopindex2 = loopindex; loopindex2 < iDays; loopindex2++)
                        if (FastTrixValues[loopindex2] <= FastTrixValues[loopindex2 + 1] && FastTrixValues[loopindex2] < FastTrixValues[loopindex2 + 2]
                            && FastTrixValues[loopindex2] <= FastTrixValues[loopindex2 - 1] && FastTrixValues[loopindex2] < FastTrixValues[loopindex2 - 2])
                            return (loopindex2); // send back index of last trough
                }
            }
            return (-1); // send back index of nonexistent item
        }

        int GetIndicatorLastPeak(int CurrentBarIndex)
        {
            for (int loopindex = CurrentBarIndex + 5; loopindex < iDays; loopindex++)
            {
                if (SlowTrixValues[loopindex] >= SlowTrixValues[loopindex + 1] && SlowTrixValues[loopindex] >= SlowTrixValues[loopindex + 2]
                    && SlowTrixValues[loopindex] >= SlowTrixValues[loopindex - 1] && SlowTrixValues[loopindex] >= SlowTrixValues[loopindex - 2])
                {
                    for (int loopindex2 = loopindex; loopindex2 < iDays; loopindex2++)
                        if (FastTrixValues[loopindex2] >= FastTrixValues[loopindex2 + 1] && FastTrixValues[loopindex2] > FastTrixValues[loopindex2 + 2]
                            && FastTrixValues[loopindex2] >= FastTrixValues[loopindex2 - 1] && FastTrixValues[loopindex2] > FastTrixValues[loopindex2 - 2])
                            return (loopindex2); // send back index of last peak
                }
            }
            return (-1); // none found, send back nonexistent item index
        }

        bool IsIndicatorPeak(int BarIndex, double[] FastTrixValues)
        {
            if (FastTrixValues[BarIndex] >= FastTrixValues[BarIndex + 1] && FastTrixValues[BarIndex] > FastTrixValues[BarIndex + 2] && FastTrixValues[BarIndex] > FastTrixValues[BarIndex - 1]) return true;
            return false;
        }

        bool IsIndicatorTrough(int BarIndex, double[] FastTrixValues)
        {
            if (FastTrixValues[BarIndex] <= FastTrixValues[BarIndex + 1] && FastTrixValues[BarIndex] < FastTrixValues[BarIndex + 2] && FastTrixValues[BarIndex] < FastTrixValues[BarIndex - 1]) return true;
            return false;
        }

        private void ClearDataArrays()
        {
            FastColors = null;
            SlowColors = null;
            FastTrixValues = null;
            SlowTrixValues = null;
            Arrows = null;
        }
        /// <summary>
        /// creates the Trix data and saves it to the SQL table
        /// </summary>
        /// <param name="TrixPeriod"></param>
        /// <param name="FastOrSlow"></param>
        private void GenerateTrix(string FastOrSlow)
        {
            int iTemp = 0;
            if (FastOrSlow.ToLower() == "fast")
                iTemp = iFastTrixPeriod;
            else
                iTemp = iSlowTrixPeriod;
            // prep the scaling factors
            double OnePntTwo = 1.2;
            double OnePntTwoSqrd = OnePntTwo * OnePntTwo;
            double OnePntTwoCubed = OnePntTwoSqrd * OnePntTwo;
            double NegativeOnePntTwoCubed = -OnePntTwoCubed;
            double Ratio1 = 3.0 * (OnePntTwoSqrd + OnePntTwoCubed);
            double Ratio2 = -3.0 * (2.0 * OnePntTwoSqrd + OnePntTwo + OnePntTwoCubed);
            double Ratio3 = 3.0 * OnePntTwo + 1.0 + OnePntTwoCubed + 3.0 * OnePntTwoSqrd;
            if (iTemp < 1) iTemp = 1;
            iTemp = (iTemp - 1) / 2 + 1;
            double SFsmall = 2.0 / (iTemp + 1.0);
            double SFlarge = 1 - SFsmall;
            // vars to hold 'last value' in the loops
            double EMA1Prev = 0.0;
            double EMA2Prev = 0.0;
            double EMA3Prev = 0.0;
            double EMA4Prev = 0.0;
            double EMA5Prev = 0.0;
            double EMA6Prev = 0.0;
            double EMAPrevFinal = 0.0;
            double TrixPrev = 0.0;
            // actual Trix value arrays
            int Terminator = oRows.Count;
            double[] TrixValues = new double[Terminator];
            string[] TrixColors = new string[iDays + 1]; // choices red, green
            Terminator--;
            // create the trix values
            for (int loopindex = Terminator; loopindex >= 0; loopindex--)
            {
                Application.DoEvents();
                if (Abort) break;
                double EMA1 = SFsmall * oRows[loopindex].Field<double>("Close") + SFlarge * EMA1Prev;
                double EMA2 = SFsmall * EMA1 + SFlarge * EMA2Prev;
                double EMA3 = SFsmall * EMA2 + SFlarge * EMA3Prev;
                double EMA4 = SFsmall * EMA3 + SFlarge * EMA4Prev;
                double EMA5 = SFsmall * EMA4 + SFlarge * EMA5Prev;
                double EMA6 = SFsmall * EMA5 + SFlarge * EMA6Prev;
                double EMAfinal = NegativeOnePntTwoCubed * EMA6 + Ratio1 * EMA5 + Ratio2 * EMA4 + Ratio3 * EMA3;
                // now for the trix = (EMAfinal - EMAPrevFinal)/EMAPrevFinal, Note: our indexing is backwards to this
                double fTemp = 0.0;
                if (EMAPrevFinal > 0)
                    fTemp = (EMAfinal - EMAPrevFinal) / EMAPrevFinal;
                else
                    fTemp = 0.0;
                TrixValues[loopindex] = fTemp;
                // create trix color info
                if (loopindex < iDays)
                {
                    if (fTemp >= TrixPrev)
                        TrixColors[loopindex] = "green";
                    else
                        TrixColors[loopindex] = "red";
                    if (TrixColors[loopindex] != TrixColors[loopindex + 1])
                        TrixColors[loopindex + 1] = "both";
                }
                // now store this for the next loop
                EMA1Prev = EMA1;
                EMA2Prev = EMA2;
                EMA3Prev = EMA3;
                EMA4Prev = EMA4;
                EMA5Prev = EMA5;
                EMA6Prev = EMA6;
                EMAPrevFinal = EMAfinal;
                TrixPrev = TrixValues[loopindex];
            } // end for (loopindex ...
            if (Abort) return;
            // assign the results to the fast or slow array vars
            if (FastOrSlow.ToLower() == "fast")
            {
                FastTrixValues = TrixValues;
                FastColors = TrixColors;
            }
            else
            {
                SlowTrixValues = TrixValues;
                SlowColors = TrixColors;
            }
        } // end generatetrix()

    } // end class
} // end namespace
