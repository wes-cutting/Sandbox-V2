using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    /// <summary>
    /// Select lists for states.
    /// </summary>
    public static class Months
    {
        static SelectList monthNumbers;

        /// <summary>
        /// Gets a select list of all states.
        /// </summary>
        public static SelectList MonthNumbers
        {
            get
            {
                if (monthNumbers == null)
                    InitializeMonths();
                return monthNumbers;
            }
        }


        /// <summary>
        /// Creates a static dictionary of all the states.
        /// </summary>
        private static void InitializeMonths()
        {
            Dictionary<string, string> months = new Dictionary<string, string>
            {                
			    { "01", "1" },
			    { "02", "2" },
                { "03", "3" },
                { "04", "4" },
                { "05", "5" },
                { "06", "6" },
                { "07", "7" },
                { "08", "8" },
                { "09", "9" },
                { "10", "10" },
                { "11", "11" },
                { "12", "12" },
		    };
            SelectList newList = new SelectList(months, "Value", "Key");
            monthNumbers = newList;
        }
    }
}