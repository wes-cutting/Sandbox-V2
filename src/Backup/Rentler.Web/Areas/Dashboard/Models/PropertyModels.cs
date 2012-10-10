using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Rentler.Configuration;
using Foolproof;
using System.Diagnostics;

namespace Rentler.Web.Areas.Dashboard.Models
{
    public class PropertyIndexModel
    {
        public PaginatedList<Building> Buildings { get; set; }
    }

    public class PropertyManageModel
    {
        public Building Building { get; set; }
        public long ViewCount { get; set; }
        public long SearchViewCount { get; set; }
    }

    public class PropertyCreateModel
    {
        public int StepsAvailable { get; set; }
        public Building Input { get; set; }
    }

    public class PropertyEditModel
    {                     
        public Building Input { get; set; }
        public int StepsAvailable { get; set; }
    }
    
    public class PropertyTermsModel
    {
        public int StepsAvailable { get; set; }
        public PropertyTermsInputModel Input { get; set; }
    }

    public class PropertyTermsInputModel
    {        
		[Required]
		public long BuildingId { get; set; }

		[Display(Name = "Credit Check")]
		public bool IsCreditCheckRequired { get; set; }

		[Display(Name = "Background Check")]
		public bool IsBackgroundCheckRequired { get; set; }

		[Required(ErrorMessage = "The Price field is required")]
        [RegularExpression("[0-9]+(\\.[0-9]{2})?", ErrorMessage = "Please enter a number for the price. ex: 500.00")]
		public decimal Price { get; set; }
				
        [RegularExpression("[0-9]+(\\.[0-9]{2})?", ErrorMessage = "Please enter a number for the deposit. ex: 500.00")]
		public decimal? Deposit { get; set; }

		[Display(Name = "Refundable")]
        [LessThanOrEqualTo("Deposit", PassOnNull= true)]
        [RegularExpression("[0-9]+(\\.[0-9]{2})?", ErrorMessage = "Please enter a number for the refundable deposit. ex: 500.00")]
		public decimal? RefundableDeposit { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
		[Required(ErrorMessage = "The Availability Date field is required")]
		public DateTime? DateAvailableUtc { get; set; }

		[Display(Name = "Lease Length")]
		public int LeaseLengthCode { get; set; }

		[Display(Name = "Smoking")]
		[Required(ErrorMessage = "The Smoking field is required")]
		public bool IsSmokingAllowed { get; set; }
        
		[Required(ErrorMessage = "The Pets field is required")]
		public bool ArePetsAllowed { get; set; }
		        
        [RegularExpression("[0-9]+(\\.[0-9]{2})?", ErrorMessage = "Please enter a number for the pet fee. ex: 250.00")]
        [Display(Name = "Pet Fee")]
		public decimal? PetFee { get; set; }			
    }

    public class PropertyPromoteModel
    {
        public PropertyPromoteModel() { }

        public int StepsAvailable { get; set; }
        public PropertyPromoteInputModel Input { get; set; }       
    }

    public class PropertyPromoteInputModel
    {
        public PropertyPromoteInputModel() { }

        public PropertyPromoteInputModel(Building building)
        {
            this.BuildingId = building.BuildingId;
            this.Title = building.Title;
            this.Description = building.Description;
            this.PurchasedRibbonId = building.RibbonId;
            this.PrimaryPhotoId = building.PrimaryPhotoId;
            this.PrimaryPhotoExtension = building.PrimaryPhotoExtension;
            this.CalendarDates = new CalendarDatesModel();
            
            // figure out selected
            if (building.TemporaryOrder != null)
            {
                // find the ribbon
                var ribbon = building.TemporaryOrder.OrderItems.Where(m => m.ProductId == "ribbon").FirstOrDefault();

                if (ribbon != null)
                {
                    this.SelectedRibbonName = Configuration.Ribbons.Current.AvailableRibbons[ribbon.ProductOption];
                    this.SelectedRibbonId = ribbon.ProductOption;
                }

                // TODO: set blackout and featured dates
                List<string> reserved = new List<string>();

                // TODO: make dynamic. hard-coded to MST/MDT
                TimeZoneInfo mstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");

                // set reserved dates
                var featuredItems = building.TemporaryOrder.OrderItems.Where(i => i.ProductId == "featureddate");

                foreach (OrderItem oi in featuredItems)
                {
                    DateTime featureUtc = DateTime.Parse(oi.ProductOption);
                    DateTime featureLocal = TimeZoneInfo.ConvertTimeFromUtc(featureUtc, mstTimeZone).Date;
                    reserved.Add(featureLocal.ToString("G"));
                }

                this.CalendarDates.ReservedDates = reserved.ToArray();
            }
        }

        public Building ToBuilding()
        {
            Building building = new Building()
            {
                BuildingId = this.BuildingId,
                Title = this.Title,
                Description = this.Description
            };
            return building;
        }

        [Required]
        public long BuildingId { get; set; }

        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }

        [StringLength(4000, ErrorMessage = "Description cannot exceed 4000 characters")]
        public string Description { get; set; }

        public Guid? PrimaryPhotoId { get; set; }

        public string PrimaryPhotoExtension { get; set; }

        public string PurchasedRibbonId { get; set; }

        public string SelectedRibbonId { get; set; }

        public string SelectedRibbonName { get; set; }

        public DateTime[] FeaturedDates { get; set; }

        public CalendarDatesModel CalendarDates { get; set; }        
    }

    public class PropertyCheckoutModel
    {
        public int StepsAvailable { get; set; }
        public List<UserCreditCard> PaymentMethods { get; set; }

        public string ReservedRibbonId { get; set; }
        public string ReservedRibbonName { get; set; }        
        public decimal ReservedRibbonPrice { get; set; }

        public List<DateTime> ReservedFeaturedDates { get; set; }

        public PropertyCheckoutInputModel Input { get; set; }
    }

    public class PropertyCheckoutInputModel
    {
        public long BuildingId { get; set; }

        public UserCreditCard SelectedPaymentMethod { get; set; }

        public bool SaveCard { get; set; }
    }

    public class CalendarDatesModel
    {
        public CalendarDatesModel()
        {
            BlackoutDates = new string[0] { };
            FeaturedDates = new string[0] { };
            ReservedDates = new string[0] { };
        }

        public string[] BlackoutDates { get; set; }
        public string[] FeaturedDates { get; set; }
        public string[] ReservedDates { get; set; }        
    }    
}