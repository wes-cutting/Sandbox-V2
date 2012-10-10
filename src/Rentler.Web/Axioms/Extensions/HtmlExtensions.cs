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
using System.Text.RegularExpressions;

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

    public static string OnlyAscii(this HtmlHelper html, string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        input = input.Replace("\r\n", " ")
            .Replace("\n", " ")
            .Replace("\t", " ");
        return Regex.Replace(input, @"[^\u0000-\u007F]", string.Empty);
    }

    public static string BuildNumber(this HtmlHelper html)
    {
        if (html.ViewContext.HttpContext.Cache["_buildnumber_"] == null)
            html.ViewContext.HttpContext.Cache["_buildnumber_"] =
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();

        return html.ViewContext.HttpContext.Cache["_buildnumber_"].ToString();
    }

    public static string BuildVersion(this HtmlHelper html)
    {
        if (html.ViewContext.HttpContext.Cache["_buildversion_"] == null)
            html.ViewContext.HttpContext.Cache["_buildversion_"] =
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        return html.ViewContext.HttpContext.Cache["_buildversion_"].ToString();
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
        return string.Format("{0}/images/noimage-{1}x{2}.jpg", App.Hostname, width, height);
#endif
    }

    public static string GetRibbonLink(this HtmlHelper html, string ribbonId, bool small = true)
    {
        string filePattern = "/images/ribbons/{0}-161x54.png";

        if (!small)
            filePattern = "/images/ribbons/{0}-180x59.png";

        return string.Format(filePattern, ribbonId);
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

    public static string GetKslListingUrl(this HtmlHelper helper, long buildingId)
    {
        return string.Format(
            "{0}{1}{2}",
            Rentler.Web.Config.KslDomain,
            Rentler.Web.Config.KslListingPath,
            buildingId
        );
    }

	public static string GetKslLoginUrl(this HtmlHelper helper, long buildingId)
	{
		return string.Format("{0}{1}{2}",
			Rentler.Web.Config.KslDomain,
			Rentler.Web.Config.KslLoginPath,
			"?login_forward=" + GetKslListingUrl(helper, buildingId));
	}

    public static string GetKslSearchUrl(this HtmlHelper helper)
    {
        return string.Format(
            "{0}{1}",
            Rentler.Web.Config.KslDomain, Rentler.Web.Config.KslSearchPath
        );
    }

    public static RouteValueDictionary GetRouteData(this HtmlHelper helper)
    {
        var routeData = new RouteValueDictionary(helper.ViewContext.RouteData.Values);
        foreach (string item in helper.ViewContext.RequestContext.HttpContext.Request.QueryString.Keys)
        {
            if (item != null && item != "page" && item != "sid" && item != "nid" && item != "Page")
            {
                if (helper.ViewContext.RequestContext.HttpContext.Request.QueryString[item] != null)
                {
                    if (helper.ViewContext.RequestContext.HttpContext.Request.QueryString[item].ToString() == "true,false")
                    {
                        routeData[item] = "true";
                    }
                    else
                    {
                        routeData[item] = helper.ViewContext.RequestContext.HttpContext.Request.QueryString[item].ToString();
                    }
                }
            }
        }

        return routeData;
    }

    public static string ToJson(this HtmlHelper helper, object o)
    {
        string r = new JavaScriptSerializer().Serialize(o);
        return r;
    }

    public static string FormatPhone(this HtmlHelper helper, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return string.Empty;

        try
        {
            return string.Format(
                "{0}-{1}-{2}",
                phoneNumber.Substring(0, 3),
                phoneNumber.Substring(3, 3),
                phoneNumber.Substring(6, 4)
            );
        }
        catch (Exception)
        {
            return phoneNumber;
        }
    }
}
