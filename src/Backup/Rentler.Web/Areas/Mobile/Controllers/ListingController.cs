using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Axioms;
using Rentler.Web.Axioms.Extensions;
using Rentler.Adapters;

namespace Rentler.Web.Areas.Mobile.Controllers
{
	public class ListingController : Controller
	{
		IListingAdapter adapter;

		public ListingController(IListingAdapter adapter)
		{
			this.adapter = adapter;
		}

		[AllowCrossSiteJson]
		public ActionResult Index(long id)
		{
			var result = adapter.GetListing(id);

			return this.NewtonJson(result);
		}
	}
}
