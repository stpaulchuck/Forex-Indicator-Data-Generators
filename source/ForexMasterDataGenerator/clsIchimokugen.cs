using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    class clsIchimokugen:clsGeneratorParent
    {

        /************** structs ****************/

        struct IchimokuDataStruct
        {
            public DateTime BarDate;
            public double Tenkan;
            public double Kijun;
        }
        struct IchimokuSpanStruct
        {
            public DateTime BarDate;
            public double SpanA;
            public double SpanB;
        }

        /****************** Properties ********************/
        int iTenkanPeriod = 2;
        public int TenkanPeriod
        { get { return iTenkanPeriod; } set { iTenkanPeriod = value; } }

        int iKijunPeriod = 5;
        public int KijunPeriod
        { get { return iKijunPeriod; } set { iKijunPeriod = value; } }

        int iSpanBperiod = 8;
        public int SpanBperiod
        { get { return iSpanBperiod; } set { iSpanBperiod = value; } }


        /************** VARS ****************/
        // sort order is oldest to newest date time!! beware
        List<IchimokuDataStruct> IchimokuDataList = new List<IchimokuDataStruct>();
        List<IchimokuSpanStruct> IchimokuSpanData = new List<IchimokuSpanStruct>();

        /****************** methods ********************/

        public override bool GenerateData()
        {
            //---------- do the deed
            if (!CreateValues())
            {
                MessageBox.Show("Error creating Ichimoku data!");
                StatusLabel.Text = "Error creating Ichimoku data!";
                return false;
            }
            //---------- save the results
            if (!SaveData())
            {
                MessageBox.Show("Error saving Ichimoku data!");
                StatusLabel.Text = "Error saving Ichimoku data!";
                return false;
            }
            //---------- good run so save properties
            Properties.Settings.Default.TenkanPeriod = iTenkanPeriod;
            Properties.Settings.Default.KijunPeriod = iKijunPeriod;
            Properties.Settings.Default.SpanBperiod = iSpanBperiod;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "Ichimoku Completed " + iDays.ToString("0") + " rows.";
            //---------- now exit
            return true;
        }


        /*
        1.Tenkan-Sen (Conversion Line ) = (Highest High + Lowest Low) / 2, for the past x periods 
        2.Kijun-Sen (Base Line) = (Highest High + Lowest Low) / 2, for the past y periods 
        3.Chikou Span (Lagging Span) = Today's closing price plotted y periods behind 
        4.Senkou Span A = (Tenkan-Sen + Kijun-Sen) / 2, plotted y periods ahead 
        5.Senkou Span B = (Highest High + Lowest Low) / 2, for the past z periods, plotted y periods ahead 
         */
        private bool CreateValues()
        {
            IchimokuDataStruct hda;
            IchimokuSpanStruct oSpans;
            double fHigh = 0, fLow = 0;
            int iStop = iDays + iKijunPeriod;
            IchimokuDataList.Clear();
            Application.DoEvents();
            for (int indexer = 0; indexer < iStop; indexer++)
            {
                if (indexer % 17 == 0)
                {
                    StatusLabel.Text = "Creating Ichimoku row " + indexer.ToString();
                }
                Application.DoEvents();
                if (Abort) throw new Exception("user aborted run");
                hda = new IchimokuDataStruct();
                DataRow oRow = oRows[indexer];
                hda.BarDate = oRow.Field<DateTime>("BarTime");
                fHigh = oRow.Field<double>("high");
                fLow = oRow.Field<double>("low");
                //------------ Tenkan_sen
                for (int subindexer = 1; subindexer < iTenkanPeriod; subindexer++)
                {
                    if (oRows[subindexer + indexer].Field<double>("High") > fHigh) fHigh = oRows[subindexer + indexer].Field<double>("High");
                    if (oRows[subindexer + indexer].Field<double>("low") < fLow) fLow = oRows[subindexer + indexer].Field<double>("Low");
                } // for (subindexer...)
                hda.Tenkan = (fHigh + fLow) / 2;
                //------------ Kijun_sen
                fHigh = oRow.Field<double>("high");
                fLow = oRow.Field<double>("low");
                for (int subindexer = 1; subindexer < iKijunPeriod; subindexer++)
                {
                    if (oRows[subindexer + indexer].Field<double>("High") > fHigh) fHigh = oRows[subindexer + indexer].Field<double>("High");
                    if (oRows[subindexer + indexer].Field<double>("low") < fLow) fLow = oRows[subindexer + indexer].Field<double>("Low");
                } // for (subindexer...)
                hda.Kijun = (fHigh + fLow) / 2;
                IchimokuDataList.Add(hda);
                //----------- now the spans
                if (indexer >= iKijunPeriod) // span a & b are cast forward this amount
                {
                    oSpans = new IchimokuSpanStruct();
                    fHigh = oRow.Field<double>("high");
                    fLow = oRow.Field<double>("low");
                    oSpans.BarDate = oRow.Field<DateTime>("BarTime");
                    //------------ Span A
                    oSpans.SpanA = (hda.Kijun + hda.Tenkan) / 2;
                    //------------ Span B
                    for (int subindexer = 1; subindexer < iSpanBperiod; subindexer++)
                    {
                        if (oRows[subindexer + indexer].Field<double>("High") > fHigh) fHigh = oRows[subindexer + indexer].Field<double>("High");
                        if (oRows[subindexer + indexer].Field<double>("low") < fLow) fLow = oRows[subindexer + indexer].Field<double>("Low");
                    } // for (spanperiod)...
                    oSpans.SpanB = (fHigh + fLow) / 2;
                    IchimokuSpanData.Add(oSpans);
                } // if > shift ...

            } // for (indexer...)

            return true;
        }

        private bool SaveData()
        {
            List<string> CommandTextList = new List<string>();

            DateTime BarDate = DateTime.Now;
            string cmd2 = "";
            IchimokuDataStruct hda = new IchimokuDataStruct();
            IchimokuSpanStruct hspan = new IchimokuSpanStruct();
            double fTenkan = 0, fKijun = 0, fSpanA = 0, fSpanB = 0;

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
            // first clear the old data
            CommandTextList.Add("delete from IchimokuData where pairname = '" + PairName + "' and barperiod = " + sBarPeriod);
            // now create the new inserts
            string cmdString = "insert into IchimokuData (PairName, BarTime, BarPeriod, Tenkan_sen, Kijun_sen, SpanA, SpanB, " +
                "TenkanPeriod, KijunPeriod, SpanBPeriod) values ('" + PairName + "', '{0:G}', " + sBarPeriod + ", {1:F5}, {2:F5}, {3:F5}, {4:F5}, " 
                + iTenkanPeriod.ToString("0") + ", " + iKijunPeriod.ToString("0") + ", " + iSpanBperiod.ToString("0") + ")";

            for (int i = 0; i < iDays; i++)
            {
                Application.DoEvents();
                if (Abort) break;
                if (i % 17 == 0) StatusLabel.Text = "Saving Ichimoku Item " + i.ToString();
                Application.DoEvents();
                hda = IchimokuDataList[i];
                BarDate = hda.BarDate;
                string SqlBarTime = BarDate.ToString("yyyy-MM-dd HH:mm:ss");
                // set up local vars from array data
                hda = IchimokuDataList[i];
                fTenkan = hda.Tenkan;
                fKijun = hda.Kijun;
                hspan = IchimokuSpanData[i];
                fSpanA = hspan.SpanA;
                fSpanB = hspan.SpanB;

                try
                {
                    cmd2 = String.Format(cmdString, SqlBarTime, fTenkan, fKijun, fSpanA, fSpanB);
                    CommandTextList.Add(cmd2);
                }
                catch (Exception s2)
                {
                    Debug.WriteLine(s2);
                    StatusLabel.Text = "*** Data Save ERROR! ***";
                    return false;
                }
            } // end for (i = ...
            try
            {
                DBmasterHandler.SendNonQueries(CommandTextList);
            }
            catch
            {
                StatusLabel.Text = "Error saving Ichimoku data!";
                return false;
            }
            StatusLabel.Text = iDays.ToString() + " rows completed.";
            if (Abort)
            {
                StatusLabel.Text = "Operation aborted by user.";
                return false;
            }
            return true;
        }


    } // end class
} // end namespace
