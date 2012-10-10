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
                "Ksl_default",
                "Ksl/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Rentler.Web.Areas.Ksl.Controllers" }
            );

			context.MapRoute(
				"Ksl_listing",
				"Ksl/listing/{id}",
				new { controller = "Listing", action = "Index", id = UrlParameter.Optional },
				new string[] { "Rentler.Web.Areas.Ksl.Controllers" }
			);
        }
    }
}
