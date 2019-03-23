using System;
using System.Collections.Generic;

namespace Forex_test_data_generator
{
	public class ClsPriceExtras
	{

		/******************************** properties ************************************/

		private string[] ColNames = new string[] { "ATR", "HiLow", "CloseOpen" };
		public string[] ColumnLabels { get { return ColNames; } }  // output

		private int m_DaysToGenerate = 0;  // input
		public int DaysToGenerate { get { return m_DaysToGenerate; } set { m_DaysToGenerate = value; } }

		private int m_ATRperiod = 14;  // input
		public int ATRperiod { get { return m_ATRperiod; } set { m_ATRperiod = value; } }

		private int m_RoCperiod = 14;  // input
		public int RoCperiod { get { return m_RoCperiod; } set { m_RoCperiod = value; } }

		private string m_LastError = "";  // output
		public string LastError { get { return m_LastError; } }

		/******************************** global vars ***************************************/
		List<double[]> lstOutputArray = new List<double[]>();
		int iIndexer = 0;
		DataVector[] PriceHistoryArray;
		double[] fHLarray = new double[] { }, fCOarray = new double[] { };
		double[] fATRarray = new double[] { };
		double[][] fRoCarray = null;

		/******************************** public methods ***************************************/
		public bool GenerateData(DataVector[] PriceHistory)
		{
			PriceHistoryArray = PriceHistory;
			lstOutputArray.Clear();

			if (!CreateOthers())
			{ return false; }

			if (!CreateATR())
			{ return false; }

			if (!FillOutputArray())
			{ return false; }

			/*
			fHLarray = new double[] { };
			fCOarray = new double[] { };
			fATRarray = new double[] { };
			fRoCarray = new double[] { };
			*/

			return true;
		}

		public bool GenerateRoCdata(double[] Prices)
		{
			if (Prices.Length <= 0)
			{
				m_LastError = "No Price Values for Rate of Change!";
				return false;
			}
			if (m_RoCperiod <= 0)
			{
				m_LastError = "No Rate of Change period values!";
				return false;
			}
			fRoCarray = new double[m_DaysToGenerate][];
			// RoC data: 100 * (Close[n] - Close[n-period]) / Close[n-period]
			int iStart = Prices.Length - 1;
			int iStop = iStart - m_DaysToGenerate;
			int iArrayPtr = m_DaysToGenerate - 1;

			for (int iIndexer = iStart; iIndexer > iStop; iIndexer--)
			{
				fRoCarray[iArrayPtr] = new double[] { 100 * (Prices[iIndexer] - Prices[iIndexer - m_RoCperiod]) / Prices[iIndexer - m_RoCperiod]};
				iArrayPtr--;
			}

			return true;
		}

		public double[][] GetRoCdata(int Days)
		{
			//double[] OutArray = new double[Days];
			//Array.ConstrainedCopy(fRoCarray, fRoCarray.Length - Days, OutArray, 0, Days);

			double[][] retVal = new double[Days][];
			Array.ConstrainedCopy(fRoCarray, fRoCarray.Length - Days, retVal, 0, Days);
			
			//retVal[0] = OutArray;
			return retVal;
		}

		public double[][] GetRoCdata()
		{
			return GetRoCdata(DaysToGenerate);
		}

		/// <summary>
		/// return Days number of ATR, OC, and HL values to caller
		/// </summary>
		/// <param name="Days"></param>
		/// <returns></returns>
		public double[][] GetATRdata(int Days)
		{
			double[][] retVal = new double[Days][];

			Array.ConstrainedCopy(lstOutputArray.ToArray(), lstOutputArray.Count - Days, retVal, 0, Days);
			return retVal;
		}

		public double[][] GetATRdata()
		{
			return GetATRdata(DaysToGenerate);
		}

		/********************************* private methods **************************************/

		/*
		Wilder started with a concept called True Range (TR), which is defined as the greatest of the following:
		Method 1: Current High less the current Low
		Method 2: Current High less the previous Close (absolute value)
		Method 3: Current Low less the previous Close (absolute value)
		
		Current ATR14 = [(Prior ATR x 13) + Current TR] / 14
		  - Multiply the previous 14-day ATR by 13.
		  - Add the most recent day's TR value.
		  - Divide the total by 14
		 */
		bool CreateATR()
		{
			fATRarray = new double[m_DaysToGenerate];
			double fATRvalue = 0.0, fPrevClose = 0.0, fTemp = 0.0;
			int iPriceOffset = PriceHistoryArray.Length - m_DaysToGenerate - m_ATRperiod;
			DataVector dv = new DataVector();

			dv = PriceHistoryArray[iPriceOffset];
			fPrevClose = dv.Close;
			fATRvalue = 0.0;

			try
			{
				for (iIndexer = 0; iIndexer < m_ATRperiod; iIndexer++)
				{
					dv = PriceHistoryArray[iIndexer + iPriceOffset];
					fTemp = Math.Max(dv.High, fPrevClose) - Math.Min(dv.Low, fPrevClose);
					fATRvalue += fTemp;
					fPrevClose = dv.Close;
				}
				fATRarray[0] = fATRvalue / m_ATRperiod;

				iPriceOffset = PriceHistoryArray.Length - m_DaysToGenerate;
				for (iIndexer = 1; iIndexer < m_DaysToGenerate; iIndexer++)
				{
					dv = PriceHistoryArray[iIndexer + iPriceOffset];
					fTemp = Math.Max(dv.High, fPrevClose) - Math.Min(dv.Low, fPrevClose);
					fATRarray[iIndexer] = (fATRarray[iIndexer - 1] * (m_ATRperiod - 1) + fTemp) / m_ATRperiod;
					fPrevClose = dv.Close;
				}
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}

			return true;
		}

		bool CreateOthers()
		{
			int iPriceOffset = PriceHistoryArray.Length - m_DaysToGenerate;
			fCOarray = new double[m_DaysToGenerate];
			fHLarray = new double[m_DaysToGenerate];
			DataVector dv = new DataVector();

			try
			{
				for (iIndexer = 0; iIndexer < m_DaysToGenerate; iIndexer++)
				{
					dv = PriceHistoryArray[iIndexer + iPriceOffset];
					fCOarray[iIndexer] = (dv.Close - dv.Open);
					fHLarray[iIndexer] = (dv.High - dv.Low);
				}
			}
			catch (Exception e22)
			{
				m_LastError = e22.Message;
				return false;
			}

			return true;
		}

		bool FillOutputArray()
		{
			int iATRoffset = fATRarray.Length - m_DaysToGenerate;
			int iHLoffset = fHLarray.Length - m_DaysToGenerate;
			int iCOoffset = fCOarray.Length - m_DaysToGenerate;

			try
			{
				for (iIndexer = 0; iIndexer < m_DaysToGenerate; iIndexer++)
				{
					double[] fValuesArray = new double[3];
					fValuesArray[0] = fATRarray[iIndexer + iATRoffset];
					fValuesArray[1] = fHLarray[iIndexer + iHLoffset];
					fValuesArray[2] = fCOarray[iIndexer + iCOoffset];
					lstOutputArray.Add(fValuesArray);
				}
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}

			return true;
		}

	}  // end class

}  // end namespace
