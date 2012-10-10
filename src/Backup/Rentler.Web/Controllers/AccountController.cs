using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Rentler.Web.Models;
using Rentler.Adapters;

namespace Rentler.Web.Controllers
{
    /// <summary>
    /// Controller containing accounting information.
    /// </summary>
    public class AccountController : Controller
    {
        IAccountAdapter accountAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountAdapter">The account adapter.</param>
        public AccountController(IAccountAdapter accountAdapter)
        {
            this.accountAdapter = accountAdapter;
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
			ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Processes login information and logs in a user.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(AccountLoginInputModel input, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = this.accountAdapter.LoginUser(input.UserName, input.Password);
                if (result.StatusCode == 200)
                {
                    // set auth cookie
                    FormsAuthentication.SetAuthCookie(result.Result.Username, input.RememberMe);

                    // allow cross-browser auth cookie (IE8)
                    Response.AddHeader("p3p", 
                        "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");

                    // redirect the user
                    if (String.IsNullOrEmpty(returnUrl))
                        return Redirect("/");
                    else
                        return Redirect(returnUrl);
                }

                // process failure
                var error = result.Errors.First();
                ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
            }
            return View(new AccountLoginModel() { Input = input });
        }

        /// <summary>
        /// Displays the registration page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Processes a user's registration.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(AccountRegisterInputModel input, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = this.accountAdapter.RegisterUser(new User()
                {
                    Username = input.UserName,
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName
                }, input.Password);

                if (result.StatusCode == 200)
                {
                    FormsAuthentication.SetAuthCookie(result.Result.Username, false);

                    // redirect the user
                    if (String.IsNullOrEmpty(returnUrl))
                        return Redirect("/");
                    else
                        return Redirect(returnUrl);
                }

                var error = result.Errors.First();
                ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
            }
            return View(new AccountRegisterModel() { Input = input });
        }

        /// <summary>
        /// Page to logout the user.
        /// </summary>
        /// <returns>Redirection to the home page.</returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }


        /// <summary>
        /// Auto reset password
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            return View(
                new AccountForgotPasswordModel 
                { 
                    Input = new AccountForgotPasswordInputModel() 
                }
            );
        }

        [HttpPost]
        public ActionResult ForgotPassword(AccountForgotPasswordInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userStatus = this.accountAdapter.ResetPassword(input.Email);

                if (userStatus.StatusCode == 200)
                {

                }

                var error = userStatus.Errors.First();
                ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
            }

            return View(new AccountForgotPasswordModel { Input = input });
        }
    }
}
