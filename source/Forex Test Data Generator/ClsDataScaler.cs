using System;
using System.Collections.Generic;
using System.Linq;

namespace Forex_test_data_generator
{
	/*######################################################*/
	public enum ScalingTypes { std, norm, scale }
	/*######################################################*/

	/*######################################################*/
	public class ClsDataScaler
	{

		/*************************** properties ***************************/
		string m_LastError = "";
		public string LastError {get{ return m_LastError; }set{ m_LastError = value; } }

		/**************************** global vars ******************************/

		/**************************** constructors ************************/

		/********************** public methods **************************/
		public double[] ScaleTheData(double[] InputData, ScalingTypes ScalingOption = ScalingTypes.scale)
		{
			throw new Exception("ScaleTheData:double[] not implemented!");
		}

		public double[][] ScaleTheData(double[][] InputData, ScalingTypes ScalingOption = ScalingTypes.scale)
		{
			throw new Exception("ScaleTheData:double[][] not implemented!");
		}

		/// <summary>
		/// used by output data writer to scale before parsing and saving the output
		/// </summary>
		/// <param name="InputDataArray"></param>
		/// <returns>returns original dictionary with scaled data</returns>
		public Dictionary<string, double[][]> ScaleTheData(Dictionary<string, double[][]> InputDataArray, ScalingTypes ScalingOption = ScalingTypes.scale)
		{
			int iCount = InputDataArray.Count;
			string[] sKeys = InputDataArray.Keys.ToArray<string>();
			string sKey = ""; // holds the curren key

			try
			{
				for (int iIndexer = 0; iIndexer < iCount; iIndexer++)
				{
					sKey = sKeys[iIndexer];
					if (sKey.Contains("#NS")) continue; // 'no scale'
					if (ScalingOption == ScalingTypes.std)
						StandardizeATensor(InputDataArray[sKey]);
					else if (ScalingOption == ScalingTypes.norm)
						NormalizeAtensor(InputDataArray[sKey]);
					else
						ScaleATensor(InputDataArray[sKey]);
				}
			}
			catch (Exception e)
			{
				m_LastError = "Error in scaling data! " + e.Message;
				throw new Exception(m_LastError);
			}
			return InputDataArray;

		}


		/************************* private methods *************************/

		/// <summary>
		/// used primarily for -1 to 1 range of output
		/// </summary>
		/// <param name="TensorIn"></param>
		private void ScaleATensor(double[][] TensorIn)
		{
			int iLength = TensorIn.Length;
			int iRank = TensorIn[0].Length;
			double fTemp = 0.0;

			//---- first run it through the normalizer to scale it 0 to 1
			NormalizeAtensor(TensorIn);
			//---- now spool through it and change the range to -1 to +1
			for (int iIndexer = 0; iIndexer < iLength; iIndexer++)
			{
				for (int jIndexer = 0; jIndexer < iRank; jIndexer++)
				{
					fTemp = TensorIn[iIndexer][jIndexer];
					fTemp *= 2.0f;
					fTemp -= 1.0f;
					if (fTemp > 1.00000) fTemp = 1.000000f;
					if (fTemp < -1.000000) fTemp = -1.000000f;
					TensorIn[iIndexer][jIndexer] = fTemp;
				} // for jIndexer
			} // for iIndexer
		}

		/// <summary>
		/// makes it into zero to one range
		/// </summary>
		/// <param name="TensorIn"></param>
		private void NormalizeAtensor(double[][] TensorIn)
		{
			int iRank = TensorIn[0].Length;
			double[] fMinArray = new double[iRank];
			double[] fMaxArray = new double[iRank];
			int iLength = TensorIn.Length;
			double fTemp = 0.0;

			//---- first find min and max
			// intialize starting values with real values
			for (int jIndexer = 0; jIndexer < iRank; jIndexer++)
			{
				fMaxArray[jIndexer] = TensorIn[0][jIndexer];
				fMinArray[jIndexer] = TensorIn[0][jIndexer];
			}
			// now seek the mins and maxes
			for (int iIndexer = 0; iIndexer < iLength; iIndexer++)
			{
				for (int jIndexer = 0; jIndexer < iRank; jIndexer++)
				{
					fTemp = TensorIn[iIndexer][jIndexer];
					if (fTemp < fMinArray[jIndexer])
					{
						fMinArray[jIndexer] = fTemp;
					} // if min
					if (fTemp > fMaxArray[jIndexer])
					{
						fMaxArray[jIndexer] = fTemp;
					} // if max
				}  // for jIndexer
			} // for iIndexer
			  //---- now scale the data
			for (int iIndexer = 0; iIndexer < iLength; iIndexer++)
			{
				for (int jIndexer = 0; jIndexer < iRank; jIndexer++)
				{
					TensorIn[iIndexer][jIndexer] = (TensorIn[iIndexer][jIndexer] - fMinArray[jIndexer]) / (fMaxArray[jIndexer] - fMinArray[jIndexer]);
				} // for jIndexer
			} // for iIndexer

		}

		/// <summary>
		/// zero mean, unit variance output (x number of std deviations)
		/// </summary>
		/// <param name="TensorIn"></param>
		private void StandardizeATensor(double[][] TensorIn)
		{
			int iRank = TensorIn[0].Length;
			double[] fMedianArray = new double[iRank];

			//---- find the median of each column
			int iLength = TensorIn.Length;
			for (int iIndexer = 0; iIndexer < iLength; iIndexer++)
			{
				for (int jIndexer = 0; jIndexer < iRank; jIndexer++)
				{
					fMedianArray[jIndexer] += (double)TensorIn[iIndexer][jIndexer];
				}
			}
			for (int iIndexer = 0; iIndexer < iRank; iIndexer++)
			{
				fMedianArray[iIndexer] = (fMedianArray[iIndexer] / (double)iLength);
			}

			//---- now get the SD of each column
			double[] fSDarray = new double[iRank];
			for (int iIndexer = 0; iIndexer < iLength; iIndexer++)
			{
				for (int jIndexer = 0; jIndexer < iRank; jIndexer++)
				{
					fSDarray[jIndexer] += Math.Pow((TensorIn[iIndexer][jIndexer] - fMedianArray[jIndexer]), 2);
				}
			}
			for (int iIndexer = 0; iIndexer < iRank; iIndexer++)
			{
				fSDarray[iIndexer] = Math.Sqrt(fSDarray[iIndexer] / ((double)(iLength - 1)));
			}
			// fSDarray now containst the SD's of each column

			//---- now scale the data to the SD and then return the data tensor
			for (int iIndexer = 0; iIndexer < iLength; iIndexer++)
			{
				for (int jIndexer = 0; jIndexer < iRank; jIndexer++)
				{
					TensorIn[iIndexer][jIndexer] = (TensorIn[iIndexer][jIndexer] - fMedianArray[jIndexer]) / fSDarray[jIndexer];
				}
			}
		}

	}  //  end class

}  //  end namespace
