using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InvestmentApps.RVIGenerator
{
	public partial class RVIGenMain : Form
    {
        /*********** global vars **********/
        const int iRVIpadding = 3; // we use today plus three back for computation of values
        int iDays = 0, iRVIperiod = 10;
        public DataTable oTable = new DataTable();
        string sTableName = "";
        double[] RVIvalues, RVIsigvalues;
        bool bAbort = false, bInitializing = false;
        bool bServerNameGood = false, bDBNameGood = false, bTableNameGood = false;
        StringCollection g_PrimaryCurrencyList = new StringCollection() { "USD", "CHF", "GBP", "NZD", "AUD", "EUR" };
		clsMySqlHandler oMySqlHandler = null;


        /*********** constructor **********/
        public RVIGenMain()
        {
            InitializeComponent();
			oMySqlHandler = new clsMySqlHandler(cmbDatabaseNames, cmbTableNames);
        }

        /*********** button handlers **********/

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
            cmbDatabaseNames.Enabled = false;
            cmbServerName.Enabled = false;
            cmbTableNames.Enabled = false;
            txtRVIspan.Enabled = false;
            lblDaysAvailable.BackColor = Control.DefaultBackColor;

            try
            {
                // fetch the price data
                StatusLabel.Text = "Fetching price data...";
                Application.DoEvents();
                if (!GetInputData()) throw new Exception("Error Fetching Input Data");

                StatusLabel.Text = "Generating RVI...";
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
                string sCommandText = "Delete from RVIdata where Pairname = '" + sPairName + "' and period = " + barperiod.ToString(); ;
				oMySqlHandler.ExecuteSQLCommand(sCommandText);
				Application.DoEvents();
                if (bAbort) return;
                GenerateRVI();
                Application.DoEvents();
                if (bAbort) return;
                StatusLabel.Text = "Saving Data to SQL tables ...";
                Application.DoEvents();
                SaveRVI();
                Application.DoEvents();
                if (bAbort) return;
                StatusLabel.Text = "Generation completed.";
                Application.DoEvents();
            }
            catch (Exception e1)
            {
                Debug.WriteLine("### read error: " + e1.Message);
                MessageBox.Show(this, "RVI Generate Error: " + e1.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusLabel.Text = "Error generating RVI";
                Application.DoEvents();

            }
            finally
            {
                txtDays.Enabled = true;
                cmbDatabaseNames.Enabled = true;
                cmbServerName.Enabled = true;
                cmbTableNames.Enabled = true;
                txtRVIspan.Enabled = true;
                lblDaysAvailable.BackColor = Color.White;
            }

        }

        void GenerateRVI()
        { 
            DataRowCollection oRows = oTable.Rows;
            RVIvalues = new double[iDays + iRVIpadding];
            RVIsigvalues = new double[iDays + iRVIpadding];

            for (int indexer = iDays + iRVIpadding - 1; indexer >= 0; indexer--)
            {
                double dNum = 0.0, dDeNum = 0.0;
                try
                {
                    for (int j = indexer; j < (indexer + iRVIperiod); j++)
                    {
                        double dValueUp = ((oRows[j].Field<double>("Close") - oRows[j].Field<double>("Open")) +
                            (2 * (oRows[j + 1].Field<double>("Close") - oRows[j + 1].Field<double>("Open"))) +
                            (2 * (oRows[j + 2].Field<double>("Close") - oRows[j + 2].Field<double>("Open"))) +
                            (oRows[j + 3].Field<double>("Close") - oRows[j + 3].Field<double>("Open"))) / 6;

                        double dValueDown = ((oRows[j].Field<double>("High") - oRows[j].Field<double>("Low")) +
                            (2 * (oRows[j + 1].Field<double>("High") - oRows[j + 1].Field<double>("Low"))) +
                            (2 * (oRows[j + 2].Field<double>("High") - oRows[j + 2].Field<double>("Low"))) +
                            (oRows[j + 3].Field<double>("High") - oRows[j + 3].Field<double>("Low"))) / 6;

                        dNum += dValueUp;
                        dDeNum += dValueDown;
                    }
                }
                catch (Exception e1)
                {
                    Debug.WriteLine(e1);
                }
                if (dDeNum != 0.0)
                    RVIvalues[indexer] = dNum / dDeNum;
                else
                    RVIvalues[indexer] = 0.00;
            }
            for (int indexer = 0; indexer < iDays; indexer++)
            {
                RVIsigvalues[indexer] = (RVIvalues[indexer] + 2 * RVIvalues[indexer + 1] + 2 * RVIvalues[indexer + 2] + RVIvalues[indexer + 3]) / 6;
            }
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

        /*********** methods **********/

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
                iRVIperiod = int.Parse(txtRVIspan.Text);
            }
            catch
            {
                iRVIperiod = 10;
                MessageBox.Show(this, "The RVI Span value is not a proper integer. Try again.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateComboBoxes()
        {
            return true;
        }

        private void RVIGenMain_Shown(object sender, EventArgs e)
        {
            bInitializing = true;

            txtRVIspan.Text = Properties.Settings.Default.RVIspan.ToString("0");
			//-------------
			foreach (string s in Properties.Settings.Default.ServerList)
			{
				cmbServerName.Items.Add(s);
			}
            string sServername = Properties.Settings.Default.LastServerName;
            if (sServername != "")
            {
                cmbServerName.Text = sServername;
            }
            else
            {
				cmbServerName.Text = cmbServerName.Items[0].ToString();
            }
			cmbServerName_SelectedIndexChanged(null, EventArgs.Empty);
			bInitializing = false;
        }

        private void RVIGenMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            int iTemp = 0;
            int.TryParse(txtRVIspan.Text, out iTemp);
            Properties.Settings.Default.RVIspan = iTemp;
            if (bDBNameGood) Properties.Settings.Default.LastDBname = cmbDatabaseNames.Text;
            if (bServerNameGood) Properties.Settings.Default.LastServerName = cmbServerName.Text;
            if (bTableNameGood) Properties.Settings.Default.LastTableName = cmbTableNames.Text;
            Properties.Settings.Default.Save();
        }


        /*********** database methods **********/

        private void cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (!oMySqlHandler.MakeSqlConnection(cmbServerName.Text))
			{
				MessageBox.Show(this, "Failed to connect to DB server!", "Error");
				return;
			}
            StatusLabel.Text = "Connected to server " + cmbServerName.Text + " ...";
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
				oMySqlHandler.GetTableNames();
				bDBNameGood = true;
            }
            catch (Exception gt)
            {
                MessageBox.Show(this, "ERROR fetching tables!\n" + gt.Message, "ERROR");
                StatusLabel.Text = "Error fetching tables!";
                Application.DoEvents();
                this.Cursor = Cursors.Default;
                bDBNameGood = false;
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
                bDBNameGood = false;
            }
            this.Cursor = Cursors.Default;
            Application.DoEvents();
            bDBNameGood = true;
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
            lblDaysAvailable.Text = "{not set}";
            lblDaysAvailable.Enabled = true;
            try
            {
				int iTableMaxRows = oMySqlHandler.GetCount();
				lblDaysAvailable.Text = (iTableMaxRows - iRVIperiod - (2 * iRVIpadding)).ToString();
                lblDaysAvailable.Enabled = false;
                lblDaysAvailable.BackColor = Color.White;
                lblDaysAvailable.ForeColor = Color.Black;
                txtDays.Text = lblDaysAvailable.Text;
            }
            catch (Exception ec)
            {
                StatusLabel.Text = "*** TABLE READ ERROR ***";
                Debug.WriteLine("***" + ec.Message);
                bTableNameGood = false;
                return;
            }

			bTableNameGood = true;
            StatusLabel.Text = "Idle ...";
        }

        private bool GetInputData()
        {
            oTable.Clear();
            int iLen = (2 * iRVIpadding) + iDays + iRVIperiod;
            // check to see if we have this much data in the table
            int iActual = oMySqlHandler.GetCount();
            if (iActual < iLen)
            {
                if (MessageBox.Show(this, "The number of usable rows in the table (" + (iActual - (2 * iRVIpadding) - iRVIperiod).ToString() 
                    + ") is less than your request (" + iDays.ToString() + ")\nDo you wish to proceed with just what is available?", 
                    "Data Length Mismatch", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Cancel)
                {
                    StatusLabel.Text = "Generation Aborted by user.";
                    return false;
                }
                StatusLabel.Text = "Number of days adjusted to available days.";
                // adjust the lengths accordingly
                iLen = iActual;
                iDays = iActual - (2 * iRVIpadding) - iRVIperiod;
                if (iDays < 1) // not enough data
                {
                    txtDays.Text = "{not set}";
                    StatusLabel.Text = "Not enough days in data table!";
                    return false;
                }
                txtDays.Text = iDays.ToString();
            }
            oTable.Clear();
            RVIvalues = null;
            RVIsigvalues = null;
            // now do the fetch
            oTable = oMySqlHandler.GetTableData("select * from " + sTableName + " order by BarTime desc limit " + iLen.ToString());
            return true;
        }

        private bool SaveRVI()
        {
			List<string> lstCommandStrings = new List<string>();
            DateTime BarDate = DateTime.Now;
            string cmd2 = "";

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

			string CommandText = "delete from RVIdata where pairname='" + sPairName + "' and barperiod=" + barperiod.ToString();
			oMySqlHandler.ExecuteSQLCommand(CommandText);

			string cmdString = "insert into RVIdata (PairName, period, bartime, RVIvalue, RVIsigvalue) values ('"
                + sPairName + "', " + barperiod.ToString() + ",'{0:G}', {1:N7}, {2:N7})";
            for (int i = 0; i < iDays; i++)
            {
                Application.DoEvents();
                if (bAbort) break;
                if (i % 17 == 0) StatusLabel.Text = "Generating Item " + i.ToString();
                Application.DoEvents();
                BarDate = oTable.Rows[i].Field<DateTime>("BarTime");
                // set up local vars from array data
                try
                {
                    cmd2 = String.Format(cmdString, BarDate.ToString("yyyy-MM-dd HH:mm"), RVIvalues[i], RVIsigvalues[i]);
					lstCommandStrings.Add(cmd2);
				}
                catch (Exception s2)
                {
                    Debug.WriteLine(s2);
                }
            } // end for (i = ...
			StatusLabel.Text = "saving the data to RVIdata table";
			Application.DoEvents();
			return oMySqlHandler.ExecuteSQLCommand(lstCommandStrings.ToArray());
        }

	} // end class
} // end namespace
