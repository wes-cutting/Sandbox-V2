using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler.Common
{
    public class Listing
    {
        public Listing() { }

        public Listing(Building building)
        {
            BuildingId = building.BuildingId;
            UserId = building.UserId;
            Acres = building.Acres;
            SquareFeet = building.SquareFeet;
            YearBuilt = building.YearBuilt;
            Bedrooms = building.Bedrooms;
            Bathrooms = building.Bathrooms;
            BuildingAmenities = building.BuildingAmenities;
            CustomAmenities = building.CustomAmenities;
            ContactInfo = building.ContactInfo;
            Title = building.Title;
            Description = building.Description;
            PrimaryPhotoId = building.PrimaryPhotoId;
            PrimaryPhotoExtension = building.PrimaryPhotoExtension;
            DateAvailableUtc = building.DateAvailableUtc;
            DateActivatedUtc = building.DateActivatedUtc;
            Price = building.Price;
            IsActive = building.IsActive;
            ReportedText = building.ReportedText;
        }

        public bool IsValidListing
        {
            get
            {
                List<ValidationResult> validationResults = new List<ValidationResult>();
                ValidationContext context = new ValidationContext(this, null, null);
                bool result = Validator.TryValidateObject(this, context, validationResults);

                if (!result) return false;
                if (!this.DateAvailableUtc.HasValue) return false;
                if (!this.DateActivatedUtc.HasValue) return false;

                return true;
            }
        }

        public DateTime? ExpirationDate
        {
            get
            {
                if (this.DateActivatedUtc.HasValue)
                    return this.DateActivatedUtc.Value.AddMonths(1).ToLocalTime();

                return null;
            }
        }

        public string ReportedText { get; set; }

        public bool IsReported { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string RibbonId { get; set; }

        public long BuildingId { get; set; }

        public int UserId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public bool IsOwnedByCurrentUser { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid? PrimaryPhotoId { get; set; }

        public string PrimaryPhotoExtension { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DateAvailableUtc { get; set; }

        public DateTime? DateActivatedUtc { get; set; }

        [Required(ErrorMessage = "The Acres field is required")]
        public float? Acres { get; set; }

        [Required(ErrorMessage = "The Square Feet field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The value entered for Square Feet must be a positive number greater than 0")]
        [Display(Name = "Square Feet")]
        public int? SquareFeet { get; set; }

        [Required(ErrorMessage = "The Year Built field is required")]
        [Range(1450, 10000, ErrorMessage = "The value entered for Year Built must be between 1450 and 10000")]
        [Display(Name = "Year Built")]
        public int? YearBuilt { get; set; }

        [Required(ErrorMessage = "The Bedrooms field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The value entered for Bedrooms must be a positive number greater than 0")]
        public int? Bedrooms { get; set; }

        [Required(ErrorMessage = "The Bathrooms field is required")]
        [RegularExpression(@"(\d?|[0-9]\d*)(\.([2,7][5]|[0,5][0]?))?", ErrorMessage = "The value entered for Bathrooms is invalid")]
        public float? Bathrooms { get; set; }

        public long PageViews { get; set; }

        public long SearchViews { get; set; }
                
        public ICollection<BuildingAmenity> BuildingAmenities { get; set; }

        public ICollection<CustomAmenity> CustomAmenities { get; set; }

        public ContactInfo ContactInfo { get; set; }
    }
}
