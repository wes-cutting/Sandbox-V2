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
    /// Controller for user's to checkout
    /// </summary>
    [Authorize]
    public class OrderController : Controller
    {
        IOrderAdapter orderAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderAdapter">The order adapter.</param>
        public OrderController(IOrderAdapter orderAdapter)
        {
            this.orderAdapter = orderAdapter;
        }

        public ActionResult Checkout(int id)
        {
            var status = this.orderAdapter.GetOrderForCheckout(User.Identity.Name, id);

            if (status.StatusCode != 200)
                return HttpNotFound();

            OrderCheckoutModel model = new OrderCheckoutModel()
            {
                Order = status.Result,
                Input = new OrderCheckoutInputModel()
            };

            // auto-select the first payment method
            if (status.Result.User.UserCreditCards.Count > 0)
                model.Input.SelectedPaymentMethod = status.Result.User.UserCreditCards.First();

            return View(model);
        }

        [HttpPost]
        public ActionResult Checkout(PropertyCheckoutInputModel input)
        {
            if (ModelState.IsValid)
            {
                Status<UserCreditCard> ccReq;

                if (input.SelectedPaymentMethod.UserCreditCardId == 0 && input.SaveCard)
                    ccReq = this.orderAdapter.AddUserCreditCard(
                        User.Identity.Name, input.SelectedPaymentMethod);
                else
                    ccReq = this.orderAdapter.UpdateUserCreditCard(
                        User.Identity.Name, input.SelectedPaymentMethod);

                if (ccReq.StatusCode == 200)
                {
                    // process payment with new/updated cc

                    // activate building, move reserved ribbon to purchased ribbon on building
                    // if payment is successful
                }

                // add error for credit card req
                if (ccReq.Errors == null)
                    ModelState.AddModelError(ccReq.StatusCode.ToString(), ccReq.Message);
                else
                {
                    var error = ccReq.Errors.First();
                    ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                }

                // add/update credit card
                // on success process payment
            }

            //var buildingReq = this.orderAdapter.GetPropertyForCheckout(
            //    User.Identity.Name, input.BuildingId);

            //if (buildingReq.StatusCode != 200)
            //    return HttpNotFound();

            //PropertyCheckoutModel model = new PropertyCheckoutModel()
            //{
            //    PaymentMethods = buildingReq.Result.User.UserCreditCards.ToList(),
            //    Input = input,
            //    StepsAvailable = GetStepsAvailable(buildingReq.Result)
            //};

            //// no ribbon
            //if (string.IsNullOrEmpty(buildingReq.Result.ReservedRibbonId))
            //{
            //    model.ReservedRibbonId = "none";
            //    model.ReservedRibbonName = "None";
            //    model.ReservedRibbonPrice = decimal.Zero;
            //}
            //else
            //{
            //    model.ReservedRibbonId = buildingReq.Result.ReservedRibbonId;
            //    model.ReservedRibbonName = Rentler.Configuration.Ribbons.Current.AvailableRibbons[buildingReq.Result.ReservedRibbonId];
            //    model.ReservedRibbonPrice = 0.99m;
            //}

            return View();
        }

    }
}
