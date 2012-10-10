using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Rentler.Data;
using System.Web.Configuration;
using Rentler.Web.App_Start;

namespace Rentler.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(HttpException)
            });
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "sitemap",
                "sitemap.xml",
                new { controller = "Home", action = "Sitemap" },
                new string[] { "Rentler.Web.Controllers" }
            );

            routes.MapRoute(
                "robots",
                "robots.txt",
                new { controller = "Home", action = "Robots" },
                new string[] { "Rentler.Web.Controllers" }
            );

            routes.MapRoute(
                "ListingId",
                "listing/{id}",
                new { controller = "Listing", action = "Index" },
                new { id = @"^[0-9]+$" },
                new string[] { "Rentler.Web.Controllers" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Rentler.Web.Controllers" }
            );

            // Show a 404 error page for anything else.
            routes.MapPageRoute("Error", "{*url}", "~/Error/404.aspx");
        }

        protected void Application_Start()
        {
			#if DEBUG
			Database.SetInitializer<RentlerContext>(new RentlerContextInitializer());
			#else
			Database.SetInitializer<RentlerContext>(new CreateDatabaseIfNotExists<RentlerContext>());
			#endif

            RouteTable.Routes.MapRoute(
                "Dashboard_Override",
                "dashboard",
                new { controller = "Property", action = "Index" },
                new string[] { "Rentler.Web.Controllers" }
            );

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //var context = Context;

            //var error = context.Server.GetLastError() as HttpException;
            //if (error == null)
            //    return;

            //var statusCode = error.GetHttpCode().ToString();

            //// we can still use the web.config custom errors information to
            //// decide whether to redirect
            //var config = (CustomErrorsSection)WebConfigurationManager.GetSection("system.web/customErrors");
            //if (config.Mode == CustomErrorsMode.On ||
            //    (config.Mode == CustomErrorsMode.RemoteOnly && context.Request.Url.Host != "localhost"))
            //{
            //    // set the response status code
            //    context.Response.StatusCode = error.GetHttpCode();

            //    // Server.Transfer to correct ASPX file for error
            //    if (config.Errors[statusCode] != null)
            //    {

            //        HttpContext.Current.Server.Transfer(config.Errors[statusCode].Redirect);
            //    }
            //    else
            //        HttpContext.Current.Server.Transfer(config.DefaultRedirect);
            //}
        }
    }
}