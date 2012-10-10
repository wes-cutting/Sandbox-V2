using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
	public class FeaturedListing
	{
		public int FeaturedListingId { get; set; }
		public long BuildingId { get; set; }
		public Building Building { get; set; }
		public DateTime ScheduledDate { get; set; }
		public string Zip { get; set; }
	}
}
