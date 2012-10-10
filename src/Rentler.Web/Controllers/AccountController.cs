using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using System.Web.Routing; 
using System.Web.Security;
using Rentler.Web.Models;
using Rentler.Adapters;
using Rentler.Auth;
using Rentler.Facades;

namespace Rentler.Web.Controllers
{
    /// <summary>
    /// Controller containing accounting information.
    /// </summary>
    /// <remarks>
    ///     All instances of the adapter need to be
    ///     moved over to the facade.
    /// </remarks>
    public class AccountController : Controller
    {
        IAccountAdapter accountAdapter;
        IAccountFacade accountFacade;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountAdapter">The account adapter.</param>
        public AccountController(IAccountAdapter accountAdapter,
            IAccountFacade accountFacade)
        { 
            this.accountAdapter = accountAdapter;
            this.accountFacade = accountFacade;
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
                    CustomAuthentication.SetAuthCookie(result.Result.Username, result.Result.UserId, input.RememberMe);

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
                    CustomAuthentication.SetAuthCookie(result.Result.Username, result.Result.UserId, false);

                    // redirect the user
                    if (String.IsNullOrEmpty(returnUrl))
                        return Redirect("/");
                    else
                        return Redirect(returnUrl);
                }

                HandleErrors(result);
            }

            return View(new AccountRegisterModel() { Input = input });
        }

        /// <summary>
        /// Page to logout the user.
        /// </summary>
        /// <returns>Redirection to the home page.</returns>
        public ActionResult Logout()
        {
            CustomAuthentication.SignOut();
            return Redirect("/");
        }


        /// <summary>
        /// Auto reset password
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            var model = new AccountForgotPasswordModel()
            {
                Input = new AccountForgotPasswordInputModel()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ForgotPassword(AccountForgotPasswordInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userStatus = this.accountAdapter.ResetPassword(input.Email);

                if (userStatus.StatusCode == 200)
                {
                    return View(
                        new AccountForgotPasswordModel()
                        {
                            Input = new AccountForgotPasswordInputModel(),
                            Message = "Password reset successfully, check your email for your temporary password"
                        }
                    );
                }
                
                if (userStatus.Errors != null)
                {
                    for (int i = 0; i < userStatus.Errors.Length; ++i)
                    {
                        ModelState.AddModelError(
                            userStatus.Errors[i].MemberNames.First(),
                            userStatus.Errors[i].ErrorMessage);
                    }
                }
                else
                {
                    // output a friendly message, actual error will be logged
                    ModelState.AddModelError("Account", userStatus.Message);
                }                
            }

            return View(new AccountForgotPasswordModel { Input = input });
        }

        /// <summary>
        /// Get action for the change password page.
        /// </summary>
        /// <returns>A view with the change password page.</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            AccountChangePasswordModel model = new AccountChangePasswordModel
            {
                Input = new AccountChangePasswordInputModel()
            };

            return View(model);
        }

        /// <summary>
        /// Post action for the change password page.
        /// </summary>
        /// <param name="input">Input model for change password information.</param>
        /// <returns>A view with the change password page.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(AccountChangePasswordInputModel input)
        {
            if (ModelState.IsValid)
            {
                var result = this.accountAdapter.ChangePassword(
                    User.Identity.Name,
                    input.OldPassword,
                    input.NewPassword,
                    input.ConfirmPassword
                );

                if (result.StatusCode == 200)
                {
                    return View(new AccountChangePasswordModel
                    {
                        Input = new AccountChangePasswordInputModel(),
                        Message = "Password changed successfully"
                    }
                    );
                }

                // business rule failure
                var error = result.Errors.First();
                ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
            }

            return View(new AccountChangePasswordModel { Input = input });
        }

        /// <summary>
        /// Gets the application for a given user.
        /// </summary>
        /// <returns>The profile view with the application.</returns>
        [Authorize]
        public ActionResult Application()
        {
            var result = this.accountAdapter.GetApplicationForUser(User.Identity.Name);
            return View(result.Result);
        }

        /// <summary>
        /// Posts the specified application for a given user.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The profile view with the updated application.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult Application(UserApplication application)
        {
            if (ModelState.IsValid)
            {
                var result = this.accountAdapter.SaveApplicationForUser(User.Identity.Name, application);
                if (result.StatusCode != 200)
                {
                    var error = result.Errors.First();
                    ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                }
                else
                {
                    application = result.Result;
                }
            }
            return View();
        }

        /// <summary>
        /// Get action for the profile page.
        /// </summary>
        /// <returns>A view with the profile page.</returns>
        [Authorize]
        public ActionResult Profile()
        {
            var user = this.accountAdapter.GetUser(User.Identity.Name);

            if (user.StatusCode != 200)
                return this.NotFoundException();

            AccountProfileInputModel input = new AccountProfileInputModel
            {
                FirstName = user.Result.FirstName,
                LastName = user.Result.LastName,
                Email = user.Result.Email
            };

            return View(new AccountProfileModel() { Input = input });
        }

        /// <summary>
        /// Post action for the profile page.
        /// </summary>
        /// <param name="input">Input model for profile information.</param>
        /// <returns>A view with the profile page.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Profile(AccountProfileInputModel input)
        {
            if (ModelState.IsValid)
            {
                var result = this.accountAdapter.UpdateProfile(
                    User.Identity.Name,
                    input.FirstName,
                    input.LastName,
                    input.Email
                );

                if (result.StatusCode == 200)
                {
                    return View(new AccountProfileModel
                    {
                        Input = input,
                        Message = "Profile information updated successfully"
                    }
                    );
                }

                var error = result.Errors.First();
                ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
            }

            return View(new AccountProfileModel { Input = input });
        }

        [Authorize]
        public ActionResult SendApp(int id)
        {
            var status = this.accountAdapter.GetUserInterest(User.Identity.Name, id);

            if (status.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            AccountSendAppModel model = new AccountSendAppModel(status.Result);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SendApp(AccountSendAppInputModel input)
        {
            if (ModelState.IsValid)
            {
                var status = this.accountAdapter.SendApplication(User.Identity.Name, input.UserInterestId, input.Application);

                if (status.StatusCode == 200)
                    return View("SendAppSent");

                HandleErrors(status);
            }

            var lead = this.accountAdapter.GetUserInterest(User.Identity.Name, input.UserInterestId);

            if (lead.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            AccountSendAppModel model = new AccountSendAppModel(lead.Result);
            model.Input = input;
            return View(model);
        }

        private void HandleErrors(Status s)
        {
            if (s.Errors != null)
            {
                for (int i = 0; i < s.Errors.Length; ++i)
                {
                    ModelState.AddModelError(
                        s.Errors[i].MemberNames.First(),
                        s.Errors[i].ErrorMessage);
                }
            }
            else
            {
                // output a friendly message, actual error will be logged
                ModelState.AddModelError("Account", s.Message);
            }
        }
    }
}
