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
    public static class States
    {
        static SelectList allStates;

        /// <summary>
        /// Gets a select list of all states.
        /// </summary>
        public static SelectList AllStates
        {
            get
            {
                if (allStates == null)
                    InitializeStates();
                return allStates;
            }
        }


        /// <summary>
        /// Creates a static dictionary of all the states.
        /// </summary>
        private static void InitializeStates()
        {
            Dictionary<string, string> states = new Dictionary<string, string>
            {
			    { "", "" },
			    { "Alabama", "AL" },
			    { "Alaska", "AK" },
			    { "American Samoa", "AS" },
			    { "Arizona", "AZ" },
			    { "Arkansas", "AR" },
			    { "California", "CA" },
			    { "Colorado", "CO" },
			    { "Connecticut", "CT" },
			    { "Delaware", "DE" },
			    { "District of Columbia", "DC" },
			    { "Federated States of Micronesia", "FM" },
			    { "Florida", "FL" },
			    { "Georgia", "GA" },
			    { "Guam", "GU" },
			    { "Hawaii", "HI" },
			    { "Idaho", "ID" },
			    { "Illinois", "IL" },
			    { "Indiana", "IN" },
			    { "Iowa", "IA" },
			    { "Kansas", "KS" },
			    { "Kentucky", "KY" },
			    { "Louisiana", "LA" },
			    { "Maine", "ME" },
			    { "Marshall Islands", "MH" },
			    { "Maryland", "MD" },
			    { "Massachusetts", "MA" },
			    { "Michigan", "MI" },
			    { "Minnesota", "MN" },
			    { "Mississippi", "MS" },
			    { "Missouri", "MO" },
			    { "Montana", "MT" },
			    { "Nebraska", "NE" },
			    { "Nevada", "NV" },
			    { "New Hampshire", "NH" },
			    { "New Jersey", "NJ" },
			    { "New Mexico", "NM" },
			    { "New York", "NY" },
			    { "North Carolina", "NC" },
			    { "North Dakota", "ND" },
			    { "Northern Mariana Islands", "MP" },
			    { "Ohio", "OH" },
			    { "Oklahoma", "OK" },
			    { "Oregon", "OR" },
			    { "Palau", "PW" },
			    { "Pennsylvania", "PA" },
			    { "Puerto Rico", "PR" },
			    { "Rhode Island", "RI" },
			    { "South Carolina", "SC" },
			    { "South Dakota", "SD" },
			    { "Tennessee", "TN" },
			    { "Texas", "TX" },
			    { "Utah", "UT" },
			    { "Vermont", "VT" },
			    { "Virgin Islands", "VI" },
			    { "Virginia", "VA" },
			    { "Washington", "WA" },
			    { "West Virginia", "WV" },
			    { "Wisconsin", "WI" },
			    { "Wyoming", "WY" }
		    };
            SelectList newList = new SelectList(states, "Value", "Key");
            allStates = newList;
        }
    }
}