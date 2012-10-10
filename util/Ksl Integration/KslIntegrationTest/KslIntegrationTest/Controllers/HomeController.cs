using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using KslIntegrationTest.Extensions;
using KslIntegrationTest.Models;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace KslIntegrationTest.Controllers
{
    public class HomeController : Controller
    {
		string host = "http://localhost:15020";
		//string host = "https://www.rentler.com";
        // Azure
        //string host = "https://127.0.0.1:444";

		public HomeController()
		{
		}

		public ActionResult Listing(string id, string email)
        {
            string newListing = string.IsNullOrWhiteSpace(id) ? "27" : id;
			var url = host + string.Format("/api/1/listing.html?id={0}&key=58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F&email={1}", newListing, email);
			var client = new WebClient();
			ViewBag.ListingHtml = client.DownloadString(url);
			ViewBag.RentlerHost = host;
            return View();
        }

		public ActionResult SavedProperties()
		{
			var url = host + "/api/1/profile/savedproperties?id=41&key=58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F&userId=B9C94FDA-6B56-40A6-AEF6-18ED3E326B7A";
			var client = new WebClient();
			ViewBag.SavedPropertiesHtml = client.DownloadString(url);
			ViewBag.RentlerHost = host;
			return View();
		}

		public ActionResult Search()
		{
			// if there is no query passed in build one otherwise
			// request the same querystring passed
			string query = string.IsNullOrWhiteSpace(this.Request.Url.Query) ? 
				"?City=&Zip=&Miles=0&PriceLower=200" +
				"&PriceUpper=1600&PropertyType=Any&PriceUnlimited=&PageSize=27" :
				this.Request.Url.Query;
				
			// add the query information
			query += "&key=58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F";

			var url = host + "/api/1/search.html" + query;

			// get the listings and return our view
			var client = new WebClient();
			ViewBag.SearchHtml = client.DownloadString(url);
			ViewBag.RentlerHost = host;

			return View();
		}

		public ActionResult AuthTest()
		{
			var url = "https://www.rentler.com/api/1/auth/authtoken";
			WebClient client = new WebClient();

			//pass the username and the apiKey to Rentler,
			//which will return a temporary token to use for authentication
			var reqObj = new NameValueCollection();
            reqObj["email"] = "scott.english007@gmail.com";
			reqObj["apiKey"] = "58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F";

			var encoding = Encoding.ASCII;
			var response = encoding.GetString(client.UploadValues(url, reqObj));

			JavaScriptSerializer serializer = new JavaScriptSerializer();

			//the token Rentler has provisioned for us will let us login!
			var result = serializer.Deserialize<StatusResult<AuthTokenResponseModel>>(response).Result;

			//302 redirect the user to the auth action, with the token and return url.
			return Redirect(string.Format("https://www.rentler.com/account/auth?token={0}&returnUrl={1}",
				result.AuthToken.ToString(), "https://www.rentler.com"));
		}

        public ActionResult AuthTestLocal()
        {
            ServicePointManager.ServerCertificateValidationCallback = 
                new System.Net.Security.RemoteCertificateValidationCallback(
                    delegate { return true; }
                );

            var url = string.Format("{0}/api/1/auth/authtoken", host);        
            WebClient client = new WebClient();

            //pass the username and the apiKey to Rentler,
            //which will return a temporary token to use for authentication
            var reqObj = new NameValueCollection();
            reqObj["email"] = "tenant5@gmail.com";
            reqObj["apiKey"] = "58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F";

            var encoding = Encoding.ASCII;
            var response = encoding.GetString(client.UploadValues(url, reqObj));

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //the token Rentler has provisioned for us will let us login!
            var result = serializer.Deserialize<StatusResult<AuthTokenResponseModel>>(response).Result;

            //302 redirect the user to the auth action, with the token and return url.
            return Redirect(string.Format("{0}/account/auth?token={1}&returnUrl={2}", host,
                result.AuthToken.ToString(), "http://localhost:39260/createlisting"));
        }

		public ActionResult ChangePasswordTest()
		{
			var client = new WebClient();
			var reqObj = new NameValueCollection();
			reqObj["email"] = "cyberkruz@gmail.com";
			reqObj["apiKey"] = "58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F";
			reqObj["newPasswordHash"] = "newShatz";

			var url = "http://localhost:15020/api/1/auth/changepassword";
			var encoding = Encoding.ASCII;
			var response = encoding.GetString(client.UploadValues(url, reqObj));

			return View("AuthTest");
		}

		public ActionResult CreateUser()
		{
			var client = new WebClient();
			var reqObj = new NameValueCollection();

            reqObj["apiKey"] = "58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F";
			reqObj["affiliateUserKey"] = "2000000";
			reqObj["email"] = "bobbyshatner5@gmail.com";
			reqObj["passwordHash"] = "lkhklhlhjklkhj";
			reqObj["firstName"] = "Bobby";
			reqObj["lastName"] = "Shatner";
            reqObj["username"] = "bobbyshatner5@gmail.com";

			var url = "http://localhost:15020/api/1/auth/createuser";
			var encoding = Encoding.ASCII;
			var response = encoding.GetString(client.UploadValues(url, reqObj));            

			return View();
		}

		public ActionResult TestDeleteProperty()
		{
			var client = new WebClient();
			var reqObj = new NameValueCollection();
			reqObj["apiKey"] = "58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F";
			reqObj["email"] = "dustin.dahl@gmail.com";
			reqObj["propertyInfoId"] = "195982";

			string url = "http://localhost:15020/api/1/property/delete";
			Encoding encoding = Encoding.ASCII;
			string response = encoding.GetString(client.UploadValues(url, reqObj));

			ViewBag.Result = response;
			return View();
		}

		public ActionResult TestActivateProperty()
		{
			var client = new WebClient();
			var reqObj = new NameValueCollection();
			reqObj["apiKey"] = "58AD1D80-7ED5-4ACD-AF7E-99C6AD3B1C2F";
			reqObj["email"] = "dustin.dahl@gmail.com";
			reqObj["id"] = "193917";

			string url = "http://localhost:15020/api/1/listing/activate";
			Encoding encoding = Encoding.ASCII;
			string response = encoding.GetString(client.UploadValues(url, reqObj));

			ViewBag.Result = response;
			return View();
			
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult CreateListing()
		{
			ViewBag.RentlerHost = host;

			return View();
		}
    }
}
