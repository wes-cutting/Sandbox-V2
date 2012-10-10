using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Areas.Dashboard.Models;

namespace Rentler.Web.Areas.Dashboard.Controllers
{
    /// <summary>
    /// Controller for managing individual properties or units.
    /// </summary>
    public class UnitController : Controller
    {
        IUnitAdapter unitAdapter;

        public UnitController(IUnitAdapter unitAdapter)
        {
            this.unitAdapter = unitAdapter;
        }                
    }
}
