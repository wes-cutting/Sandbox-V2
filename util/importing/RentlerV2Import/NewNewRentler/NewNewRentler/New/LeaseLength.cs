using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.New
{

	public enum LeaseLength
	{
		MonthToMonth = 0,
		OneMonth = 30,
		TwoMonths = 60,
		ThreeMonths = 90,
		FourMonths = 120,
		FiveMonths = 150,
		SixMonths = 180,
		Year = 365,
		EighteenMonths = 540,
		TwoYears = 730,
		TwoPlusYears = 731,
		RentToOwn = -1,
		Undefined = -2
	}

	public static class LeaseLengthConverter
	{
		public static LeaseLength Convert(string type)
		{
			var p = LeaseLength.Undefined;

			switch(type)
			{
				case "4 Months":
					p = LeaseLength.FourMonths;
					break;
				case "3 Months" :
					p = LeaseLength.ThreeMonths;
					break;
				case "1 Month" :
					p = LeaseLength.OneMonth;
					break;
				case "2+ Years":
					p = LeaseLength.TwoPlusYears;
					break;
				case "2 Months" :
					p = LeaseLength.TwoMonths;
					break;
				case "1 Year" :
					p = LeaseLength.Year;
					break;
				case "6 Months" :
					p = LeaseLength.SixMonths;
					break;
				case "Rent to own/Lease option":
					p = LeaseLength.RentToOwn;
					break;
				case "5 Months":
					p = LeaseLength.FiveMonths;
					break;
				case "2 Years":
					p = LeaseLength.TwoYears;
					break;
				case "Month to Month":
					p = LeaseLength.MonthToMonth;
					break;
				case "18 Months":
					p = LeaseLength.EighteenMonths;
					break;
				default:
					p = LeaseLength.Undefined;
					break;
			}

			return p;
		}
	}
}
