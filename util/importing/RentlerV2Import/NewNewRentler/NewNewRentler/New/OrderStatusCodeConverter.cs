using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.New
{
	public class OrderStatusCodeConverter
	{
		public static int Convert(string type)
		{
			int result = 0;

			switch (type)
			{
				case "ServiceUnavailable":
					result = 5;
					break;
				case "Succeeded":
					result = 2;
					break;
				case "CardDeclined":
					result = 4;
					break;
				default:
					result = 3;
					break;
			}

			return result;
		}
	}
}
