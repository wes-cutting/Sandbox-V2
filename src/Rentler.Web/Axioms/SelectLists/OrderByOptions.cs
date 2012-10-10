using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class OrderByOptions
    {       
        static SelectList allOptions;

        /// <summary>
        /// Gets a select list of all states.
        /// </summary>
        public static SelectList All
        {
            get
            {
                if (allOptions == null)
                    InitializeOptions();
                return allOptions;
            }
        }


        /// <summary>
        /// Creates a static dictionary of all the states.
        /// </summary>
        private static void InitializeOptions()
        {
            Dictionary<string, string> options = new Dictionary<string, string>
            {
                { "Newest to Oldest", "NewOld" },
                { "Oldest to Newest", "OldNew" },
                { "Price (High to Low)", "PriceHighLow" },
                { "Price (Low to High)", "PriceLowHigh" }			    
		    };
            SelectList newList = new SelectList(options, "Value", "Key");
            allOptions = newList;
        }
    }
}