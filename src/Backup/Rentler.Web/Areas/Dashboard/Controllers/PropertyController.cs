using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Areas.Dashboard.Models;
using Rentler.Adapters;
using System.Threading;
using Rentler.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Rentler.Web.Areas.Dashboard.Controllers
{
    /// <summary>
    /// Controller for users to manage their properties.
    /// </summary>
    [Authorize]
    public class PropertyController : Controller
    {
        IPropertyAdapter propertyAdapter;
        ISearchAdapter searchAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyController"/> class.
        /// </summary>
        /// <param name="propertyAdapter">The property adapter.</param>
        public PropertyController(
            IPropertyAdapter propertyAdapter,
            ISearchAdapter searchAdapter)
        {
            this.propertyAdapter = propertyAdapter;
            this.searchAdapter = searchAdapter;
        }

        /// <summary>
        /// Controller action for searching one's properties.
        /// </summary>
        /// <param name="search">The property search parameters.</param>
        /// <returns>The search results as a property search.</returns>
        public ActionResult Search(PropertySearch search)
        {
            var result = this.searchAdapter.SearchUserProperties(User.Identity.Name, search);

            if (Request.IsAjaxRequest())
                return PartialView("SearchResults", result.Result);

            return View(result.Result);
        }

        /// <summary>
        /// Entry point for landlord to manage a single property
        /// </summary>
        /// <param name="id">the property identifier</param>
        /// <returns></returns>
        public ActionResult Manage(long id)
        {
            var status = this.propertyAdapter.GetProperty(id, User.Identity.Name);

            if (status.StatusCode != 200)
                return RedirectToAction("index", "home", new { area = "landlord" });

            PropertyManageModel model = new PropertyManageModel();
            model.Building = status.Result;

            var countStatus = this.propertyAdapter.GetListingViews(id);
            var searchCountStatus = this.propertyAdapter.GetListingSearchViews(id);
            model.ViewCount = countStatus.Result;
            model.SearchViewCount = searchCountStatus.Result;

            return View(model);
        }

        public ActionResult Create()
        {
            PropertyCreateModel model = new PropertyCreateModel();
            model.StepsAvailable = GetStepsAvailable(null);
            var result = this.propertyAdapter.GetInfoForNewProperty(User.Identity.Name);
            if (result.StatusCode != 200)
                return HttpNotFound();
            model.Input = result.Result;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Building input)
        {
            if (ModelState.IsValid)
            {
                // set some defaults for terms on create
                input.LeaseLength = LeaseLength.Year;
                input.ArePetsAllowed = true;

                var result = this.propertyAdapter.CreateBuilding(User.Identity.Name, input);
                
                if (result.StatusCode == 200)
                    return RedirectToAction("terms", new { id = result.Result.BuildingId });

                HandleErrors(result);                
            }

            // validate
            var freshBuilding = this.propertyAdapter.GetInfoForNewProperty(User.Identity.Name);
            if (freshBuilding.StatusCode != 200)
                return HttpNotFound();

            // add missing info            
            if (input.CustomAmenities == null)
                input.CustomAmenities = freshBuilding.Result.CustomAmenities;
            
            if (input.ContactInfo == null)
                input.ContactInfo = freshBuilding.Result.ContactInfo;
            
            input.User = freshBuilding.Result.User;

            // return
            PropertyCreateModel model = new PropertyCreateModel();
            model.Input = input;
            model.StepsAvailable = GetStepsAvailable(freshBuilding.Result);
            
            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var status = this.propertyAdapter.GetPropertyListingInfo(id, User.Identity.Name);

            if (status.StatusCode != 200)
                return HttpNotFound();

            PropertyEditModel model = new PropertyEditModel();
                        
            model.Input = status.Result;
            model.StepsAvailable = GetStepsAvailable(status.Result);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Building input)
        {
            if (ModelState.IsValid)
            {                
                var status = this.propertyAdapter.UpdatePropertyListingInfo(User.Identity.Name, input);

                if (status.StatusCode == 200)
                    return RedirectToAction("terms", new { id = input.BuildingId });

                HandleErrors(status);
            }

            var buildingVal = this.propertyAdapter.GetPropertyListingInfo(input.BuildingId, User.Identity.Name);

            if (buildingVal.StatusCode != 200)
                return HttpNotFound();

            // load user since it was not posted
            input.User = buildingVal.Result.User;

            PropertyEditModel model = new PropertyEditModel() 
            { 
                Input = input,
                StepsAvailable = GetStepsAvailable(buildingVal.Result)
            };
            if (input.CustomAmenities == null)
                input.CustomAmenities = new List<CustomAmenity>();

            return View(model);
        }

        public ActionResult Terms(long id)
        {
            var status = this.propertyAdapter.GetProperty(id, User.Identity.Name);

            if (status.StatusCode != 200)
                return HttpNotFound();

            Building building = status.Result;

            PropertyTermsModel model = new PropertyTermsModel();

            model.StepsAvailable = GetStepsAvailable(status.Result);

            model.Input = new PropertyTermsInputModel()
            {
                BuildingId = building.BuildingId,
                IsBackgroundCheckRequired = building.IsBackgroundCheckRequired,
                IsCreditCheckRequired = building.IsCreditCheckRequired,
                Price = building.Price,
                Deposit = building.Deposit,
                RefundableDeposit = building.RefundableDeposit,
                DateAvailableUtc = building.DateAvailableUtc,
                LeaseLengthCode = building.LeaseLengthCode,
                IsSmokingAllowed = building.IsSmokingAllowed,
                ArePetsAllowed = building.ArePetsAllowed,
                PetFee = building.PetFee
            };            

            return View(model);
        }

        [HttpPost]
        public ActionResult Terms(PropertyTermsInputModel input)
        {
            if (ModelState.IsValid)
            {
                // rebuild building
                Building building = new Building()
                {
                    BuildingId = input.BuildingId,
                    IsBackgroundCheckRequired = input.IsBackgroundCheckRequired,
                    IsCreditCheckRequired = input.IsCreditCheckRequired,
                    Price = input.Price,
                    Deposit = (input.Deposit.HasValue) ? input.Deposit.Value : decimal.Zero,
                    RefundableDeposit = (input.RefundableDeposit.HasValue) ? input.RefundableDeposit.Value : decimal.Zero,
                    DateAvailableUtc = input.DateAvailableUtc,
                    LeaseLengthCode = input.LeaseLengthCode,
                    IsSmokingAllowed = input.IsSmokingAllowed,
                    ArePetsAllowed = input.ArePetsAllowed                    
                };
                
                // pets are not allowed so no fee can be charged
                if (!input.ArePetsAllowed)
                    building.PetFee = decimal.Zero;
                else
                    building.PetFee = (input.PetFee.HasValue) ? input.PetFee.Value : decimal.Zero;

                var status = this.propertyAdapter.UpdatePropertyTerms(User.Identity.Name, building);

                if (status.StatusCode == 200)
                    return RedirectToAction("promote", new { id = input.BuildingId });

                HandleErrors(status);
            }

            var buildingVal = this.propertyAdapter.GetProperty(input.BuildingId, User.Identity.Name);

            if (buildingVal.StatusCode != 200)
                return HttpNotFound();

            // return the model back with model state errors
            PropertyTermsModel model = new PropertyTermsModel()
            {
                Input = input,
                StepsAvailable = GetStepsAvailable(buildingVal.Result)
            };

            return View(model);
        }

        public ActionResult Promote(long id)
        {
            var status = this.propertyAdapter.GetPropertyPromoteInfo(id, User.Identity.Name);

            if (status.StatusCode != 200)
                return HttpNotFound();

            Building building = status.Result;
            
            PropertyPromoteModel model = new PropertyPromoteModel();
            model.StepsAvailable = GetStepsAvailable(status.Result);
            model.Input = new PropertyPromoteInputModel(building);

            return View(model);
        }

        [HttpPost]
        public ActionResult Promote(PropertyPromoteInputModel input)
        {
            if (ModelState.IsValid)
            {                
                // rebuild building
                Building building = input.ToBuilding();

                if (input.SelectedRibbonId.ToLower() == "none")
                    input.SelectedRibbonId = null;
                
                var status = this.propertyAdapter.UpdatePropertyPromotions(
                    User.Identity.Name, building, input.SelectedRibbonId, input.FeaturedDates);

                if (status.StatusCode == 200)
                {
                    // see if user selected a ribbon or requested to feature their property (coming soon)
                    if (status.Result.TemporaryOrder == null)
                        return Redirect("/listing/index/" + input.BuildingId);
                    else
                        return Redirect("/dashboard/order/checkout/" + status.Result.TemporaryOrderId);
                }

                HandleErrors(status);
            }

            var buildingVal = this.propertyAdapter.GetProperty(input.BuildingId, User.Identity.Name);

            if (buildingVal.StatusCode != 200)
                return HttpNotFound();

            // reset model properties not persisted
            input.PrimaryPhotoId = buildingVal.Result.PrimaryPhotoId;
            input.PrimaryPhotoExtension = buildingVal.Result.PrimaryPhotoExtension;
            
            input.CalendarDates = new CalendarDatesModel();
            // TODO: also need to restore blackout and featured dates too
            input.CalendarDates.ReservedDates = 
                input.FeaturedDates.Select(d => d.ToString("G")).ToArray();

            PropertyPromoteModel model = new PropertyPromoteModel() 
            { 
                Input = input,
                StepsAvailable = GetStepsAvailable(buildingVal.Result)
            };

            // return the model back with model state errors
            return View(model);
        }
                
        public ActionResult Activate(long id)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return Json(Status.UnAuthorized<bool>());

            var status = this.propertyAdapter.ActivateBuilding(id, User.Identity.Name);

            if (status.StatusCode == 200)
                return Json(Status.OK<bool>(true));

            return Json(
                Status.Error<bool>(
                    "An error occurred and we were unable to Activate this listing. Please contact Rentler Support if this problem persists", 
                    false
                )
            );
        }

        public ActionResult Deactivate(long id)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return Json(Status.UnAuthorized<bool>());

            var status = this.propertyAdapter.DeactivateBuilding(id, User.Identity.Name);

            if (status.StatusCode == 200)
                return Json(Status.OK<bool>(true));

            return Json(
                Status.Error<bool>(
                    "An error occurred and we were unable to Deactivate this listing. Please contact Rentler Support if this problem persists",
                    false
                )
            );            
        }
                
        public ActionResult ConfirmDeactivate(long id, string confirm)
        {            
            if (string.IsNullOrWhiteSpace(confirm) || !confirm.ToLower().Equals("deactivate"))
                return Redirect(string.Format("/dashboard/property/manage/{0}#deactivate", id));

            var status = this.propertyAdapter.DeactivateBuilding(id, User.Identity.Name);

            if (status.StatusCode == 200)
                return Redirect(string.Format("/dashboard/property/manage/{0}", id));

            return Redirect(string.Format("/dashboard/property/manage/{0}#deactivate", id));
        }
        
        public ActionResult Delete(long id, string confirm)
        {
            if (!confirm.ToLower().Equals("delete"))
                return Redirect(string.Format("/dashboard/property/manage/{0}#delete", id));

            var status = this.propertyAdapter.DeleteBuilding(id, User.Identity.Name);

            if (status.StatusCode == 200)
                return Json(Status.OK<bool>(true));

            return Json(
                Status.Error<bool>(
                    "An error occurred and we were unable to Activate this listing. Please contact Rentler Support if this problem persists",
                    false
                )
            );
        }  
        
        [HttpGet]
        public ActionResult Photos(long id)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return Json(Status.UnAuthorized<List<Photo>>());

            Status<Photo[]> result = this.propertyAdapter.GetPhotos(User.Identity.Name, id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }                                   

        private int GetStepsAvailable(Building building)
        {            
            if (building == null)
                return 1;
            
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(building, null, null);
            bool validated = Validator.TryValidateObject(building, context, validationResults);
            if (!validated)
                return 1;
           
            if (!building.DateAvailableUtc.HasValue)
                return 2;

            if(!building.TemporaryOrderId.HasValue)
                return 3;

            // checkout
            return 4;
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
