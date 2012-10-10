using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Rentler.Web.Axioms
{
	public class JsonNetResult : JsonResult
	{
		public override void ExecuteResult(ControllerContext context)
		{
			if(context == null)
				throw new ArgumentNullException("context");

			var response = context.HttpContext.Response;

			response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

			if(ContentEncoding != null)
				response.ContentEncoding = ContentEncoding;

			if(Data == null)
				return;

			var serializeObject = JsonConvert.SerializeObject(
				Data,
				Formatting.Indented,
				new JsonSerializerSettings
				{
					PreserveReferencesHandling = PreserveReferencesHandling.Objects
				});
			response.Write(serializeObject);
		}
	}
}