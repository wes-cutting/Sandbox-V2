using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class FeaturedListingsImporter : IRentlerImporter
	{
		public void Import()
		{
			var oldFeatures = new List<Old.FeaturedListing>();
			using(var context = new Old.RentlerNewEntities())
				oldFeatures = context.FeaturedListings.Include("Building").ToList();

			//buildingid, scheduleddate, zip
			var newFeatures = new List<New.FeaturedListing>();
			foreach(var item in oldFeatures)
			{
				newFeatures.Add(new New.FeaturedListing
				{
					BuildingId = item.BuildingId,
					ScheduledDate = item.ScheduledDate,
					Zip = item.Building.Zip
				});
			}

			using(var context = new New.RentlerEntities())
				context.BulkInsert(newFeatures);
		}
	}
}
