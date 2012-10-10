using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using Rentler.Validation;

namespace Rentler
{
    [CustomValidation(typeof(ContactInfoValidation), "ValidatePhoneNumber")]
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }

        public int UserId { get; set; }

        [Required]
        [Display(Name = "Listing Type")]
        public int ContactInfoTypeCode { get; set; }

        public ContactInfoType ContactInfoType
        {
            get { return (ContactInfoType)this.ContactInfoTypeCode; }
            set { this.ContactInfoTypeCode = (int)value; }
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(250)]
        [Required]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone")]
        [RegularExpression(@"(?:(?:(\s*\(?([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\)?\s*(?:[.-]\s*)?)([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})", ErrorMessage = "Please enter a valid number.")]
        /*[RequiredIf("ShowPhoneNumber", true, ErrorMessage = "The Phone field is required")]*/
        public string PhoneNumber { get; set; }

        [Required]
        public bool ShowPhoneNumber { get; set; }

        [Required]
        public bool ShowEmailAddress { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }
    }
}
