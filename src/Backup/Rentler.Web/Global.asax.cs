using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Rentler.Data;

namespace Rentler.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Listing",
                "listing/{id}",
                new { controller = "Listing", action = "Index" },
                new string[] { "Rentler.Web.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Rentler.Web.Controllers" }
            );

        }

        protected void Application_Start()
        {
			#if DEBUG
			Database.SetInitializer<RentlerContext>(new RentlerContextInitializer());
			#else
			Database.SetInitializer<RentlerContext>(new CreateDatabaseIfNotExists<RentlerContext>());
			#endif
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}