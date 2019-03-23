using System;
using System.Collections;
using System.Linq;

namespace Forex_test_data_generator
{

	public class DataVector : IEnumerable 
	{
		public string TradeDate, TradeTime;
		public double Open, High, Low, Close;
		public Int64 Volume;

		// you'll have to cast the return value to it's proper type
		public object this[string ItemName]
		{
			get
			{
				switch (ItemName)
				{
					case "Close":
						return Close;
					case "Date":
						return TradeDate;
					case "Time":
						return TradeTime;
					case "Open":
						return Open;
					case "High":
						return High;
					case "Low":
						return Low;
					default:
						return null;
				}
			}
			set
			{
				switch (ItemName)
				{
					case "Close":
						Close = (double)value;
						break;
					case "Date":
						TradeDate = (string)value;
						break;
					case "Time":
						TradeTime = (string)value;
						break;
					case "Open":
						Open = (double)value;
						break;
					case "High":
						High = (double)value;
						break;
					case "Low":
						Low = (double)value;
						break;
					default:
						throw new Exception("no such field as - " + ItemName);
				}
			}
		}

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}  // end class

}  // end namespace
