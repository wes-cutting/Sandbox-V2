using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public partial class Building
    {
        public Common.PropertyPreview ToBuildingPreview()
        {
            return new Common.PropertyPreview()
            {
                BuildingId = this.BuildingId,
                UserId = this.UserId,
                Address1 = this.Address1,
                Address2 = this.Address2,
                City = this.City,
                PrimaryPhotoExtension = this.PrimaryPhotoExtension,
                PrimaryPhotoId = this.PrimaryPhotoId,
                State = this.State,
                Zip = this.Zip,
                IsActive = this.IsActive,
                IsRemovedByAdmin = this.IsRemovedByAdmin
            };
        }

        public Common.Listing ToListing()
        {
            return new Common.Listing()
            {
                BuildingId = this.BuildingId,
                UserId = this.UserId,
                Acres = this.Acres,
                Bedrooms = this.Bedrooms,
                Bathrooms = this.Bathrooms,
                Address1 = this.Address1,
                Address2 = this.Address2,
                City = this.City,
                State = this.State,
                Zip = this.Zip,
                Description = this.Description,
                IsReported = this.IsReported,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Price = this.Price,
                PrimaryPhotoExtension = this.PrimaryPhotoExtension,
                PrimaryPhotoId = this.PrimaryPhotoId,
                RibbonId = this.RibbonId,
                SquareFeet = this.SquareFeet,
                Title = this.Title,
                YearBuilt = this.YearBuilt,
                DateActivatedUtc = this.DateActivatedUtc,
                DateAvailableUtc = this.DateAvailableUtc,
                IsActive = this.IsActive,
                ReportedText = this.ReportedText
            };
        }

		public Common.ReportedListing ToReportedListing()
		{
			return new Common.ReportedListing()
			{
				BuildingId = this.BuildingId,
				UserId = this.UserId,
				Acres = this.Acres,
				Bedrooms = this.Bedrooms,
				Bathrooms = this.Bathrooms,
				Address1 = this.Address1,
				Address2 = this.Address2,
				City = this.City,
				State = this.State,
				Zip = this.Zip,
				Description = this.Description,
				IsReported = this.IsReported,
				Latitude = this.Latitude,
				Longitude = this.Longitude,
				Price = this.Price,
				PrimaryPhotoExtension = this.PrimaryPhotoExtension,
				PrimaryPhotoId = this.PrimaryPhotoId,
				RibbonId = this.RibbonId,
				SquareFeet = this.SquareFeet,
				Title = this.Title,
				YearBuilt = this.YearBuilt,
				DateActivatedUtc = this.DateActivatedUtc,
				DateAvailableUtc = this.DateAvailableUtc,
				IsActive = this.IsActive,
				ReportedText = this.ReportedText,
				LandlordEmail = this.User.Email
			};
		}
    }
}
