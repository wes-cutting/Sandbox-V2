using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Areas.Dashboard.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Rentler.Web.Axioms.Extensions;
using Rentler.Auth;

namespace Rentler.Web.Areas.Ksl.Controllers
{
    public class OrderController : Controller
    {
        IOrderAdapter orderAdapter;
        IPropertyAdapter propertyAdapter;
        IAuthAdapter authAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderAdapter">The order adapter.</param>
        public OrderController(
            IOrderAdapter orderAdapter, 
            IPropertyAdapter propertyAdapter,
            IAuthAdapter authAdapter)
        {
            this.orderAdapter = orderAdapter;
            this.propertyAdapter = propertyAdapter;
            this.authAdapter = authAdapter;
        }

        public ActionResult Checkout(int id, Guid? token)
        {            
            if (!User.Identity.IsAuthenticated && token.HasValue)
            {
                var user = authAdapter.ValidateAuthToken(token.Value);

                if (user.StatusCode == 200)
                {
                    CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);
                    return RedirectToAction("checkout");
                }
            }

            var status = this.orderAdapter.GetOrderForCheckout(User.Identity.Name, id);

            if (status.StatusCode != 200)
                return this.NotFoundException();

            Rentler.Web.Models.OrderCheckoutModel model = new Rentler.Web.Models.OrderCheckoutModel()
            {
                Order = status.Result,
                Input = new Rentler.Web.Models.OrderCheckoutInputModel()
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

                    UserCreditCard card = null;
                    if (order.Result.UserCreditCardId != null)
                        card = orderAdapter.GetUserCreditCard(
                            User.Identity.Name, order.Result.UserCreditCardId.Value);
                    else
                        card = new UserCreditCard();

                    var model = new OrderCheckoutModel
                    {
                        Order = order.Result,
                        Input = new OrderCheckoutInputModel
                        {
                            SaveCard = input.SaveCard,
                            SelectedPaymentMethod = card
                        }
                    };

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
            return Redirect("/ksl/property/complete/" + input.BuildingId);
        }
    }
}
