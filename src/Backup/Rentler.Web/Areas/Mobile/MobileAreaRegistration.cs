using System.Web.Mvc;

namespace Rentler.Web.Areas.Mobile
{
	public class MobileAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Mobile";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
                "MobileListing",
                "mobile/listing/{id}",
                new { controller = "Listing", action = "Index" },
                new string[] { "Rentler.Web.Areas.Mobile.Controllers" }
            );

			context.MapRoute(
				"Mobile_default",
				"Mobile/{controller}/{action}/{id}",
				new { action = "Index", controller = "Home", id = UrlParameter.Optional},
				new string[] { "Rentler.Web.Areas.Mobile.Controllers" }
			);
		}
	}
}
