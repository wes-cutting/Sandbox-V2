using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rentler.Web.Areas.Dashboard.Models
{
    public class AccountProfileModel
    {
        public string Message { get; set; }
        public AccountProfileInputModel Input { get; set; }
    }

    /// <summary>
    /// Input for update account profile.
    /// </summary>
    public class AccountProfileInputModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
    }

    public class AccountChangePasswordModel
    {
        public string Message { get; set; }
        public AccountChangePasswordInputModel Input { get; set; }
    }

    public class AccountChangePasswordInputModel
    {
        [Required]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        public string ConfirmPassword { get; set; }
    }
}