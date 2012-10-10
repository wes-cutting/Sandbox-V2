using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class Acres
    {
        private static SelectList acres;

        public static SelectList AllAcres
        {
            get
            {
                if (acres == null)
                    InitializeAcres();
                return acres;
            }
        }

        private static void InitializeAcres()
        {
            Dictionary<string, double> acreGroups = new Dictionary<string, double>()
            {            
                { "Less than .25", 0.0D },
                { ".25", 0.25D },
                { ".25+", 0.26D },
                { ".33", 0.33D },
                { ".33+", 0.34D },
                { ".5", 0.5D },
                { ".5+", 0.51D },
                { ".75", 0.75D },
                { ".75+", 0.76D },
                { "1", 1.0D },
                { "1+", 1.1D },
                { "1.25", 1.25D },
                { "1.25+", 1.26D },
                { "1.33", 1.33D },
                { "1.33+", 1.34D },
                { "1.5", 1.5D },
                { "1.5+", 1.51D },
                { "2", 2.0D },
                { "2+", 2.1D },
                { "3", 3.0D },
                { "3+", 3.1D },
                { "5", 5.0D },
                { "5+", 5.1D },
                { "10", 10.0D },
                { "10+", 10.1D }
            };
            SelectList final = new SelectList(acreGroups, "Value", "Key");
            acres = final;
        }
    }
}