using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using System.Web.Security;
using Rentler.Web.Axioms.Extensions;
using Rentler.Web.Areas.Dashboard.Models;
using System.ComponentModel.DataAnnotations;
using Rentler.Auth;
using Rentler.Common;
using Rentler.Facades;
using Rentler.Configuration;

namespace Rentler.Web.Areas.Ksl.Controllers
{
	public class PropertyController : Controller
	{
		IPropertyAdapter propertyAdapter;
		ISearchAdapter searchAdapter;
		IAuthAdapter authAdapter;
		IListingAdapter listingAdapter;
        IFeaturedAdapter featuredAdapter;
        IPropertyFacade propertyFacade;

		public PropertyController(
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
		public ActionResult Index(string email, Guid apiKey)
		{
			var isvalid = authAdapter.ValidateApiKey(apiKey);
			if (isvalid.StatusCode != 200)
				return this.NewtonJson(isvalid);

			var props = propertyFacade.SearchForUserProperty(new PropertySearch { 
				 
			});

			var properties = searchAdapter.SearchUserProperties(
				email, new KslPropertySearch { ResultsPerPage = int.MaxValue });

			return this.NewtonJson(properties);
		}

		[HttpPost]
		public ActionResult Activate(long buildingId, Guid apiKey)
		{
			var isvalid = authAdapter.ValidateApiKey(apiKey);
			if(isvalid.StatusCode != 200)
				return this.NewtonJson(isvalid);

			var result = this.propertyAdapter.ActivateBuilding(buildingId, apiKey);

			var response = new Status<bool>
			{
				StatusCode = result.StatusCode,
				Errors = result.Errors,
				Message = result.Message,
				Result = result.Result != null
			};

			return this.NewtonJson(response);
		}

		[HttpPost]
		public ActionResult DeActivate(long buildingId, Guid apiKey)
		{
			var isvalid = authAdapter.ValidateApiKey(apiKey);
			if(isvalid.StatusCode != 200)
				return this.NewtonJson(isvalid);

			var result = this.propertyAdapter.DeactivateBuilding(buildingId, apiKey);

			var response = new Status<bool>
			{
				StatusCode = result.StatusCode,
				Errors = result.Errors,
				Message = result.Message,
				Result = result.Result != null
			};

			return this.NewtonJson(response);
		}

		[HttpGet]
		public ActionResult Favorites(string email, Guid apiKey)
		{
			var isvalid = authAdapter.ValidateApiKey(apiKey);
			if(isvalid.StatusCode != 200)
				return this.NewtonJson(isvalid);

			var favorites = listingAdapter.GetFavoritesForUser(email, 1, int.MaxValue);

			return this.NewtonJson(favorites);
		}

        public ActionResult Create(Guid? token, int? PropertyTypeCode)
        {
            if (!User.Identity.IsAuthenticated && token.HasValue)
            {
                var user = authAdapter.ValidateAuthToken(token.Value);

                if (user.StatusCode == 200)
                {
					CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);
                    return RedirectToAction("create");
                }
            }

			Rentler.Web.Models.PropertyCreateModel model = new Models.PropertyCreateModel();
            model.IsKsl = true;

            // set property type from ksl from user selection
            model.Input.PropertyTypeCode = PropertyTypeCode.HasValue ? PropertyTypeCode.Value : 0;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Models.PropertyCreateInputModel input, string command)
        {
            if (ModelState.IsValid)
            {
                var result = this.propertyAdapter.CreateProperty(input.ToBuilding());

                if (result.StatusCode == 200)
                {
					if (command == "list")
						return Redirect("/ksl/listing/list/" + result.Result.BuildingId);

                    if (command == "save")
                    {
                        string uri = string.Format(
                            "{0}/dashboard/property/manage/{1}", 
                            Config.Hostname, 
                            result.Result.BuildingId
                        );

                        return Redirect(uri);
                    }
                }

                HandleErrors(result);
            }

            Rentler.Web.Models.PropertyCreateModel model = new Models.PropertyCreateModel(input);
            model.IsKsl = true;
            return View(model);
        }
        
        public ActionResult Edit(long id, Guid? token)
        {
            if (!User.Identity.IsAuthenticated && token.HasValue)
            {
                var user = authAdapter.ValidateAuthToken(token.Value);

                if (user.StatusCode == 200)
                {
                    CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);
                    return RedirectToAction("edit");
                }
            }

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
                    {
                        string uri = string.Format(
                            "{0}/dashboard/property/manage/{1}",
                            Config.Hostname,
                            request.Result.BuildingId
                        );

                        return Redirect(uri);
                    }

                    if (command == "list")
                        return RedirectToAction("list", new { id = request.Result.BuildingId });

                    HandleErrors(request);
                }
            }

            PropertyEditModel model = new PropertyEditModel(input);
            return View(model);
        }
        
        public ActionResult Complete(int id)
        {
            var building = this.searchAdapter.GetUserProperty(User.Identity.Name, id);

            if (building.StatusCode == 404)
                return this.NotFoundException();
            if (building.StatusCode == 403)
                return new HttpUnauthorizedResult();
            if (building.StatusCode == 500)
                throw new HttpException(500, "Unknown error");

			return Redirect(App.KslUrl + "index.php?sid=17403849&nid=651&ad=" + id);
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
