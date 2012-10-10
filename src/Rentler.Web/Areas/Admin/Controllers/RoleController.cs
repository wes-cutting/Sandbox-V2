using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Facades;
using Rentler.Web.Areas.Admin.Models;

namespace Rentler.Web.Areas.Admin.Controllers
{
  //  [Authorize(Roles="Admin")]
    public class RoleController : Controller
    {
        IRoleFacade roleFacade;

        public RoleController(IRoleFacade roleFacade)
        {
            this.roleFacade = roleFacade;
        }

        public ActionResult Search()
        {
            RoleSearchModel model = new RoleSearchModel();
            model.Roles = this.roleFacade.GetRoles();
            model.Input = new RoleCreateInputModel();
            return View(model);
        }
    }
}
