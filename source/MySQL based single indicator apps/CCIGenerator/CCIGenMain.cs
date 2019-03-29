using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace CCIGenerator
{
    public partial class CCIGenMain : Form
    {
        /************** VARS ****************/
        // data in table rows is newest date to oldest date!! beware
        DataTable oTable = new DataTable();
        string sTableName = "";
        int iDays = 0, iTrendCCIperiod = 0, iEntryCCIperiod = 0, iTableMaxRows = 0;
        // sort order is oldest to newest date time!! beware
        SortedList<DateTime, double> EntryCCIValues = new SortedList<DateTime, double>();
        SortedList<DateTime, double> TrendCCIValues = new SortedList<DateTime, double>();
        SortedList<DateTime, int> GoldBarValues = new SortedList<DateTime, int>();
        bool bAbort = false, bInitializing = false;
        StringCollection g_PrimaryCurrencyList = new StringCollection() { "USD", "CHF", "GBP", "NZD", "AUD", "EUR" };
		clsMySqlHandler oMySqlHandler = null;


		/************** Constructor ****************/

		public CCIGenMain()
        {
            bInitializing = true;
            InitializeComponent();
			oMySqlHandler = new clsMySqlHandler(cmbDatabaseNames, cmbTableNames);
        }


		/************** event handlers ****************/

		private void CCIGenMain_Shown(object sender, EventArgs e)
		{
			bInitializing = true;

			txtTrendCCI.Text = Properties.Settings.Default.TrendCCI.ToString();
			txtEntryCCI.Text = Properties.Settings.Default.EntryCCI.ToString();
			foreach (string s in Properties.Settings.Default.ServerList)
			{
				cmbServerName.Items.Add(s);
			}
			string sTemp = Properties.Settings.Default.LastServer;
			if (cmbServerName.Items.Contains(sTemp))
			{ cmbServerName.Text = sTemp; }
			else
			{ cmbServerName.Text = cmbServerName.Items[0].ToString(); }
			oMySqlHandler.MakeSqlConnection(cmbServerName.Text);
			bInitializing = false;
		}

		private void CCIGenMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			int iTemp = 0;
			int.TryParse(txtEntryCCI.Text, out iTemp);
			Properties.Settings.Default.EntryCCI = iTemp;
			int.TryParse(txtTrendCCI.Text, out iTemp);
			Properties.Settings.Default.TrendCCI = iTemp;
			Properties.Settings.Default.LastServer = cmbServerName.Text;
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
				string CommandText = "Use " + cmbDatabaseNames.Text;
				if (!oMySqlHandler.GetTableNames())
				{
					throw new Exception(oMySqlHandler.LastError);
				}
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

		private void cmbTableNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool bOriginal = bInitializing;
			bInitializing = true;
			if (!ValidateTextBoxes()) return;
			bInitializing = bOriginal;
			sTableName = cmbTableNames.Text;

			StatusLabel.Text = "Getting table row count for " + cmbTableNames.Text + " ...";
			Application.DoEvents();
			try
			{
				iTableMaxRows = oMySqlHandler.GetCount();
				if (iTableMaxRows < iTrendCCIperiod || iTableMaxRows < iEntryCCIperiod)
				{
					lblDaysAvailable.Text = "0";
					txtDays.Text = "0";
					MessageBox.Show(this, "Not enough data rows in the source data for your selected CCI periods.",
						"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					lblDaysAvailable.Text = (iTableMaxRows - (iTrendCCIperiod > iEntryCCIperiod ? iTrendCCIperiod : iEntryCCIperiod)).ToString();
					txtDays.Text = lblDaysAvailable.Text;
				}
			}
			catch (Exception ec)
			{
				StatusLabel.Text = "*** TABLE ROW COUNT READ ERROR ***";
				Debug.WriteLine("***" + ec.Message);
				return;
			}
			StatusLabel.Text = "Idle ...";
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

		private void txtDays_TextChanged(object sender, EventArgs e)
		{
			ResetUsefulDays();
		}

		private void txtEntryCCI_TextChanged(object sender, EventArgs e)
		{
			ResetUsefulDays();
		}

		private void txtTrendCCI_TextChanged(object sender, EventArgs e)
		{
			ResetUsefulDays();
		}

		/// <summary>
		/// this is the meat and potatoes method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGenerate_Click(object sender, EventArgs e)
		{
			bAbort = false;
			ValidateTextBoxes();
			ValidateComboBoxes();
			int iLen = (iDays + (iTrendCCIperiod > iEntryCCIperiod ? iTrendCCIperiod : iEntryCCIperiod));
			string CommandText = "select * from " + cmbTableNames.Text + " order by bartime desc limit " + iLen.ToString();
			try
			{
				oTable = oMySqlHandler.GetTableData(CommandText);
			}
			catch (Exception e7)
			{
				MessageBox.Show(this, e7.Message, "SQL error");
				return;
			}
			TrendCCIValues.Clear();
			EntryCCIValues.Clear();
			GoldBarValues.Clear();

			if (oTable.Rows.Count <= iTrendCCIperiod) // can't do the math, not enough rows
			{
				MessageBox.Show(this, "Not enough price rows in the returned rowset. \nEither increase " +
					"the data in the table or try a smaller CCI time period.", "Data Length Error");
				StatusLabel.Text = "*** not enough data ***";
				return;
			}
			bool bDoneGood = true;
			bDoneGood &= FillDataArray(TrendCCIValues, iTrendCCIperiod);
			if (bDoneGood)
			{
				bDoneGood &= FillDataArray(EntryCCIValues, iEntryCCIperiod);
				if (bDoneGood)
				{
					bDoneGood &= FindGoldBars(GoldBarValues);
				}
			}
			if (!bDoneGood)
			{
				MessageBox.Show(this, "There was an error creating the CCI data.", "Data Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//------------------------
			SaveCCIData();
			//------------------------
			// display the results

		}


		/************** methods ****************/
		private bool ValidateTextBoxes()
        {
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

            try
            {
                iTrendCCIperiod = int.Parse(txtTrendCCI.Text);
            }
            catch
            {
                iTrendCCIperiod = 0;
                MessageBox.Show(this, "The Trend CCI period value is not a proper integer. Try again.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                iEntryCCIperiod = int.Parse(txtEntryCCI.Text);
            }
            catch
            {
                iEntryCCIperiod = 0;
                MessageBox.Show(this, "The Entry CCI period value is not a proper integer. Try again.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateComboBoxes()
        {
            return true;
        }

        private bool FindGoldBars(SortedList<DateTime, int> GBarray)
        {
            //----------- vars
            int iUpBars = 20, iDownBars = 20, iLastGoldBarIndex = 0;

            //---------- data scanner
            int iCount = TrendCCIValues.Count;
            if (TrendCCIValues.Values[0] < 0) iLastGoldBarIndex = -1;
            else iLastGoldBarIndex = 1;
            //---------- insert two dummy entries
            GoldBarValues.Add(TrendCCIValues.Keys[0], 0);
            GoldBarValues.Add(TrendCCIValues.Keys[1], 0);

            try
            {
                for (int indexer = 2; indexer < iCount; indexer++) // starts from oldest to newest
                {
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
                    }
                    else
                    {
                        GoldBarValues.Add(TrendCCIValues.Keys[indexer], 0);
                    }
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1);
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
        /// takes the price data and creates the CCI output data
        /// </summary>
        /// <param name="OutputArray">a ref to the output data array</param>
        /// <param name="CCIPeriod">int value of averaging period</param>
        /// <returns>true if no problems, otherwise false</returns>
        private bool FillDataArray(SortedList<DateTime, double> OutputArray, int CCIPeriod)
        {
            int iTotalRows = oTable.Rows.Count;
            double[] daSMA = new double[iTotalRows]; // array of the Simple Moving Averages
            double[] daMD = new double[iTotalRows]; // array of the Mean Deviations
            double[] daTP = new double[iTotalRows]; // array of the Typical Prices
            double[] daDev = new double[iTotalRows]; // array of the deviations

            DataRowCollection oRows = oTable.Rows;
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
                    if (bAbort) break;
                }
                if (bAbort) return false;

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
                    if (bAbort) break;
                }
                if (bAbort) return false;

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
                    if (bAbort) break;
                } // end for()
                if (bAbort) return false;

                // finally we can derive the CCI value
                double fCCIvalue = 0.0;
                for (int tpindexer = 0; tpindexer < iDays; tpindexer++)
                {
                    // CCI = (Typical Price  -  20-period SMA of TP) / (.015 x Mean Deviation)
                    fCCIvalue = daDev[tpindexer] / (0.015 * daMD[tpindexer]);
                    OutputArray.Add(oRows[tpindexer].Field<DateTime>("BarTime"), fCCIvalue);
                    Application.DoEvents();
                    if (bAbort) return false;
                }
                if (bAbort) return false;
            } // end try{}
            catch
            {
                return false;
            }
            // everything went okay
            return true;
        }

		private bool SaveCCIData()
		{

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
			string sPairName = sTableName.Substring(0, 6);

			oMySqlHandler.ExecuteSQLCommand("delete from CCIData where pairname = '" + sPairName + "' and period = " + barperiod.ToString());

            string cmdString = "insert into CCIData (PairName, BarTime, Period, EntryCCI, TrendCCI, GoldBar"
                + ") values ('" + sPairName + "', '{0:G}', " + barperiod.ToString() + ", {1:F4}, {2:F4}, {3})";

			List<string> lstCommandStrings = new List<string>();
            for (int i = 0; i < iDays; i++)
            {
                Application.DoEvents();
                if (bAbort) break;
                if (i % 17 == 0) StatusLabel.Text = "Creating Item " + i.ToString();
                Application.DoEvents();
                BarDate = EntryCCIValues.Keys[i];
                // set up local vars from array data
                 entryvar = EntryCCIValues[BarDate];
                 trendvar = TrendCCIValues[BarDate];
                 if (GoldBarValues.Count >= iDays)
                     goldvar = GoldBarValues[BarDate];
                 else
                     goldvar = 0;
                try
                {
                    cmd2 = String.Format(cmdString, BarDate.ToString("yyyy-MM-dd HH:mm"), entryvar, trendvar, goldvar);
					lstCommandStrings.Add(cmd2);
				}
                catch (Exception s2)
                {
                    Debug.WriteLine(s2);
                    StatusLabel.Text = "*** Data Save ERROR! ***";
                    return false;
                }
            } // end for (i = ...
			StatusLabel.Text = "saving the data to the DB";
			Application.DoEvents();
			oMySqlHandler.ExecuteSQLCommand(lstCommandStrings.ToArray());
            StatusLabel.Text = iDays.ToString() + " rows completed.";
			Application.DoEvents();
            if (bAbort) return false;
            return true;
        }

        private void ResetUsefulDays()
        {
            if (bInitializing) return;

            bInitializing = true;
            ValidateTextBoxes();
            bInitializing = false;
            int idaycount = 0;

            if (Int32.TryParse(txtDays.Text, out idaycount))
            {
                int iUsefuldays = iTableMaxRows - (iTrendCCIperiod > iEntryCCIperiod ? iTrendCCIperiod : iEntryCCIperiod);
                if (iUsefuldays <= 0)
                {
                    StatusLabel.Text = "!! Input Error !!";
                    Application.DoEvents();
                    MessageBox.Show(this, "There are not enough days in the data table for the " +
                        "CCI periods you have chosen. Reduce the periods or get more data.", "CCI Period Error");
                    return;
                }
                lblDaysAvailable.Text = (iTableMaxRows - (iTrendCCIperiod > iEntryCCIperiod ? iTrendCCIperiod : iEntryCCIperiod)).ToString();
                if (idaycount > iUsefuldays)
                {
                    StatusLabel.Text = "** Not enough Data ***";
                    txtDays.Text = iUsefuldays.ToString();
                    Application.DoEvents();
                    MessageBox.Show(this, "There are not enough useful days of data in the data table for the " +
                        "number of Days To Get and CCI periods you have entered. I have truncated " +
                        "the value of Days To Get to the maximum that can be used.", "Size Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(this, "Could not parse the Days To Generate value!!", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }   // end class
} // end namespace
