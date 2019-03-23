using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Forex_test_data_generator
{
	public class ClsVolatility
	{
		/******************************* properties **********************************/
		private int m_VolitilityPeriod = 0;
		public int VolitilityPeriod { get { return m_VolitilityPeriod; } set { m_VolitilityPeriod = value; } }

		private int m_DaysToGenerate = 0;
		public int DaysToGenerate { get { return m_DaysToGenerate; } set { m_DaysToGenerate = value; } }

		private string m_LastError = "";
		public string LastError { get { return m_LastError; } }

		/****************************** global vars ***********************************/
		double[] lstOutputArray = null; // holds data internally until called for after data is created
		double[] fClosingPrices = { }; // the input data
		int iIndexer = 0, jIndexer = 0;

		/******************************* public methods **********************************/
		public bool CreateVolatilityData(double[] ClosePrices)
		{
			int iStop = ClosePrices.Length - m_VolitilityPeriod;
			fClosingPrices = new double[ClosePrices.Length];
			Array.ConstrainedCopy(ClosePrices, 0, fClosingPrices, 0, ClosePrices.Length);
			lstOutputArray = new double[m_DaysToGenerate];
			double fMedian = 0.0, fVariance = 0.0;

			try
			{
				if ((m_VolitilityPeriod + m_DaysToGenerate) > ClosePrices.Length)
				{
					throw new Exception("data size mismatch in Volitility Create()", new Exception("period plus days not equal to incoming price data length"));
				}

				// the outer loop
				for (iIndexer = 0; iIndexer < iStop; iIndexer++)
				{
					fMedian = 0.0;
					fVariance = 0.0;
					//---- find median
					for (jIndexer = 0; jIndexer < m_VolitilityPeriod; jIndexer++)
					{
						fMedian += fClosingPrices[iIndexer + jIndexer];
					}
					fMedian /= m_DaysToGenerate;
					//---- find the variance
					for (jIndexer = 0; jIndexer < m_VolitilityPeriod; jIndexer++)
					{
						fVariance += Math.Pow((fClosingPrices[iIndexer + jIndexer] - fMedian), 2);
					}
					fVariance /= m_DaysToGenerate;
					lstOutputArray[iIndexer] = Math.Sqrt(fVariance);
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine("Error in CreateVolatilityData(): " + e.Message + "   stacktrace = " + e.StackTrace);
				m_LastError = "Error in CreateVolatilityData(): " + e.Message;
				return false;
			}
			return true;
		}

		public double[][] GetVolatilityData(int Days)
		{
			double[][] retVal = new double[Days][];
			//Array.ConstrainedCopy(lstOutputArray, lstOutputArray.Length - Days, retVal, 0, Days);
			int iStart = lstOutputArray.Length - m_DaysToGenerate;
			int iStop = lstOutputArray.Length;
			for (iIndexer = iStart; iIndexer < iStop; iIndexer++)
			{
				retVal[iIndexer] = new double[] { lstOutputArray[iIndexer] };
			}

			return retVal;
		}

		public double[][] GetVolatilityData()
		{
			return GetVolatilityData(lstOutputArray.Length);
		}



	}  // end class

}  // end namespace
