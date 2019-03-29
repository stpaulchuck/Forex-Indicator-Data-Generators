using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace HeikenAshiGenerator
{
	public partial class HeikenAshiGenMain : Form
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
		// data in table rows is newest date to oldest date!! beware
		DataTable oTable = new DataTable();
		//List<string> PairList = new List<string>(new string[] { "GBPUSD", "AUDUSD", "NZDUSD", "EURUSD", "USDJPY", "USDCHF", "USDCAD" });
		string sTableName = "";
		int iDays = 0, iTableMaxRows = 0;
		// sort order is oldest to newest date time!! beware
		SortedList<DateTime, HADataStruct> HAValues = new SortedList<DateTime, HADataStruct>();
		bool bAbort = false, bInitializing = false;
		StringCollection g_PrimaryCurrencyList = new StringCollection() { "USD", "CHF", "GBP", "NZD", "AUD", "EUR" };
		clsMySqlHandler oMySqlHandler = null;

		/************** Constructor ****************/
		public HeikenAshiGenMain()
		{
			InitializeComponent();
			oMySqlHandler = new clsMySqlHandler(cmbDatabaseNames, cmbTableNames);
		}

		/************** events ****************/
		private void HeikenAshiGenMain_Shown(object sender, EventArgs e)
		{
			bInitializing = true;
			foreach (string s in Properties.Settings.Default.ServerList)
			{
				cmbServerName.Items.Add(s);
			}
			string sTemp = Properties.Settings.Default.LastServer;
			if (cmbServerName.Items.Contains(sTemp))
			{
				cmbServerName.Text = sTemp;
			}
			else
			{
				cmbServerName.Text = cmbServerName.Items[0].ToString();
			}
			oMySqlHandler.MakeSqlConnection(cmbServerName.Text);
			bInitializing = false;
		}

		private void HeikenAshiGenMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.LastDBname = cmbDatabaseNames.Text;
			Properties.Settings.Default.LastServer = cmbServerName.Text;
			Properties.Settings.Default.LastTableName = cmbTableNames.Text;
			Properties.Settings.Default.Save();
		}

		private void cmbTableName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bInitializing) return;

			bool bOriginal = bInitializing;
			bInitializing = true;
			if (!ValidateTextBoxes())
			{
				bInitializing = false;
				return;
			}
			sTableName = cmbTableNames.Text;

			StatusLabel.Text = "Getting table row count for " + sTableName + " ...";
			Application.DoEvents();
			try
			{
				iTableMaxRows = oMySqlHandler.GetCount();
				lblDaysAvailable.Text = iTableMaxRows.ToString();
				txtDays.Text = lblDaysAvailable.Text;
			}
			catch (Exception ec)
			{
				MessageBox.Show(this, "Table read error - " + ec.Message);
				StatusLabel.Text = "*** TABLE READ ERROR ***";
				Debug.WriteLine("***" + ec.Message);
				bInitializing = false;
				return;
			}
			StatusLabel.Text = "Idle ...";
			bInitializing = false;
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

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
			((Button)sender).Focus();
			bAbort = true;
			Application.DoEvents();
			StatusLabel.Text = "Operation Aborted By User!";
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
				oMySqlHandler.ExecuteSQLCommand(CommandText);
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

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			StatusLabel.Text = "Creating data...";
			if (!ValidateTextBoxes()) return;
			if (!ValidateComboBoxes()) return;
			//----------- fetch the input data
			string CommandText = "select * from " + sTableName + " order by bartime desc limit " + iDays.ToString();
			try
			{
				oTable = oMySqlHandler.GetTableData(CommandText);
			}
			catch (Exception e69)
			{
				MessageBox.Show(this, "Input data fetch error: " + e69.Message, "Sql Error");
				StatusLabel.Text = "Error fetching input data!";
				return;
			}
			//---------- do the deed
			if (!CreateData())
			{
				MessageBox.Show(this, "Error Creating Data!");
				StatusLabel.Text = "Error Creating Data!";
				return;
			}
			StatusLabel.Text = "Saving data...";
			if (!SaveData())
			{
				MessageBox.Show(this, "Error Saving Data!");
				StatusLabel.Text = "Error Saving Data!";
				return;
			}
			StatusLabel.Text = iDays.ToString() + " rows of data saved.";
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

			return true;
		}

		private bool ValidateComboBoxes()
		{
			return true;
		}

		/*
1. HA-Close = (Open(0) + High(0) + Low(0) + Close(0)) / 4

2. HA-Open = (HA-Open(-1) + HA-Close(-1)) / 2 

3. HA-High = Maximum of the High(0), HA-Open(0) or HA-Close(0) 

4. HA-Low = Minimum of the Low(0), HA-Open(0) or HA-Close(0) 
         */
		private bool CreateData()
		{
			HAValues.Clear();
			try
			{
				DataRowCollection oRows = oTable.Rows;
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
					if (bAbort)
					{
						StatusLabel.Text = "operation aborted by user!";
						return false;
					}
					if (indexer % 17 == 0)
					{
						StatusLabel.Text = "creating row " + indexer.ToString();
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
				MessageBox.Show(this, ex.Message, "Data Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private bool SaveData()
		{
			DateTime BarDate = DateTime.Now;
			string cmd2 = "", sColor = "";
			HADataStruct hda = new HADataStruct();
			double fOpen = 0, fClose = 0, fHigh = 0, fLow = 0;
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

			oMySqlHandler.ExecuteSQLCommand("delete from HeikenAshiData where pairname = '" + sPairName + "' and period = " + barperiod.ToString());

			string cmdString = "insert into HeikenAshiData (PairName, BarTime, Period, Open, High, Low, Close, Color)"
				+ " values ('" + sPairName + "', '{0:G}', " + barperiod.ToString() + ", {1:F4}, {2:F4}, {3:F4}, {4:F4}, '{5:G}')";

			List<string> lstCommandStrings = new List<string>();
			for (int i = 0; i < iDays; i++)
			{
				Application.DoEvents();
				if (bAbort) break;
				if (i % 17 == 0) StatusLabel.Text = "Creating Item " + i.ToString();
				Application.DoEvents();
				BarDate = HAValues.Keys[i];
				// set up local vars from array data
				hda = HAValues[BarDate];
				fOpen = hda.OpenPrice;
				fClose = hda.ClosePrice;
				fHigh = hda.HighPrice;
				fLow = hda.LowPrice;
				sColor = hda.Color;

				try
				{
					cmd2 = String.Format(cmdString, BarDate.ToString("yyyy-MM-dd HH:mm"), fOpen, fHigh, fLow, fClose, sColor);
					lstCommandStrings.Add(cmd2);
				}
				catch (Exception s2)
				{
					Debug.WriteLine(s2);
					StatusLabel.Text = "*** Data Save ERROR! ***";
					return false;
				}
			} // end for (i = ...
			if (bAbort) return false;
			StatusLabel.Text = "saving data to DB";
			Application.DoEvents();
			if (!oMySqlHandler.ExecuteSQLCommand(lstCommandStrings.ToArray()))
			{
				MessageBox.Show(this, "Error saving data: " + oMySqlHandler.LastError, "SQL error");
				return false;
			}
			StatusLabel.Text = iDays.ToString() + " rows completed.";
			return true;
		}

	} // end class
} // end namespace

#region Heiken Ashi Definition

/*
1. The Heikin-Ashi Close is simply an average of the open, 
high, low and close for the current period. 

HA-Close = (Open(0) + High(0) + Low(0) + Close(0)) / 4

2. The Heikin-Ashi Open is the average of the prior Heikin-Ashi 
candlestick open plus the close of the prior Heikin-Ashi candlestick. 

HA-Open = (HA-Open(-1) + HA-Close(-1)) / 2 

3. The Heikin-Ashi High is the maximum of three data points: 
the current period's high, the current Heikin-Ashi 
candlestick open or the current Heikin-Ashi candlestick close. 

HA-High = Maximum of the High(0), HA-Open(0) or HA-Close(0) 

4. The Heikin-Ashi low is the minimum of three data points: 
the current period's low, the current Heikin-Ashi 
candlestick open or the current Heikin-Ashi candlestick close.

HA-Low = Minimum of the Low(0), HA-Open(0) or HA-Close(0) 
      
 */

#endregion
