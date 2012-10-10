using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class SquareFeet
    {
        private static SelectList squareFeetLowestToHighest;
        private static SelectList squareFeetHighestToLowest;

        public static SelectList AllSquareFeetLowestToHighest
        {
            get
            {
                if (squareFeetLowestToHighest == null)
                    Initialize();
                return squareFeetLowestToHighest;
            }
        }

        public static SelectList AllSquareFeetHighestToLowest
        {
            get
            {
                if (squareFeetHighestToLowest == null)
                    Initialize();
                return squareFeetHighestToLowest;
            }
        }

        private static void Initialize()
        {
            Dictionary<string, int?> newItems = new Dictionary<string, int?>()
            {            
                { "500", 500 },
                { "1000", 1000 },
                { "1500", 1500 },
                { "2000", 2000 },
                { "2500", 2500 },
                { "3000", 3000 },
                { "3500", 3500 },
                { "4000", 4000 },
                { "4500", 4500 },
                { "5000", 5000 },
                { "5500", 5500 },
                { "6000", 6000 },
                { "6500", 6500 },
                { "7000", 7000 },
                { "7500", 7500 },
                { "8000", 8000 },
                { "8500", 8500 },
                { "9000", 9000 },
                { "9500", 9500 },
                { "10000", 10000 },
            };

            // add all the items to a new dictionary
            Dictionary<string, int?> lowToHigh = new Dictionary<string, int?>() { { "Any", null } };
            foreach (var item in newItems.Keys)
                lowToHigh.Add(item, newItems[item]);

            // reverse them
            var result = newItems.OrderByDescending(m => m.Value);

            // add them again to another one but backwards
            Dictionary<string, int?> highToLow = new Dictionary<string, int?>() { { "Any", null } };
            foreach (var item in result)
                highToLow.Add(item.Key, item.Value);

            // create the select lists
            squareFeetLowestToHighest = new SelectList(lowToHigh, "Value", "Key");
            squareFeetHighestToLowest = new SelectList(highToLow, "Value", "Key");
        }
    }
}