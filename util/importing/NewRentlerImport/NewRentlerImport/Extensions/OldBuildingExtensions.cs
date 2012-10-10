using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewRentlerImport.Extensions
{
	public static class OldBuildingExtensions
	{
		public static OldInfo ToOldInfo(this Building b)
		{
			return new OldInfo
			{
				Building = b,
				PropertyInfo = b.PropertyInfo,
				Listing = b.Listings.Any() ? b.Listings.First() : null
			};
		}
	}

	public class OldInfo
	{
		public Building Building { get; set; }
		public PropertyInfo PropertyInfo { get; set; }
		public Listing Listing { get; set; }

		public New.Building ConvertToBuilding()
		{
			var b = new New.Building();

			//building
			b.BuildingId = this.Building.BuildingId;
			b.UserId = this.Building.UserId;
			b.Address1 = this.Building.AddressLine1;
			b.Address2 = this.Building.AddressLine2;
			b.City = this.Building.City;
			b.State = this.Building.State;
			b.Zip = this.Building.Zip;
			b.Latitude = this.Building.Latitude;
			b.Longitude = this.Building.Longitude;
			//b.PrimaryPhotoId = home.Building.PrimaryPhotoId;
			b.PrimaryPhotoExtension = this.Building.PrimaryPhotoExtension;
			b.UpdateDate = this.Building.UpdateDate;
			b.UpdatedBy = this.Building.UpdatedBy;
			b.CreateDate = this.Building.CreateDate;
			b.CreatedBy = this.Building.CreatedBy;
			b.IsDeleted = this.Building.IsDeleted;
			b.RemovedByAdmin = this.Building.ListingRemovedByAdmin;

			//propertyinfo
			b.Bedrooms = this.PropertyInfo.Bedrooms.HasValue ? this.PropertyInfo.Bedrooms.Value : 0;
			b.Bathrooms = this.PropertyInfo.Bathrooms.HasValue ? this.PropertyInfo.Bathrooms.Value : 0;
			b.PropertyType = this.PropertyInfo.Type;
			b.SquareFeet = this.PropertyInfo.SquareFootage;
			b.YearBuilt = this.PropertyInfo.YearBuilt;
			b.Acres = Convert.ToDecimal((this.PropertyInfo.Acres));
			b.Price = this.PropertyInfo.Price;
			b.DateAvailable = this.PropertyInfo.DateAvailableUtc;
			b.Deposit = this.PropertyInfo.Deposit;

			//listing
			if(this.Listing != null)
			{
				b.IsActive = this.Listing.IsActive;
				b.Description = this.Listing.Description;
				b.DateActivated = this.Listing.DateActivated;
			}

			//building counts
			if(this.Listing != null)
			{
				if(this.Listing.ListingCount != null)
				{
					b.BuildingCount = new New.BuildingCount
					{
						ViewCount = this.Listing.ListingCount.ViewCount,
						SearchCount = this.Listing.ListingCount.SearchCount,
						BuildingId = this.Building.BuildingId
					};
				}
				else
				{
					b.BuildingCount = new New.BuildingCount
					{
						BuildingId = this.Building.BuildingId,
						SearchCount = 0,
						ViewCount = 0
					};
				}
			}

			//new stuff
			b.ListingType = "Personal";
			b.ContactInfoId = 6;

			return b;
		}
	}
}
