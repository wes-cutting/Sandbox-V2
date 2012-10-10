using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Areas.Dashboard.Models;
using Rentler.Adapters;
using System.Threading;
using Rentler.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Rentler.Auth;
using Rentler.Common;
using Rentler.Facades;

namespace Rentler.Web.Areas.Dashboard.Controllers
{
	/// <summary>
	/// Controller for users to manage their properties.
	/// </summary>
	public class PropertyController : Controller
	{
		IPropertyAdapter propertyAdapter;
		ISearchAdapter searchAdapter;
		IListingAdapter listingAdapter;
		IFeaturedAdapter featuredAdapter;
		IOrderAdapter orderAdapter;
		IAuthAdapter authAdapter;
        IPropertyFacade propertyFacade;

		/// <summary>
		/// Initializes a new instance of the <see cref="PropertyController"/> class.
		/// </summary>
		/// <param name="propertyAdapter">The property adapter.</param>
		public PropertyController(
			IPropertyAdapter propertyAdapter,
			ISearchAdapter searchAdapter,
			IListingAdapter listingAdapter,
			IFeaturedAdapter featuredAdapter,
			IOrderAdapter orderAdapter,
			IAuthAdapter authAdapter,
            IPropertyFacade propertyFacade)
		{
			this.propertyAdapter = propertyAdapter;
			this.searchAdapter = searchAdapter;
			this.listingAdapter = listingAdapter;
			this.featuredAdapter = featuredAdapter;
			this.orderAdapter = orderAdapter;
			this.authAdapter = authAdapter;

            this.propertyFacade = propertyFacade;
		}

        #region Standard Property

        

        

        [Authorize]
        public ActionResult Delete(long id, string confirm)
        {
            if (!confirm.ToLower().Equals("delete"))
                return Redirect(string.Format("/dashboard/property/manage/{0}#delete", id));

            var status = this.propertyAdapter.DeleteBuilding(id, User.Identity.Name);

            if (status.StatusCode == 200)
                return RedirectToAction("search");

            return Redirect(string.Format("/dashboard/property/manage/{0}#delete", id));
        }

        #endregion

        #region Listing

        #endregion

        

		/// <summary>
		/// Displays the advertisement page for landlords to 
		/// advertise their listing.
		/// </summary>
		/// <param name="id">The id of the building.</param>
		/// <returns>View with all the advertising details.</returns>
		[Authorize]
		public ActionResult Advertise(long id)
		{
			var result = this.listingAdapter.GetListing(id);
			if (result.StatusCode != 200)
				return this.NotFoundException();

			var preview = this.listingAdapter.GenerateBuildingPreview(result.Result);

			PropertyAdvertiseModel model = new PropertyAdvertiseModel();
			model.Listing = result.Result;
			model.Preview = preview.Result;
			model.ListingViews = this.listingAdapter.GetListingViews(result.Result.BuildingId).Result;

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
                    if(command == "list")
                        return RedirectToAction("list", new { id = result.Result.BuildingId });
                    
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
                    if (command == "save")
                        return RedirectToAction("manage", new { id = request.Result.BuildingId });

                    if (command == "list")
                        return RedirectToAction("list", new { id = request.Result.BuildingId });

                    HandleErrors(request);
                }
            }

