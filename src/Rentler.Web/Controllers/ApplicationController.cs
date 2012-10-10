using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Models;

namespace Rentler.Web.Controllers
{
    /// <summary>
    /// Controller for managing the requests for
    /// applications.
    /// </summary>
    [Authorize]
    public class ApplicationController : Controller
    {
        IPropertyAdapter propertyAdapter;

        public ApplicationController(IPropertyAdapter propertyAdapter)
        {
            this.propertyAdapter = propertyAdapter;
        }

        [Authorize]
        public ActionResult RequestApp(int id)
        {
            var status = this.propertyAdapter.GetUserInterest(User.Identity.Name, id);

            if (status.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            PropertyRequestAppModel model = new PropertyRequestAppModel(status.Result);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RequestApp(PropertyRequestAppInputModel input)
        {
            if (ModelState.IsValid)
            {
                var status = this.propertyAdapter.SendUserResponse(User.Identity.Name, input.LeadId, input.Message);

                if (status.StatusCode == 200)
                {
                    return View("RequestAppSent");
                }

                HandleErrors(status);
            }

            var lead = this.propertyAdapter.GetUserInterest(User.Identity.Name, input.LeadId);

            if (lead.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            PropertyRequestAppModel model = new PropertyRequestAppModel(lead.Result);
            model.Input = input;
            return View(model);
        }

        [Authorize]
        public ActionResult Property(int id)
        {
            var result = this.propertyAdapter.GetLeadsForProperty(id, User.Identity.Name);
            if (result.StatusCode != 200)
                return this.NotFoundException();
            ApplicationPropertyModel model = new ApplicationPropertyModel();
            model.BuildingId = id;
            model.Results = result.Result;
            return PartialView(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteLead(int id)
        {
            var status = this.propertyAdapter.DeleteUserInterest(User.Identity.Name, id);
            return Json(status);
        }

        [Authorize]
        public ActionResult ViewLead(int id)
        {
            var status = this.propertyAdapter.GetUserInterestWithApplication(User.Identity.Name, id);

            if (status.StatusCode != 200)
                throw new HttpException(404, "Not Found");

            PropertyViewLeadModel model = new PropertyViewLeadModel(status.Result);
            return View(model);
        }

        private void HandleErrors(Status s)
        {
            if (s.Errors != null)
            {
                for (int i = 0; i < s.Errors.Length; ++i)
                {
                    ModelState.AddModelError(
                        s.Errors[i].MemberNames.First(),
                        s.Errors[i].ErrorMessage);
                }
            }
            else
            {
                // output a friendly message, actual error will be logged
                ModelState.AddModelError("Property", s.Message);
            }
        }
    }
}
