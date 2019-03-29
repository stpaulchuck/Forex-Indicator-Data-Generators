using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace IchimokuGenerator
{
	public partial class IchimokuGeneratorMain : Form
    {
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

		/************** VARS ****************/
		clsMySqlHandler oMySqlHandler = null;
        // data in table rows is newest date to oldest date!! beware
        DataTable oTable = new DataTable();
        StringCollection g_PrimaryCurrencyList = new StringCollection() { "USD", "CHF", "GBP", "NZD", "AUD", "EUR" };
        string sTableName = "";
        int iDays = 0, iTableMaxRows = 0;
        // sort order is oldest to newest date time!! beware
        List<IchimokuDataStruct> IchimokuDataList = new List<IchimokuDataStruct>();
        List<IchimokuSpanStruct> IchimokuSpanData = new List<IchimokuSpanStruct>();
        bool bAbort = false, bInitializing = false;
        int iTenkanPeriod = 0, iKijunPeriod = 0, iSpanBPeriod = 0;

        /************** Constructor ****************/
        public IchimokuGeneratorMain()
        {
            InitializeComponent();
			oMySqlHandler = new clsMySqlHandler(cmbDatabaseNames, cmbTableNames);
        }

        /************** methods ****************/
        private bool ValidateTextBoxes()
        {
            if (!int.TryParse(txtTenkanPeriod.Text, out iTenkanPeriod))
            {
                MessageBox.Show(this, "The Tenkan Period value is not a proper integer. Try again.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(txtKijunPeriod.Text, out iKijunPeriod))
            {
                MessageBox.Show(this, "The Kijun Period value is not a proper integer. Try again.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(txtSpanBPeriod.Text, out iSpanBPeriod))
            {
                MessageBox.Show(this, "The Span B Period value is not a proper integer. Try again.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!bInitializing)
            {
                try
                {
                    iDays = int.Parse(txtDays.Text);
                    int iMaxDays = int.Parse(lblDaysAvailable.Text);
                    if (iDays > iMaxDays) //error!!
                    {
                        txtDays.Text = iMaxDays.ToString();
                        iDays = iMaxDays;
                        MessageBox.Show(this, "Your number of days was greater than the \navailable " +
                            "data so I truncated the value.", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                {
                    iDays = 0;
                    MessageBox.Show(this, "The number of days value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            } // end if !bInitializing

            return true;
        }

        private bool ValidateComboBoxes()
        {
            return true;
        }

        private void cmbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bOriginal = bInitializing;
            bInitializing = true;
            if (!ValidateTextBoxes()) return;
            bInitializing = bOriginal;
            sTableName = cmbTableNames.Text;

            StatusLabel.Text = "Getting table row count for " + sTableName + " ...";
            Application.DoEvents();
            try
            {
				iTableMaxRows = oMySqlHandler.GetCount();
                lblDaysAvailable.Text = (iTableMaxRows - iKijunPeriod - (Math.Max(iSpanBPeriod, Math.Max(iKijunPeriod, iTenkanPeriod)))).ToString();
                txtDays.Text = lblDaysAvailable.Text;
            }
            catch (Exception ec)
            {
                StatusLabel.Text = "*** TABLE READ ERROR ***";
                Debug.WriteLine("***" + ec.Message);
                return;
            }
            StatusLabel.Text = "Idle ...";
        }

        private void cmbDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;

            // get table list
            /********************************/
            this.Cursor = Cursors.WaitCursor;
            cmbTableNames.Items.Clear();
            cmbTableNames.Text = "";
            StatusLabel.Text = "Getting Table list from DB " + cmbDatabaseNames.Text;
            DataTable dtTableList = new DataTable();
            try
            {
				oMySqlHandler.GetTableNames();
			}
            catch (Exception gt)
            {
                MessageBox.Show(this, "ERROR fetching tables!\n" + gt.Message, "ERROR");
                StatusLabel.Text = "Error fetching tables!";
                Application.DoEvents();
                return;
            }

			if (cmbTableNames.Items.Count > 0)
            {
                cmbTableNames.Text = cmbTableNames.Items[0].ToString();
                if (cmbTableNames.Items.Contains(Properties.Settings.Default.LastTableName))
                    cmbTableNames.Text = Properties.Settings.Default.LastTableName;
                StatusLabel.Text = "Idle ...";
            }
            else
            {
                MessageBox.Show(this, "No Currency Pair Tables Found", "ERROR");
                StatusLabel.Text = "No Currency Pair Tables Found!";
            }
            this.Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDaysAvailable.Text = "{not set}";
            StatusLabel.Text = "Connecting to server " + cmbServerName.Text + " ...";
            Application.DoEvents();

			try
            {
				oMySqlHandler.MakeSqlConnection(cmbServerName.Text);
            }
            catch (Exception eo)
            {
                MessageBox.Show(this, eo.Message, "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusLabel.Text = "*** SERVER CONNECT ERROR ***";
                return;
            }
            Application.DoEvents();
        }

        private void IchimokuGeneratorMain_Shown(object sender, EventArgs e)
        {
            bInitializing = true;

            txtTenkanPeriod.Text = Properties.Settings.Default.TenkanPeriod.ToString();
            txtSpanBPeriod.Text = Properties.Settings.Default.SpanBPeriod.ToString();
            txtKijunPeriod.Text = Properties.Settings.Default.KijunPeriod.ToString();
            cmbTableNames.Text = Properties.Settings.Default.LastTableName;
            cmbDatabaseNames.Text = Properties.Settings.Default.LastDBname;
            cmbServerName.Text = Properties.Settings.Default.LastServerName;
			cmbServerName_SelectedIndexChanged(null, EventArgs.Empty);
            bInitializing = false;
        }

        private void IchimokuGeneratorMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastDBname = cmbDatabaseNames.Text;
            Properties.Settings.Default.LastServerName = cmbServerName.Text;
            Properties.Settings.Default.LastTableName = cmbTableNames.Text;
            int iTemp = 0;
            int.TryParse(txtKijunPeriod.Text, out iTemp);
            Properties.Settings.Default.KijunPeriod = iTemp;
            int.TryParse(txtSpanBPeriod.Text, out iTemp);
            Properties.Settings.Default.SpanBPeriod = iTemp;
            int.TryParse(txtTenkanPeriod.Text, out iTemp);
            Properties.Settings.Default.Save();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            ((Button)sender).Focus();
            bAbort = true;
            Application.DoEvents();
            StatusLabel.Text = "Operation Aborted By User!";
            Application.DoEvents();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "Creating data...";
            if (!ValidateTextBoxes()) return;
            if (!ValidateComboBoxes()) return;
            //----------- fetch the input data
            string CommandText = "select * from " + sTableName + " order by bartime desc limit " + (iDays + iKijunPeriod + Math.Max(iSpanBPeriod, Math.Max(iKijunPeriod, iTenkanPeriod))).ToString();
			oTable = oMySqlHandler.GetTableData(CommandText);

            // disable input controls
            txtKijunPeriod.Enabled = false;
            txtSpanBPeriod.Enabled = false;
            txtTenkanPeriod.Enabled = false;
            cmbDatabaseNames.Enabled = false;
            cmbServerName.Enabled = false;
            cmbTableNames.Enabled = false;
            txtDays.Enabled = false;
            btnGenerate.Enabled = false;
            btnAbort.Focus();
            Application.DoEvents();

            //---------- do the deed
            bool bResults = true;
            string sError = "";
            if (!CreateData())
            {
                bResults = false;
                sError = "Error Creating Data!";
            }
            if (bResults)
            {
                StatusLabel.Text = "Saving data...";
                if (!SaveData())
                {
                    bResults = false;
                    sError = "Error Saving Data!";
                }
            }
            // enable input controls
            txtKijunPeriod.Enabled = true;
            txtSpanBPeriod.Enabled = true;
            txtTenkanPeriod.Enabled = true;
            cmbDatabaseNames.Enabled = true;
            cmbServerName.Enabled = true;
            cmbTableNames.Enabled = true;
            txtDays.Enabled = true;
            btnGenerate.Enabled = true;

            if (!bResults)
            {
                MessageBox.Show(this, sError);
                StatusLabel.Text = sError;
                return;
            }
            StatusLabel.Text = iDays.ToString() + " rows of data saved.";
        }

        /*
        1.Tenkan-Sen (Conversion Line ) = (Highest High + Lowest Low) / 2, for the past x periods 
        2.Kijun-Sen (Base Line) = (Highest High + Lowest Low) / 2, for the past y periods 
        3.Chikou Span (Lagging Span) = Today's closing price plotted y periods behind 
        4.Senkou Span A = (Tenkan-Sen + Kijun-Sen) / 2, plotted y periods ahead 
        5.Senkou Span B = (Highest High + Lowest Low) / 2, for the past z periods, plotted y periods ahead 
         */
        private bool CreateData()
        {
            IchimokuDataStruct hda;
            IchimokuSpanStruct oSpans;
            DataRowCollection oRows = oTable.Rows;
            double fHigh = 0, fLow = 0;
            int iStop = iDays + iKijunPeriod;
            IchimokuDataList.Clear();
            Application.DoEvents();
            for (int indexer = 0; indexer < iStop; indexer++)
            {
                Application.DoEvents();
                if (bAbort) throw new Exception("user aborted run");
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
                    for (int subindexer = 1; subindexer < iSpanBPeriod; subindexer++)
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
            DateTime BarDate = DateTime.Now;
            string cmd2 = "";
            IchimokuDataStruct hda = new IchimokuDataStruct();
            IchimokuSpanStruct hspan = new IchimokuSpanStruct();
            double fTenkan = 0, fKijun = 0, fSpanA = 0, fSpanB = 0;

            string sPairName = sTableName.Substring(0, 6);
            TimeSpan ospan = new TimeSpan(), ospan2 = new TimeSpan();
            // get the minutes of the bars, look out for weekend spans
            int iStop = oTable.Rows.Count - 2;
            for (int indexer = 0; indexer < iStop; indexer++)
            {
                ospan = oTable.Rows[indexer].Field<DateTime>("BarTime") - oTable.Rows[indexer + 1].Field<DateTime>("BarTime");
                ospan2 = oTable.Rows[indexer + 1].Field<DateTime>("BarTime") - oTable.Rows[indexer + 2].Field<DateTime>("BarTime");
                if (ospan2 == ospan) break;
            }
            int barperiod = (int)Math.Abs(ospan.TotalMinutes);

            string CommandText = "delete from IchimokuData where pairname = '" + sPairName + "' and barperiod = " + barperiod.ToString();
			oMySqlHandler.ExecuteSQLCommand(CommandText);
            string cmdString = "insert into IchimokuData (PairName, BarTime, BarPeriod, Tenkan_sen, Kijun_sen, SpanA, SpanB, TenkanPeriod, KijunPeriod, SpanBPeriod) values ('" + sPairName + "', '{0:G}', " + barperiod.ToString() + ", {1:F5}, {2:F5}, {3:F5}, {4:F5}, " + txtTenkanPeriod.Text + ", " + txtKijunPeriod.Text + ", " + txtSpanBPeriod.Text + ")";

			List<string> lstCommandStrings = new List<string>();
            for (int i = 0; i < iDays; i++)
            {
                Application.DoEvents();
                if (bAbort) break;
                if (i % 17 == 0) StatusLabel.Text = "Formatting Item " + i.ToString();
                Application.DoEvents();
                hda = IchimokuDataList[i];
                BarDate = hda.BarDate;
                // set up local vars from array data
                hda = IchimokuDataList[i];
                fTenkan = hda.Tenkan;
                fKijun = hda.Kijun;
                hspan = IchimokuSpanData[i];
                fSpanA = hspan.SpanA;
                fSpanB = hspan.SpanB;

                try
                {
                    cmd2 = String.Format(cmdString, BarDate.ToString("yyyy-MM-dd HH:mm"), fTenkan, fKijun, fSpanA, fSpanB);
					lstCommandStrings.Add(cmd2);
				}
                catch (Exception s2)
                {
                    Debug.WriteLine(s2);
                    StatusLabel.Text = "*** Data Save ERROR! ***";
                    return false;
                }
            } // end for (i = ...
			StatusLabel.Text = "saving data to DB";
			Application.DoEvents();
			oMySqlHandler.ExecuteSQLCommand(lstCommandStrings.ToArray());
            StatusLabel.Text = iDays.ToString() + " rows completed.";
            if (bAbort) return false;
            return true;
        }

    } // end class
} // end namespace

/*
1.Tenkan-Sen (Conversion Line ) = (Highest High + Lowest Low) / 2, for the past x periods 
2.Kijun-Sen (Base Line) = (Highest High + Lowest Low) / 2, for the past y periods 
3.Chikou Span (Lagging Span) = Today's closing price plotted y periods behind 
4.Senkou Span A = (Tenkan-Sen + Kijun-Sen) / 2, plotted y periods ahead 
5.Senkou Span B = (Highest High + Lowest Low) / 2, for the past z periods, plotted y periods ahead 
 */