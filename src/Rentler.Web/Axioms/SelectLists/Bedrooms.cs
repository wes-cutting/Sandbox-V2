using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class Bedrooms
    {
        private static SelectList bedrooms;

        public static SelectList AllBedrooms
        {
            get
            {
                if (bedrooms == null)
                    InitializeBedrooms();
                return bedrooms;
            }
        }

        private static void InitializeBedrooms()
        {
            Dictionary<string, int?> bedroomGroups = new Dictionary<string, int?>()
            {            
                { "Any", null },
                { "1+", 1 },
                { "2+", 2 },
                { "3+", 3 },
                { "4+", 4 },
                { "5+", 5 },
            };
            SelectList final = new SelectList(bedroomGroups, "Value", "Key");
            bedrooms = final;
        }
    }
}