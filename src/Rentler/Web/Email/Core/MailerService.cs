using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using ServiceStack.Text;

namespace Rentler.Web.Email
{
	public class MailerService
	{
		public static Status<bool> SendMail<T>(T model, string path)
		{
			var requestUrl = ResolveUrl(path);

			var resSerializer = new JsonSerializer<Status<bool>>();
			var modelSerializer = new JsonSerializer<T>();

			string data = QueryStringSerializer.SerializeToString<T>(model);

			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] bytes = encoding.GetBytes(data);

			var req = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
			req.Method = "POST";
			req.ContentType = "application/x-www-form-urlencoded";
			req.ContentLength = bytes.Length;

			WebResponse res = null;

			try
			{
				var stream = req.GetRequestStream();
				stream.Write(bytes, 0, bytes.Length);
				stream.Close();

				res = req.GetResponse();
			}
			catch(Exception exc)
			{
				return Status.Error<bool>(exc.Message, false);
			}

			Status<bool> result = null;

			using(var reader = new StreamReader(res.GetResponseStream()))
				result = resSerializer.DeserializeFromString(reader.ReadToEnd());

			return result;
		}

		static string ResolveUrl(string path)
		{
			return Configuration.App.Hostname + "/email/" + path;
		}
	}
}
