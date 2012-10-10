using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Areas.Dashboard.Models;
using Rentler.Adapters;

namespace Rentler.Web.Areas.Dashboard.Controllers
{
    /// <summary>
    /// Controller containing account management actions.
    /// </summary>
    [Authorize]
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
        /// Action redirecting to the profile management.
        /// </summary>
        /// <returns>A redirection to the profile management page.</returns>
        public ActionResult Index()
        {
            return RedirectToAction("profile");
        }

        /// <summary>
        /// Gets the application for a given user.
        /// </summary>
        /// <returns>The profile view with the application.</returns>
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

        /// <summary>
        /// Get action for the change password page.
        /// </summary>
        /// <returns>A view with the change password page.</returns>
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

        public ActionResult SendApp(int id)
        {
            var status = this.accountAdapter.GetUserInterest(User.Identity.Name, id);

            if (status.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            AccountSendAppModel model = new AccountSendAppModel(status.Result);
            return View(model);            
        }

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
