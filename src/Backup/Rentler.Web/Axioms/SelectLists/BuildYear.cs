using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class BuildYear
    {
        private static SelectList buildYearLowestToHighest;
        private static SelectList buildYearHighestToLowest;

        public static SelectList AllBuildYearLowestToHighest
        {
            get
            {
                if (buildYearLowestToHighest == null)
                    Initialize();
                return buildYearLowestToHighest;
            }
        }

        public static SelectList AllBuildYearHighestToLowest
        {
            get
            {
                if (buildYearHighestToLowest == null)
                    Initialize();
                return buildYearHighestToLowest;
            }
        }

        private static void Initialize()
        {
            // add all the years
            Dictionary<string, int?> years = new Dictionary<string,int?>();
            for(int x = 1896; x <= DateTime.UtcNow.Year; ++x)
                years.Add(x.ToString(), x);

            // add all the items to a new dictionary
            Dictionary<string, int?> lowToHigh = new Dictionary<string, int?>() { { "Any", null } };
            foreach (var item in years.Keys)
                lowToHigh.Add(item, years[item]);

            // reverse them
            var result = years.OrderByDescending(m => m.Value);

            // add them again to another one but backwards
            Dictionary<string, int?> highToLow = new Dictionary<string, int?>() { { "Any", null } };
            foreach (var item in result)
                highToLow.Add(item.Key, item.Value);

            // create the select lists
            buildYearLowestToHighest = new SelectList(lowToHigh, "Value", "Key");
            buildYearHighestToLowest = new SelectList(highToLow, "Value", "Key");
        }
    }
}