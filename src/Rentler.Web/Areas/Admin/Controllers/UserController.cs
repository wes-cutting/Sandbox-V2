using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Areas.Admin.Models;
using Rentler.Facades;

namespace Rentler.Web.Areas.Admin.Controllers
{
   // [Authorize(Roles="Admin")]
    public class UserController : Controller
    {
        IUserFacade userFacade;

        public UserController(IUserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        public ActionResult Search(string query, int? page)
        {
            page = page.HasValue ? page.Value : 1;
            var result = this.userFacade.SearchForUsers(query, page.Value);
            if (result.StatusCode != 200)
                return this.NotFoundException();
            UserSearchModel model = new UserSearchModel();
            model.Results = result.Result;
            model.Page = page.Value;
            model.Query = query;
            return View(model);
        }

        public ActionResult Details(int? userId)
        {
            return View();
        }
    }
}
