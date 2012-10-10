using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KslIntegrationV2.Models;

namespace KslIntegrationV2.Controllers
{
	public class HomeController : Controller
	{
		string host = "https://stage.rentler.com";
		//string host = "http://localhost:48325";
		Guid apiKey = Guid.Parse("7A4F5D0A-C9D0-4FA2-9A5E-36061055BBFB");

		//
		// GET: /Home/

		public ActionResult Search()
		{
			var token = RentlerAuthClient.GetAuthToken();

			var model = new RentlerModel
			{
				RentlerHost = host,
				Token = token.Result
			};

			return View(model);
		}

		public ActionResult Listing(long? id)
		{
			ViewBag.ID = id;

			var token = RentlerAuthClient.GetAuthToken();

			var model = new RentlerModel
			{
				RentlerHost = host,
				Token = token.Result
			};

			return View(model);
		}

		public ActionResult Create(long? id)
		{
			ViewBag.ID = id;

			var token = RentlerAuthClient.GetAuthToken();

			var model = new RentlerModel
			{
				RentlerHost = host,
				Token = token.Result
			};

			return View(model);
		}
	}
}
