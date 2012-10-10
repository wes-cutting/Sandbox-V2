using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel;

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

        [Required]
        public bool AgreesToTerms { get; set; }
    }

    public class AccountForgotPasswordModel
    {
        public AccountForgotPasswordInputModel Input { get; set; }
    }

    public class AccountForgotPasswordInputModel
    {
        [Required]
        [StringLength(255)]
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
