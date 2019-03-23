using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Forex_test_data_generator
{
	public class ClsDataFileReader
	{

		/******************************** properties ************************************/

		private DataVector[] m_PriceTensor = { };
		public DataVector[] PriceTensor { get { return m_PriceTensor; } }

		public int NumberOfDaysAvailable { get { return m_PriceTensor.Length; } }

		public string DateOfLastEntry { get { return m_PriceTensor[0].TradeDate; } }

		private string m_LastError = "";
		public string LastError {get { return m_LastError; } }

		private double[] m_closePrices = { };
		public double[] ClosePrices { get{ return m_closePrices; } }

		private string m_CurrencyPairFileName = "";
		public string CurrencyPairFileName { get { return m_CurrencyPairFileName; } }

		
		/******************************** global vars ************************************/
		List<DataVector> DataVectors = new List<DataVector>();


		/******************************* public methods **********************************/
		public bool GetDataFromFile(string FolderPath, bool IsReload = false, bool FixFliers = false)
		{
			char[] SplitChar = { ',' };
			string[] splitArray = { };
			string sLoadPath = "";

			if (!Directory.Exists(FolderPath))
			{ return false; }

			if (!IsReload)
			{
				OpenFileDialog oDlg = new OpenFileDialog
				{
					InitialDirectory = FolderPath,
					Multiselect = false,
					FileName = "Data History Files",
					DefaultExt = ".csv",
					Filter = "comma delimited files (.csv)|*.csv"
				};
				if (!(oDlg.ShowDialog() == DialogResult.OK))
				{
					m_LastError = "File open aborted.";
					return false;
				}
				m_CurrencyPairFileName = oDlg.SafeFileName.Substring(0, oDlg.SafeFileName.LastIndexOf('.'));
				sLoadPath = oDlg.FileName;
			}
			else
			{
				sLoadPath = FolderPath + "\\" + m_CurrencyPairFileName + ".csv";
			}

			StreamReader oReader = new StreamReader(sLoadPath);
			try
			{
				while (oReader.Peek() >= 0)
				{
					// date, time, open, high, low, close, vol
					splitArray = oReader.ReadLine().Split(SplitChar);
					DataVector dv = new DataVector
					{
						TradeDate = splitArray[0],
						TradeTime = splitArray[1],
						Open = double.Parse(splitArray[2]),
						High = double.Parse(splitArray[3]),
						Low = double.Parse(splitArray[4]),
						Close = double.Parse(splitArray[5]),
						Volume = int.Parse(splitArray[6])
					};
					DataVectors.Add(dv);
				}
				oReader.Close();
			}
			catch (Exception e)
			{
				m_LastError = "error reading and parsing data file: " + e.Message;
				if (oReader != null)
				{
					oReader.Close();
					return false;
				}
			}

			if (FixFliers)
			{
				if (!FixDataFliers())
				{
					m_LastError = "error trying to fix fliers in data";
					return false;
				}
			}

			m_closePrices = new double[] { };
			m_PriceTensor = new DataVector[] { };
			if (DataVectors.Count > 0)
			{
				m_PriceTensor = DataVectors.ToArray<DataVector>();
				DataVectors.Clear();
				m_closePrices = new double[m_PriceTensor.Length];
				for (int iIndexer = 0; iIndexer < m_PriceTensor.Length; iIndexer++)
				{
					m_closePrices[iIndexer] = m_PriceTensor[iIndexer].Close;
				}
			}
			return true;
		}

		public bool FixDataFliers()
		{
			// todo: finish fix fliers code

			return true;
		}

	}  // end classs

}  // end namespace
