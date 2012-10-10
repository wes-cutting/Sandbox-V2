using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Models;
using Rentler.Web.Axioms;
using Mvc.Mailer;
using System.Text;
using Rentler.Configuration;
 
namespace Rentler.Web.Controllers
{
    /// <summary>
    /// Controller containing the home information for the application.
    /// </summary>
    public class HomeController : Controller 
    { 
        IListingAdapter listingAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController(IListingAdapter listingAdapter) 
        {
            this.listingAdapter = listingAdapter;
        }

        /// <summary>
        /// Returns the home page for the application.
        /// </summary>
        /// <remarks>Output cached for an hour for unauthenticated users.</remarks>
        /// <returns>The home page for the application</returns>
        [UnAuthenticatedCache(Duration = 3600)]
        public ActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();
            return View(model);
        }

        /// <summary>
        /// Returns the privacy policy.
        /// </summary>
        /// <remarks>Output cached for a day for unauthenticated users.</remarks>
        /// <returns>A view with the privacy policy.</returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Returns a view of the about page.
        /// </summary>
        /// <remarks>Output cached for a day for unauthenticated users.</remarks>
        /// <returns>A view of the about page.</returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Returns a view of the terms page.
        /// </summary>
        /// <remarks>Output cached for a day for unauthenticated users.</remarks>
        /// <returns>A view of the terms page.</returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult Terms()
        {
            return View();
        }

        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult Offline()
        {
            return View();
        }

        /// <summary>
        /// Returns a view with the robots.txt for the application.
        /// </summary>
        /// <returns></returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult Robots()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("User-agent: *");
            if (App.Hostname == "https://www.rentler.com")
            {
                builder.AppendLine("Disallow:");
                builder.AppendLine("Disallow: /api/");
                builder.AppendLine("Disallow: /dashboard/");
                builder.AppendLine("Disallow: /bin/");
                builder.AppendLine("Disallow: /account/");
                builder.AppendLine("Disallow: /portfolio");
                builder.AppendLine("");
                builder.AppendLine("Sitemap: " + App.Hostname + "/sitemap.xml");
            }
            else
            {
                builder.AppendLine("Disallow: /");
            }
            
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/plain", "robots.txt");
        }

        /// <summary>
        /// Returns an xml sitemap of the whole site.
        /// </summary>
        /// <returns></returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult Sitemap()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            builder.AppendLine("<urlset ");
            builder.AppendLine("   xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            builder.AppendLine("   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            builder.AppendLine("   xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9");
            builder.AppendLine("      http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

            var listingIdResults = this.listingAdapter.GetAllListingIds();
            foreach (var item in listingIdResults.Result)
                AddSite(builder, App.Hostname + "/listing/" + item.ToString());

            builder.AppendLine("</urlset>");
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "application/xml", "sitemap.xml");
        }

        /// <summary>
        /// Adds the site xml information to the string builder
        /// for use with the Sitemap action.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="url">The URL.</param>
        private void AddSite(StringBuilder builder, string url)
        {
            builder.AppendLine("   <url>");
            builder.AppendLine("      <loc>" + url + "</loc>");
            builder.AppendLine("      <changefreq>always</changefreq>");
            builder.AppendLine("   </url>");
        }
    }
}
