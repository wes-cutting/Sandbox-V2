using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Redis
{
    /// <summary>
    /// Class containing the keys used for the redis
    /// cache throughout the system.
    /// </summary>
    public static class CacheKeys
    {
		public const string LISTING = "rentler:listing:";
        public const string LISTING_VIEWS = "rentler:views:listingviews";

        public const string LISTING_SEARCH_VIEWS = "rentler:views:listingsearchviews";

        public const string TOTAL_LISTING_VIEWS = "rentler:views:totallistingviews";

        public const string TOTAL_SEARCH_VIEWS = "rentler:views:totalsearchviews";

		public const string ZIP_INFOS = "rentler:zipinfos";

		public const string API_KEYS = "rentler:apikeys";

		public const string AUTH_TOKENS = "rentler:authtokens";

		public const string AFFILIATE_USER_IDS = "rentler:affiliateusers";

		public const string USER_IDS = "rentler:userids";
	}
}
