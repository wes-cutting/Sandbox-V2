using System.Web.Mvc;

namespace Rentler.Web.Areas.Ksl
{
	public class KslAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Ksl";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Ksl_listing2",
				"ksl/listing/index",
				new { controller = "Listing", action = "Index" },
				new string[] { "Rentler.Web.Areas.Ksl.Controllers" }
				);

			context.MapRoute(
				"Ksl_listing_favorite",
				"Ksl/listing/favorite",
				new { controller = "Listing", action = "Favorite" },
				new string[] { "Rentler.Web.Areas.Ksl.Controllers" }
			);

			context.MapRoute(
				"Ksl_listing_unfavorite",
				"Ksl/listing/unfavorite",
				new { controller = "Listing", action = "UnFavorite" },
				new string[] { "Rentler.Web.Areas.Ksl.Controllers" }
			);

			context.MapRoute(
				"Ksl_listing",
				"Ksl/listing/{ad}",
				new { controller = "Listing", action = "Index", ad = UrlParameter.Optional },
				new string[] { "Rentler.Web.Areas.Ksl.Controllers" }
			);

			context.MapRoute(
				"Ksl_default",
				"Ksl/{controller}/{action}/{id}",
				new { action = "Index", controller = "Home", id = UrlParameter.Optional },
				new string[] { "Rentler.Web.Areas.Ksl.Controllers" }
			);
		}
	}
}
