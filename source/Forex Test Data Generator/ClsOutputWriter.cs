using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Forex_test_data_generator
{
	public class ClsOutputWriter
	{
		/**************************** properties ***********************************/
		private string[] m_DatesArray = { };  // input
		public string[] DatesArray { set { m_DatesArray = value; } }

		/* might make this visible for external unscaling of data
		private Dictionary<string, double> m_DataScalingFactors = new Dictionary<string, double>() { {"OHLC", 1.0 } };
		public Dictionary<string,double> DataScalingFactors { get { return m_DataScalingFactors; } }
		*/

		private Dictionary<string, double[][]> m_DataDictionary = new Dictionary<string, double[][]>();  // input
		public Dictionary<string, double[][]> DataDictionary { set { m_DataDictionary = value; } }

		private Dictionary<string, string[]> m_DataLabelsDictionary = new Dictionary<string, string[]>();  // input
		public Dictionary<string, string[]> DataLabelsDictionary { set { m_DataLabelsDictionary = value; } }

		private string m_OutputFileFolder = "";  //  input
		public string OutputFileFolder { get { return m_OutputFileFolder; } set { m_OutputFileFolder = value; } }

		private ScalingTypes m_DataScalingOption = ScalingTypes.scale; // usually -1/1 or 0/1, empty means no scaling
		public ScalingTypes DataScalingOption { get { return m_DataScalingOption; } set { m_DataScalingOption = value; } }

		private string m_LastError = "";  // output
		public string LastError { get { return m_LastError; } }

		private string m_CurrencyPairName = "";  // output
		public string CurrencyPairName { set { m_CurrencyPairName = value; } }


		/**************************** global vars ***********************************/
		Dictionary<string, double> dicScalingFactors = new Dictionary<string, double>();
		List<string> lstOutputArray = new List<string>();
		List<string> lstOutputOrder = new List<string>(); // header sequence to sequence data from data dictionary
		bool bSaveHeader = false, bSaveTime = true, bSaveDates = true;
		bool bIsScaledData = false, bDataIsDeltas = false;

		/**************************** public methods ***********************************/
		public bool WriteTheFile(bool ScaleData, bool DataIsDeltas = false, bool SaveHeader = false, bool SaveDates = true, bool SaveTime = true)
		{
			dicScalingFactors.Clear();  // make sure any old data is gone
			bSaveHeader = SaveHeader;
			bSaveTime = SaveTime;
			bSaveDates = SaveDates;
			bIsScaledData = ScaleData;
			bDataIsDeltas = DataIsDeltas;
			if (m_DataDictionary.Count <= 0 || m_DataLabelsDictionary.Count <= 0)
			{
				m_LastError = "There is missing data or missing labels. Cannot proceed.";
				return false;
			}

			if (!Directory.Exists(m_OutputFileFolder) || m_OutputFileFolder == "")
			{
				m_LastError = "Output folder does not exist.";
				return false;
			}
			if (bIsScaledData)
			{
				if (!ScaleTheData())
				{
					m_LastError = "scaling the output data failed";
					return false;
				}
				//SaveDataScaleRatiosToDisc();
			}

			lstOutputArray.Clear();
			try
			{
				if (bSaveHeader) CreateHeader();
				CreateBody();
				SaveDataFileToDisc();
			}
			catch (Exception e)
			{
				m_LastError = e.Message;
				return false;
			}

			/*
			if (!SavePriceScaleRatioToDisc())
			{
				m_LastError = "Could not save Target scaling ratio to disc.";
				return false;
			}
			*/
			return true;
		}

		/**************************** private methods ***********************************/

		private bool CreateHeader()
		{
			string sHeader = "";
			string[] splitArray = { };
			char[] splitChar = { ',' };

			if (bSaveDates) sHeader += "TradeDate,";
			if (bSaveTime) sHeader += "TradeTime,";
			foreach (KeyValuePair<string, string[]> kvp in m_DataLabelsDictionary)
			{
				//lstOutputOrder.Add(kvp.Key);
				foreach (string s in kvp.Value as string[])
				{
					sHeader += (s + ",");
				}
			}
			sHeader = sHeader.Remove(sHeader.Length - 1);
			lstOutputArray.Add(sHeader);

			return true;
		}

		private bool CreateBody()
		{
			int j = 0, iStop = 0, iIndexer = 0;
			string[] sDataKeyArray = m_DataLabelsDictionary.Keys.ToArray(), splitArray = { };
			string sDataString = "";
			char[] splitChar = { ',' };

			double[][] fDataArray = m_DataDictionary[sDataKeyArray[0]];
			iStop = fDataArray.Length;
			string[] sBodyArray = new string[iStop];
			for (j = 0; j < iStop; j++)
			{
				sDataString = "";
				splitArray = m_DatesArray[j].Split(splitChar);
				if (bSaveDates) sDataString += splitArray[0] + ',';
				if (bSaveTime) sDataString += splitArray[1] + ',';
				foreach (double fData in fDataArray[j])
				{
					sDataString += (fData.ToString("##0.0######") + ",");
				}
				sDataString = sDataString.Remove(sDataString.Length - 1);
				sBodyArray[j] = sDataString;
			}
			// now cycle the rest of the data to the output
			iStop = sDataKeyArray.Length;
			if (iStop > 1)
			{
				int iStart = 0;
				if (bSaveHeader) iStart++; // leave room for the header
				for (iIndexer = iStart; iIndexer < iStop; iIndexer++)
				{
					fDataArray = m_DataDictionary[sDataKeyArray[iIndexer]];
					int iStop2 = fDataArray.Length;
					for (j = 0; j < iStop2; j++)
					{
						sDataString = "";
						foreach (double fData in fDataArray[j])
						{
							sDataString += (fData.ToString("##0.0######") + ",");
						}
						sDataString = sDataString.Remove(sDataString.Length - 1);
						sBodyArray[j] = (sBodyArray[j] + "," + sDataString);
					}
				}
			}
			// now add those body strings to the output string array
			foreach (string s in sBodyArray)
			{
				lstOutputArray.Add(s);
			}
			sBodyArray = new string[] { };
			return true;
		}

		private void SaveDataFileToDisc()
		{
			string sQualifier = "";
			if (bIsScaledData) sQualifier = "-Scaled";
			if (bDataIsDeltas) sQualifier += "-Deltas";
			StreamWriter oWriter = new StreamWriter(m_OutputFileFolder + "\\ForexData" + sQualifier + " - " + m_CurrencyPairName + ".csv", false);
			
			foreach (string s in lstOutputArray)
			{
				oWriter.WriteLine(s);
			}
			oWriter.Flush();
			oWriter.Close();
			oWriter.Dispose();
			oWriter = null;
		}

		/*
		private bool CreateTargetDataBody(TargetTypes m_TargetType)
		{
			lstTargetData.Clear();
			if (m_TargetDataArray.Length <= 0) return true;

			string st = m_TargetType.ToString();
			lstTargetData.Add("PredictDate,TargetData-" + st);
			try
			{
				int iStop = m_TargetDataArray.Length;
				for (int iIndexer = 0; iIndexer < iStop; iIndexer++)
				{
					lstTargetData.Add(m_DatesArray[iIndexer] + "," + m_TargetDataArray[iIndexer].ToString());
				}
			}
			catch (Exception e2)
			{
				m_LastError = e2.Message;
				return false;
			}
			return true;
		}

		private bool SaveTargetFileToDisc()
		{
			if (lstTargetData.Count <= 0) return true;

			StreamWriter oWriter = new StreamWriter(m_OutputFileFolder + "\\ForexTargetData - " + m_CurrencyPairName + ".csv", false);

			foreach (string s in lstTargetData)
			{
				oWriter.WriteLine(s);
			}
			oWriter.Flush();
			oWriter.Close();
			oWriter.Dispose();
			oWriter = null;

			return true;
		}
		*/

		private bool SaveDataScaleRatiosToDisc()
		{

			StreamWriter oWriter = new StreamWriter(m_OutputFileFolder + "\\ForexScalingRatio - " + m_CurrencyPairName + ".csv", false);

			oWriter.WriteLine(dicScalingFactors);

			oWriter.Flush();
			oWriter.Close();
			oWriter.Dispose();
			oWriter = null;

			return true;
		}

		private bool ScaleTheData()
		{
			ClsDataScaler oScaler = new ClsDataScaler();
			oScaler.ScaleTheData(m_DataDictionary, m_DataScalingOption);
			return true;
		}

	}  // end class

}  //  end namespace
