using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Models
{
    public class ListingIndexModel
    {        
        public Building Listing { get; set; }
        
        public string FullAddress
        {
            get
            {
                if (Listing == null)
                    return string.Empty;

                return string.Format("{0} {1}, {2}, {3} {4}",
                    Listing.Address1, Listing.Address2, Listing.City,
                    Listing.State, Listing.Zip);
            }
        }
        
        public DateTime? ExpirationDate 
        {
            get
            {
                if (Listing.DateActivatedUtc.HasValue)
                    return Listing.DateActivatedUtc.Value.AddDays(30).ToLocalTime();

                return null;
            }
        }

        public TimeSpan? TimeOnline
        {
            get
            {
                if (Listing.DateActivatedUtc.HasValue)
                    return DateTime.UtcNow - Listing.DateActivatedUtc.Value;

                return null;
            }
        }

        public TimeSpan? TimeLeft
        {
            get
            {
                if (ExpirationDate.HasValue)                
                    return ExpirationDate - DateTime.UtcNow;                

                return null;
            }
        }

        public bool UserHasSaved { get; set; }

        public long ListingViews { get; set; }
    }

	public class ListingInterestedModel
	{
		public long BuildingId { get; set; }
		public string Content { get; set; }
	}
}