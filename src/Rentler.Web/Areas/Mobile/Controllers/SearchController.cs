using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Axioms;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Areas.Mobile.Controllers
{
	public class SearchController : Controller
	{
		ISearchAdapter searchAdapter;
		IFriendlyZipAdapter zipAdapter;

		public SearchController(ISearchAdapter searchAdapter, IFriendlyZipAdapter zipAdapter)
		{
			this.searchAdapter = searchAdapter;
			this.zipAdapter = zipAdapter;
		}

		[AllowCrossSiteJson]
		public ActionResult Index(Search search)
		{
			var results = this.searchAdapter.Search(search);

			return this.NewtonJson(results);
		}

		[AllowCrossSiteJson]
        public ActionResult SearchLocation(Search search, float lat, float lng, float? miles)
        {
			if (miles.HasValue)
				return (this.NewtonJson(this.searchAdapter.SearchLocation(lat, lng, miles.Value)));
			else
				return (this.NewtonJson(this.searchAdapter.SearchLocation(lat, lng)));
        }

		public ActionResult Map()
		{
			return View();
		}
	}
}
