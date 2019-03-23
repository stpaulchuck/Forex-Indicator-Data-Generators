using System;
using System.Collections.Generic;
using System.Linq;

namespace Forex_test_data_generator
{
	public class ClsStochastics
	{

		/******************************* properties **********************************/
		private int m_Kperiod = 0;
		public int Kperiod { get { return m_Kperiod; } set { m_Kperiod = value; } }

		private int m_Dperiod = 0;
		public int Dperiod { get { return m_Dperiod; } set { m_Dperiod = value; } }

		private int m_Slowing = 0;
		public int Slowing { get { return m_Slowing; } set { m_Slowing = value; } }

		private int m_DaysToGenerate = 0;
		public int DaysToGenerate { get { return m_DaysToGenerate; } set { m_DaysToGenerate = value; } }

		private string[] ColNames = new string[] { "pctK", "pctD" };
		public string[] ColumnLabels { get { return ColNames; } }

		private string m_LastError = "";
		public string LastError { get { return m_LastError; } }

		/****************************** global vars ***********************************/
		List<double[]> lstOutputArray = new List<double[]>(); // holds data internally until called for after data is created
		DataVector[] PriceHistoryArray = { };
		double[] fHighestHighArray = { }, fLowestLowArray = { }, fFastKarray = { };
		int iIndexer = 0;

		/******************************* public methods **********************************/
		public bool CreateStochastics(DataVector[] PriceHistory)
		{
			if (m_DaysToGenerate == 0 || m_Dperiod == 0 || m_Kperiod == 0 || m_Slowing == 0)
			{
				m_LastError = "At lest one parameter value is Zero.";
				return false;
			}
			lstOutputArray.Clear();
			PriceHistoryArray = PriceHistory;

			// fill highest and lowest arrays
			if (!CreateHighestAndLowest())
			{ return false; }

			// now create the output values
			if (!CreateKandD())
			{ return false; }

			// no scaling needed, it's a percentage value

			// now clean up
			fHighestHighArray = new double[] { };
			fLowestLowArray = new double[] { };
			fFastKarray = new double[] { };

			// and exit
			return true;
		}

		/// <summary>
		/// return Days number of MACD values to caller
		/// </summary>
		/// <param name="Days"></param>
		/// <returns></returns>
		public double[][] GetStoch(int Days)
		{
			double[][] retVal = new double[Days][];

			Array.ConstrainedCopy(lstOutputArray.ToArray(), lstOutputArray.Count - Days, retVal, 0, Days);
			return retVal;
		}

		public double[][] GetStoch()
		{
			return GetStoch(DaysToGenerate);
		}


		/***************************** private methods ************************************/

		/*                       \/ removed to scale it to 0 to 1 range
		Raw Stochastics(n) = (((100 *))) (Recent Close - Lowest Low) / (Highest High - Lowest Low);
		%K = 3-period moving average of Raw Stochastics; - slowing
		%D = 3-periods moving average of %K;
		n = number of periods used in the calculation to define highest high and lowest low.		 
		*/

		bool CreateKandD()
		{

			DataVector dv = new DataVector();
			int iStop = m_DaysToGenerate + m_Dperiod - 1;
			int iHiLoOffset = m_Slowing - 1; // stepover for fHighestHighArray and fLowestLowArray
			int iDataOffset = PriceHistoryArray.Length - iStop; // ditto for price history array
			List<double[]> lstKandDarray = new List<double[]>();
			double[] fKandDdata = new double[] { };

			// create slow K
			double[] fSlowKarray = new double[iStop];

			double fSumTop = 0.0, fSumBottom = 0.0;

			try
			{
				for (iIndexer = 0; iIndexer < iStop; iIndexer++)
				{
					fSumBottom = 0.0;
					fSumTop = 0.0;

					for (int k = (iIndexer - m_Slowing + 1); k <= iIndexer; k++)
					{
						dv = PriceHistoryArray[k + iDataOffset];
						fSumTop += (dv.Close - fLowestLowArray[k + iHiLoOffset]);
						fSumBottom += (fHighestHighArray[k + iHiLoOffset] - fLowestLowArray[k + iHiLoOffset]);
					}

					// add result to slowK array
					if (fSumBottom == 0.0)
					{
						fSlowKarray[iIndexer] = 100.0;
					}
					else
					{
						fSlowKarray[iIndexer] = fSumTop / fSumBottom; // * 100.0;
					}
				}

				// create %D and put both into double[] and save in OutputArray
				double fTemp = 0.0;
				for (iIndexer = (m_Dperiod - 1); iIndexer < iStop; iIndexer++)
				{
					fTemp = 0.0;
					for (int k = 0; k < m_Dperiod; k++)
					{
						fTemp += fSlowKarray[iIndexer - k];
					}
					fKandDdata = new double[2];
					fKandDdata[0] = fSlowKarray[iIndexer];
					fKandDdata[1] = (fTemp / m_Dperiod);
					lstKandDarray.Add(fKandDdata);
				}
				lstOutputArray = lstKandDarray;
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}

			fSlowKarray = new double[] { };

			return true;
		}

		bool CreateHighestAndLowest()
		{
			DataVector dv = new DataVector();
			int iStop = PriceHistoryArray.Length;

			int iTemp = m_DaysToGenerate + m_Dperiod - 1 + m_Slowing - 1; // total length of hi/lo data
			fHighestHighArray = new double[iTemp];
			fLowestLowArray = new double[iTemp];

			List<double> lstHighWindow = new List<double>();
			List<double> lstLowWindow = new List<double>();

			int iStartOffset = m_Kperiod - 1;
			int iPriceTensorOffset = iStop - (iTemp + m_Kperiod - 1); // starting point

			try
			{
				dv = PriceHistoryArray[iPriceTensorOffset]; // need non-zero initializers
				lstHighWindow.Add(dv.High);
				lstLowWindow.Add(dv.Low);
				// fill up the window arrays with initial data
				for (iIndexer = 0; iIndexer < iStartOffset; iIndexer++)
				{
					dv = PriceHistoryArray[iIndexer + iPriceTensorOffset];
					lstHighWindow.Add(dv.High);
					lstLowWindow.Add(dv.Low);
				}

				iPriceTensorOffset = iStop - iTemp;
				for (iIndexer = 0; iIndexer < iTemp; iIndexer++)
				{
					dv = PriceHistoryArray[iIndexer + iPriceTensorOffset];
					lstHighWindow.RemoveAt(0);
					lstHighWindow.Add(dv.High);
					fHighestHighArray[iIndexer] = lstHighWindow.Max<double>();
					lstLowWindow.RemoveAt(0);
					lstLowWindow.Add(dv.Low);
					fLowestLowArray[iIndexer] = lstLowWindow.Min<double>();
				}
				lstHighWindow.Clear();
				lstLowWindow.Clear();
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}

			return true;
		}

		// no scaling factor as this is a %, thus 0 to 1.00

	}  // end classs

}  // end namespace
