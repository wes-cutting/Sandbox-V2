using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Models;
using Rentler.Web.Axioms;
using Rentler.Facades;
using Rentler.Common;
using System.ComponentModel.DataAnnotations;

namespace Rentler.Web.Controllers
{
	/// <summary>
	/// Controller for viewing listings.
	/// </summary>
	public class ListingController : Controller
	{
		IListingAdapter listingAdapter;
        IListingFacade listingFacade;
        IPropertyAdapter propertyAdapter;
        ISearchAdapter searchAdapter;
        IOrderAdapter orderAdapter;
        IFeaturedAdapter featuredAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListingController"/> class.
		/// </summary>
		/// <param name="listingAdapter">The listing adapter.</param>
		public ListingController(
            IListingAdapter listingAdapter,
            IPropertyAdapter propertyAdapter,
            ISearchAdapter searchAdapter,
            IOrderAdapter orderAdapter,
            IFeaturedAdapter featuredAdapter,
            IListingFacade listingFacade)
		{
			this.listingAdapter = listingAdapter;
            this.propertyAdapter = propertyAdapter;
            this.searchAdapter = searchAdapter;
            this.orderAdapter = orderAdapter;
            this.featuredAdapter = featuredAdapter;
            this.listingFacade = listingFacade;
		}

        /// <summary>
		/// View a listing by its id.
		/// </summary>
		/// <param name="id">The id of the listing to view.</param>
		/// <returns>The listing page</returns>
		//disabled until we can separately cache ajax/straight listings
		//[UnAuthenticatedCache(Duration = 3600)]
		public ActionResult Index(long? id)
		{
            if (!id.HasValue)
                return this.NotFoundException();

			var status = this.listingAdapter.GetListing(id.Value);

            if (status.StatusCode != 200)
                return this.NotFoundException();

            this.listingAdapter.IncrementListingViews(id.Value);

			// this is ok because the adapter will return 0 if count cannot
			// be retrieved
			var viewCount = this.listingAdapter.GetListingViews(id.Value).Result;
			var userHasSaved = this.listingAdapter.ListingWasSavedBy(id.Value, User.Identity.Name).Result;

			var model = new ListingIndexModel();
			model.Listing = status.Result;
			model.ListingViews = viewCount;
			model.UserHasSaved = userHasSaved;

			//set the login url to Rentler
			model.LoginUrl = "/account/login?ReturnUrl=/listing/" + status.Result.BuildingId;

            if (Request.IsAjaxRequest())
                return PartialView("ListingDetails", model);

			return View(model);
		}

        /// <summary>
        /// Shows a view for reporting a listing for the corresponding
        /// listing id.
        /// </summary>
        /// <param name="id">The id of the listing.</param>
        /// <returns></returns>
		[ActionName("Report")]
		public ActionResult ReportListing(long id)
		{
			ViewBag.Id = id;
			return View();
		}

        /// <summary>
        /// Pushes a report to the listing.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="reportedText">The reported text.</param>
        /// <returns></returns>
		[HttpPost, ActionName("Report")]
		public ActionResult ReportListing(long id, string reportedText)
		{
			var status = this.listingAdapter.ReportListing(id, reportedText);

			if(status.StatusCode != 200)
			{
				string msg = string.Format("Failed to report this listing: {0}", status.Message);
				return RedirectToAction("ReportError", new { id = id, error = msg });
			}

			return RedirectToAction("Index", new { id = id });
		}

        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
		public ActionResult ReportError(long id, string error)
		{
			ViewBag.Id = id;
			ViewBag.Error = error;

			return View();
		}

        /// <summary>
        /// Interesteds the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
		[HttpGet]
		public ActionResult Interested(long? id)
		{
            if (!id.HasValue)
                return this.NotFoundException();

			var status = this.listingAdapter.GetListing(id.Value);

			//if (status.StatusCode != 200)            
			//    return HttpNotFound();

			return View(status.Result);
		}

        /// <summary>
        /// Action for displaying the user's saved properties.
        /// </summary>
        /// <returns>View of the user's saved properties.</returns>
        [Authorize]
        public ActionResult Favorites(int? page, int? pageSize)
        {
            var result = this.listingAdapter.GetFavoritesForUser(User.Identity.Name, page, pageSize);
            return View(result.Result);
        }

        [Authorize]
        public ActionResult List(long? id)
        {
            if (!id.HasValue)
                return this.NotFoundException();

            var status = this.propertyAdapter.GetPropertyListingInfo(id.Value, User.Identity.Name);

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
        public ActionResult Terms(long? id)
        {
            if (!id.HasValue)
                return this.NotFoundException();

            var status = this.propertyAdapter.GetProperty(id.Value, User.Identity.Name);

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
        public ActionResult Promote(long? id)
        {
            if (!id.HasValue)
                return this.NotFoundException();

            var status = this.propertyAdapter.GetPropertyPromoteInfo(id.Value, User.Identity.Name);

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
                        return Redirect("/order/checkout/" + status.Result.TemporaryOrderId);
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

            return Redirect("/property/manage/" + id);
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
