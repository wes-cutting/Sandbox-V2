using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Axioms;

namespace Rentler.Web
{
	public static class ControllerExtensions
	{
		public static JsonNetResult NewtonJson(this Controller controller, object model)
		{
			return new JsonNetResult { Data = model };
		}

        public static ActionResult NotFoundException(this Controller controller)
        {
            throw new HttpException(404, "Not Found");
        }
	}
}