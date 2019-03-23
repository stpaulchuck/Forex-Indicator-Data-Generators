using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;


namespace Forex_test_data_generator
{
	public partial class frmGeneratorMain : Form
	{
		/********************** global vars **************************/
		bool bParameterChanged = false, bInitializing = false;
		ClsDataFileReader oDataReader = new ClsDataFileReader();
		ClsCCI oCCIgen = new ClsCCI();
		ClsOHLCgenerator oOHLCgen = new ClsOHLCgenerator();
		ClsMACD oMACDgen = new ClsMACD();
		ClsStochastics oStochGen = new ClsStochastics();
		ClsPriceExtras oOthersGen = new ClsPriceExtras();
		ClsVolatility oVolitility = new ClsVolatility();
		//ClsTargetData oTargetData = new ClsTargetData();
		int iDays = 0, iCCItrend = 0, iCCIentry = 0, iMACDfast = 0, iMACDslow = 0, iMACDsignal = 0;
		int iStochKperiod = 0, iStochDperiod = 0, iStochSlowing = 0, iATRperiod = 0;
		int iMA1period = 0, iMA2period = 0, iMA3period = 0, iRoCperiod = 0, iVolitilityPeriod = 0;
		DataVector[] dvPriceHistory = { };
		string sCurrencyPairFileName = "";


		/**************************** constructor ************************/
		public frmGeneratorMain()
		{
			InitializeComponent();
		}

		/************************ event handlers ************************/

		private void frmTestDataMain_Load(object sender, EventArgs e)
		{
			bInitializing = true;
			FetchParams();
			bInitializing = false;
			ShowDays();
			StatusLabel.Text = "** SELECT AND LOAD DATA **";
		}

		private void Parameters_TextChanged(object sender, EventArgs e)
		{
			if (bInitializing) return;
			if (tabControl1.SelectedIndex == 0)
			{
				if (!SaveParams()) return;
				ShowDays();
				bParameterChanged = false;
				return;
			}
			bParameterChanged = true;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Indicator_CheckedChanged(object sender, EventArgs e)
		{
			if (bInitializing) return;
			if (!SaveParams()) return;
			ShowDays();
		}

		private void btnLoadFile_Click(object sender, EventArgs e)
		{
			StatusLabel.Text = "Fetching data file and loading price data.";
			// call the reader/writer
			if (!oDataReader.GetDataFromFile(txtFolderLocation.Text, false, chkFixOutliers.Checked))
			{
				MessageBox.Show(this, "File fetch threw an error: " + oDataReader.LastError, "Read Error", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				StatusLabel.Text = "Data Fetch Aborted";
				return;
			}
			sCurrencyPairFileName = oDataReader.CurrencyPairFileName;
			StatusLabel.Text = "Data fetch completed successfully.";
			ShowDays();
		}

		private void chkAllIndicators_CheckedChanged(object sender, EventArgs e)
		{
			foreach (Control cb in groupBox1.Controls)
			{
				if (cb.Tag != null)
				{
					if (cb.Tag.ToString() == "Indicator")
					{ ((CheckBox)cb).Checked = chkAllIndicators.Checked; }
				}
			}
		}

		private void chkAllOHLC_CheckedChanged(object sender, EventArgs e)
		{
			bool bDoCheck = false;
			if (chkAllOHLC.Checked)
			{
				bDoCheck = true;
			}
			chkOpen.Checked = bDoCheck;
			chkHigh.Checked = bDoCheck;
			chkLow.Checked = bDoCheck;
			chkClose.Checked = bDoCheck;
		}

		private void chkScaleData_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.ScaleTheData = chkScaleData.Checked;
			Properties.Settings.Default.Save();
		}

		private void chkIncludeHeader_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.IncludeHeader = chkIncludeHeader.Checked;
			Properties.Settings.Default.Save();
		}

		private void chkIncludeDates_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.IncludeDates = chkIncludeDates.Checked;
			Properties.Settings.Default.Save();
		}

