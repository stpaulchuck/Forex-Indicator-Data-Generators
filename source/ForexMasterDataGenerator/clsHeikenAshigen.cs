using System;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;


namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsHeikenAshigen:clsGeneratorParent
    {

        /************** data struct ****************/
        struct HADataStruct
        {
            public double OpenPrice;
            public double HighPrice;
            public double LowPrice;
            public double ClosePrice;
            public string Color;
        }

        /************** VARS ****************/
        // sort order is oldest to newest date time!! beware
        SortedList<DateTime, HADataStruct> HAValues = new SortedList<DateTime, HADataStruct>();


        /************** methods ****************/
        
        public override bool GenerateData()
        {
            //---------- do the deed
            if (!CreateValues())
            {
                MessageBox.Show("Error creating Heiken Ashi data!");
                StatusLabel.Text = "Error creating Heiken Ashi data!";
                return false;
            }
            //---------- save the results
            if (!SaveData())
            {
                MessageBox.Show("Error saving Heiken Ashi data!");
                StatusLabel.Text = "Error saveing Heiken Ashi data!";
                return false;
            }

            StatusLabel.Text = "Heiken Ashi Completed " + iDays.ToString("0") + " rows.";
            //---------- now exit
            return true;
        }

        /*
1. HA-Close = (Open(0) + High(0) + Low(0) + Close(0)) / 4

2. HA-Open = (HA-Open(-1) + HA-Close(-1)) / 2 

3. HA-High = Maximum of the High(0), HA-Open(0) or HA-Close(0) 

4. HA-Low = Minimum of the Low(0), HA-Open(0) or HA-Close(0) 
 */
        private bool CreateValues()
        {
            HAValues.Clear();
            try
            {
                // first set up the zeroeth row in the outpu
                HADataStruct hda = new HADataStruct(), lasthda = new HADataStruct();
                DataRow oRow = oRows[iDays - 1];
                hda.OpenPrice = oRow.Field<double>("Open");
                hda.HighPrice = oRow.Field<double>("High");
                hda.LowPrice = oRow.Field<double>("Low");
                hda.ClosePrice = oRow.Field<double>("Close");
                if (oRow.Field<double>("Open") > oRow.Field<double>("Close"))
                {
                    hda.Color = "red";
                }
                else
                {
                    hda.Color = "green";
                }
                HAValues.Add(oRow.Field<DateTime>("BarTime"), hda);
                // now loop the rows and output HA
                for (int indexer = iDays - 2; indexer >= 0; indexer--) // oldest data at the end
                {
                    oRow = oRows[indexer];
                    if (Abort)
                    {
                        StatusLabel.Text = "operation aborted by user!";
                        return false;
                    }
                    if (indexer % 17 == 0)
                    {
                        StatusLabel.Text = "creating Heiken Ashi row " + indexer.ToString();
                    }
                    lasthda = hda;
                    hda = new HADataStruct();
                    hda.OpenPrice = (lasthda.OpenPrice + lasthda.ClosePrice) / 2;
                    hda.ClosePrice = (oRow.Field<double>("Open") + oRow.Field<double>("High") +
                        oRow.Field<double>("Low") + oRow.Field<double>("Close")) / 4;
                    hda.HighPrice = Math.Max(oRow.Field<double>("High"), Math.Max(hda.OpenPrice, hda.ClosePrice));
                    hda.LowPrice = Math.Min(oRow.Field<double>("Low"), Math.Min(hda.OpenPrice, hda.ClosePrice));
                    hda.Color = (hda.ClosePrice > hda.OpenPrice ? "green" : "red");
                    HAValues.Add(oRow.Field<DateTime>("BarTime"), hda);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool SaveData()
        {
            List<string> CommandTextList = new List<string>(); // fill up with outputs and send to db handler

            if (Abort) return false;
            DateTime BarDate = DateTime.Now;
            string cmd2 = "", sColor = "";
            HADataStruct hda = new HADataStruct();
            double fOpen = 0, fClose = 0, fHigh = 0, fLow = 0;

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

            // first, clear out the old data for this pair and bar period
            CommandTextList.Add("delete from HeikenAshiData where pairname = '" + PairName + "' and period = " + sBarPeriod);
            // now create all the insert statements
            string cmdString = "insert into HeikenAshiData (PairName, BarTime, Period, [Open], High, Low, [Close], Color)"
                + " values ('" + PairName + "', '{0:G}', " + sBarPeriod + ", {1:F4}, {2:F4}, {3:F4}, {4:F4}, '{5:G}')";
            
            for (int i = 0; i < iDays; i++)
            {
                Application.DoEvents();
                if (Abort) break;
                if (i % 17 == 0) StatusLabel.Text = "Saving Heiken Ashi Item " + i.ToString();
                Application.DoEvents();
                BarDate = HAValues.Keys[i];
                // set up local vars from array data
                hda = HAValues[BarDate];
                fOpen = hda.OpenPrice;
                fClose = hda.ClosePrice;
                fHigh = hda.HighPrice;
                fLow = hda.LowPrice;
                sColor = hda.Color;
                string SqlBarTime = BarDate.ToString("yyyy-MM-dd HH:mm:ss");

                try
                {
                    cmd2 = String.Format(cmdString, SqlBarTime, fOpen, fHigh, fLow, fClose, sColor);
                    CommandTextList.Add(cmd2);
                }
                catch (Exception s2)
                {
                    Debug.WriteLine(s2);
                    StatusLabel.Text = "*** HeikenAshi Data Save ERROR! ***";
                    return false;
                }
            } // end for (i = ...
            try
            {
                DBmasterHandler.SendNonQueries(CommandTextList);
            }
            catch (Exception d)
            {
                Debug.WriteLine(d);
                StatusLabel.Text = "HeikenAshi Data Save Failed!";
                return false;
            }
            StatusLabel.Text = "Heiken Ashi " + iDays.ToString() + " rows completed.";
            if (Abort) return false;
            return true;
        }

    } // end class
} // end namespace
