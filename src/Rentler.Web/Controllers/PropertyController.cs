using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Facades;
using Rentler.Common;
using Rentler.Web.Models;
using Rentler.Adapters;
using Rentler.Auth;

namespace Rentler.Web.Controllers
{
    /// <summary>
    /// Controller for managing property information.
    /// </summary>
    public class PropertyController : Controller
    {
        IPropertyFacade propertyFacade;
        IPropertyAdapter propertyAdapter;
        IAuthAdapter authAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public PropertyController(
            IAuthAdapter authAdapter,
            IPropertyFacade propertyFacade,
            IPropertyAdapter propertyAdapter)
        {
            this.authAdapter = authAdapter;
            this.propertyFacade = propertyFacade;
            this.propertyAdapter = propertyAdapter;
        }

        /// <summary>
        /// The base dashboard page. Determines whether to go
        /// to the user's favorites or to the property search.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            // if they have a property then go to my properties
            var result = this.propertyFacade.SearchForUserProperty(
                new Common.PropertySearch() { ResultsPerPage = 1 });

            if (result.StatusCode != 200 || result.Result.Results.Count < 1)
                return Redirect("/listing/favorites");
            else
                return Redirect("/property/search");
        }

        /// <summary>
        /// Controller action for searching one's properties.
        /// </summary>
        /// <param name="search">The property search parameters.</param>
        /// <returns>The search results as a property search.</returns>
        [Authorize]
        public ActionResult Search(PropertySearch search, Guid? token)
        {
			if (!User.Identity.IsAuthenticated && token.HasValue)
			{
				var user = authAdapter.ValidateAuthToken(token.Value);
				if (user.StatusCode == 200)
					CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);
				return Redirect("/property/search");
			}

            var result = this.propertyFacade.SearchForUserProperty(search);
            if (Request.IsAjaxRequest())
                return PartialView("SearchResults", result.Result);
            return View(result.Result);
        }

        /// <summary>
        /// Entry point for landlord to manage a single property.
        /// </summary>
        /// <param name="id">the property identifier</param>
        /// <returns></returns>
        public ActionResult Manage(long id, Guid? token)
        {
            if (!User.Identity.IsAuthenticated && token.HasValue)
            {
                var user = authAdapter.ValidateAuthToken(token.Value);
                if (user.StatusCode == 200)
                    CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);
                return Redirect("/property/manage/" + id);
            }

            if (!User.Identity.IsAuthenticated)
                return Redirect("/account/login?returnUrl=" + "/property/manage/" + id);

            var listing = this.propertyFacade.ManageListingById(id);
            if (listing.StatusCode != 200)
                throw new HttpException(404, "Not Found");
            PropertyManageModel model = new PropertyManageModel();
            model.Listing = listing.Result;
            if (!model.Listing.IsValidListing)
                return View("Manage-NotValid", model);
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            PropertyCreateModel model = new PropertyCreateModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(PropertyCreateInputModel input, string command)
        {
            if (ModelState.IsValid)
            {
                var result = this.propertyAdapter.CreateProperty(input.ToBuilding());

                if (result.StatusCode == 200)
                {
                    if (command == "list")
                        return Redirect("/listing/list/" + result.Result.BuildingId);
                    if (command == "save")
                        return RedirectToAction("manage", new { id = result.Result.BuildingId });
                }

                HandleErrors(result);
            }

            PropertyCreateModel model = new PropertyCreateModel(input);
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(long id)
        {
            var request = this.propertyAdapter.GetProperty(id, User.Identity.Name);

            if (request.StatusCode != 200)
                return this.NotFoundException();

            PropertyEditModel model = new PropertyEditModel(
                new PropertyEditInputModel(request.Result)
            );
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(PropertyEditInputModel input, string command)
        {
            if (ModelState.IsValid)
            {
                var request = this.propertyAdapter.UpdateProperty(input.ToBuilding());

                if (request.StatusCode == 200)
                {
                    if (command == "list")
                        return Redirect("/listing/list/" + request.Result.BuildingId);
                    if (command == "save")
                        return RedirectToAction("manage", new { id = request.Result.BuildingId });

                    HandleErrors(request);
                }
            }

            PropertyEditModel model = new PropertyEditModel(input);
            return View(model);
        }

        [Authorize]
        public ActionResult Delete(long id, string confirm)
        {
            if (!confirm.ToLower().Equals("delete"))
                return Redirect(string.Format("/property/manage/{0}#delete", id));

            var status = this.propertyFacade.DeleteProperty(id);

            if (status.StatusCode == 200)
                return RedirectToAction("search");

            return Redirect(string.Format("/property/manage/{0}#delete", id));
        }

        [Authorize]
        public ActionResult Activate(long id)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return Json(Status.UnAuthorized<bool>());

            var status = this.propertyAdapter.ActivateBuilding(id, User.Identity.Name);

            if (status.StatusCode == 200)
                return Json(Status.OK<bool>(true));

            return Json(
                Status.Error<bool>(
                    "An error occurred and we were unable to Activate this listing. Please contact Rentler Support if this problem persists",
                    false
                )
            );
        }

        [Authorize]
        public ActionResult Deactivate(long id)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return Json(Status.UnAuthorized<bool>());

            var status = this.propertyAdapter.DeactivateBuilding(id, User.Identity.Name);

            if (status.StatusCode == 200)
                return Json(Status.OK<bool>(true));

            return Json(
                Status.Error<bool>(
                    "An error occurred and we were unable to Deactivate this listing. Please contact Rentler Support if this problem persists",
                    false
                )
            );
        }

        [Authorize]
        public ActionResult RequestApp(int id)
        {
            var status = this.propertyAdapter.GetUserInterest(User.Identity.Name, id);

            if (status.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            PropertyRequestAppModel model = new PropertyRequestAppModel(status.Result);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RequestApp(PropertyRequestAppInputModel input)
        {
            if (ModelState.IsValid)
            {
                var status = this.propertyAdapter.SendUserResponse(User.Identity.Name, input.LeadId, input.Message);

                if (status.StatusCode == 200)
                {
                    return View("RequestAppSent");
                }

                HandleErrors(status);
            }

            var lead = this.propertyAdapter.GetUserInterest(User.Identity.Name, input.LeadId);

            if (lead.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            PropertyRequestAppModel model = new PropertyRequestAppModel(lead.Result);
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
                ModelState.AddModelError("Property", s.Message);
            }
        }
    }
}
