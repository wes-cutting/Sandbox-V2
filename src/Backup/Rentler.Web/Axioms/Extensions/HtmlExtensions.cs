using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Configuration;
using Rentler;
using Rentler.Web.Axioms;
using System.Web.Routing;
using System.Web.Script.Serialization;

public static partial class HtmlExtensions
{
    public static string GetListingTitle(this HtmlHelper html, Building listing)
    {
        return
            listing.City + ", " +
            listing.State + " " +
            (listing.PropertyType == Rentler.PropertyType.Apartment ? "Apartment" : "Home") +
            " Rental " + listing.Address1 + " - Rentler.com";
    }

    public static string BuildNumber(this HtmlHelper html)
    {
        if (html.ViewContext.HttpContext.Cache["_buildnumber_"] == null)
            html.ViewContext.HttpContext.Cache["_buildnumber_"] =
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();

        return html.ViewContext.HttpContext.Cache["_buildnumber_"].ToString();
    }

    public static string Truncater(this HtmlHelper html, string input, int length)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        if (input.Length < length - 2)
            return input;

        return input.Substring(0, length - 3) + "...";
    }

    public static string GetPhotoLink(this HtmlHelper html, long buildingId, Guid? photoId, int width, int height, string extension)
    {
        if (!photoId.HasValue)
            return GetNoImageLink(width, height);

        return Storage.GetPhotoLink(buildingId, photoId.Value, width, height, extension);
    }

    public static string GetNoImageLink(int width, int height)
    {
#if DEBUG
        return string.Format("{0}/images/noimage-{1}x{2}.jpg", App.Hostname, width, height);
#else
        return string.Format("https://{0}/images/noimage-{1}x{2}.jpg", App.Hostname, width, height);
#endif
    }

    public static string GoogleMapsLink(this HtmlHelper html, string fullAddress, float latitude, float longitude)
    {
        return string.Format(
            "https://maps.google.com/maps?" +
            "f=q&source=s_q&hl=en&geocode=&q={0}&aq=0&ie=UTF8&hq=&hnear={0}&ll={1},{2}&spn=0.013537,0.027874&z=16&iwloc=A",
            fullAddress, latitude, longitude
        );
    }

    public static string FriendlyTimeSpan(this HtmlHelper html, TimeSpan? time)
    {
        if (time == null)
            return "Unknown";

        TimeSpan difference = time.Value;
        string timeString = "Unknown";
        if ((int)difference.TotalSeconds > 0)
        {
            timeString = ((int)difference.TotalSeconds).ToString();
            timeString += difference.TotalSeconds == 1 ? " second" : " seconds";
        }
        if ((int)difference.TotalMinutes > 0)
        {
            timeString = ((int)difference.TotalMinutes).ToString();
            timeString += difference.TotalMinutes == 1 ? " minute" : " minutes";
        }
        if ((int)difference.TotalHours > 0)
        {
            timeString = ((int)difference.TotalHours).ToString();
            timeString += difference.TotalHours == 1 ? " hour" : " hours";
        }
        if ((int)difference.TotalDays > 0)
        {
            timeString = ((int)difference.TotalDays).ToString();
            timeString += difference.TotalDays == 1 ? " day" : " days";
        }
        return timeString;
    }

	public static RouteValueDictionary GetRouteData(this HtmlHelper helper)
	{
		var routeData = new RouteValueDictionary(helper.ViewContext.RouteData.Values);
		foreach(string item in helper.ViewContext.RequestContext.HttpContext.Request.QueryString.Keys)
			if(item != "page")
				routeData[item] = helper.ViewContext.RequestContext.HttpContext.Request.QueryString[item].ToString();

		return routeData;
	}

    public static string ToJson(this HtmlHelper helper, object o)
    {
        string r = new JavaScriptSerializer().Serialize(o);
        return r;
    }
}
