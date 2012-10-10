using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Configuration
{
	public sealed class States
	{
		private static volatile States instance;
		private static object syncRoot = new Object();

		private States()
		{
			this.StatesList = new Dictionary<string, string>();
			this.ConfigureStates();
		}

		public Dictionary<string, string> StatesList { get; set; }

		public Dictionary<string,string> StatesListForComparisons { get; set; }

		void ConfigureStates()
		{
			StatesList = new Dictionary<string, string>() { 
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

			StatesListForComparisons = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { 
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
		}

		public static States Current
		{
			get
			{
				if(instance == null)
					lock(syncRoot)
						if(instance == null)
							instance = new States();
				return instance;
			}
		}
	}
}
