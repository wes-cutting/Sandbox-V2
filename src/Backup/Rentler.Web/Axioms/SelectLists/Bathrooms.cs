using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class Bathrooms
    {
        private static SelectList bathrooms;

        public static SelectList AllBathrooms
        {
            get
            {
                if (bathrooms == null)
                    InitializeAcres();
                return bathrooms;
            }
        }

        private static void InitializeAcres()
        {
            Dictionary<string, int?> bathroomGroups = new Dictionary<string, int?>()
            {            
                { "Any", null },
                { "1+", 1 },
                { "2+", 2 },
                { "3+", 3 },
                { "4+", 4 },
                { "5+", 5 },
            };
            SelectList final = new SelectList(bathroomGroups, "Value", "Key");
            bathrooms = final;
        }
    }
}