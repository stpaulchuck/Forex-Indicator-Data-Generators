using System;

namespace Forex_test_data_generator
{
	public class ClsCCI
	{

		/******************************** properties ************************************/
		private int m_TrendPeriod = 0;
		public int TrendPeriod { get { return m_TrendPeriod; } set { m_TrendPeriod = value; } }

		private int m_EntryPeriod = 0;
		public int EntryPeriod { get { return m_EntryPeriod; } set { m_EntryPeriod = value; } }

		private int m_DaysToGenerate = 0;
		public int DaysToGenerate { get { return m_DaysToGenerate; } set { m_DaysToGenerate = value; } }

		private string m_LastError = "";
		public string LastError { get { return m_LastError; } }

		private string[] ColNames = new string[] { "TrendCCI", "EntryCCI", "CCIdiff" };
		public string[] ColumnLabels { get { return ColNames; } }

		// TP is 'typical price' = (close + high + low)/3
		// AvgTP is the TrendPeriod or EntryPeriod average of TP
		// Deviation is TP - AvgTP

		/********************************* global vars ***********************************/
		double[] TParray = { };  //  typical price array
		double[] TPavgArray = { }; // tp average
		double[][] OutputArray = { };  // this is the CCI data output array to caller
		int iIndexer = 0, iTemp = 0, iDataStartOffset = 0;

		/******************************* public methods *************************************/
		public bool CreateCCI(DataVector[] PriceHistory)
		{
			DataVector dv;

			iTemp = PriceHistory.Length;
			iDataStartOffset = PriceHistory.Length - DaysToGenerate; // how far forward to start
			TParray = new double[iTemp];
			TPavgArray = new double[iTemp];
			// create array of TypicalPrice values
			int iStop = PriceHistory.Length;
			for (iIndexer = 0; iIndexer < iStop; iIndexer++)
			{
				dv = PriceHistory[iIndexer];
				TParray[iIndexer] = (dv.High + dv.Low + dv.Close) / 3; // price typical
			}

			Array.Clear(OutputArray, 0, OutputArray.Length);
			Array.Resize(ref OutputArray, DaysToGenerate);
			// load the output array with empty double arrays to hold values
			iTemp = ColNames.Length;
			for (iIndexer = 0; iIndexer < DaysToGenerate; iIndexer++)
			{
				OutputArray[iIndexer] = new double[iTemp];
			}

			// generate Trend
			CreateCCIvalues(m_TrendPeriod, 0);
			// generate Entry
			CreateCCIvalues(m_EntryPeriod, 1);
			// generate difference
			CreateCCIdifferences(1, 0, 2);

			// now clean up
			TParray = new double[] { };
			TPavgArray = new double[] { };

			return true;
		}

		/// <summary>
		/// return Days number of CCI values to caller
		/// </summary>
		/// <param name="Days"></param>
		/// <returns></returns>
		public double[][] GetCCI(int Days)
		{
			double[][] retVal = new double[Days][];

			Array.ConstrainedCopy(OutputArray, OutputArray.Length - Days, retVal, 0, Days);
			return retVal;
		}

		public double[][] GetCCI()
		{
			return GetCCI(DaysToGenerate);
		}


		/******************************* private methods *************************************/
		bool CreateCCIvalues(int AverageLength, int DataArrayPos)
		{
			double fCCIval = 0.0, fMedianDeviation = 0.0;
			int iStop = 0;

			Array.Clear(TPavgArray, 0, TPavgArray.Length);
			iStop = DaysToGenerate + iDataStartOffset;

			try
			{
				for (iIndexer = iDataStartOffset; iIndexer < iStop; iIndexer++)
				{
					fCCIval = 0.0; // just borrowing this output variable to use for this calc
					for (int j = 0; j < AverageLength; j++)
					{
						fCCIval += TParray[iIndexer - j];
					}
					TPavgArray[iIndexer] = fCCIval / AverageLength;
				}

				for (iIndexer = iDataStartOffset; iIndexer < iStop; iIndexer++)
				{
					fMedianDeviation = 0.0;
					// meandeviation = median of averageLength array of TP's - current TPavg
					for (int j = 0; j < AverageLength; j++)
					{
						fMedianDeviation += Math.Abs(TParray[iIndexer - j] - TPavgArray[iIndexer]);
					}
					fMedianDeviation = fMedianDeviation / AverageLength;

					// cci = (TP - TPavg) / (0.015 * meandiviation)
					fCCIval = (TParray[iIndexer] - TPavgArray[iIndexer]) / (0.015 * fMedianDeviation);

					double[] fOutputs = OutputArray[iIndexer - iDataStartOffset];
					fOutputs[DataArrayPos] = fCCIval;
					OutputArray[iIndexer - iDataStartOffset] = fOutputs;
				}
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}

			return true;
		}

		bool CreateCCIdifferences(int FirstValPos, int SecondValPos, int DataArrayPos)
		{
			try
			{
				for (iIndexer = 0; iIndexer < DaysToGenerate; iIndexer++)
				{
					double[] fOutputs = OutputArray[iIndexer];
					fOutputs[DataArrayPos] = fOutputs[FirstValPos] - fOutputs[SecondValPos];
					OutputArray[iIndexer] = fOutputs;
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
