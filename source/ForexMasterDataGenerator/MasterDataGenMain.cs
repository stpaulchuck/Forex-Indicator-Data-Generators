using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace InvestmentApps.DataGenerators.ForexMasterDataGenerator
{
    public partial class MasterGenMain : Form
    {
        /*********** global vars **********/
        int iDays = 0, iExtraRowsToFetch = 0, iMaxRowsAvailable = 0;
        public DataTable oTable = new DataTable();
        string sTableName = "";
        bool bAbort = false, bInitializing = false, bParameterTextChanged = false;
        bool bServerNameGood = false, bDBNameGood = false, bTableNameGood = false;
        StringCollection g_PrimaryCurrencyList = new StringCollection() { "USD", "CHF", "GBP", "NZD", "AUD", "EUR" };
        //****************************
        clsCCIgen CCIgenerator = new clsCCIgen();
        clsHeikenAshigen HAgenerator = new clsHeikenAshigen();
        clsIchimokugen IchimokuGenerator = new clsIchimokugen();
        clsRVI3gen RVI3generator = new clsRVI3gen();
        clsStochasticIndex StochGenerator = new clsStochasticIndex();
        clsTrixgen TrixGenerator = new clsTrixgen();
        clsDBmasterHandler MasterDBhandler = new clsDBmasterHandler();


        /*********** constructor **********/
        
        public MasterGenMain()
        {
            InitializeComponent();
        }

        /*********** button handlers **********/

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            bAbort = true;
            clsGeneratorParent.Abort = true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxes()) return;
            if (!int.TryParse(txtDays.Text, out iDays))
            {
                StatusLabel.Text = "Days to get not a proper integer.";
                MessageBox.Show(this, "Days to get value is not a proper integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!CalculateAvailableRows()) return;
            clsGeneratorParent.Abort = false;
            clsGeneratorParent.oRows = oTable.Rows;
            clsGeneratorParent.StatusLabel = this.StatusLabel;
            clsGeneratorParent.iDays = iDays;
            clsGeneratorParent.PairName = sTableName.Substring(0, 6).ToUpper();
            clsGeneratorParent.BarPeriod = int.Parse(sTableName.Substring(6));
            clsGeneratorParent.DBmasterHandler = MasterDBhandler;

            // call each indicator that's checked class::generate() method
            if (bAbort)
            {
                StatusLabel.Text = "Operation aborted by user!";
                return;
            }
            if (chkCCIgen.Checked)
            {
                if (!CCIgenerator.GenerateData()) return;
            }
            if (bAbort)
            {
                StatusLabel.Text = "Operation aborted by user!";
                return;
            }
            if (chkHeikenAshiGen.Checked)
            {
                if (!HAgenerator.GenerateData()) return;
            }
            if (bAbort)
            {
                StatusLabel.Text = "Operation aborted by user!";
                return;
            }
            if (chkIchimokuGen.Checked)
            {
                if (!IchimokuGenerator.GenerateData()) return;
            }
            if (bAbort)
            {
                StatusLabel.Text = "Operation aborted by user!";
                return;
            }
            if (chkRVI3gen.Checked)
            {
                if (!RVI3generator.GenerateData()) return;
            }
            if (bAbort)
            {
                StatusLabel.Text = "Operation aborted by user!";
                return;
            }
            if (chkTrixGen.Checked)
            {
                if (!TrixGenerator.GenerateData()) return;
            }
            if (bAbort)
            {
                StatusLabel.Text = "Operation aborted by user!";
                return;
            }
            if (chkStochastics.Checked)
            {
                if (!StochGenerator.GenerateData()) return;
            }
            if (bAbort)
            {
                StatusLabel.Text = "Operation aborted by user!";
                return;
            }
            StatusLabel.Text = "All Data Generation Completed.";
        }


        /*********** methods **********/

        private bool ValidateTextBoxes()
        {
            iExtraRowsToFetch = 0;
            int iTemp = 0;
            try
            {
                iDays = int.Parse(txtDays.Text);
            }
            catch
            {
                iDays = 0;
                MessageBox.Show(this, "The Days To Check value is not a proper integer. Try again.", "input error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //********** CCI generator
            if (chkCCIgen.Checked)
            {
                try
                {
                    CCIgenerator.TrendCCIperiod = int.Parse(txtTrendCCIperiod.Text);
                }
                catch
                {
                    CCIgenerator.TrendCCIperiod = 14;
                    MessageBox.Show(this, "The Trend CCI period value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (CCIgenerator.TrendCCIperiod > iExtraRowsToFetch) iExtraRowsToFetch = CCIgenerator.TrendCCIperiod;
                try
                {
                    CCIgenerator.EntryCCIperiod = int.Parse(txtEntryCCIperiod.Text);
                }
                catch
                {
                    CCIgenerator.EntryCCIperiod = 6;
                    MessageBox.Show(this, "The Entry CCI period value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (CCIgenerator.EntryCCIperiod > iExtraRowsToFetch) iExtraRowsToFetch = CCIgenerator.EntryCCIperiod;
            } // cci
            if (chkIchimokuGen.Checked)
            {
                try
                {
                    IchimokuGenerator.TenkanPeriod = int.Parse(txtTenkanPeriod.Text);
                }
                catch
                {
                    IchimokuGenerator.TenkanPeriod = 2;
                    MessageBox.Show(this, "The Tenkan Period value is not a proper integer. Try again.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                try
                {
                    IchimokuGenerator.KijunPeriod = int.Parse(txtKijunPeriod.Text);
                }
                catch
                {
                    IchimokuGenerator.KijunPeriod = 6;
                    MessageBox.Show(this, "The Kijun Period value is not a proper integer. Try again.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                try
                {
                    IchimokuGenerator.SpanBperiod = int.Parse(txtSpanBperiod.Text);
                }
                catch
                {
                    IchimokuGenerator.SpanBperiod = 8;
                    MessageBox.Show(this, "The Span B Period value is not a proper integer. Try again.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                iTemp = IchimokuGenerator.KijunPeriod + (Math.Max(IchimokuGenerator.SpanBperiod, Math.Max(IchimokuGenerator.KijunPeriod, IchimokuGenerator.TenkanPeriod)));
                if (iTemp > iExtraRowsToFetch) iExtraRowsToFetch = iTemp;
            } // ichimoku
            if (chkRVI3gen.Checked)
            {
                try
                {
                    RVI3generator.RVIperiod = int.Parse(txtRVIspan.Text);
                }
                catch
                {
                    RVI3generator.RVIperiod = 10;
                    MessageBox.Show(this, "The RVI Span value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                iTemp = RVI3generator.RVIperiod + (2 * RVI3generator.RVIpadding);
                if (iTemp > iExtraRowsToFetch) iExtraRowsToFetch = iTemp;
            }
            if (chkTrixGen.Checked)
            {
                try
                {
                    TrixGenerator.FastTrixPeriod = int.Parse(txtFastTrixSpan.Text);
                }
                catch
                {
                    TrixGenerator.FastTrixPeriod = 20;
                    MessageBox.Show(this, "The Fast Trix Span value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (TrixGenerator.FastTrixPeriod > iExtraRowsToFetch) iExtraRowsToFetch = TrixGenerator.FastTrixPeriod;
                try
                {
                    TrixGenerator.SlowTrixPeriod = int.Parse(txtSlowTrixSpan.Text);
                }
                catch
                {
                    TrixGenerator.SlowTrixPeriod = 35;
                    MessageBox.Show(this, "The Slow Trix Span value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (TrixGenerator.SlowTrixPeriod > iExtraRowsToFetch) iExtraRowsToFetch = TrixGenerator.SlowTrixPeriod;
            }
            if (chkStochastics.Checked)
            {
                try
                {
                    StochGenerator.LookbackPeriod = int.Parse(txtStochLookback.Text);
                }
                catch
                {
                    StochGenerator.LookbackPeriod = 14;
                    MessageBox.Show(this, "The Stochastic Lookback value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                try
                {
                    StochGenerator.SlowPercentKperiod = int.Parse(txtSlowKperiod.Text);
                }
                catch
                {
                    StochGenerator.SlowPercentKperiod = 3;
                    MessageBox.Show(this, "The Slow K value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                try
                {
                    StochGenerator.Dperiod = int.Parse(txtPerCentDperiod.Text);
                }
                catch
                {
                    StochGenerator.Dperiod = 3;
                    MessageBox.Show(this, "The Stochastic D period value is not a proper integer. Try again.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                iTemp = StochGenerator.LookbackPeriod + StochGenerator.SlowPercentKperiod + StochGenerator.Dperiod;
                if (iTemp > iExtraRowsToFetch) iExtraRowsToFetch = iTemp;
            }
            return true;
        }

        private bool ValidateComboBoxes()
        {
            return true;
        }

        private void SaveProperties()
        {
            if (bDBNameGood)
            {
                    Properties.Settings.Default.LastMySqlDBname = cmbDatabaseNames.Text;
            }
            if (bServerNameGood)
            {
                    Properties.Settings.Default.LastMySqlServer = cmbServerName.Text;
            }
            if (bTableNameGood)
            {
                    Properties.Settings.Default.LastMySqlTable = cmbTableNames.Text;
            }
            Properties.Settings.Default.Save();
        }


        /*********** event handlers **********/

        private void MasterGenMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProperties();
        }

        private void MasterGenMain_Shown(object sender, EventArgs e)
        {
            bInitializing = true;
            //-------------
            MasterDBhandler.StatusLabel = StatusLabel;
            ValidateTextBoxes(); // sets the properties of the generators if values are usable
			foreach (string s in Properties.Settings.Default.MySqlServerList)
			{
				cmbServerName.Items.Add(s);
			}
			string sTemp = Properties.Settings.Default.LastMySqlServer;
			if (cmbServerName.Items.Contains(sTemp))
			{
				cmbServerName.Text = sTemp;
			}
			else
			{
				cmbServerName.Text = cmbServerName.Items[0].ToString();
			}
            bInitializing = false;
			cmbServerName_SelectedIndexChanged(null, EventArgs.Empty);
			//if (cmbServerName.Items.Count > 0 && cmbDatabaseNames.Items.Count > 0 && cmbTableNames.Items.Count > 0)
			//    CalculateAvailableRows();
		}

		private void Parameter_TextChanged(object sender, EventArgs e)
        {
            bParameterTextChanged = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (bParameterTextChanged)
                {
                    if (!ValidateTextBoxes())
                    {
                        return;
                    }
                    else
                    {
                        CalculateAvailableRows();
                    }
                } // if param text changed
            } // if index == 0
        } // tabcontrol1_selectedindexchanged

        /*********** database methods **********/

        private void cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            bInitializing = true;
            cmbDatabaseNames.Text = "";
            cmbDatabaseNames.Items.Clear();
            cmbTableNames.Text = "";
            cmbTableNames.Items.Clear();
            lblDaysAvailable.Text = "{not set}";
            StatusLabel.Text = "Connecting to server " + cmbServerName.Text + " ...";
            Application.DoEvents();
            MasterDBhandler.ServerName = cmbServerName.Text;
            GetDBList();
            bInitializing = false;
        }

        private void cmbDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            /********************************/
            bInitializing = true;
            this.Cursor = Cursors.WaitCursor;
            cmbTableNames.Items.Clear();
            cmbTableNames.Text = "";
            StatusLabel.Text = "Getting Table list from DB " + cmbDatabaseNames.Text;
            DataTable dtTableList = new DataTable();
            bDBNameGood = true;
            try
            {
                MasterDBhandler.ServerName = cmbServerName.Text;
                MasterDBhandler.DBname = cmbDatabaseNames.Text;
                MasterDBhandler.IsMySqlDB = true;
                MasterDBhandler.FindTables(cmbTableNames);
            }
            catch (Exception gt)
            {
                MessageBox.Show(this, "ERROR fetching tables!\n" + gt.Message, "ERROR");
                StatusLabel.Text = "Error fetching tables!";
                Application.DoEvents();
                this.Cursor = Cursors.Default;
                bInitializing = false;
                bDBNameGood = false;
                return;
            }
            if (cmbTableNames.Items.Count <= 0)
            {
                MessageBox.Show(this, "No Currency Pair Tables Found", "ERROR");
                StatusLabel.Text = "No Currency Pair Tables Found!";
                bDBNameGood = false;
            }
            bInitializing = false;
            CalculateAvailableRows();
            this.Cursor = Cursors.Default;
            Application.DoEvents();
        }

		private void cmbTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            CalculateAvailableRows();
        }

        private bool CalculateAvailableRows()
        {
            StatusLabel.Text = "Getting table row count for " + sTableName + " ...";
            Application.DoEvents();
            lblDaysAvailable.Enabled = true;
            lblDaysAvailable.Text = "{not set}";
            iMaxRowsAvailable = 0;
            if (!ValidateTextBoxes()) return false; // some bad text
            sTableName = cmbTableNames.Text;

            oTable.Clear();

            int iLen = iDays + iExtraRowsToFetch;
            // check to see if we have this much data in the table
            MasterDBhandler.ServerName = cmbServerName.Text;
            MasterDBhandler.DBname = cmbDatabaseNames.Text;
            MasterDBhandler.TableName = cmbTableNames.Text;
            MasterDBhandler.IsMySqlDB = true;
            int iActual = MasterDBhandler.GetRowCount();
            iMaxRowsAvailable = iActual;
            if (iActual == 0)
            {
                MessageBox.Show(this, "There are zero rows in that table. Can't proceed.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblDaysAvailable.Text = "0";
                return false;
            }
			iLen = iActual;
			iDays = iActual - iExtraRowsToFetch;
			lblDaysAvailable.Text = iDays.ToString();
			txtDays.Text = iDays.ToString();
            if (iActual < iLen)
            {
                if (MessageBox.Show(this, "The number of usable rows in the table (" + (iActual - iExtraRowsToFetch).ToString()
                    + ") is less than your request (" + iDays.ToString() + ")\nDo you wish to proceed with just what is available?",
                    "Data Length Mismatch", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.Cancel)
                {
                    StatusLabel.Text = "Generation Aborted by user.";
                    return false;
                }
                StatusLabel.Text = "Number of days adjusted to available days.";
                // adjust the lengths accordingly
                iLen = iActual;
                iDays = iActual - iExtraRowsToFetch;
                if (iDays < 1) // not enough data
                {
                    txtDays.Text = "{not set}";
                    StatusLabel.Text = "Not enough days in data table!";
                    return false;
                }
                txtDays.Text = iDays.ToString();
            }
            oTable.Clear();
            // now do the fetch
            StatusLabel.Text = "Fetching Data Rows";
            oTable = MasterDBhandler.GetTableData(iLen);
            StatusLabel.Text = "Idle...";
            return true;
        }

         /// <summary>
        /// cascades the seach to DB's then Tables
        /// </summary>
        /// <returns></returns>
        private void GetDBList()
        {
            bInitializing = true;
            this.Cursor = Cursors.WaitCursor;
            MasterDBhandler.ServerName = cmbServerName.Text;
            //MasterDBhandler.InstanceName = cmbInstances.Text;
            try
            {
                bool bResult = MasterDBhandler.FindDatabases(cmbDatabaseNames);
                bDBNameGood = bResult;
				if (!bResult)
				{
					MessageBox.Show("Error getting databases: " + MasterDBhandler.LastError);
					bInitializing = false;
					this.Cursor = Cursors.Default;
					return;
				}
                if (cmbDatabaseNames.Items.Count <= 0)
                {
                    MessageBox.Show(this, "No Databases Found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bDBNameGood = false;
					bInitializing = false;
					this.Cursor = Cursors.Default;
					return;
                }
                else
                {
                    if (bResult)
                    {
                        MasterDBhandler.DBname = cmbDatabaseNames.Text;
						if (cmbDatabaseNames.Items.Count == 1)
						{ GetTables(); }
						string sTemp = Properties.Settings.Default.LastMySqlTable;
						if (cmbTableNames.Items.Contains(sTemp))
						{
							cmbTableNames.Text = sTemp;
							clsDatabaseHandlerParent.TableName = sTemp;
							CalculateAvailableRows();
							return;
						}
					} // result = true
				} // db's > 0
            }
            catch (Exception e2)
            {
                MessageBox.Show(this, "Error finding Databases for .\r\n" + e2.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
            bInitializing = false;
        }

        private void GetTables()
        {
            bInitializing = true;
            this.Cursor = Cursors.WaitCursor;
            MasterDBhandler.DBname = cmbDatabaseNames.Text;
            try
            {
                bTableNameGood = MasterDBhandler.FindTables(cmbTableNames);
            }
            catch (Exception e3)
            {
                bTableNameGood = false;
                MessageBox.Show(this, "Error finding Data Tables for Database: " + cmbDatabaseNames.Text + " .\r\n"
                    + e3.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
            bInitializing = false;
        }



    } // end class
} // end namespace
