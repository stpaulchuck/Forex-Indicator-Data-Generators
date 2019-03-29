using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;

namespace StochasticsGenerator.cs
{
    public partial class StochasticMain : Form
    {
        /************** global vars ****************/
        //MySqlConnection oConn = null;
        //MySqlCommand oCmd = null;
        //MySqlDataAdapter oDA = null;
		clsMySqlHandler oSqlHandler = null;
        // data in table rows is newest date to oldest date!! beware
        DataTable oTable = new DataTable();
        int iPercentKslowPeriod = 3, iPercentDperiod = 3, iLookBackPeriod = 14, iTableMaxRows = 0, iBarPeriod = 0;
        bool bAbort = false, bInitializing = false;
        StringCollection PrimaryCurrencyList = new StringCollection() { "USD" };
        StringCollection SecondaryCurrencyList = new StringCollection() { "GBP", "NZD", "AUD", "EUR" };
        string ServerName = "", sPairName = "";

        /****************** event handlers ********************/

        private void StochasticMain_Shown(object sender, EventArgs e)
        {
            bInitializing = true;

			oSqlHandler = new clsMySqlHandler(cmbDatabaseNames, cmbTableNames);
			iLookBackPeriod = Properties.Settings.Default.LookbackPeriod;
            txtLookBack.Text = iLookBackPeriod.ToString();
            iPercentDperiod = Properties.Settings.Default.Dperiod;
            txtDperiod.Text = iPercentDperiod.ToString();
            iPercentKslowPeriod = Properties.Settings.Default.SlowKperiod;
            txtSlowKPeriod.Text = iPercentKslowPeriod.ToString();
            cmbServerName.Text = Properties.Settings.Default.LastServerName;
			foreach (string s in Properties.Settings.Default.ServerList)
			{
				cmbServerName.Items.Add(s);
			}
			if (cmbServerName.Text == "") cmbServerName.Text = cmbServerName.Items[0].ToString();
			if (!cmbServerName_SelectedIndexChanged(null, EventArgs.Empty))
            {
				MessageBox.Show(this, oSqlHandler.LastError, "Connection Error");
				bInitializing = false; return;
			}
			//oSqlHandler.GetDatabases(cmbServerName.Text);
            bInitializing = false;
        }

        private bool cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDaysAvailable.Text = "{not set}";
            string srvr = cmbServerName.Text;
            StatusLabel.Text = "Connecting to server " + srvr + " ...";
            Application.DoEvents();
			this.UseWaitCursor = true;
			try
			{
				if (!oSqlHandler.MakeSqlConnection(srvr))
				{
					throw new Exception("Failed to open connection to server " + cmbServerName.Text, new Exception(oSqlHandler.LastError));
				}
            }
            catch (Exception e1)
            {
                string sksks = e1.Message;

				MessageBox.Show(this, "Unable to connect to server " + srvr + "! Try another from the list.",
                   "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusLabel.Text = "Login for server " + srvr + " failed.";
				this.UseWaitCursor = false;
                return false;
            }
			StatusLabel.Text = "Connected to server " + srvr;
            Properties.Settings.Default.LastServerName = srvr;
            Properties.Settings.Default.Save();
            StatusLabel.Text = "Idle ...";
            cmbServerName.Text = srvr;
			this.UseWaitCursor = false;
			if (oSqlHandler.GetDatabases())
			{
				if (cmbDatabaseNames.Items.Count == 1) // load the tables
				{
					cmbDatabaseNames.Text = cmbDatabaseNames.Items[0].ToString();
					return oSqlHandler.GetTableNames();
				}
				return true; // let them pick the db to use
			}
			else
			{
				MessageBox.Show(this, "Database fetch failed: " + oSqlHandler.LastError);
				return false;
			}
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            double[,] retVal = CalculateStochs();
			if (retVal != null)
			{
				SaveData(retVal);
			}
			else
			{
				StatusLabel.Text = "Error during data save";
				MessageBox.Show(this, "Error during Stoch calculations! " + oSqlHandler.LastError, "Error");
				return;
			}
			StatusLabel.Text = "completed generating and saving Stochastics";
		}

        private void btnAbort_Click(object sender, EventArgs e)
        {
            bAbort = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtLookBack_TextChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;

            if (ValidateInputs())
            {
                iLookBackPeriod = int.Parse(txtLookBack.Text);
                Properties.Settings.Default.LookbackPeriod = iLookBackPeriod;
                Properties.Settings.Default.Save();
                AdjustIdays();
            }
        }

        private void txtSlowKPeriod_TextChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;

