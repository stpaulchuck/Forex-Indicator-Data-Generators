using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Forex_test_data_generator
{
	public class ClsMACD
	{

		/************************************** properties **************************************/
		private int m_FastPeriod = 0; // input
		public int FastPeriod { get { return m_FastPeriod; } set { m_FastPeriod = value; } }

		private int m_SlowPeriod = 0;  // input
		public int SlowPeriod { get { return m_SlowPeriod; } set { m_SlowPeriod = value; } }

		private int m_SignalPeriod = 0;  //  input
		public int SignalPeriod { get { return m_SignalPeriod; } set { m_SignalPeriod = value; } }

		private int[] m_MAperiods = { 14, 14, 0 };  //  input
		public int[] MAperiods { get { return m_MAperiods; } set { m_MAperiods = value; } }

		private string[] m_MAtypes = { "sma", "ema", "null" };  //  input
		public string[] MAtypes { get { return m_MAtypes; } set { m_MAtypes = value; } }

		private int m_DaysToGenerate = 0;  //  input
		public int DaysToGenerate { get { return m_DaysToGenerate; } set { m_DaysToGenerate = value; } }

		private string m_LastError = "";  //  output
		public string LastError { get { return m_LastError; } }

		private string[] ColNames = new string[] { "MACD", "MACDsignal", "MACDdiff" };  // output
		public string[] ColumnLabels { get { return ColNames; } }


		/****************************** global vars **********************************************/
		double[][] OutputArray = { };
		double[] ClosePrices = { };
		double[] MACDvals = { };
		double[][] MAsOut = { };
		int iIndexer = 0, iStartOffset = 0;

		/******************************* public methods *********************************************/

		public bool CreateMACD(double[] ClosePrices)
		{
			int iTemp = 0;
			double fMACDfast = 0.0, fMACDslow = 0.0, fSignal = 0.0;
			double fMACDfastMult = CalculateEMAmulitplier(m_FastPeriod);
			double fMACDslowMult = CalculateEMAmulitplier(m_SlowPeriod);
			double fMACDsigMult = CalculateEMAmulitplier(m_SignalPeriod);
			List<double[]> OutList = new List<double[]>(); // faster than mathing the offsets

			// clear and size the arrays
			OutputArray = new double[DaysToGenerate][];
			MACDvals = new double[DaysToGenerate + m_SignalPeriod];
			try
			{
				// calc the MACD line into MACDvals array
				iStartOffset = ClosePrices.Length - DaysToGenerate - m_SignalPeriod; // start point in close price array
																											// initialize the vars
																											//---- fast macd ema
				iTemp = iStartOffset - m_FastPeriod;
				fMACDfast = ClosePrices[iTemp];
				iTemp++;
				// EMA: { Close - EMA(previous day)} x multiplier +EMA(previous day).
				for (iIndexer = iTemp; iIndexer < iStartOffset; iIndexer++)
				{
					fMACDfast = (ClosePrices[iIndexer] - fMACDfast) * fMACDfastMult + fMACDfast;
				}

				//---- slow macd sma
				iTemp = iStartOffset - m_SlowPeriod;
				for (iIndexer = iTemp; iIndexer < iStartOffset; iIndexer++)
				{
					fMACDslow = (ClosePrices[iIndexer] - fMACDslow) * fMACDslowMult + fMACDslow;
				}
				//---- MACD Line: (fast EMA - slow EMA)
				MACDvals[0] = fMACDfast - fMACDslow;

				// now do the rest of the MACDvals
				iTemp = MACDvals.Length;
				for (iIndexer = 1; iIndexer < iTemp; iIndexer++)
				{
					fMACDfast = (ClosePrices[iIndexer + iStartOffset] - fMACDfast) * fMACDfastMult + fMACDfast;
					fMACDslow = (ClosePrices[iIndexer + iStartOffset] - fMACDslow) * fMACDslowMult + fMACDslow;
					MACDvals[iIndexer] = fMACDfast - fMACDslow;
				}

				// get the first EMA value from the nine item lead in
				fSignal = MACDvals[0];
				iStartOffset = m_SignalPeriod;
				for (iIndexer = 1; iIndexer < m_SignalPeriod; iIndexer++)
				{
					// EMA: { Close - EMA(previous day)} x multiplier +EMA(previous day).
					fSignal = (MACDvals[iIndexer] - fSignal) * fMACDsigMult + fSignal;
				}
				for (iIndexer = iStartOffset; iIndexer < iTemp; iIndexer++)
				{
					double[] oTemp = new double[ColNames.Length];
					oTemp[0] = MACDvals[iIndexer];
					fSignal = (MACDvals[iIndexer] - fSignal) * fMACDsigMult + fSignal;
					oTemp[1] = fSignal;
					oTemp[2] = oTemp[0] - oTemp[1]; // macd diff
					OutList.Add(oTemp);
				}
				OutputArray = new double[OutList.Count][];
				OutputArray = OutList.ToArray();
			}
			catch (Exception e)
			{
				string s = e.Message;
				m_LastError = s;
				return false;
			}
			finally
			{
				OutList.Clear(); // removes all the contents
				MACDvals = new double[] { };
			}
			return true;
		}

		public bool CreateMAs(double[] Prices, bool UseAlternateEmaMethod = false)
		{
			// the MA's do not have to be on the closing price, but usually are
			// consider doing the MA's on the deltas when deltas are selected
			int iTemp = 0;
			foreach (int ix in m_MAperiods)
			{ if (ix > 0) iTemp++; }
			if (iTemp < 1)
			{
				MAsOut = new double[][] { }; // zero out the output array in case of retry with no ma's selected
				return true;
			}
			ClosePrices = Prices;
			// preload the output with arrays of zeros
			MAsOut = new double[m_DaysToGenerate][];
			for (int iIndexer = 0; iIndexer < m_DaysToGenerate; iIndexer++)
			{
				MAsOut[iIndexer] = new double[iTemp];
			}
			iTemp = 0;
			try
			{
				foreach (string s in m_MAtypes)
				{
					if (s == "sma")
					{
						CreateSMA(iTemp);
						iTemp++;
					}
					else if (s == "ema")
					{
						if (UseAlternateEmaMethod)
							CreateAlternateEMA(iTemp);
						else
							CreateEMA(iTemp);
						iTemp++;
					}
					//---- if "null" then just do nothing
				}
			}
			catch (Exception x)
			{
				m_LastError = "failure in moving averages: " + x.Message;
				return false;
			}

			return true;
		}

		/// <summary>
		/// return Days number of MACD values to caller
		/// </summary>
		/// <param name="Days"></param>
		/// <returns></returns>
		public double[][] GetMACD(int Days)
		{
			double[][] retVal = new double[Days][];

			Array.ConstrainedCopy(OutputArray, OutputArray.Length - Days, retVal, 0, Days);
			return retVal;
		}

		public double[][] GetMACD()
		{
			return GetMACD(DaysToGenerate);
		}

		public double[][] GetMAs(int Days)
		{
			double[][] retVal = new double[Days][];
			Array.ConstrainedCopy(MAsOut, MAsOut.Length - Days, retVal, 0, Days);
			return retVal;
		}

		public double[][] GetMAs()
		{ return GetMAs(DaysToGenerate); }

		/********************************** private methods ******************************************/

		private bool CreateSMA(int ColumnNumber)
		{
			double fTemp = 0.0;
			int iPeriod = m_MAperiods[ColumnNumber], iArrayIndexer = 0;
			int iStart = ClosePrices.Length - m_DaysToGenerate;
			int iStop = ClosePrices.Length;
			for (int iIndexer = iStart; iIndexer < iStop; iIndexer++)
			{
				fTemp = 0.0;
				for (int jIndexer = 0; jIndexer < iPeriod; jIndexer++)
				{
					fTemp += ClosePrices[iIndexer - jIndexer];
				}
				fTemp = fTemp / ((double)iPeriod);
				MAsOut[iArrayIndexer][ColumnNumber] = fTemp;
				iArrayIndexer++;
				/////////////// test code
				if (iArrayIndexer > (MAsOut.Length - iPeriod))
				{ Debug.Print("fTemp = " + fTemp); }
			}

			return true;
		}

		private bool CreateEMA(int ColumnNumber)
		{
			int iPeriod = m_MAperiods[ColumnNumber], iArrayIndexer = 0;
			double fFudgeFactor = CalculateEMAmulitplier(iPeriod);
			double fRunningEMA = 0.0;
			int iStart = ClosePrices.Length - m_DaysToGenerate;
			int iStop = ClosePrices.Length;
			// get the sma of iPeriod length to start up
			for (int jIndexer = 0; jIndexer < iPeriod; jIndexer++)
			{
				fRunningEMA += ClosePrices[iStart - jIndexer];
			}
			fRunningEMA = fRunningEMA / (double)iPeriod;
			// now do the ema's
			for (int iIndexer = iStart; iIndexer < iStop; iIndexer++)
			{
				fRunningEMA = ((ClosePrices[iIndexer] - fRunningEMA) * fFudgeFactor) + fRunningEMA;
				MAsOut[iArrayIndexer][ColumnNumber] = fRunningEMA;
				iArrayIndexer++;
			}

			return true;
		}

		private bool CreateAlternateEMA(int ColumnNumber)
		{
			int iPeriod = m_MAperiods[ColumnNumber], iArrayIndexer = 0;
			double fFudgeFactor = CalculateEMAmulitplier(iPeriod);
			double fRunningEMA = 0.0;
			int iStart = ClosePrices.Length - m_DaysToGenerate;
			int iStop = ClosePrices.Length;
			// get the sma of iPeriod length to start up
			for (int jIndexer = 0; jIndexer < iPeriod; jIndexer++)
			{
				fRunningEMA += ClosePrices[iStart - jIndexer];
			}
			fRunningEMA = fRunningEMA / (double)iPeriod;
			// now do the ema's
			for (int iIndexer = iStart; iIndexer < iStop; iIndexer++)
			{
				fRunningEMA = (ClosePrices[iIndexer] * fFudgeFactor) + ((1 - fFudgeFactor) * fRunningEMA);
				MAsOut[iArrayIndexer][ColumnNumber] = fRunningEMA;
				iArrayIndexer++;
			}

			return true;
		}

		/*
		Initial SMA: 10-period sum / 10 <--- starting point
		Multiplier: (2 / (Time periods + 1) ) = (2 / (10 + 1) ) = 0.1818 (18.18%)
		EMA: {Close - EMA(previous day)} x multiplier + EMA(previous day).
		*/
		double CalculateEMAmulitplier(int TimePeriods)
		{
			double retVal = 0.0;

			retVal = 2f / ((double)TimePeriods + 1f);
			return retVal;
		}

	}  // end classs

}  // end namespace
