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

		public SearchController(ISearchAdapter searchAdapter)
		{
			this.searchAdapter = searchAdapter;
		}

		[AllowCrossSiteJson]
		public ActionResult Index(Search search)
		{
			var results = this.searchAdapter.Search(search);

			return this.NewtonJson(results);
		}
	}
}
