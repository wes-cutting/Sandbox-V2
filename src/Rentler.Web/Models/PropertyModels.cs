using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Rentler.Common;
using Foolproof;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Models
{
    public class PropertyIndexModel
    {
        public PaginatedList<Building> Buildings { get; set; }
    }

    public class PropertyAdvertiseModel
    {
        public Building Listing { get; set; }
        public BuildingPreview Preview { get; set; }
        public long ListingViews { get; set; }
    }

    public class PropertyManageModel
    {
        public Listing Listing { get; set; }
    }

    public class PropertyCreateModel
    {
        public PropertyCreateModel()
        {
            Input = new PropertyCreateInputModel();
        }

        public PropertyCreateModel(PropertyCreateInputModel input)
        {
            Input = input;
        }

        public PropertyCreateInputModel Input { get; set; }
        public bool IsKsl { get; set; }
    }

    public class PropertyCreateInputModel
    {
        public Building ToBuilding()
        {
            return new Building()
            {
                PropertyTypeCode = this.PropertyTypeCode,
                Address1 = this.Address1,
                Address2 = this.Address2,
                City = this.City,
                State = this.State,
                Zip = this.Zip
            };
        }

        [Required]
        public int PropertyTypeCode { get; set; }

        public PropertyType PropertyType
        {
            get { return (PropertyType)this.PropertyTypeCode; }
            set { this.PropertyTypeCode = (int)value; }
        }

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
    }

    public class PropertyEditModel
    {
        public PropertyEditModel()
        {
            Input = new PropertyEditInputModel();
        }

        public PropertyEditModel(PropertyEditInputModel input)
        {
            Input = input;
        }

        public PropertyEditInputModel Input { get; set; }
    }

    public class PropertyEditInputModel
    {
        public PropertyEditInputModel() { }

        public PropertyEditInputModel(Building building)
        {
            BuildingId = building.BuildingId;
            PropertyTypeCode = building.PropertyTypeCode;
            Address1 = building.Address1;
            Address2 = building.Address2;
            City = building.City;
            State = building.State;
            Zip = building.Zip;
            IsListed = building.Acres.HasValue;
        }

        public Building ToBuilding()
        {
            return new Building()
            {
                BuildingId = this.BuildingId,
                PropertyTypeCode = this.PropertyTypeCode,
                Address1 = this.Address1,
                Address2 = this.Address2,
                City = this.City,
                State = this.State,
                Zip = this.Zip
            };
        }

        public long BuildingId { get; set; }

        [Required]
        public int PropertyTypeCode { get; set; }

        public PropertyType PropertyType
        {
            get { return (PropertyType)this.PropertyTypeCode; }
            set { this.PropertyTypeCode = (int)value; }
        }

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

        public bool IsListed { get; set; }
    }

    public class PropertyListModel
    {
        public PropertyListModel()
        {
            Input = new Listing();
        }

        public PropertyListModel(Building building)
        {
            if (building.User == null)
                UserContacts = new List<ContactInfo>();
            else
                UserContacts = building.User.ContactInfos;

            Input = new Listing(building);

            TemporaryOrderId = building.TemporaryOrderId;
        }

        public int StepsAvailable { get; set; }

        public ICollection<ContactInfo> UserContacts { get; set; }

        public Listing Input { get; set; }

        public int? TemporaryOrderId { get; set; }
    }

    public class PropertyTermsModel
    {
        public int StepsAvailable { get; set; }
        public PropertyTermsInputModel Input { get; set; }
    }

    public class PropertyTermsInputModel
    {
        public PropertyTermsInputModel() { }

        public PropertyTermsInputModel(Building building)
        {
            BuildingId = building.BuildingId;
            IsBackgroundCheckRequired = building.IsBackgroundCheckRequired;
            IsCreditCheckRequired = building.IsCreditCheckRequired;
            Price = building.Price;
            Deposit = building.Deposit;
            RefundableDeposit = building.RefundableDeposit;
            DateAvailableUtc = building.DateAvailableUtc;
            LeaseLengthCode = building.LeaseLengthCode;
            IsSmokingAllowed = building.IsSmokingAllowed;
            ArePetsAllowed = building.ArePetsAllowed;
            PetFee = building.PetFee;
            TemporaryOrderId = building.TemporaryOrderId;
        }

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
        [LessThanOrEqualTo("Deposit", PassOnNull = true)]
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

        public int? TemporaryOrderId { get; set; }

        public Building ToBuilding()
        {
            Building building = new Building()
            {
                BuildingId = this.BuildingId,
                IsBackgroundCheckRequired = this.IsBackgroundCheckRequired,
                IsCreditCheckRequired = this.IsCreditCheckRequired,
                Price = this.Price,
                Deposit = (this.Deposit.HasValue) ? this.Deposit.Value : decimal.Zero,
                RefundableDeposit = (this.RefundableDeposit.HasValue) ? this.RefundableDeposit.Value : decimal.Zero,
                DateAvailableUtc = this.DateAvailableUtc,
                LeaseLengthCode = this.LeaseLengthCode,
                IsSmokingAllowed = this.IsSmokingAllowed,
                ArePetsAllowed = this.ArePetsAllowed,
                PetFee = (this.PetFee.HasValue) ? this.PetFee.Value : decimal.Zero
            };

            return building;
        }
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

        public PropertyPromoteInputModel(Building building, List<FeaturedListing> featured)
        {
            this.BuildingId = building.BuildingId;
            this.Title = building.Title;
            this.Description = building.Description;
            this.PurchasedRibbonId = building.RibbonId;
            this.PrimaryPhotoId = building.PrimaryPhotoId;
            this.PrimaryPhotoExtension = building.PrimaryPhotoExtension;
            this.CalendarDates = new CalendarDatesModel();
            this.TemporaryOrderId = building.TemporaryOrderId;

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
            if (building.HasPriority)
            {
                this.PurchasedPriority = true;
                this.ListingHasPriority = true;
            }
            if (featured != null && featured.Count > 0)
            {
                //first, note any the user has already purchased
                this.CalendarDates.FeaturedDates =
                    featured.Where(f => f.BuildingId == building.BuildingId)
                            .Select(f => f.ScheduledDate.ToString("G")).Distinct().ToArray();


                // set blackout dates, by figuring out
                // how many dates we have in the schedule that contain 3 or more listings
                // in a given zip code

                // first, filter featured to just our zip code
                featured = featured.Where(f => f.Zip == building.Zip).ToList();

                // then build our dictionary, counting the number
                // of featured listings in each day
                var schedule = new Dictionary<DateTime, int>();
                for (int i = 0; i < featured.Count; i++)
                {
                    if (!schedule.ContainsKey(featured[i].ScheduledDate))
                        schedule.Add(featured[i].ScheduledDate, 1);
                    else
                        schedule[featured[i].ScheduledDate] += 1;
                }

                //TODO: also count the reserved days we know about


                // and finally, figure out which days (if any) are already at capacity.
                this.CalendarDates.BlackoutDates =
                    schedule.Where(s => s.Value >= 3)
                            .Select(s => s.Key.ToString("G")).ToArray();
            }
        }

        public Building ToBuilding()
        {
            Building building = new Building()
            {
                BuildingId = this.BuildingId,
                Title = this.Title.StripNonAscii(),
                Description = this.Description.StripNonAscii()
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

        [Display(Name = "Prioritize my listing")]
        public bool ListingHasPriority { get; set; }

        public bool PurchasedPriority { get; set; }

        public DateTime[] FeaturedDates { get; set; }

        public CalendarDatesModel CalendarDates { get; set; }

        public int? TemporaryOrderId { get; set; }
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

        public int OrderId { get; set; }

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

    public class PropertyRequestAppModel
    {
        public PropertyRequestAppModel() { }
        public PropertyRequestAppModel(UserInterest userInterest)
        {
            this.Lead = userInterest;
            this.Input = new PropertyRequestAppInputModel();
        }

        public UserInterest Lead { get; set; }

        public PropertyRequestAppInputModel Input { get; set; }
    }

    public class PropertyRequestAppInputModel
    {
        public int LeadId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
    }

    public class PropertyViewLeadModel
    {
        public PropertyViewLeadModel() { }
        public PropertyViewLeadModel(UserInterest lead)
        {
            this.Lead = lead;
        }
        public UserInterest Lead { get; set; }
    }
}