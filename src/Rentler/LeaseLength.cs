using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Rentler
{
    public enum LeaseLength
    {
        [Description("Month to Month")]
        MonthToMonth = -1,

        [Description("1 Month")]
        OneMonth = 30,

        [Description("2 Months")]
        TwoMonths = 60,

        [Description("3 Months")]
        ThreeMonths = 90,

        [Description("4 Months")]
        FourMonths = 120,

        [Description("5 Months")]
        FiveMonths = 150,

        [Description("6 Months")]
        SixMonths = 180,

        [Description("1 Year")]
        Year = 365,

        [Description("18 Months")]
        EighteenMonths = 540,

        [Description("2 Years")]
        TwoYears = 730,

        [Description("2+ Years")]
        TwoPlusYears = 731,

        [Description("Rent to own/Lease option")]
        RentToOwn = -2,

		[Description("Undefined")]
		Undefined = 0
    }
}
