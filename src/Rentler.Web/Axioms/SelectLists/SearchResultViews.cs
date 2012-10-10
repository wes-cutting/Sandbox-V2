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
    public static class SearchResultViews
    {
        static SelectList allViews;

        /// <summary>
        /// Gets a select list of all states.
        /// </summary>
        public static SelectList AllViews
        {
            get
            {
                if (allViews == null)
                    InitializeViews();
                return allViews;
            }
        }


        /// <summary>
        /// Creates a static dictionary of all the states.
        /// </summary>
        private static void InitializeViews()
        {
            Dictionary<string, string> views = new Dictionary<string, string>
            {
			    { "Grid View", "Grid" },
			    { "Map View", "Map" },			    
		    };
            SelectList newList = new SelectList(views, "Value", "Key");
            allViews = newList;
        }
    }
}