using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using System.Text.RegularExpressions;

namespace KslListingImporter
{
	public static class RentlerConverter
	{
		public static IEnumerable<Rentler.Data.User> ConvertToUsers(IEnumerable<Member> members)
		{
			Console.WriteLine("Converting Ksl members to Rentler users...");
			var users = new List<User>();

			foreach(var item in members)
			{
				var u = new User()
				{
					UserId = Guid.NewGuid(),
					CreateDate = DateTime.UtcNow,
					CreatedBy = "ksl import",
					Email = item.email,
					FirstName = item.first,
					LastName = item.last,
					PhoneNumber = item.priPhone,
					Username = item.email
				};

				u.AffiliateUser = new AffiliateUser()
				{
					AffiliateUserKey = item.id.ToString(),
					ApiKey = new Guid("DA05C498-C338-4EE4-9A70-6872E38349CC"),
					PasswordHash = item.passwordHash
				};

				users.Add(u);
			}

			return users;
		}

		public static List<Building> ConvertToBuildings(
			IEnumerable<Home> homes, IEnumerable<Classified> classifieds, IEnumerable<HomeCooling> homeCoolings,
			IEnumerable<HomeHeating> homeHeatings, IEnumerable<HomeProperty> homeProperties, IEnumerable<User> users)
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
					Description = item.Description.TruncateElipsed(500),
					IsActive = true
				});

				////heating!
				//var heatingAmenity = new Dictionary<int, string>();
				//heatingAmenity[6] = "Other";
				//heatingAmenity[5] = "Hydronic";
				//heatingAmenity[4] = "Steam Radiant";
				//heatingAmenity[3] = "Geothermal";
				//heatingAmenity[2] = "Radiant Heat";
				//heatingAmenity[1] = "Forced Air";
				//heatingAmenity[0] = null;

				//if(heatingAmenity[int.Parse(item.Heating)] != null)
				//    b.PropertyInfo.PropertyInfoAmenitiesWithOptions.Add(new PropertyInfoAmenitiesWithOption()
				//    {
				//        AmenityId = 3,
				//        Option = heatingAmenity[int.Parse(item.Heating)],
				//        CreateDate = DateTime.UtcNow,
				//        CreatedBy = "ksl import"
				//    });

				////cooling!
				//var coolingAmenity = new Dictionary<int, string>();
				//coolingAmenity[0] = null;
				//coolingAmenity[1] = "Central Air";
				//coolingAmenity[2] = "Evaporative Cooler";
				//coolingAmenity[3] = null;
				//coolingAmenity[4] = null;

				//if(coolingAmenity[int.Parse(item.Cooling)] != null)
				//    b.PropertyInfo.PropertyInfoAmenitiesWithOptions.Add(new PropertyInfoAmenitiesWithOption()
				//    {
				//        AmenityId = 4,
				//        Option = coolingAmenity[int.Parse(item.Cooling)],
				//        CreateDate = DateTime.UtcNow,
				//        CreatedBy = "ksl import"
				//    });


				//todo: garage? Not sure I can translate this one.

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

				//attach user
				var user = users.SingleOrDefault(u => u.AffiliateUser.AffiliateUserKey.Equals(item.UserId));
				if(user == null)
					continue;

				b.UserId = user.UserId;
				buildings.Add(b);
			}

			//Remove html formatting from descriptions.
			Console.WriteLine("Removing Html formatting from descriptions...");
			foreach(var b in buildings)
				foreach(var item in b.Listings)
					item.Description = Regex.Replace(item.Description, "<[^>]*>", string.Empty);

			return buildings;
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

		public static List<LandlordInfo> CreateLandlordInfos(IEnumerable<User> users)
		{
			var infos = new List<LandlordInfo>();

			foreach(var item in users)
			{
				infos.Add(new LandlordInfo
				{
					UserId = item.UserId,
					AllowEmail = false,
					//todo: might need to come back to this and change it.
					AllowPhone = true,
					BackgroundCreditStatus = "Unknown",
					PhoneNumber = item.PhoneNumber
				});
			}

			return infos;
		}
	}
}
