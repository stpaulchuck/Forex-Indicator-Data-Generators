using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forex_test_data_generator
{
	public class ClsOHLCgenerator
	{
		/****************************** properties ***********************************/
		private DataVector[] m_ShortPriceHistory = { };  //  input
		public DataVector[] ShortPriceHistory { set { m_ShortPriceHistory = value; } }

		private string[] m_ColumnNames = new string[] { };  // input
		public string[] ColumnLabels { get { return m_ColumnNames; } }

		private string m_LastError = "";  //  output
		public string LastError { get { return m_LastError; } }

		/******************************* global vars **********************************/
		int iIndexer = 0;
		DataVector dv;
		double[][] OhlcArray = { };
		double[][] ClassificationValues = { };

		/******************************** public methods *********************************/
		/// <summary>
		/// formats the OHLC output from list of column names on input
		/// to get the data call GetOHLC()
		/// if 'deltas', it adjusts the data as the difference from today and yesterday
		/// </summary>
		/// <param name="RequestedInputDataIDs"></param>
		/// <returns> returns true if good or false if fail, reason in LastError</returns>
		public bool GenerateOHLCdata(string RequestedInputDataIDs, bool DoDeltas = false)
		{
			int iTemp = 0, iStepper = 0;
			SetColumnNames(RequestedInputDataIDs);

			try
			{
				iTemp = RequestedInputDataIDs.Length;
				OhlcArray = new double[m_ShortPriceHistory.Length][];
				RequestedInputDataIDs = RequestedInputDataIDs.ToUpper();

				int iStop = m_ShortPriceHistory.Length;
				for (iIndexer = 0; iIndexer < iStop; iIndexer++)
				{
					iStepper = 0;
					double[] dArray = new double[iTemp];
					dv = m_ShortPriceHistory[iIndexer];

					if (RequestedInputDataIDs.Contains("O"))
					{
						dArray[iStepper] = dv.Open;
						iStepper++;
					}
					if (RequestedInputDataIDs.Contains("H"))
					{
						dArray[iStepper] = dv.High;
						iStepper++;
					}
					if (RequestedInputDataIDs.Contains("L"))
					{
						dArray[iStepper] = dv.Low;
						iStepper++;
					}
					if (RequestedInputDataIDs.Contains("C"))
					{
						dArray[iStepper] = dv.Close;
						iStepper++;
					}
					OhlcArray[iIndexer] = dArray;
				}
				if (DoDeltas)
				{
					if (!CreateDeltas())
						return false;
				}
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}
			return true;
		}

		public double[][] GetOHLC(int Days)
		{
			double[][] retVal = new double[Days][];
			int iLen = OhlcArray.Length;

			Array.ConstrainedCopy(OhlcArray, iLen - Days, retVal, 0, Days);
			return retVal;
		}

		public double[][] GetOHLC()
		{
			return OhlcArray;
		}

		public bool GenerateClassificationBinaryValues(string ClassificationLabels, string DataSourceColumnName)
		{
			bool bUpDown = false;
			string[] splitArray = ClassificationLabels.Split(new char[] { ',' });
			if (splitArray[0] == "up" || splitArray[0] == "buy")
			{ bUpDown = true; }
			

			int iStop = m_ShortPriceHistory.Length -1;
			ClassificationValues = new double[m_ShortPriceHistory.Length][];
			ClassificationValues[0] = new double[] { 0,0 };
			try
			{
				for (int iIndexer = 0; iIndexer < iStop; iIndexer++)
				{
					if ((double)m_ShortPriceHistory[iIndexer][DataSourceColumnName] < (double)m_ShortPriceHistory[iIndexer + 1][DataSourceColumnName])
					{
						if (bUpDown) ClassificationValues[iIndexer + 1] = new double[] { 1, 0 };
						else ClassificationValues[iIndexer + 1] = new double[] { 0, 1 };
					}
					else
					{
						if (bUpDown) ClassificationValues[iIndexer + 1] = new double[] { 0, 1 };
						else ClassificationValues[iIndexer + 1] = new double[] { 1, 0 };
					}
				}
			}
			catch (Exception e4)
			{
				ClassificationValues = new double[][] { };  // clear it out so no crap data gets forwarded
				m_LastError = "error generating binary classification data: " + e4.Message;
				return false;
			}
			return true;
		}

		public double[][] GetClassificationIntValues(int Days)
		{
			double[][] retVal = new double[Days][];
			int iLen = ClassificationValues.Length;

			Array.ConstrainedCopy(ClassificationValues, iLen - Days, retVal, 0, Days);
			return retVal;
		}

		public double[][] GetClassificationIntValues()
		{
			return GetClassificationIntValues(m_ShortPriceHistory.Length);
		}

		/******************************* private methods **********************************/

		private void SetColumnNames(string sData)
		{
			int iTemp = 0;

			m_ColumnNames = new string[sData.Length];
			if (sData.Contains("O"))
			{
				m_ColumnNames[iTemp] = "OpenPrices";
				iTemp++;
			}
			if (sData.Contains("H"))
			{
				m_ColumnNames[iTemp] = "HighPrices";
				iTemp++;
			}
			if (sData.Contains("L"))
			{
				m_ColumnNames[iTemp] = "LowPrices";
				iTemp++;
			}
			if (sData.Contains("C"))
			{
				m_ColumnNames[iTemp] = "ClosePrices";
			}

		}

		private bool CreateDeltas()
		{
			try
			{
				int jStop = m_ColumnNames.Length;
				int iStop = OhlcArray.Length;
				double[][] newOHLCarray = new double[iStop - 1][];
				double[] faOld, faNow;
				for (int iIndexer = 1; iIndexer < iStop; iIndexer++)
				{
					faNow = OhlcArray[iIndexer];
					faOld = OhlcArray[iIndexer - 1];
					for (int jIndexer = 0; jIndexer < jStop; jIndexer++)
					{
						faOld[jIndexer] = faNow[jIndexer] - faOld[jIndexer];
					}
					newOHLCarray[iIndexer - 1] = faOld;
				}
				OhlcArray = newOHLCarray;
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}
			return true;
		}

	}  //  end class

}  //  end namespace
