using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.Extensions
{
	public static class ControllerExtensions
	{
		public static JsonNetResult NewtonJson(this Controller controller, object model)
		{
			return new JsonNetResult { Data = model };
		}
	}
}