		private void chkIncludeTimes_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.InclueTimes = chkIncludeTimes.Checked;
			Properties.Settings.Default.Save();
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			if (oDataReader.NumberOfDaysAvailable == 0)
			{
				//SoundPlayer sp = new SoundPlayer(Application.StartupPath + "\\_all_wrong.wav");
				SoundPlayer sp = new SoundPlayer(Properties.Resources._all_wrong);
				sp.Play();
				MessageBox.Show(this, "Hey stupid! There's no price data loaded!!", "Oooops!!");
				return;
			}


			StatusLabel.Text = "Generating Output Data";
			// validate input values
			if (!ValidateParams())
			{ return; }
			ShowDays();

			if (!GenerateTheData())
			{ return; }

			StatusLabel.Text = "saving data to file...";
			// save the result to the save folder

			if (WriteTheOutputFile())
			{
				StatusLabel.Text = "file saved, generation complete";
			}

		}

		private void chkDeltas_CheckedChanged(object sender, EventArgs e)
		{
			if (chkDeltas.Checked)
			{
				iDays--;
				if (iDays < 0) iDays = 0;
			}
			else
			{
				iDays++;
			}
			txtDays.Text = iDays.ToString("0");
		}

		private void btnFindFolder_Click(object sender, EventArgs e)
		{
			OpenFileDialog oDlg = new OpenFileDialog
			{
				Title = "Locate Input Folder",
				DefaultExt = ".csv",
				Filter = "CSV files (.csv)|*.csv"
			};
			if (!Directory.Exists(txtFolderLocation.Text))
			{
				oDlg.InitialDirectory = ".";
			}
			else
			{
				oDlg.InitialDirectory = txtFolderLocation.Text;
			}
			oDlg.Multiselect = false;
			if (oDlg.ShowDialog() == DialogResult.OK)
			{
				string sTemp = oDlg.FileName;
				if (sTemp.Contains("\\"))
				{
					int iEnd = sTemp.LastIndexOf('\\');
					sTemp = sTemp.Substring(0, iEnd);
				}
				txtFolderLocation.Text = sTemp;
				Properties.Settings.Default.InputFolderPath = sTemp;
				Properties.Settings.Default.Save();
			}
		}

		private void btnFindOutput_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog oDlg = new FolderBrowserDialog
			{
				Description = "Select Output Folder",
				SelectedPath = txtOutputFolder.Text
			};
			if (oDlg.ShowDialog(this) == DialogResult.OK)
			{
				txtOutputFolder.Text = oDlg.SelectedPath;
				Properties.Settings.Default.OutputFolderPath = oDlg.SelectedPath;
				Properties.Settings.Default.Save();
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bParameterChanged && tabControl1.SelectedIndex == 0)
			{
				SaveParams();
			}  //  end if page 0 and parameter changed
		}  // end selected index changed

		private void chkFixOutliers_CheckedChanged(object sender, EventArgs e)
		{
			if (sCurrencyPairFileName == "") return;
			if (chkFixOutliers.Checked)
			{
				oDataReader.FixDataFliers();
			}
			else
			{
				// reload the virgin file, indicating this is a reload
				oDataReader.GetDataFromFile(txtFolderLocation.Text, true);
			}
		}

		/********************** private methods **************************/
		bool ValidateParams()
		{
			bool bError = false;
			List<string> sBadEntryName = new List<string>();

			if (!int.TryParse(txtCCIEntryPeriod.Text, out iCCIentry))
			{ bError = true; sBadEntryName.Add("CCI Entry Period"); }
			if (!int.TryParse(txtCCITrendPeriod.Text, out iCCItrend))
			{ bError = true; sBadEntryName.Add("CCI Trend Period"); }
			if (!int.TryParse(txtMACDfastPeriod.Text, out iMACDfast))
			{ bError = true; sBadEntryName.Add("MACD Fast Period"); }
			if (!int.TryParse(txtMACDSignalPeriod.Text, out iMACDsignal))
			{ bError = true; sBadEntryName.Add("MACD Signal Period"); }
			if (!int.TryParse(txtMACDslowPeriod.Text, out iMACDslow))
			{ bError = true; sBadEntryName.Add("MACD SLow Period"); }
			if (!int.TryParse(txtStocasticDperiod.Text, out iStochDperiod))
			{ bError = true; sBadEntryName.Add("Stochastic D Period"); }
			if (!int.TryParse(txtStochasticKperiod.Text, out iStochKperiod))
			{ bError = true; sBadEntryName.Add("Stochastic K Period"); }
			if (!int.TryParse(txtStochasticSlowing.Text, out iStochSlowing))
			{ bError = true; sBadEntryName.Add("Stochastic SLowing Period"); }
			if (!int.TryParse(txtATRperiod.Text, out iATRperiod))
			{ bError = true; sBadEntryName.Add("ATR Period"); }
			if (!int.TryParse(txtVolatilityPeriod.Text, out iVolitilityPeriod))
			{ bError = true; sBadEntryName.Add("Volitility Period"); }
			if (!int.TryParse(txtMA1period.Text, out iMA1period))
			{ bError = true; sBadEntryName.Add("MA1 period"); }
			if (!int.TryParse(txtMA1period.Text, out iMA2period))
			{ bError = true; sBadEntryName.Add("MA2 period"); }
			if (!int.TryParse(txtMA1period.Text, out iMA3period))
			{ bError = true; sBadEntryName.Add("MA3 period"); }
			if (!int.TryParse(txtRoCperiod.Text, out iRoCperiod))
			{ bError = true; sBadEntryName.Add("RoC period"); }
			if (oDataReader.NumberOfDaysAvailable != 0)
			{
				if (!int.TryParse(txtDays.Text, out iDays))
				{ bError = true; sBadEntryName.Add("Days to generate."); }
			}
			if (bError)
			{
				string sOut = "";
				foreach (string s in sBadEntryName)
				{
					sOut += ", " + s;
				}
				sOut = sOut.Remove(0, 2);
				MessageBox.Show(this, "Bad Parameter(s): " + sOut + "; Fix them and try again.");
				tabControl1.SelectTab(1);
				return false;
			}
			bParameterChanged = false;
			return true;
		}

		bool SaveParams()
		{
			if (!ValidateParams())
			{ return false; }
			Properties.Settings.Default.CCIentry = iCCIentry;
			Properties.Settings.Default.CCItrend = iCCItrend;
			Properties.Settings.Default.MACDfast = iMACDfast;
			Properties.Settings.Default.MACDsignal = iMACDsignal;
			Properties.Settings.Default.MACDslow = iMACDslow;
			Properties.Settings.Default.STOCH_d = iStochDperiod;
			Properties.Settings.Default.STOCH_k = iStochKperiod;
			Properties.Settings.Default.STOCH_slowing = iStochSlowing;
			Properties.Settings.Default.ATRperiod = iATRperiod;
			Properties.Settings.Default.ScaleTheData = chkScaleData.Checked;
			Properties.Settings.Default.MA1period = iMA1period;
			Properties.Settings.Default.MA2period = iMA2period;
			Properties.Settings.Default.MA3period = iMA3period;
			Properties.Settings.Default.VolitilityPeriod = iVolitilityPeriod;
			Properties.Settings.Default.GenVolitility = chkVolatility.Checked;
			string smaString = "", emaString = "";
			if (chkMA1.Checked)
			{ if (rbMA1sma.Checked) smaString = "1"; else emaString = "1"; }
			if (chkMA2.Checked)
			{
				if (rbMA2sma.Checked) { if (smaString != "") { smaString += ","; } smaString += "2"; }
				else { if (emaString != "") { emaString += ","; } emaString += "2"; }
			}
			if (chkMA3.Checked)
			{
				if (rbMA3sma.Checked) { if (smaString != "") { smaString += ","; } smaString += "3"; }
				else { if (emaString != "") { emaString += ","; } emaString += "3"; }
			}
			Properties.Settings.Default.EMAsToGen = emaString;
			Properties.Settings.Default.SMAsToGen = smaString;
			Properties.Settings.Default.RoCperiod = iRoCperiod;
			Properties.Settings.Default.RoCchecked = chkRofChng.Checked;
			Properties.Settings.Default.AddClasifData = chkAddUpDown.Checked;
			Properties.Settings.Default.Save();
			return true;
		}

		void FetchParams()
		{
			txtOutputFolder.Text = Properties.Settings.Default.OutputFolderPath;
			chkAddUpDown.Checked = Properties.Settings.Default.AddClasifData;
			iCCIentry = Properties.Settings.Default.CCIentry;
			txtCCIEntryPeriod.Text = iCCIentry.ToString();
			iCCItrend = Properties.Settings.Default.CCItrend;
			txtCCITrendPeriod.Text = iCCItrend.ToString();
			iMACDfast = Properties.Settings.Default.MACDfast;
			txtMACDfastPeriod.Text = iMACDfast.ToString();
			iMACDsignal = Properties.Settings.Default.MACDsignal;
			txtMACDSignalPeriod.Text = iMACDsignal.ToString();
			iMACDslow = Properties.Settings.Default.MACDslow;
			txtMACDslowPeriod.Text = iMACDslow.ToString();
			iStochDperiod = Properties.Settings.Default.STOCH_d;
			txtStocasticDperiod.Text = iStochDperiod.ToString();
			iStochKperiod = Properties.Settings.Default.STOCH_k;
			txtStochasticKperiod.Text = iStochKperiod.ToString();
			iStochSlowing = Properties.Settings.Default.STOCH_slowing;
			txtStochasticSlowing.Text = iStochSlowing.ToString();
			iATRperiod = Properties.Settings.Default.ATRperiod;
			txtATRperiod.Text = iATRperiod.ToString("0");
			chkScaleData.Checked = Properties.Settings.Default.ScaleTheData;
			chkDeltas.Checked = Properties.Settings.Default.OHLCasDeltas;
			chkIncludeHeader.Checked = Properties.Settings.Default.IncludeHeader;
			chkIncludeTimes.Checked = Properties.Settings.Default.InclueTimes;
			chkIncludeDates.Checked = Properties.Settings.Default.IncludeDates;
			iRoCperiod = Properties.Settings.Default.RoCperiod;
			txtRoCperiod.Text = iRoCperiod.ToString();
			chkRofChng.Checked = Properties.Settings.Default.RoCchecked;
			iMA1period = Properties.Settings.Default.MA1period;
			txtMA1period.Text = iMA1period.ToString();
			iMA2period = Properties.Settings.Default.MA2period;
			txtMA2period.Text = iMA2period.ToString();
			iMA3period = Properties.Settings.Default.MA3period;
			txtMA3period.Text = iMA3period.ToString();
			string smaText = Properties.Settings.Default.SMAsToGen;
			string emaText = Properties.Settings.Default.EMAsToGen;
			chkVolatility.Checked = Properties.Settings.Default.GenVolitility;
			txtVolatilityPeriod.Text = Properties.Settings.Default.VolitilityPeriod.ToString();
			iVolitilityPeriod = int.Parse(txtVolatilityPeriod.Text);
			chkMA1.Checked = false;
			chkMA2.Checked = false;
			chkMA3.Checked = false;
			if (smaText.Contains("1")) { chkMA1.Checked = true; rbMA1sma.Checked = true; }
			if (smaText.Contains("2")) { chkMA2.Checked = true; rbMA2sma.Checked = true; }
			if (smaText.Contains("3")) { chkMA3.Checked = true; rbMA3sma.Checked = true; }
			if (emaText.Contains("1")) { chkMA1.Checked = true; rbMA1ema.Checked = true; }
			if (emaText.Contains("2")) { chkMA2.Checked = true; rbMA2ema.Checked = true; }
			if (emaText.Contains("3")) { chkMA3.Checked = true; rbMA3ema.Checked = true; }

		}

		private void ShowDays()
		{
			int p = 0, q = 0, r = 0, s = 0, m = 0;

			if (chkCCI.Checked) p = Math.Max(iCCIentry, iCCItrend);
			if (chkMACD.Checked) q = Math.Max(iMACDfast + iMACDsignal, Math.Max(iMACDslow + iMACDsignal, iMACDsignal));
			if (chkStoch.Checked) r = iStochSlowing + iStochKperiod + iStochDperiod;
			if (chkATR.Checked) s = iATRperiod;
			if (chkMA1.Checked) m = iMA1period;
			if (chkMA2.Checked && (iMA2period > m)) m = iMA2period;
			if (chkMA3.Checked && (iMA3period > m)) m = iMA3period;
			if (chkRofChng.Checked && (iRoCperiod > m)) m = iRoCperiod;
			if (chkVolatility.Checked && (iVolitilityPeriod > m)) m = iVolitilityPeriod;

			p = Math.Max(s, Math.Max(p, Math.Max(q, r)));
			if (m > p) p = m;

			if (chkDeltas.Checked) p++; // have to chop off a day when creating deltas

			p = oDataReader.NumberOfDaysAvailable - p;
			if (p < 0)
			{ p = 0; }
			/*
			if (iDays > p || iDays == 0)
			{
				iDays = p;
			}
			else
			{
				p = iDays;
			}
			*/
			txtDays.Text = p.ToString();

		}

		private string[] GetDates()
		{
			DataVector[] dvLocal = oDataReader.PriceTensor;
			List<string> lstDates = new List<string>();
			int iStop = oDataReader.NumberOfDaysAvailable;
			int iStart = iStop - iDays;

			for (int iIndexer = iStart; iIndexer < iStop; iIndexer++)
			{
				lstDates.Add(dvLocal[iIndexer].TradeDate + ", " + dvLocal[iIndexer].TradeTime);
			}

			return lstDates.ToArray();
		}

		private bool WriteTheOutputFile()
		{
			ClsOutputWriter oWriter = new ClsOutputWriter();

			// now for the indicator data
			Dictionary<string, double[][]> DataDic = new Dictionary<string, double[][]>();
			Dictionary<string, string[]> LabelsDic = new Dictionary<string, string[]>();
			//---- get dates array first
			if (chkATR.Checked)
			{
				DataDic.Add("ATR", oOthersGen.GetATRdata());
				LabelsDic.Add("ATR", oOthersGen.ColumnLabels);
			}
			if (chkCCI.Checked)
			{
				LabelsDic.Add("CCI", oCCIgen.ColumnLabels);
				DataDic.Add("CCI", oCCIgen.GetCCI());
			}
			if (chkMACD.Checked)
			{
				LabelsDic.Add("MACD", oMACDgen.ColumnLabels);
				DataDic.Add("MACD", oMACDgen.GetMACD());
			}
			if (chkStoch.Checked)
			{
				LabelsDic.Add("Stoch", oStochGen.ColumnLabels);
				DataDic.Add("Stoch", oStochGen.GetStoch());
			}
			if (chkOpen.Checked || chkHigh.Checked || chkLow.Checked || chkClose.Checked)
			{
				LabelsDic.Add("OHLC", oOHLCgen.ColumnLabels);
				DataDic.Add("OHLC", oOHLCgen.GetOHLC());
			}
			List<string> sDicString = new List<string>();
			if (chkMA1.Checked)
			{
				sDicString.Add("MA1");
			}
			if (chkMA2.Checked)
			{
				sDicString.Add("MA2");
			}
			if (chkMA3.Checked)
			{
				sDicString.Add("MA3");
			}
			if (sDicString.Count > 0)
			{
				LabelsDic.Add("MAs", sDicString.ToArray());
				DataDic.Add("MAs", oMACDgen.GetMAs());
			}
			if (chkRofChng.Checked)
			{
				LabelsDic.Add("RoC", new string[] { "RoC" });
				DataDic.Add("RoC", oOthersGen.GetRoCdata());
			}
			if (chkVolatility.Checked)
			{
				LabelsDic.Add("Volitility", new string[] { "Volitility" });
				DataDic.Add("Volitility", oVolitility.GetVolatilityData());
			}
			if (chkAddUpDown.Checked)
			{
				LabelsDic.Add("ClasifData#NS", new string[] { "up", "down" });
				DataDic.Add("ClasifData#NS", oOHLCgen.GetClassificationIntValues());
			}

			if (rbTypeStd.Checked)
				oWriter.DataScalingOption = ScalingTypes.std;
			if (rbTypeNorm.Checked)
				oWriter.DataScalingOption = ScalingTypes.norm;
			oWriter.DataDictionary = DataDic;
			oWriter.DataLabelsDictionary = LabelsDic;
			oWriter.CurrencyPairName = sCurrencyPairFileName;
			oWriter.OutputFileFolder = txtOutputFolder.Text;
			oWriter.DatesArray = GetDates();

			if (!oWriter.WriteTheFile(chkScaleData.Checked, chkDeltas.Checked, chkIncludeHeader.Checked, chkIncludeDates.Checked, chkIncludeTimes.Checked))
			{
				StatusLabel.Text = "Writing output file error. Generation failed.";
				MessageBox.Show(this, "Writing output file error: " + oWriter.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private bool GenerateTheData()
		{
			dvPriceHistory = oDataReader.PriceTensor;
			// call the sub class objects and generate the indicator values
			if (chkCCI.Checked)
			{
				StatusLabel.Text = "beginning CCI gen";
				oCCIgen.DaysToGenerate = iDays;
				oCCIgen.EntryPeriod = iCCIentry;
				oCCIgen.TrendPeriod = iCCItrend;
				if (!oCCIgen.CreateCCI(dvPriceHistory))
				{
					StatusLabel.Text = "CCI generator error. Generator stopped.";
					MessageBox.Show(this, "CCI generator error: " + oCCIgen.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				StatusLabel.Text = "CCI completed.";
			}
			if (chkMACD.Checked)
			{
				StatusLabel.Text = "begining MACD gen";
				oMACDgen.DaysToGenerate = iDays;
				oMACDgen.FastPeriod = iMACDfast;
				oMACDgen.SlowPeriod = iMACDslow;
				oMACDgen.SignalPeriod = iMACDsignal;

				if (!oMACDgen.CreateMACD(oDataReader.ClosePrices))
				{
					StatusLabel.Text = "MACD generator error. Generation stopped.";
					MessageBox.Show(this, "MACD generator error: " + oMACDgen.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				StatusLabel.Text = "MACD gen completed";
			}
			if (chkStoch.Checked)
			{
				StatusLabel.Text = "generating Stochastics";
				oStochGen.DaysToGenerate = iDays;
				oStochGen.Dperiod = iStochDperiod;
				oStochGen.Kperiod = iStochKperiod;
				oStochGen.Slowing = iStochSlowing;

				if (!oStochGen.CreateStochastics(dvPriceHistory))
				{
					StatusLabel.Text = "Stoch generator error. Generation stopped.";
					MessageBox.Show(this, "Stochastics generator error: " + oStochGen.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				StatusLabel.Text = "completed Stochastics";
			}
			if (chkATR.Checked)
			{
				StatusLabel.Text = "generating ATR and others";
				oOthersGen.DaysToGenerate = iDays;
				oOthersGen.ATRperiod = iATRperiod;

				if (!oOthersGen.GenerateData(dvPriceHistory))
				{
					StatusLabel.Text = "ATR/others generator error. Generation stopped.";
					MessageBox.Show(this, "ATR/others generator error: " + oOthersGen.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				StatusLabel.Text = "completed ATR/others";
			}

			if (chkMA1.Checked || chkMA2.Checked || chkMA3.Checked)
			{
				int[] iPeriods = { 0, 0, 0 };
				string[] sTypes = { "null", "null", "null" };
				if (chkMA1.Checked)
				{
					iPeriods[0] = iMA1period;
					if (rbMA1ema.Checked)
					{ sTypes[0] = "ema"; }
					else
					{ sTypes[0] = "sma"; }
				}
				if (chkMA2.Checked)
				{
					iPeriods[1] = iMA1period;
					if (rbMA2ema.Checked)
					{ sTypes[1] = "ema"; }
					else
					{ sTypes[1] = "sma"; }
				}
				if (chkMA3.Checked)
				{
					iPeriods[2] = iMA1period;
					if (rbMA3ema.Checked)
					{ sTypes[2] = "ema"; }
					else
					{ sTypes[2] = "sma"; }
				}
				oMACDgen.MAperiods = iPeriods;
				oMACDgen.MAtypes = sTypes;
				oMACDgen.DaysToGenerate = iDays;
				StatusLabel.Text = "generating moving averages";
				Application.DoEvents();
				if (!oMACDgen.CreateMAs(oDataReader.ClosePrices, chkAltEma.Checked))
				{
					StatusLabel.Text = "moving average gen. failed";
					MessageBox.Show(this, "Moving Average Generator Failed:" + oOthersGen.LastError, "Generate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}

			if (chkRofChng.Checked)
			{
				StatusLabel.Text = "generating rate of change";
				Application.DoEvents();
				oOthersGen.RoCperiod = iRoCperiod;
				oOthersGen.DaysToGenerate = iDays;
				if (!oOthersGen.GenerateRoCdata(oDataReader.ClosePrices))
				{
					StatusLabel.Text = "Rate of Change generator failed. Generation stopped.";
					MessageBox.Show(this, "Rate of Change generator failed: " + oOthersGen.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}

			if (chkVolatility.Checked)
			{
				StatusLabel.Text = "generating volitility values";
				Application.DoEvents();
				oVolitility.DaysToGenerate = iDays;
				oVolitility.VolitilityPeriod = iVolitilityPeriod;
				if (!oVolitility.CreateVolatilityData(oDataReader.ClosePrices))
				{
					StatusLabel.Text = "Volitility generator failed. Generation stopped.";
					MessageBox.Show(this, "Volitility generator failed: " + oVolitility.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}

			int iTemp = iDays;
			if (chkDeltas.Checked) iTemp++;
			DataVector[] dvShortHistory = new DataVector[iTemp];
			Array.ConstrainedCopy(dvPriceHistory, (dvPriceHistory.Length - iTemp), dvShortHistory, 0, iTemp);
			oOHLCgen.ShortPriceHistory = dvShortHistory;

			if (chkOpen.Checked || chkHigh.Checked || chkLow.Checked || chkClose.Checked)
			{
				string sSelector = "";
				if (chkOpen.Checked) sSelector += "O";
				if (chkHigh.Checked) sSelector += "H";
				if (chkLow.Checked) sSelector += "L";
				if (chkClose.Checked) sSelector += "C";
				if (!oOHLCgen.GenerateOHLCdata(sSelector, chkDeltas.Checked))
				{
					StatusLabel.Text = "OHLC generator error. Generation stopped.";
					MessageBox.Show(this, "OHLC generator error: " + oOHLCgen.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}

			if (chkAddUpDown.Checked)
			{
				if (!oOHLCgen.GenerateClassificationBinaryValues("up,down", cmbClassBase1.Text))
				{
					StatusLabel.Text = "OHLC Classification generator error. Generation stopped.";
					MessageBox.Show(this, "OHLC Classification generator error: " + oOHLCgen.LastError, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}
			return true;
		}

	} // end class
}  // end namespace
