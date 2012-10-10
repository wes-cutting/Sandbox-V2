using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;

namespace KslIntegrationV2
{
	public static class RentlerAuthClient
	{
		//static string url = "http://localhost:48325/ksl/auth";
		static string url = "https://stage.rentler.com/ksl/auth";
		static Guid apiKey = Guid.Parse("7A4F5D0A-C9D0-4FA2-9A5E-36061055BBFB");

		public static Status<Guid?> GetAuthToken()
		{
			var requestUrl = url;

			string data = string.Format("apiKey={0}&email={1}",
				apiKey.ToString(),
				"dustin.dahl@gmail.com");
			
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] bytes = encoding.GetBytes(data);

			var req = (HttpWebRequest)HttpWebRequest.Create(url);
			req.Method = "POST";
			req.ContentType = "application/x-www-form-urlencoded";
			req.ContentLength = bytes.Length;

			var stream = req.GetRequestStream();
			stream.Write(bytes, 0, bytes.Length);
			stream.Close();

			var res = req.GetResponse();

			Status<Guid?> result = null;

			using(var reader = new StreamReader(res.GetResponseStream()))
				result = JsonConvert.DeserializeObject<Status<Guid?>>(reader.ReadToEnd());

			return result;
		}
	}
}