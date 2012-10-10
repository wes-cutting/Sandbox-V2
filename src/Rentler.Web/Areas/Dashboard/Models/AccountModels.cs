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

    public class AccountSendAppModel
    {
        public AccountSendAppModel() { }
        
        public AccountSendAppModel(UserInterest lead)
        {
            this.Lead = lead;

            if (lead.User == null || lead.User.UserApplication == null)
                this.Input = new AccountSendAppInputModel();
            
            this.Input = new AccountSendAppInputModel(lead.User.UserApplication);            
        }

        public AccountSendAppModel(UserInterest lead, UserApplication application)
        {
            this.Lead = lead;
            this.Input = new AccountSendAppInputModel(application);
        }

        public UserInterest Lead { get; set; }
        public AccountSendAppInputModel Input { get; set; }
    }

    public class AccountSendAppInputModel
    {
        public AccountSendAppInputModel()
        {
            this.Application = new UserApplication();
        }

        public AccountSendAppInputModel(UserApplication application)
        {
            this.Application = application;
        }

        public int UserInterestId { get; set; }
        public UserApplication Application { get; set; }
    }
}