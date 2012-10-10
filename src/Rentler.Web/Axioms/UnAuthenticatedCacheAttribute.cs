using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Rentler.Configuration;

namespace Rentler.Web.Axioms
{
    /// <summary>
    /// Custom cache attribute that excludes unauthenticated
    /// users from the cache.
    /// </summary>
    public class UnAuthenticatedCacheAttribute : OutputCacheAttribute
    {
        private OutputCacheLocation? originalLocation;

        /// <summary>
        /// This method is an implementation of 
        /// <see cref="M:System.Web.Mvc.IActionFilter.OnActionExecuting(System.Web.Mvc.ActionExecutingContext)"/> 
        /// and supports the ASP.NET MVC infrastructure. It is not intended to be used directly from your code.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If caching isn't enabled then just kill it
            if (!App.L1CacheEnabled)
            {
                Location = OutputCacheLocation.None;
                base.OnActionExecuting(filterContext);
                return;
            }
            
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // required fix because attributes are cached
                // so if you hit a page unauthenticated it will
                // remain unauthenticated
                originalLocation = originalLocation ?? Location;

                // it's crucial not to cache Authenticated content
                Location = OutputCacheLocation.None;
            }
            else
            {
                Location = originalLocation ?? Location;
            }

            // set a filter callback. Also required because of
            // the caching content.
            filterContext.HttpContext.Response.Cache
                .AddValidationCallback(OnlyIfAnonymous, null);

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Filter method for detecting anonymous users.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="data">The data.</param>
        /// <param name="status">The status.</param>
        public void OnlyIfAnonymous(HttpContext httpContext, 
            object data, ref HttpValidationStatus status)
        {
            if (httpContext.User.Identity.IsAuthenticated)
                status = HttpValidationStatus.IgnoreThisRequest;
            else
                status = HttpValidationStatus.Valid;
        }
    }
}