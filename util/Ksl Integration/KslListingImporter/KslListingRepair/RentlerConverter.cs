using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using System.Text.RegularExpressions;

namespace KslListingRepair
{
	public class RentlerConverter
	{

		public static List<Building> ConvertToBuildingsLight(
			IEnumerable<Home> homes, IEnumerable<Classified> classifieds)
		{
			Console.WriteLine("Scribbling down important information from Ksl...");

			var places = (from c in classifieds
						  join h in homes on c.sid equals h.aid
						  where c.status == "a" &&			//available
								c.market_type == "sale" &&	//we want 'sale' types
								int.Parse(c.nid) != 526
						  select new
						  {
							  UserId = c.userid,
							  Address1 = c.address1,
							  Address2 = c.address2,
							  City = c.city,
							  State = c.state,
							  Zip = c.zip,
							  Latitude = c.lat,
							  Longitude = c.lon,
							  Title = c.title,
							  Description = c.description,
							  Price = c.price,
							  Acres = GetAcres(h.acres),
							  Sqft = h.sqft,
							  Bedrooms = h.bed,
							  Bathrooms = h.bath,
							  Garage = h.garage,
							  Cooling = h.cooling,
							  Heating = h.heating,
							  sid = c.sid,
							  C = c,
							  H = h
						  }).DistinctBy(a => a.H.aid).ToList();

			Console.WriteLine("Converting scribbles to Rentler Buildings...");
			var buildings = new List<Building>();
			foreach(var item in places)
			{
				var b = new Building()
				{
					AddressLine1 = item.Address1,
					AddressLine2 = item.Address2,
					City = item.City,
					State = item.State,
					Zip = item.Zip,
					CreateDate = DateTime.UtcNow,
					CreatedBy = "ksl import",
					Latitude = item.Latitude,
					Longitude = item.Longitude,
					sid = item.sid,

					//add the property info
					PropertyInfo = new PropertyInfo()
					{
						Acres = double.Parse(item.Acres),
						Bathrooms = (int)double.Parse(item.Bathrooms),
						Bedrooms = int.Parse(item.Bedrooms),
						CreateDate = DateTime.UtcNow,
						CreatedBy = "ksl import",
						DateAvailableUtc = DateTime.UtcNow,
						IsAvailable = true,
						Price = decimal.Parse(item.Price),
						SquareFootage = (int)double.Parse(item.Sqft),
						Type = GetPropertyType(int.Parse(item.C.nid))
					}
				};

				//if there is already a listing with the same
				//address, skip it, it's probably a dupe
				//(people like to post dupes a lot here for some reason).
				if(buildings.Any(bu => bu.AddressLine1.Equals(b.AddressLine1) &&
					bu.AddressLine2.Equals(b.AddressLine2)))
				{
					Console.WriteLine("Ignoring duplicate listing: {0}", b.AddressLine1);
					continue;
				}

				//add the listing
				b.Listings.Add(new Listing()
				{
					CreateDate = DateTime.UtcNow,
					CreatedBy = "ksl import",
					DateListedUtc = DateTime.UtcNow,
					Description = item.Description.Truncate(4000),
					IsActive = true
				});

				//rooms!
				for(int i = 0; i < int.Parse(item.Bedrooms); i++)
				{
					b.PropertyInfo.Rooms.Add(new Room()
					{
						CreateDate = DateTime.UtcNow,
						CreatedBy = "ksl import",
						FloorName = "Main Floor",
						Name = "Bedroom"
					});
				}
				for(int i = 0; i < (int)double.Parse(item.Bathrooms); i++)
				{
					b.PropertyInfo.Rooms.Add(new Room()
					{
						CreateDate = DateTime.UtcNow,
						CreatedBy = "ksl import",
						FloorName = "Main Floor",
						Name = "Bathroom"
					});
				}

				buildings.Add(b);
			}

			//Remove html formatting from descriptions.
			Console.WriteLine("Removing Html formatting from descriptions...");
			foreach(var b in buildings)
				foreach(var item in b.Listings)
					item.Description = Regex.Replace(item.Description, "<[^>]*>", string.Empty);

			return buildings;
		}

		static string GetAcres(string sqft)
		{
			double result;
			double.TryParse(sqft, out result);

			double[] valid = new double[] { 0, 0.5, 1.0, 5.0, 10.0 };

			for(int i = 0; i < valid.Length; i++)
				if(valid[i] >= result)
					return valid[i].ToString();

			return "10.0";
		}

		static string GetPropertyType(int nid)
		{
			/*
			276,Homes,Apartments For Rent
			278,Homes,Homes For Rent
			525,Homes,Horse/Livestock Property For Rent
			358,Homes,Manufactured Homes For Rent
			356,Homes,Townhomes/Condos For Rent
			526,Homes,Vacation/Recreational Housing For Rent
			 */

			/*	Property Types *
				Horse / Livestock = Horse / Livestock
				Manufactured Homes = Manufactured Home
				Apartments = Apartments	
				Townhouse / Condo = Townhouse / Condo
				Single-Family = Single-Family Home
				Not Specified = ...Just roll them into Single-Family Home
				Cabins / Recreational = new category
			*/

			switch(nid)
			{
				case 276:
					return "Apartment";
				case 278:
					return "Single-Family Home";
				case 525:
					return "Horse/Livestock";
				case 358:
					return "Manufactured Home";
				case 356:
					return "Condo/Townhome";
				default:
					return "Single-Family Home";
			}
		}
	}
}
