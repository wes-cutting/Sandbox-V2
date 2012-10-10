using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using System.Web.Security;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Areas.Ksl.Controllers
{
	public class PropertyController : Controller
	{
		IPropertyAdapter propertyAdapter;
		ISearchAdapter searchAdapter;
		IAuthAdapter authAdapter;
		IListingAdapter listingAdapter;

		public PropertyController(
			IPropertyAdapter propertyAdapter,
			ISearchAdapter searchAdapter,
			IAuthAdapter authAdapter,
			IListingAdapter listingAdapter)
		{
			this.propertyAdapter = propertyAdapter;
			this.searchAdapter = searchAdapter;
			this.authAdapter = authAdapter;
			this.listingAdapter = listingAdapter;
		}

		[HttpGet]
		public ActionResult Index(string email, Guid apiKey)
		{
			var isvalid = authAdapter.ValidateApiKey(apiKey);
			if(isvalid.StatusCode != 200)
				return this.NewtonJson(isvalid);

			var properties = searchAdapter.SearchUserProperties(
				email, new PropertySearch { ResultsPerPage = int.MaxValue });

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

		public ActionResult Create(Guid? token)
		{
			if(!User.Identity.IsAuthenticated && token.HasValue)
			{
				var user = authAdapter.ValidateAuthToken(token.Value);

				if(user.StatusCode == 200)
					FormsAuthentication.SetAuthCookie(user.Result.Username, true);
			}

			return View();
		}
	}
}
