using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using System.ComponentModel.DataAnnotations;
using Rentler.Web.Models;

namespace Rentler.Web.Controllers
{
    /// <summary>
    /// Controller for user's to checkout
    /// </summary>
    [Authorize]
    public class OrderController : Controller
    {
        IOrderAdapter orderAdapter;
        IPropertyAdapter propertyAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderAdapter">The order adapter.</param>
        public OrderController(
            IOrderAdapter orderAdapter, IPropertyAdapter propertyAdapter)
        {
            this.orderAdapter = orderAdapter;
            this.propertyAdapter = propertyAdapter;
        }

        public ActionResult Checkout(int id)
        {
            var status = this.orderAdapter.GetOrderForCheckout(User.Identity.Name, id);

            if (status.StatusCode != 200)
                return this.NotFoundException();

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
        public ActionResult Checkout(OrderCheckoutInputModel input)
        {
            if (ModelState.IsValid)
            {
                bool isNew = input.SelectedPaymentMethod.UserCreditCardId == 0;

                // process payment with cc, and save or update the card if requested.
                var order = this.orderAdapter.ProcessOrder(
                    User.Identity.Name, input.OrderId, input.SelectedPaymentMethod, input.SaveCard);

                //if it didn't work, build the viewmodel, add errors, and return the view.
                if (order.StatusCode != 200)
                {
                    ValidationResult error = null;

                    if (order.Errors != null)
                        error = order.Errors.FirstOrDefault();

                    if (error != null)
                        ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                    else
                        ModelState.AddModelError("Order", "There was an error processing your order.");

                    // process order failed, so model should be ready for user to make changes and re-submit
                    var model = new OrderCheckoutModel
                    {
                        Order = order.Result,
                        Input = input
                    };

                    // new cards that failed are not saved. need to resubmit as new card again
                    if (isNew) model.Input.SelectedPaymentMethod.UserCreditCardId = 0;

                    return View(model);
                }

                //otherwise, we're good

                // activate building, move reserved ribbon to purchased ribbon on building
                // if payment is successful
                this.propertyAdapter.ActivateBuilding(input.BuildingId, User.Identity.Name);

                //fulfill order here
                this.orderAdapter.FulfillCompletedOrder(User.Identity.Name, order.Result.OrderId);
            }
            else
            {
                var order = this.orderAdapter.GetOrderForCheckout(User.Identity.Name, input.OrderId);

                return View(new OrderCheckoutModel
                {
                    Order = order.Result,
                    Input = input
                });
            }

            //redirect to our complete page
            return Redirect("/listing/complete/" + input.BuildingId);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteCard(int id, string returnUrl)
        {
            orderAdapter.RemoveUserCreditCard(id);

            return Redirect(returnUrl);
        }
    }
}
