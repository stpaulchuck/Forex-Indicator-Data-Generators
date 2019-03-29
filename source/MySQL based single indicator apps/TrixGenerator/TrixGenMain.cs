using MySql.Data.MySqlClient;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace TrixGenerator
{
	// todo: refactor TrixGenerator for MySQL db instead of MS SqlServer

	public partial class TrixGenMain : Form
	{
		/*********** global vars **********/
		int iDays = 0, iSlowTrixPeriod = 0, iFastTrixPeriod = 0;
		public DataTable oTable = new DataTable();
		string sTableName = "";
		double[] FastTrixValues, SlowTrixValues;
		string[] FastColors, SlowColors, Arrows;
		bool bAbort = false, bInitializing = false;
		string sUID = Properties.Settings.Default.UID, sPWD = Properties.Settings.Default.PWD;
		StringCollection g_PrimaryCurrencyList = new StringCollection() { "USD", "CHF", "GBP", "NZD", "AUD", "EUR" };
		clsMySqlHandler oSqlHandler = null;


		/************** methods ***********/
		public TrixGenMain()
		{
			InitializeComponent();
			oSqlHandler = new clsMySqlHandler(cmbDatabaseNames, cmbTableNames);
		}

		private void TrixGenMain_Shown(object sender, EventArgs e)
		{
			bInitializing = true;
			foreach (string s in Properties.Settings.Default.ServerList)
			{
				cmbServerName.Items.Add(s);
			}
			string sTemp = Properties.Settings.Default.LastServerName;
			if (sTemp != "")
			{
				cmbServerName.Text = sTemp;
			}
			else
			{
				cmbServerName.Text = cmbServerName.Items[0].ToString();
			}
			txtSlowTrix.Text = Properties.Settings.Default.SlowTrixSpan.ToString();
			txtFastTrix.Text = Properties.Settings.Default.FastTrixSpan.ToString();
			sTemp = Properties.Settings.Default.LastServerName;
			if (cmbServerName.Items.Contains(sTemp))
				cmbServerName.Text = sTemp;
			else
				cmbServerName.Text = cmbServerName.Items[0].ToString();
			oSqlHandler.MakeSqlConnection(cmbServerName.Text);

			bInitializing = false;
		}

		private void TrixGenMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			int iTemp = 0;
			int.TryParse(txtFastTrix.Text, out iTemp);
			Properties.Settings.Default.FastTrixSpan = iTemp;
			int.TryParse(txtSlowTrix.Text, out iTemp);
			Properties.Settings.Default.SlowTrixSpan = iTemp;
			Properties.Settings.Default.LastDBname = cmbDatabaseNames.Text;
			Properties.Settings.Default.LastServerName = cmbServerName.Text;
			Properties.Settings.Default.LastTableName = cmbTableNames.Text;
			Properties.Settings.Default.Save();
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			StatusLabel.Text = "Validating...";
			btnAbort.Focus();
			bAbort = false;
			if (!ValidateTextBoxes())
			{
				StatusLabel.Text = "Input Data Error!";
				return;
			}
			if (!ValidateComboBoxes())
			{
				StatusLabel.Text = "Input Data Error!";
				return;
			}

			txtDays.Enabled = false;
			txtFastTrix.Enabled = false;
			txtSlowTrix.Enabled = false;
			cmbDatabaseNames.Enabled = false;
			cmbServerName.Enabled = false;
			cmbTableNames.Enabled = false;
			lblDaysAvailable.BackColor = Control.DefaultBackColor;

			int iLen = iDays + (3 * (iSlowTrixPeriod > iFastTrixPeriod ? iSlowTrixPeriod : iFastTrixPeriod)) - 1;
			// fetch the price data
			try
			{
				StatusLabel.Text = "Fetching price data...";
				Application.DoEvents();
				oTable.Clear();

				int iActual = oSqlHandler.GetCount();
				if (iActual < iLen)
				{
					if (MessageBox.Show(this, "The number of rows in the table (" + iActual.ToString() + ") is less than your request ("
						+ iDays.ToString() + ")\nDo you wish to proceed with just what is available?", "Data Length Mismatch",
						MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Cancel)
					{
						StatusLabel.Text = "Generation Aborted by user.";
						return;
					}
					StatusLabel.Text = "Number of days adjusted to available days.";
					// adjust the lengths accordingly
					iLen = iActual;
					iDays = iActual - (3 * (iSlowTrixPeriod > iFastTrixPeriod ? iSlowTrixPeriod : iFastTrixPeriod)) - 1;
					txtDays.Text = iDays.ToString();

				}
				oTable.Clear();
				ClearDataArrays();
				// now do the fetch
				oTable = oSqlHandler.GetTableData("select * from " + sTableName + " order by BarTime desc limit " + iLen.ToString());

				StatusLabel.Text = "Generating Trix...";
				Application.DoEvents();

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
				// first clear the data for this currency pair
				oSqlHandler.ExecuteSQLCommand("Delete from TrixData where Pairname = '" + sPairName + "' and barperiod = " + barperiod.ToString());
				Application.DoEvents();
				if (bAbort) return;
				StatusLabel.Text = "Generating Fast Trix...";
				Application.DoEvents();
				GenerateTrix("Fast");
				Application.DoEvents();
				if (bAbort) return;
				StatusLabel.Text = "Generating Slow Trix...";
				Application.DoEvents();
				GenerateTrix("Slow");
				Application.DoEvents();
				if (bAbort) return;
				StatusLabel.Text = "Generating Divergence Arrows...";
				Application.DoEvents();
				CreateArrows();
				Application.DoEvents();
				if (bAbort) return;
				StatusLabel.Text = "Saving Data to SQL tables ...";
				Application.DoEvents();
				SaveTrix();
				Application.DoEvents();
				if (bAbort) return;
				StatusLabel.Text = "Generation completed.";
				Application.DoEvents();
			}
			catch (Exception e1)
			{
				Debug.WriteLine("### read error: " + e1.Message);
				StatusLabel.Text = "Error generating Trix";
				Application.DoEvents();

			}
			finally
			{
				txtDays.Enabled = true;
				txtFastTrix.Enabled = true;
				txtSlowTrix.Enabled = true;
				cmbDatabaseNames.Enabled = true;
				cmbServerName.Enabled = true;
				cmbTableNames.Enabled = true;
				lblDaysAvailable.BackColor = Color.White;
			}
			//  generate Trix
		}

		private bool ValidateTextBoxes()
		{
			try
			{
				iDays = int.Parse(txtDays.Text);
			}
			catch
			{
				iDays = 0;
				MessageBox.Show(this, "The number of days value is not a proper integer. Try again.", "Input Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			try
			{
				iFastTrixPeriod = int.Parse(txtFastTrix.Text);
			}
			catch
			{
				iFastTrixPeriod = 0;
				MessageBox.Show(this, "The Fast Trix Span value is not a proper integer. Try again.", "Input Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			try
			{
				iSlowTrixPeriod = int.Parse(txtSlowTrix.Text);
			}
			catch
			{
				iSlowTrixPeriod = 0;
				MessageBox.Show(this, "The Slow Trix Span value is not a proper integer. Try again.", "Input Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private bool ValidateComboBoxes()
		{
			return true;
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
			double SFlarge = 1.0 - SFsmall;
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
			int Terminator = oTable.Rows.Count;
			double[] TrixValues = new double[Terminator];
			string[] TrixColors = new string[iDays + 1]; // choices red, green
			Terminator--;
			// create the trix values
			DataRowCollection oRows = oTable.Rows;
			for (int loopindex = Terminator; loopindex >= 0; loopindex--)
			{
				Application.DoEvents();
				if (bAbort) break;
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
			if (bAbort) return;
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

		private void CreateArrows()
		{
			Arrows = new string[iDays + 1];
			DataRowCollection oRows = oTable.Rows;
			double RedTrixPrev = -99.0, GreenTrixPrev = 99.0; // nonsense value to preload the vars
			double PrevLow = -99.0, PrevHigh = 99.0; // nonsense value to preload vars
			int i1 = 0, i2 = 0, indextemp = 0;

			// create trix divergence arrow info
			for (int loopindex2 = iDays; loopindex2 > 0; loopindex2--)
			{
				Application.DoEvents();
				if (bAbort) break;
				Arrows[loopindex2] = "";
				if (FastColors[loopindex2] == "both")
				{
					// see if there is a trix-price divergence
					if (FastColors[loopindex2 - 1] == "red")
					{
						if (!IsIndicatorPeak(loopindex2, FastTrixValues)) continue;
						if (rbUseMyLast.Checked)
						{
							i1 = Math.Sign(FastTrixValues[loopindex2] - RedTrixPrev);
							i2 = Math.Sign(oRows[loopindex2].Field<double>("High") - PrevHigh);
						}
						else // use THV4 method
						{
							indextemp = GetIndicatorLastPeak(loopindex2);
							if (indextemp == -1) continue; // can't go past the end of the array
							i1 = Math.Sign(FastTrixValues[loopindex2] - FastTrixValues[indextemp]);
							i2 = Math.Sign(oRows[loopindex2].Field<double>("High") - oRows[indextemp].Field<double>("High"));
						}
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
						if (rbUseMyLast.Checked)
						{
							i1 = Math.Sign(FastTrixValues[loopindex2] - GreenTrixPrev);
							i2 = Math.Sign(oRows[loopindex2].Field<double>("Low") - PrevLow);
						}
						else
						{
							indextemp = GetIndicatorLastTrough(loopindex2);
							if (indextemp == -1) continue; // can't go past end of array
							i1 = Math.Sign(FastTrixValues[loopindex2] - FastTrixValues[indextemp]);
							i2 = Math.Sign(oRows[loopindex2].Field<double>("Low") - oRows[indextemp].Field<double>("Low"));
						}
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

		private bool SaveTrix()
		{
			int iBarPeriod = 0;
			string sPairName = "";

			DateTime BarDate = DateTime.Now;
			string cmd2 = "", sFastColor = "none", sSlowColor = "none";
			double fastvar = 0.0, slowvar = 0.0;

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
			if (sTableName.Length >= 6) // contains the periodicity in name
			{
				sPairName = sTableName.Substring(0, 6);
			}
			else
			{
				MessageBox.Show(this, "Table: " + sTableName + " not usable due to table name.", "table name format");
				return false;
			}

			string CommandText = "delete from TrixData where pairname='" + sPairName + "' and barperiod=" + iBarPeriod;
			oSqlHandler.ExecuteSQLCommand(CommandText);

			string cmdString = "insert into TrixData (PairName, Period, BarTime, FastTrix, SlowTrix, FastColor, SlowColor,"
				+ "RedArrow, GreenArrow) values ('" + sPairName + "', " + barperiod.ToString() + ",'{0:G}', {1:N7}, {2:N7}, '{3}', '{4}', {5}, {6})";
			int RedArrow = 0, GreenArrow = 0;
			List<string> lstCommandStrings = new List<string>();
			for (int i = 0; i <= iDays; i++)
			{
				Application.DoEvents();
				if (bAbort) break;
				if (i % 17 == 0) StatusLabel.Text = "Saving Item " + i.ToString();
				Application.DoEvents();
				BarDate = oTable.Rows[i].Field<DateTime>("BarTime");
				//string sDT = oTable.Rows[i].Field<DateTime>("BarTime").ToString("yyyy-MM-dd HH:mm");
				// set up local vars from array data
				fastvar = FastTrixValues[i];
				slowvar = SlowTrixValues[i];
				sFastColor = FastColors[i];
				sSlowColor = SlowColors[i];
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
					cmd2 = String.Format(cmdString, BarDate.ToString("yyyy-MM-dd HH:mm"), fastvar, slowvar, sFastColor, sSlowColor, RedArrow, GreenArrow);
					lstCommandStrings.Add(cmd2);
				}
				catch (Exception s2)
				{
					Debug.WriteLine("Error saving data: " + s2.Message + "   stacktrace = " + s2.StackTrace);
				}
			} // end for (i = ...
			if (!oSqlHandler.ExecuteSQLCommand(lstCommandStrings.ToArray()))
			{
				StatusLabel.Text = "Error Saving data!!";
				MessageBox.Show(this, "Error saving data: " + oSqlHandler.LastError);
				return false;
			}
			StatusLabel.Text = "completed trix generation";
			return true;
		}

		private void ClearDataArrays()
		{
			FastColors = null;
			SlowColors = null;
			FastTrixValues = null;
			SlowTrixValues = null;
			Arrows = null;
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

		private void cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
		{
			lblDaysAvailable.Text = "{not set}";
			StatusLabel.Text = "Connecting to server " + cmbServerName.Text + " ...";
			Application.DoEvents();
			GetDatabases();
			Application.DoEvents();
		}

		private void GetDatabases()
		{
			if (cmbServerName.Items.Count < 1) return;
			this.Cursor = Cursors.WaitCursor;
			StatusLabel.Text = "Getting DB list from Server " + cmbServerName.Text;
			Application.DoEvents();

			try
			{
				oSqlHandler.GetDatabases();
			}
			catch (Exception e1)
			{
				MessageBox.Show(this, "Unable to connect to server: " + cmbServerName.Text
					+ "\nError message is: " + e1.Message, "Server ERROR");
				this.Cursor = Cursors.Default;
				StatusLabel.Text = "Error connecting to server!";
				Application.DoEvents();
				return;
			}
			Application.DoEvents();
			this.Cursor = Cursors.Default;
			StatusLabel.Text = "databases load completed";
			return;
		}

		private void cmbDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bInitializing) return;

			// get table list
			this.Cursor = Cursors.WaitCursor;
			cmbTableNames.Items.Clear();
			cmbTableNames.Text = "";
			StatusLabel.Text = "Getting Table list from DB " + cmbDatabaseNames.Text;
			try
			{
				oSqlHandler.GetTableNames();
			}
			catch (Exception gt)
			{
				MessageBox.Show(this, "ERROR fetching tables!\n" + gt.Message, "ERROR");
				StatusLabel.Text = "Error fetching tables!";
				Application.DoEvents();
				this.Cursor = Cursors.Default;
				return;
			}
			if (cmbTableNames.Items.Count > 0)
			{
				if (cmbTableNames.Items.Contains(Properties.Settings.Default.LastTableName))
					cmbTableNames.Text = Properties.Settings.Default.LastTableName;
				else
					cmbTableNames.Text = cmbTableNames.Items[0].ToString();
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

			StatusLabel.Text = "Getting table row count for " + sTableName + " ...";
			Application.DoEvents();
			try
			{
				int iTableMaxRows = oSqlHandler.GetCount();
				lblDaysAvailable.Text = (iTableMaxRows - (3 * (iSlowTrixPeriod > iFastTrixPeriod ? iSlowTrixPeriod : iFastTrixPeriod)) - 1).ToString();
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

	} // end class
} // end namespace
