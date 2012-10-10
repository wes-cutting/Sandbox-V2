using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel;
using Rentler.Web.Axioms;

namespace Rentler.Web.Models
{
    /// <summary>
    /// Model for the base login page.
    /// </summary>
    public class AccountLoginModel
    {
        public AccountLoginInputModel Input { get; set; }
    }

    /// <summary>
    /// Input form for the login page
    /// </summary>
    public class AccountLoginInputModel
    {
        [Required]
        [Display(Name = "Email")]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Model for the base registration page.
    /// </summary>
    public class AccountRegisterModel
    {
        public AccountRegisterInputModel Input { get; set; }
    }

    /// <summary>
    /// Input for the base registration page.
    /// </summary>
    public class AccountRegisterInputModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required, Mandatory(ErrorMessage="You must Agree to the Terms and Conditions")]
        public bool AgreesToTerms { get; set; }
    }

    public class AccountForgotPasswordModel
    {
        public AccountForgotPasswordInputModel Input { get; set; }
        public string Message { get; set; }
    }

    public class AccountForgotPasswordInputModel
    {
        [Required]
        [StringLength(255)]
        [DisplayName("Email")]
        public string Email { get; set; }
    }

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
