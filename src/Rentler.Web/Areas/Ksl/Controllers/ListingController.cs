using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using System.Web.Security;
using Rentler.Web.Models;
using Rentler.Web.Axioms.Extensions;
using Rentler.Auth;
using System.ComponentModel.DataAnnotations;
using Rentler.Facades;

namespace Rentler.Web.Areas.Ksl.Controllers
{
	/// <summary>
	/// Controller managing listing functions for 
	/// the KSL integration.
	/// </summary>
	public class ListingController : Controller
	{
		IPropertyAdapter propertyAdapter;
		ISearchAdapter searchAdapter;
		IAuthAdapter authAdapter;
		IListingAdapter listingAdapter;
		IFeaturedAdapter featuredAdapter;
		IPropertyFacade propertyFacade;

		public ListingController(
			IPropertyAdapter propertyAdapter,
			ISearchAdapter searchAdapter,
			IAuthAdapter authAdapter,
			IListingAdapter listingAdapter,
			IFeaturedAdapter featuredAdapter,
			IPropertyFacade propertyFacade)
		{
			this.propertyAdapter = propertyAdapter;
			this.searchAdapter = searchAdapter;
			this.authAdapter = authAdapter;
			this.listingAdapter = listingAdapter;
			this.featuredAdapter = featuredAdapter;
			this.propertyFacade = propertyFacade;
		}

		[HttpGet]
		public ActionResult Index(long? ad, Guid? token)
		{
            if (!ad.HasValue)
                return this.NotFoundException();

			RedisPublisher.Publish("token", "Listing page " + ad.Value + " token: " + token.HasValue.ToString());

			if (!User.Identity.IsAuthenticated && token.HasValue)
			{
				var user = authAdapter.ValidateAuthToken(token.Value);

				if (user.StatusCode == 200)
					CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);

				return Redirect("/ksl/listing/index?ad=" + ad.Value);
			}

			var status = this.listingAdapter.GetListing(ad.Value);

			// this is ok because the adapter will return 0 if count cannot
			// be retrieved
			var viewCount = this.listingAdapter.GetListingViews(ad.Value).Result;

			var userHasSaved = this.listingAdapter.ListingWasSavedBy(ad.Value, User.Identity.Name).Result;

			if (status.StatusCode != 200)
				return this.NotFoundException();

			this.listingAdapter.IncrementListingViews(ad.Value);

			var model = new ListingIndexModel();
			model.Listing = status.Result;
			model.ListingViews = viewCount;
			model.UserHasSaved = userHasSaved;

			//set the login url to Ksl
			model.LoginUrl = string.Format("{0}{1}?login_forward=",
				Rentler.Web.Config.KslDomain,
				Rentler.Web.Config.KslLoginPath);

			model.LoginUrl += Url.Encode(string.Format("{0}{1}{2}",
				Rentler.Web.Config.KslDomain,
				Rentler.Web.Config.KslListingPath,
				 status.Result.BuildingId));

			return View(model);
		}


		public ActionResult List(long? id, Guid? token)
		{
            if (!id.HasValue)
                return this.NotFoundException();

			if (!User.Identity.IsAuthenticated && token.HasValue)
			{
				var user = authAdapter.ValidateAuthToken(token.Value);

				if (user.StatusCode == 200)
				{
					CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);
					return RedirectToAction("list", new { id = id });
				}
			}

			var status = this.propertyAdapter.GetPropertyListingInfo(id.Value, User.Identity.Name);

			if (status.StatusCode != 200)
				return this.NotFoundException();

			Rentler.Web.Models.PropertyListModel model = new Models.PropertyListModel(status.Result);

			model.StepsAvailable = GetStepsAvailable(status.Result);

			return View(model);
		}

		[HttpPost]
		public ActionResult List(Rentler.Common.Listing input)
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
		public ActionResult Promote(PropertyPromoteInputModel input)
		{
			if (ModelState.IsValid)
			{
				// rebuild building
				Building building = input.ToBuilding();

				if (input.SelectedRibbonId.ToLower() == "none")
					input.SelectedRibbonId = null;

				var status = this.propertyAdapter.UpdatePropertyPromotions(
					User.Identity.Name,
					building,
					input.SelectedRibbonId,
					input.FeaturedDates,
					input.ListingHasPriority ? "prioritylisting" : string.Empty);

				if (status.StatusCode == 200)
				{
					// see if user selected a ribbon or requested to feature their property
					if (status.Result.TemporaryOrder == null)
					{
						this.propertyAdapter.ActivateBuilding(building.BuildingId, User.Identity.Name);

						return Redirect("/ksl/property/complete/" + input.BuildingId);
					}
					else
						return Redirect("/ksl/order/checkout/" + status.Result.TemporaryOrderId);
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
			if (input.FeaturedDates != null)
				input.CalendarDates.ReservedDates =
					input.FeaturedDates.Select(d => d.ToString("G")).ToArray();

			PropertyPromoteModel model = new PropertyPromoteModel()
			{
				Input = input,
				StepsAvailable = GetStepsAvailable(buildingVal.Result)
			};

			// return the model back with model state errors
			return View(model);
		}

		[HttpPost]
		public ActionResult Favorite(long buildingId, string email)
		{
			var result = listingAdapter.CreateSavedBuilding(buildingId, email);

			return this.NewtonJson(result);
		}

		[HttpPost]
		public ActionResult UnFavorite(long buildingId, string email)
		{
			var result = listingAdapter.DeleteSavedBuilding(buildingId, email);

			return this.NewtonJson(result);
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

			if (!building.DateAvailableUtc.HasValue)
				return 2;

			if (!building.TemporaryOrderId.HasValue)
				return 3;

			// checkout
			return 4;
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
