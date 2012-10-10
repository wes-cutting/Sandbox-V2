using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class Years
    {
        static SelectList expirationYears;

        /// <summary>
        /// Gets a select list of all states.
        /// </summary>
        public static SelectList ExpirationYears
        {
            get
            {
                if (expirationYears == null)
                    InitializeExpirationYears();
                return expirationYears;
            }
        }


        /// <summary>
        /// Creates a static dictionary of all the states.
        /// </summary>
        private static void InitializeExpirationYears()
        {
            Dictionary<string, string> years = new Dictionary<string, string>();
            years.Add("", "");
            
            for (int i = DateTime.Now.Year; i <= (DateTime.Now.Year + 20); ++i)
                years.Add(i.ToString(), i.ToString());

            SelectList newList = new SelectList(years, "Value", "Key");
            expirationYears = newList;
        }
    }
}