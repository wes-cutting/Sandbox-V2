using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Extensions;
using Rentler.Data;

namespace Rentler.Adapters
{
	public interface IFeaturedAdapter
	{
		void AddFeatured(Building building, List<DateTime> days);
		Status<List<BuildingPreview>> GetFeatured(string[] zips);

		Status<List<FeaturedListing>> GetFeaturedDates();
	}

    public class FeaturedAdapter : IFeaturedAdapter
    {
        IFriendlyZipAdapter zipAdapter;

        class BuildingPreviewTemp : BuildingPreview
        {
            public string Zip { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }

        public FeaturedAdapter(IFriendlyZipAdapter zipAdapter)
        {
            this.zipAdapter = zipAdapter;
        }

        public void AddFeatured(Building building, List<DateTime> days)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            var today = DateTime.UtcNow.AddHours(tzi.GetUtcOffset(DateTime.Now).Hours);

            //make sure the featured days are scheduled to exactly midnight 
            //of the day they've been purchased.
            for (int i = 0; i < days.Count; i++)
                days[i] = days[i].Date.AddHours(-tzi.GetUtcOffset(days[i].Date).Hours);

            using (var context = new RentlerContext())
            {
                foreach (var item in days)
                {
                    context.FeaturedListings.Add(new FeaturedListing
                    {
                        BuildingId = building.BuildingId,
                        ScheduledDate = item,
                        Zip = building.Zip
                    });
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Pseudo-randomly selects up to three featured 
        /// items that match a list of zip codes.
        /// </summary>
        /// <param name="zips">A list of zip codes to use to select featured items by location.</param>
        /// <returns>A list of of up to three featured items.</returns>
        public Status<List<BuildingPreview>> GetFeatured(string[] zips)
        {
            var items = new List<BuildingPreviewTemp>();

            var tzi = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            var today = DateTime.UtcNow.AddHours(tzi.GetUtcOffset(DateTime.Now).Hours);

            //offset the time forward so we have the correct time in UTC
            //for the date to point to midnight LOCAL to the Time Zone.
            //For example, 4/23/2012 MST is midnight; in UTC with
            //Daylight Saving Time, it is 6:00am.
            today = today.Date.ToUniversalTime().AddHours(-tzi.GetUtcOffset(today).Hours);

            //let's grab us some featured stuff.
            using (var context = new RentlerContext())
                items = context.FeaturedListings
                    .Include("Building")
                    .Where(f => f.ScheduledDate == today &&
								f.Building.IsActive &&
								!f.Building.IsDeleted &&
								!f.Building.IsRemovedByAdmin)
                    .Select(f => new BuildingPreviewTemp
                    {
                        Address1 = f.Building.Address1,
                        Bathrooms = f.Building.Bathrooms.Value,
                        Bedrooms = f.Building.Bedrooms.Value,
                        City = f.Building.City,
                        IsActive = f.Building.IsActive,
                        IsFeatured = true,
                        IsRemovedByAdmin = f.Building.IsRemovedByAdmin,
                        Price = f.Building.Price,
                        PrimaryPhotoExtension = f.Building.PrimaryPhotoExtension,
                        BuildingId = f.Building.BuildingId,
                        PrimaryPhotoId = f.Building.PrimaryPhotoId,
                        RibbonId = f.Building.RibbonId,
                        State = f.Building.State,
                        Title = f.Building.Title,
                        Zip = f.Building.Zip,
                        Latitude = f.Building.Latitude,
                        Longitude = f.Building.Longitude
                    }).ToList();

            //no featured today? just return.
            if (items.Count == 0)
                return Status.OK(new List<BuildingPreview>());

            var results = new List<BuildingPreviewTemp>();

            //no location data? just return three, randomly.
            if (zips.Length == 0)
                results = items.Shuffle().Take(3).ToList<BuildingPreviewTemp>();
            else
                //find any featured items that match the location, then 
                //shuffle and take up to three, randomly
                results = items.Where(i => zips.Contains(i.Zip))
                               .Shuffle().Take(3).ToList<BuildingPreviewTemp>();

            /*
                If we haven't found any, then we need to find the closest location
                that has some featured items.
            */

            //none in the zips we have? try to find the closest zip that has any
            if (results.Count == 0)
            {
                var zi = zipAdapter.GetZipInfo(items.Select(i => i.Zip).ToArray());

                int take = 3;
                float lat, lng = 0;


                //use the average lat/lng of the 
                //results we have to get a target.
                lat = zi.Average(z => z.Latitude);
                lng = zi.Average(z => z.Longitude);

                //calculate distance from our target
                //to the other featured items, order them closest-to-farthest
                var rank = items
                    .Select(i => new KeyValuePair<double, BuildingPreviewTemp>(
                        GetDistance(lat, lng, i.Latitude, i.Longitude), i))
                    .OrderBy(i => i.Key)
                    .Select(t => t.Value)
                    .Take(take).ToList();

                results.AddRange(rank);
            }

            /*
                okay, from this point, if we have less than three, we need to get
                three if we can by expanding the net.
            */

            //first, if we don't have enough to work with, 
            //don't bother. Otherwise, get a target.
            if (items.Count >= 3 && results.Count < 3)
            {
                //ignore any items we already have
                items.RemoveAll(i => results.Any(r => r.BuildingId == i.BuildingId));

                int take = 3 - results.Count;
                float lat, lng = 0;

                //use the average lat/lng of the 
                //results we have to get a target.
                lat = results.Average(r => r.Latitude);
                lng = results.Average(r => r.Longitude);

                //calculate distance from our target to the other 
                //featured items, ordered closest-to-farthest
                var rank = items
                    .Select(i => new KeyValuePair<double, BuildingPreviewTemp>(
                        GetDistance(lat, lng, i.Latitude, i.Longitude), i))
                    .OrderBy(i => i.Key).ToList();

                //and take up to whatever we need (up to 2)
                results.AddRange(rank.Select(r => r.Value).Take(take));
            }

            return Status.OK(results.ToList<BuildingPreview>());
        }

        /// <summary>
        /// Gets the distance between two lat/lng's in miles.
        /// </summary>
        /// <param name="lat1">The lat1.</param>
        /// <param name="lon1">The lon1.</param>
        /// <param name="lat2">The lat2.</param>
        /// <param name="lon2">The lon2.</param>
        /// <remarks>Uses the Haversine "as the crow flies" formula.</remarks>
        /// <returns>The distance in miles.</returns>
        public double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist =
                Math.Sin(DegToRad(lat1)) * Math.Sin(DegToRad(lat2)) +
                Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) *
                Math.Cos(DegToRad(theta));

            dist = Math.Acos(dist);
            dist = RadToDeg(dist);
            dist = dist * 60 * 1.1515;

            return (dist);
        }

        static double DegToRad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        static double RadToDeg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        public Status<List<FeaturedListing>> GetFeaturedDates()
        {
            using (var context = new RentlerContext())
            {
                var tzi = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
                var today = DateTime.UtcNow.AddHours(tzi.GetUtcOffset(DateTime.Now).Hours);
                today = today.Date.ToUniversalTime().AddHours(-tzi.GetUtcOffset(today).Hours);

                var dates = (from f in context.FeaturedListings
                             where f.ScheduledDate >= today
                             select f).ToList();

                return Status.OK(dates);
            }
        }
    }
}
