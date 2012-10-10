using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class Building
    {
        public Building()
        {
            LeaseLength = Rentler.LeaseLength.Year;
            ArePetsAllowed = true;
        }

        public long BuildingId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime? CreateDateUtc { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? UpdateDateUtc { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        [StringLength(50)]
        public string RibbonId { get; set; }

		public DateTime? DateRibbonActivated { get; set; }
        
        public int? ContactInfoId { get; set; }

        [StringLength(250)]
        [Required]
        [Display(Name = "Rental Property Address")]
        public string Address1 { get; set; }

        [StringLength(250)]
        [Display(Name = "Line 2")]
        public string Address2 { get; set; }

        [StringLength(250)]
        [Required]
        public string City { get; set; }

        [StringLength(50)]
        [Required]
        public string State { get; set; }

        [StringLength(10)]
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "The value entered for Zip Code is invalid")]
        public string Zip { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        [Required]
        public int PropertyTypeCode { get; set; }

        public PropertyType PropertyType
        {
            get { return (PropertyType)this.PropertyTypeCode; }
            set { this.PropertyTypeCode = (int)value; }
        }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }
        
        /*[Range(1, int.MaxValue, ErrorMessage = "The value entered for Square Feet must be a positive number greater than 0")]*/
        [Display(Name = "Square Feet")]
        public int? SquareFeet { get; set; }
        
        public float? Acres { get; set; }
        
        /*[Range(1450, 10000, ErrorMessage = "The value entered for Year Built must be between 1450 and 10000")]*/
        [Display(Name = "Year Built")]
        public int? YearBuilt { get; set; }
        
        /*[Range(1, int.MaxValue, ErrorMessage = "The value entered for Bedrooms must be a positive number greater than 0")]*/
        public int? Bedrooms { get; set; }
        
        [RegularExpression(@"(\d?|[0-9]\d*)(\.([2,7][5]|[0,5][0]?))?", ErrorMessage = "The value entered for Bathrooms is invalid")]        
        public float? Bathrooms { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime? DateAvailableUtc { get; set; }

        public DateTime? DateActivatedUtc { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsCreditCheckRequired { get; set; }

        [Required]
        public bool IsBackgroundCheckRequired { get; set; }

        [Required]
        public decimal Deposit { get; set; }

        [Required]
        public decimal RefundableDeposit { get; set; }

        public int LeaseLengthCode { get; set; }

        public LeaseLength LeaseLength
        {
            get { return (LeaseLength)this.LeaseLengthCode; }
            set { this.LeaseLengthCode = (int)value; }
        }

        [Required]
        public bool IsSmokingAllowed { get; set; }

        [Required]
        public bool ArePetsAllowed { get; set; }

        [Required]
        public bool IsRemovedByAdmin { get; set; }

        [Required]
        public bool IsReported { get; set; }

        [StringLength(1000)]
        public string ReportedText { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

		[Required]
		public bool HasPriority { get; set; }

		public DateTime? DatePrioritized { get; set; }

        [Required]
        public decimal PetFee { get; set; }

        public int? TemporaryOrderId { get; set; }

        public Guid? PrimaryPhotoId { get; set; }
        
        public string PrimaryPhotoExtension { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }

        public virtual ICollection<CustomAmenity> CustomAmenities { get; set; }

        public virtual ICollection<BuildingAmenity> BuildingAmenities { get; set; }

        public virtual ICollection<SavedBuilding> SavedBuildings { get; set; }

        public virtual Order TemporaryOrder { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<UserInterest> Leads { get; set; }
    }
}