            if (ValidateInputs())
            {
                iPercentKslowPeriod = int.Parse(txtSlowKPeriod.Text);
                Properties.Settings.Default.SlowKperiod = iPercentKslowPeriod;
                Properties.Settings.Default.Save();
                AdjustIdays();
            }
        }

        private void txtDperiod_TextChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;

            if (ValidateInputs())
            {
                iPercentDperiod = int.Parse(txtDperiod.Text);
                Properties.Settings.Default.Dperiod = iPercentDperiod;
                Properties.Settings.Default.Save();
                AdjustIdays();
            }
        }

        private void txtDays_TextChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
            AdjustIdays();
        }

        private void cmbDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
			oSqlHandler.GetTableNames();
        }

        private void cmbTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bInitializing) return;
			bInitializing = true;
			int iCount = oSqlHandler.GetCount();
			iCount -= (iPercentDperiod + iPercentKslowPeriod + iLookBackPeriod);
			lblDaysAvailable.Text = iCount.ToString();
            txtDays.Text = lblDaysAvailable.Text;
			string sTemp = cmbTableNames.Text;
			bInitializing = false;
        }


        /**************** constructor ******************/
        public StochasticMain()
        {
            InitializeComponent();
        }

		/***************** private methods ********************/
		bool SaveData(double [,] tClosePCkPCdTensor)
		{
			string sCommandText = "";
			StatusLabel.Text = "storing data...";
			int iCounter = 0, iDaysToGet = int.Parse(txtDays.Text);
			try
			{
				string sBarPeriod = iBarPeriod.ToString();
				string CommandText = "delete from stochastics where pairname='" + sPairName + "' and barperiod=" + sBarPeriod;
				oSqlHandler.ExecuteSQLCommand(CommandText);
				string sLookBack = iLookBackPeriod.ToString(), sKslowPeriod = iPercentKslowPeriod.ToString(), sDperiod = iPercentDperiod.ToString();

				string sInsert = "insert into stochastics (PairName, BarDateTime,BarPeriod,LookbackPeriod,K_slowmult,D_mult,"
					+ "PercentK,PercentKslow,PercentD,PercentDslow) VALUES ('" + sPairName + "','{0}'," + sBarPeriod
					+ "," + sLookBack + "," + sKslowPeriod + "," + sDperiod + ",{1},{2},{3},{4})";

				DateTime fDT = DateTime.Today;
				string sBarTime = "";
				List<string> lstCommandsList = new List<string>();
				for (int iDex = 0; iDex < iDaysToGet; iDex++)
				{
					fDT = oTable.Rows[iDex].Field<DateTime>("BarTime");
					sBarTime = fDT.Year.ToString("0000") + "-" + fDT.Month.ToString("00") + "-" + fDT.Day.ToString("00");
					sCommandText = string.Format(sInsert, sBarTime, tClosePCkPCdTensor[iDex, 1].ToString(),
						tClosePCkPCdTensor[iDex, 2].ToString(), tClosePCkPCdTensor[iDex, 2].ToString(), tClosePCkPCdTensor[iDex, 3].ToString());
					lstCommandsList.Add(sCommandText);
					iCounter++;
					if (iCounter % 17 == 0)
					{
						StatusLabel.Text = "saving data row " + iCounter.ToString();
					}
				}
				oSqlHandler.ExecuteSQLCommand(lstCommandsList.ToArray());
			}
			catch (Exception e)
			{
				MessageBox.Show(this, "Error saving data! - " + e.Message, "MySQL error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			StatusLabel.Text = "calculations done";
			return true;
		}

        double[,] CalculateStochs()
        {
            if (!ValidateInputs()) return null;
            iBarPeriod = 0;
            int iDaysToGet = int.Parse(txtDays.Text);
            int iWorkingDays = iLookBackPeriod + iPercentDperiod + iPercentKslowPeriod;

            string sTableName = cmbTableNames.Text;
            if (sTableName.Length > 6) // contains the periodicity in name
            {
                iBarPeriod = int.Parse(sTableName.Substring(6));
                sPairName = sTableName.Substring(0, 6);
            }
            else
            {
                MessageBox.Show(this, "Table: " + sTableName + " not usable due to table name.", "No period data.");
                return null;
            }

            string sCmd = "select * from " + sTableName + " order by bartime desc limit " + (iDaysToGet + iWorkingDays).ToString("0");
			oTable = oSqlHandler.GetTableData(sCmd);
			int iTableLength = oTable.Rows.Count;
            if (iTableLength < (iWorkingDays + iDaysToGet))
            {
                if (DialogResult.Cancel == MessageBox.Show(this, "Query only returned " + iTableLength.ToString()
                        + " Rows. 'OK' to Continue with  or 'Cancel'.", "Bad Row Count", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    oTable.Clear();
                    return null;
                }
            }
            //------ now prep the highest high, lowest low values
            StatusLabel.Text = "Starting highest-high/lowest-low creation.";
            double[,] tHiLowTensor = new double[iTableLength, 4];
            double[,] tClosePCkPCdTensor = new double[iTableLength, 4];
            int iDex = 0;
            //------ load the input data
            foreach (DataRow oDR in oTable.Rows)
            {
                tHiLowTensor[iDex, 0] = oDR.Field<double>("High");
                tHiLowTensor[iDex, 1] = oDR.Field<double>("Low");
                tClosePCkPCdTensor[iDex, 0] = oDR.Field<double>("Close");
                iDex++;
            }
            //-------- now calculate highest high and lowest low
            StatusLabel.Text = "Calculating highest-highs/lowest-lows";
            Application.DoEvents();
            int iStop = iTableLength - iLookBackPeriod;
            for (iDex = 0; iDex <= iStop; iDex++)
            {
                tHiLowTensor[iDex, 2] = tHiLowTensor[iDex, 0]; // current row high value
                tHiLowTensor[iDex, 3] = tHiLowTensor[iDex, 1]; // current row low value
                for (int jDex = 1; jDex < iLookBackPeriod; jDex++)
                {
                    if (tHiLowTensor[iDex + jDex, 0] > tHiLowTensor[iDex, 2])
                    { tHiLowTensor[iDex, 2] = tHiLowTensor[iDex + jDex, 0]; }
                    if (tHiLowTensor[iDex + jDex, 1] < tHiLowTensor[iDex, 3])
                    { tHiLowTensor[iDex, 3] = tHiLowTensor[iDex + jDex, 1]; }
                }
            } // highest hi and lowest lo are in same row as high and low, synced with iTable row number

            Application.DoEvents();
			/*
            public DateTime BarTime;
            public float PercentK;
            public float PercentKslow;
            public float PercentD;
            public float PercentDslow;
            */
			//----- calculate %K
			StatusLabel.Text = "Starting %K calculations.";
			iStop = iDaysToGet + iPercentDperiod + iPercentKslowPeriod;
            for (iDex = 0; iDex < iStop; iDex++)
            {
                if (bAbort)
                { return null; }
                tClosePCkPCdTensor[iDex, 1] = 100 * ((tClosePCkPCdTensor[iDex, 0] - tHiLowTensor[iDex, 3]) / (tHiLowTensor[iDex, 2] - tHiLowTensor[iDex, 3])); 
            }
			//------ calculate %D/slow %K
			StatusLabel.Text = "Starting %D, slow %K calculations.";
			for (iDex = 0; iDex < iDaysToGet; iDex++)
            {
                if (bAbort)
                { return null; }
                double pcK = tClosePCkPCdTensor[iDex, 1];
                for (int jDex = 1; jDex < iPercentKslowPeriod; jDex++) // column 2
                {
                    pcK += tClosePCkPCdTensor[iDex + jDex, 1];
                }
                pcK /= iPercentKslowPeriod;
                tClosePCkPCdTensor[iDex, 2] = pcK;
            }

			//------ calculate slow %D
			StatusLabel.Text = "Starting slow %D calculations.";
			for (iDex = 0; iDex < iDaysToGet; iDex++) // column 3
            {
                if (bAbort)
                { return null; }
                double pcK = tClosePCkPCdTensor[iDex, 2];
                for (int jDex = 1; jDex < iPercentDperiod; jDex++)
                {
                    pcK += tClosePCkPCdTensor[iDex + jDex, 2];
                }
                pcK /= iPercentDperiod;
                tClosePCkPCdTensor[iDex, 3] = pcK;
            }
            StatusLabel.Text = "Computation completed.";
			return tClosePCkPCdTensor;
		}

        bool GetTableNames()
        {
            if (bInitializing) return true;

            bInitializing = true; // stops looping
            cmbTableNames.Items.Clear();
            cmbTableNames.Text = "";
            StatusLabel.Text = "Getting Table list from DB " + cmbDatabaseNames.Text;
			if (oSqlHandler.GetTableNames())
			{
				cmbTableNames.Text = "Choose Table";
				Application.DoEvents();
				StatusLabel.Text = "table inventory complete";
				bInitializing = false;
				return true;
			}
			else
			{
				cmbTableNames.Text = "error";
				StatusLabel.Text = "error fetching table names!";
				MessageBox.Show(this, "Error fetching table names: " + oSqlHandler.LastError);
				return false;
			}
        }

        bool ValidateInputs()
        {
            int iTest = 0;
     
            if (!int.TryParse(txtLookBack.Text, out iTest))
            {
                MessageBox.Show(this, "Lookback value not a legitimate integer value.", "Bad Input");
                return false;
            }
            if (iTest < 6)
            {
                MessageBox.Show(this, "Lookback value less than 6 days.", "Bad Input");
                return false;
            }
            if (!int.TryParse(txtSlowKPeriod.Text, out iTest))
            {
                MessageBox.Show(this, "Slow K Period value not a legitimate integer value.", "Bad Inout");
                return false;
            }
            if (iTest < 2)
            {
                MessageBox.Show(this, "Slow K Period value less than 2 days.", "Bad Input");
                return false;
            }
            if (!int.TryParse(txtDperiod.Text, out iTest))
            {
                MessageBox.Show(this, "D Period value not a legitimate integer value.", "Bad Inout");
                return false;
            }
            if (iTest < 2)
            {
                MessageBox.Show(this, "Slow D value less than 2 days.", "Bad Input");
                return false;
            }
            if (!int.TryParse(txtDays.Text, out iTest))
            {
                MessageBox.Show(this, "Number of Days value not a legitimate integer value.", "Bad Inout");
                return false;
            }
            if (iTest < 100)
            {
                MessageBox.Show(this, "Days to get value less than 100 days.", "Bad Input");
                return false;
            }

            return true;
        }

        public bool GetDatabases()
        {
            cmbDatabaseNames.Items.Clear();
            cmbDatabaseNames.Text = "";
            if (cmbServerName.Text == "") return false;
            try
            {
                StatusLabel.Text = "Getting DB list from Server " + ServerName;
				if (oSqlHandler.GetDatabases())
				{
					if (Properties.Settings.Default.LastDBname.Length > 3)
						cmbDatabaseNames.Text = Properties.Settings.Default.LastDBname;
					else
					{
						if (cmbDatabaseNames.Items.Count > 1)
							cmbDatabaseNames.Text = "Choose DBase";
						if (cmbDatabaseNames.Items.Count == 1)
							cmbDatabaseNames.Text = cmbDatabaseNames.Items[0].ToString();
						if (cmbDatabaseNames.Items.Count <= 0)
							cmbDatabaseNames.Text = "No DBs";
					}
                    StatusLabel.Text = "Idle ...";
                }
                else
                {
                    MessageBox.Show(this, "No forex Databases Found!", "DB Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StatusLabel.Text = "No Databases Found on server " + ServerName;
                    cmbDatabaseNames.Text = "*none found*";
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Error fetching database list: " + e.Message);
                StatusLabel.Text = "Error fetching database list.";
                return false;
            }
			StatusLabel.Text = "DB list fetch completed";
            return true;
        }

        void AdjustIdays()
        {
            if (lblDaysAvailable.Text == txtDays.Text)
            {
                lblDaysAvailable.Text = (iTableMaxRows - (iPercentDperiod + iPercentKslowPeriod + iLookBackPeriod)).ToString();
                txtDays.Text = lblDaysAvailable.Text;
            }
            else
            {
                int iDays = int.Parse(txtDays.Text);
                lblDaysAvailable.Text = (iTableMaxRows - (iPercentDperiod + iPercentKslowPeriod + iLookBackPeriod)).ToString();
                if (iDays >= (iTableMaxRows - (iPercentDperiod + iPercentKslowPeriod + iLookBackPeriod)))
                {
                    iDays = (iTableMaxRows - (iPercentDperiod + iPercentKslowPeriod + iLookBackPeriod));
                }
                txtDays.Text = iDays.ToString();
            }
        }

    }  // end class

}  // end namespace

/*
 CREATE TABLE `stochastics` (
  `PairName` varchar(8) NOT NULL,
  `BarDateTime` datetime NOT NULL,
  `BarPeriod` int(11) NOT NULL,
  `LookbackPeriod` int(11) NOT NULL,
  `K_slowmult` int(11) NOT NULL,
  `D_mult` int(11) NOT NULL,
  `PercentK` float NOT NULL,
  `PercentKslow` float NOT NULL,
  `PercentD` float NOT NULL,
  `PercentDslow` float NOT NULL,
  PRIMARY KEY (`PairName`,`BarDateTime`,`BarPeriod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
*/
/*
* %K = 100(C - L14)/(H14 - L14)

Where:
C = the most recent closing price
L14 = the low of the 14 previous trading sessions
H14 = the highest price traded during the same 14-day period
%K= the current market rate for the currency pair
%D = 3-period moving average of %K
*/