            PropertyEditModel model = new PropertyEditModel(input);
            return View(model);
        }

        [Authorize]
		public ActionResult List(long id)
		{						
			var status = this.propertyAdapter.GetPropertyListingInfo(id, User.Identity.Name);

			if (status.StatusCode != 200)
				return this.NotFoundException();
            
            PropertyListModel model = new PropertyListModel(status.Result);
			
			model.StepsAvailable = GetStepsAvailable(status.Result);
			
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public ActionResult List(Listing input)
		{
			if (ModelState.IsValid)
			{
				var status = this.propertyAdapter.UpdatePropertyListingInfo(input);

				if (status.StatusCode == 200)
					return RedirectToAction("terms", new { id = input.BuildingId });

				HandleErrors(status);
			}

			var buildingVal = this.propertyAdapter.GetPropertyListingInfo(input.BuildingId, User.Identity.Name);

			if (buildingVal.StatusCode != 200)
				return this.NotFoundException();
			            
            PropertyListModel model = new PropertyListModel()
			{
				Input = input,
				StepsAvailable = GetStepsAvailable(buildingVal.Result),
                UserContacts = buildingVal.Result.User.ContactInfos,
                TemporaryOrderId = buildingVal.Result.TemporaryOrderId
			};

            if (input.BuildingAmenities == null)
                input.BuildingAmenities = new List<BuildingAmenity>();

            if (input.CustomAmenities == null)
                input.CustomAmenities = new List<CustomAmenity>();
			
			return View(model);
		}

		[Authorize]
		public ActionResult Terms(long id)
		{
			var status = this.propertyAdapter.GetProperty(id, User.Identity.Name);

			if (status.StatusCode != 200)
				return this.NotFoundException();

			PropertyTermsModel model = new PropertyTermsModel();
			model.Input = new PropertyTermsInputModel(status.Result);
			model.StepsAvailable = GetStepsAvailable(status.Result);

			return View(model);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Terms(PropertyTermsInputModel input)
		{
			if (ModelState.IsValid)
			{
				var status = this.propertyAdapter.UpdatePropertyTerms(User.Identity.Name, input.ToBuilding());

				if (status.StatusCode == 200)
					return RedirectToAction("promote", new { id = input.BuildingId });

				HandleErrors(status);
			}

			var buildingVal = this.propertyAdapter.GetProperty(input.BuildingId, User.Identity.Name);

			if (buildingVal.StatusCode != 200)
				return this.NotFoundException();

			// return the model back with model state errors
			PropertyTermsModel model = new PropertyTermsModel()
			{
				Input = input,
				StepsAvailable = GetStepsAvailable(buildingVal.Result)
			};

			return View(model);
		}

		[Authorize]
		public ActionResult Promote(long id)
		{
			var status = this.propertyAdapter.GetPropertyPromoteInfo(id, User.Identity.Name);

			var featured = this.featuredAdapter.GetFeaturedDates();

			if (status.StatusCode != 200)
				return this.NotFoundException();

			Building building = status.Result;

			PropertyPromoteModel model = new PropertyPromoteModel();
			model.StepsAvailable = GetStepsAvailable(status.Result);
			model.Input = new PropertyPromoteInputModel(building, featured.Result);

			return View(model);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Promote(PropertyPromoteInputModel input)
		{
			if (ModelState.IsValid)
			{
				// rebuild building
				Building building = input.ToBuilding();

				if (string.IsNullOrWhiteSpace(input.SelectedRibbonId) || input.SelectedRibbonId.ToLower() == "none")
					input.SelectedRibbonId = null;

				var status = this.propertyAdapter.UpdatePropertyPromotions(
					User.Identity.Name,
					building,
					input.SelectedRibbonId,
					input.FeaturedDates,
					input.ListingHasPriority ? "prioritylisting" : string.Empty
				);

				if (status.StatusCode == 200)
				{
					// see if user selected a ribbon or requested to feature their property (coming soon)
					if (status.Result.TemporaryOrder == null)
					{
						this.propertyAdapter.ActivateBuilding(building.BuildingId, User.Identity.Name);

						return RedirectToAction("complete", new { id = input.BuildingId });
					}
					else
						return Redirect("/dashboard/order/checkout/" + status.Result.TemporaryOrderId);
				}

				HandleErrors(status);
			}

			var buildingVal = this.propertyAdapter.GetProperty(input.BuildingId, User.Identity.Name);

			if (buildingVal.StatusCode != 200)
				return this.NotFoundException();

			// reset model properties not persisted
			input.PrimaryPhotoId = buildingVal.Result.PrimaryPhotoId;
			input.PrimaryPhotoExtension = buildingVal.Result.PrimaryPhotoExtension;

			input.CalendarDates = new CalendarDatesModel();
			// TODO: also need to restore blackout and featured dates too

			if (input.CalendarDates.ReservedDates.Any())
				input.CalendarDates.ReservedDates =
					input.FeaturedDates.Select(d => d.ToString("G")).ToArray();
			else
				input.CalendarDates.ReservedDates = new string[] { };

			PropertyPromoteModel model = new PropertyPromoteModel()
			{
				Input = input,
				StepsAvailable = GetStepsAvailable(buildingVal.Result)
			};

			// return the model back with model state errors
			return View(model);
		}

		[Authorize]
		public ActionResult Complete(int id)
		{
			var building = this.searchAdapter.GetUserProperty(User.Identity.Name, id);

			if (building.StatusCode == 404)
				return this.NotFoundException();
			if (building.StatusCode == 403)
				return new HttpUnauthorizedResult();
			if (building.StatusCode == 500)
				throw new HttpException(500, "Unknown error");

            return Redirect("/dashboard/property/manage/" + id);
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
		public ActionResult ConfirmDeactivate(long id, string confirm)
		{
			if (string.IsNullOrWhiteSpace(confirm) || !confirm.ToLower().Equals("deactivate"))
				return Redirect(string.Format("/dashboard/property/manage/{0}#deactivate", id));

			var status = this.propertyAdapter.DeactivateBuilding(id, User.Identity.Name);

			if (status.StatusCode == 200)
				return Redirect(string.Format("/dashboard/property/manage/{0}", id));

			return Redirect(string.Format("/dashboard/property/manage/{0}#deactivate", id));
		}

		[HttpGet]
		[Authorize]
		public ActionResult Photos(long id)
		{
			if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
				return Json(Status.UnAuthorized<List<Photo>>());

			Status<Photo[]> result = this.propertyAdapter.GetPhotos(User.Identity.Name, id);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		[Authorize]
		public ActionResult DeleteCard(int id, string returnUrl)
		{
			orderAdapter.RemoveUserCreditCard(id);

			return Redirect(returnUrl);
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

		[Authorize]
		public ActionResult DeleteLead(int id)
		{
			var status = this.propertyAdapter.DeleteUserInterest(User.Identity.Name, id);
			return Json(status);
		}

		[Authorize]
		public ActionResult ViewLead(int id)
		{
			var status = this.propertyAdapter.GetUserInterestWithApplication(User.Identity.Name, id);

			if (status.StatusCode != 200)
				throw new HttpException(404, "Not Found");

			PropertyViewLeadModel model = new PropertyViewLeadModel(status.Result);
			return View(model);
		}

		private int GetStepsAvailable(Building building)
		{
			if (building == null)
				return 1;

			List<ValidationResult> validationResults = new List<ValidationResult>();
			ValidationContext context = new ValidationContext(building, null, null);
			bool validated = Validator.TryValidateObject(building, context, validationResults);
			if (!validated)
				return 1;

            if (!building.Acres.HasValue)
                return 2;

			if (!building.DateAvailableUtc.HasValue)
				return 3;

			if (!building.TemporaryOrderId.HasValue)
				return 4;

			// checkout
			return 5;
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
