using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class BuildingsAndRibbonsImporter : IRentlerImporter
	{
		public void Import()
		{
			var oldBuildings = new List<Old.Building>();

			Console.WriteLine("Getting old buildings...");
			using(var old = new Old.RentlerNewEntities())
				oldBuildings = old.Buildings.ToList();

			var newUsers = new Dictionary<Guid, int>();
			using(var newContext = new New.RentlerEntities())
				newUsers = newContext.Users
					.Select(u => new { Key = u.ReferenceId, Value = u.UserId })
					.ToDictionary(x => x.Key, y => y.Value);

			Console.WriteLine("Building ribbons map...");

			var oldRibbons = new List<Old.Ribbon>();
			using(var old = new Old.RentlerNewEntities())
				oldRibbons = old.Ribbons.ToList();

			var newRibbons = new Dictionary<string, string>();
			newRibbons.Add("3cargarage", "3 Car Garage");
			newRibbons.Add("bigyard", "Big Yard");
			newRibbons.Add("brandnew", "Brand New");
			newRibbons.Add("greatschools", "Great Schools");
			newRibbons.Add("nearapark", "Near a Park");
			newRibbons.Add("noyardwork", "No Yardwork");
			newRibbons.Add("petfriendly", "Pet Friendly");
			newRibbons.Add("snowremoval", "Snow Removal");
			newRibbons.Add("utilitiesincluded", "Utilities Included");
			newRibbons.Add("newlyremodeled", "Newly Remodeled");
			newRibbons.Add("airconditioning", "Air Conditioning");
			newRibbons.Add("clubhouse", "Clubhouse");
			newRibbons.Add("fencedyard", "Fenced Yard");
			newRibbons.Add("firstmonthfree", "First Month Free");
			newRibbons.Add("fitnesscenter", "Fitness Center");
			newRibbons.Add("granitecountertops", "Granite Countertops");
			newRibbons.Add("hardwoodfloors", "Hardwood Floors");
			newRibbons.Add("hottub", "Hot Tub");
			newRibbons.Add("monthtomonth", "Month to Month");
			newRibbons.Add("newcarpet", "New Carpet");
			newRibbons.Add("newpaint", "New Paint");
			newRibbons.Add("onsitemaintenance", "Onsite Maintenance");
			newRibbons.Add("onsitemanager", "Onsite Manager");
			newRibbons.Add("playground", "Playground");
			newRibbons.Add("pool", "Pool");
			newRibbons.Add("smokerfriendly", "Smoker Friendly");
			newRibbons.Add("stainlessappliances", "Stainless Appliances");
			newRibbons.Add("washerdryer", "Washer/Dryer");
			newRibbons = newRibbons.ToDictionary(x => x.Value, y => y.Key);

			var map = new Dictionary<int, string>();
			foreach(var item in oldRibbons)
				map.Add(item.RibbonId, newRibbons[item.Name]);

			Console.WriteLine("Got old buildings, moving them into the new one.");

			using(var context = new New.RentlerEntities())
			{
				var buildings = new List<New.Building>();
				foreach(var item in oldBuildings)
				{
					var b = new New.Building();
					b.Acres = (float)item.Acres;
					b.Address1 = item.Address1;
					b.Address2 = item.Address2;
					b.ArePetsAllowed = item.PetsAllowed.HasValue ? item.PetsAllowed.Value : false;
					b.Bathrooms = (float)item.Bathrooms;
					b.Bedrooms = item.Bedrooms > 0 ? item.Bedrooms : 1;
					b.BuildingId = item.BuildingId;
					b.City = item.City;
					b.PropertyTypeCode = (int)New.PropertyInfoConverter.Convert(item.PropertyType);
					b.CreateDateUtc = item.CreateDate;
					b.CreatedBy = item.CreatedBy;
					b.DateActivatedUtc = item.DateActivated;
					b.DateAvailableUtc = item.DateAvailable;
					b.Deposit = item.Deposit.HasValue ? item.Deposit.Value : 0;
					b.Description = item.Description;
					b.IsActive = item.IsActive;
					b.IsBackgroundCheckRequired = item.BackgroundCheckRequired;
					b.IsCreditCheckRequired = item.CreditCheckRequired;
					b.IsDeleted = item.IsDeleted;
					b.IsRemovedByAdmin = item.RemovedByAdmin;
					b.IsSmokingAllowed = item.SmokingAllowed;
					b.Latitude = string.IsNullOrWhiteSpace(item.Latitude) ? 0 : float.Parse(item.Latitude);
					b.Longitude = string.IsNullOrWhiteSpace(item.Longitude) ? 0 : float.Parse(item.Longitude);
					b.LeaseLengthCode = (int)New.LeaseLengthConverter.Convert(item.LeaseLength);
					b.PetFee = item.PetFee.HasValue ? item.PetFee.Value : 0;
					b.Price = item.Price.HasValue ? item.Price.Value : 0;
					b.RefundableDeposit = item.RefundableDeposit.HasValue ? item.Price.Value : 0;
					b.SquareFeet = item.SquareFeet > 0 ? item.SquareFeet : 1;
					b.State = item.State;
					b.Title = item.Title;
					b.UpdateDateUtc = item.UpdateDate.HasValue ? item.UpdateDate.Value : DateTime.UtcNow;
					b.UpdatedBy = string.IsNullOrWhiteSpace(item.UpdatedBy) ? "dusda" : item.UpdatedBy;
					b.UserId = newUsers[item.UserId];
					b.YearBuilt = item.YearBuilt;
					b.Zip = item.Zip;
					b.ContactInfoId = (int)item.ContactInfoId;
					b.RibbonId = item.RibbonId.HasValue ? map[item.RibbonId.Value] : null;
					b.PrimaryPhotoId = item.PrimaryPhotoId;
					b.PrimaryPhotoExtension = item.PrimaryPhotoExtension;
					b.HasPriority = false;
					buildings.Add(b);
				}
				context.BulkInsert(buildings, true);
			}

			Console.WriteLine("Awesome, on to photos...");
			oldBuildings.Clear();

		}
	}
}
