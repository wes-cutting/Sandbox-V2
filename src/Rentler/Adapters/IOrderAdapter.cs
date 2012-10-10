using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using Rentler.Commerce;
using System.ComponentModel.DataAnnotations;
using Rentler.Configuration;
using Rentler.Web.Email;

namespace Rentler.Adapters
{
    public interface IOrderAdapter
    {
        Status<Order> GetOrderForCheckout(string username, int orderId);

        Status<Order> GetOrderForComplete(string username, int orderId);

        Status<Order> ProcessOrder(string username, int orderId, UserCreditCard card, bool saveCard);

        Status<UserCreditCard> AddUserCreditCard(string username, UserCreditCard card);

        Status<UserCreditCard> UpdateUserCreditCard(string username, UserCreditCard card);

        UserCreditCard GetUserCreditCard(string username, int cardId);

        Status<bool> RemoveUserCreditCard(int userCreditCardId);

        Status<bool> FulfillCompletedOrder(string username, int orderId);        
    }

    public class OrderAdapter : IOrderAdapter
    {
        PaymentManager manager;
        private IOrderMailer mailer;

        public OrderAdapter(PaymentManager manager, IOrderMailer mailer)
        {
            this.manager = manager;
            this.mailer = mailer;
        }

        public Status<Order> GetOrderForCheckout(string username, int orderId)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Status.ValidationError<Order>(null, "username", "The username is required");

            try
            {
                // we can continue with the order
                // if it's in any of these states
                int[] statusCodes = new int[] { 
					(int)OrderStatus.New,
					(int)OrderStatus.CardDeclined,
					(int)OrderStatus.ServiceUnavailable
				};

                using (RentlerContext context = new RentlerContext())
                {
                    var order = (from o in context.Orders
                                     .Include("OrderItems")
                                     .Include("Building")
                                     .Include("User.UserCreditCards")
                                 where o.User.Username == username &&
                                 statusCodes.Contains(o.OrderStatusCode) &&
                                 o.OrderId == orderId
                                 select o).SingleOrDefault();

                    if (order == null)
                        return Status.NotFound<Order>();

                    //ignore deleted cards
                    order.User.UserCreditCards =
                        order.User.UserCreditCards.Where(m => !m.IsDeleted).ToList();

                    return Status.OK<Order>(order);
                }
            }
            catch (Exception ex)
            {
                // log exception
                return Status.Error<Order>("An unexpected error occurred while trying to load the requested order", null);
            }
        }

        public Status<Order> GetOrderForComplete(string username, int orderId)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Status.ValidationError<Order>(null, "username", "The username is required");

            try
            {
                // we can continue with the order
                // if it's in any of these states
                int[] statusCodes = new int[] { 
					(int)OrderStatus.Succeeded,
				};

                using (RentlerContext context = new RentlerContext())
                {
                    var order = (from o in context.Orders
                                     .Include("OrderItems")
                                     .Include("Building")
                                     .Include("User.UserCreditCards")
                                 where o.User.Username == username &&
                                 statusCodes.Contains(o.OrderStatusCode) &&
                                 o.OrderId == orderId
                                 select o).SingleOrDefault();

                    if (order == null)
                        return Status.NotFound<Order>();

                    //ignore deleted cards
                    order.User.UserCreditCards =
                        order.User.UserCreditCards.Where(m => !m.IsDeleted).ToList();

                    return Status.OK<Order>(order);
                }
            }
            catch (Exception ex)
            {
                // log exception
                return Status.Error<Order>("An unexpected error occurred while trying to load the requested order", null);
            }
        }

        public Status<UserCreditCard> AddUserCreditCard(string username, UserCreditCard card)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Status.ValidationError<UserCreditCard>(null, "username", "The username is required");

            if (card == null)
                return Status.ValidationError<UserCreditCard>(null, "card", "card is null");

            card.CreatedBy = "orderadapter";
            card.CreateDate = DateTime.UtcNow;

            // validate the contactinfo
            var cardValidation = Status.Validatate<UserCreditCard>(card);
            if (cardValidation.StatusCode != 200)
                return Status.ValidationError<UserCreditCard>(card, "", "credit card is not valid");

            card.AccountReference = Guid.NewGuid();

            card = manager.CreateCustomerCard(card);

            if (card == null)
                return Status.Error("Problem creating card on payment service.", card);

            using (RentlerContext context = new RentlerContext())
            {
                try
                {
                    var user = (from u in context.Users
                                where u.IsDeleted == false &&
                                u.Username == username
                                select u).SingleOrDefault();

                    user.UserCreditCards.Add(card);
                    context.SaveChanges();

                    return Status.OK<UserCreditCard>(card);
                }
                catch (Exception ex)
                {
                    return Status.Error<UserCreditCard>(ex.Message, card);
                }
            }
        }

        public Status<UserCreditCard> UpdateUserCreditCard(string username, UserCreditCard card)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Status.ValidationError<UserCreditCard>(null, "username", "The username is required");

            if (card == null)
                return Status.ValidationError<UserCreditCard>(null, "card", "card is null");

            card = manager.UpdateCustomerCard(card);

            if (card.UserCreditCardId == 0)
                return AddUserCreditCard(username, card);

            using (RentlerContext context = new RentlerContext())
            {
                try
                {
                    var userCreditCard = (from uc in context.UserCreditCards
                                          where uc.User.IsDeleted == false &&
                                          uc.User.Username == username &&
                                          uc.UserCreditCardId == card.UserCreditCardId
                                          select uc).SingleOrDefault();

                    userCreditCard.Alias = card.Alias;
                    userCreditCard.AccountReference = card.AccountReference;
                    userCreditCard.CardName = card.CardName;
                    userCreditCard.CardNumber = card.CardNumber;
                    userCreditCard.ExpirationMonth = card.ExpirationMonth;
                    userCreditCard.ExpirationYear = card.ExpirationYear;
                    userCreditCard.Address1 = card.Address1;
                    userCreditCard.Address2 = card.Address2;
                    userCreditCard.City = card.City;
                    userCreditCard.Email = card.Email;
                    userCreditCard.FirstName = card.FirstName;
                    userCreditCard.LastName = card.LastName;
                    userCreditCard.Phone = card.Phone;
                    userCreditCard.State = card.State;
                    userCreditCard.Zip = card.Zip;
                    userCreditCard.IsDeleted = card.IsDeleted;
                    userCreditCard.UpdatedBy = "orderadapter";
                    userCreditCard.UpdateDate = DateTime.UtcNow;

                    context.SaveChanges();

                    return Status.OK<UserCreditCard>(card);
                }
                catch (Exception ex)
                {
                    return Status.Error<UserCreditCard>(ex.Message, card);
                }
            }
        }

        public Status<bool> RemoveUserCreditCard(int userCreditCardId)
        {
            UserCreditCard card;

            using (var context = new RentlerContext())
            {
                card = (from c in context.UserCreditCards
                        where c.UserCreditCardId == userCreditCardId
                        select c).FirstOrDefault();

                if (card == null)
                    return Status.NotFound<bool>();

                card.IsDeleted = true;
                manager.UpdateCustomerCard(card);

                context.SaveChanges();

                return Status.OK(true);
            }
        }        

        public Status<Order> ProcessOrder(
            string username, int orderId, UserCreditCard card, bool saveCard)
        {
            bool isNewCard = card.UserCreditCardId == 0;

            if (isNewCard)
            {
                // we're adding the new card regardless but we don't know if its valid
                // if the user requested to save it we will make it active after the payment
                // is successful, otherwise it will remain inactive
                card.IsDeleted = true;

                var cardResult = AddUserCreditCard(username, card);
                
                if (cardResult.StatusCode != 200)
                {
                    var orderStatus = GetOrderForCheckout(username, orderId);
                    
                    orderStatus.StatusCode = 500;
                    orderStatus.Errors = new ValidationResult[] { 
                        new ValidationResult("An unexpected failure occurred: payment was not processed", new string[] { "Order" })
                    };
                    
                    return orderStatus;
                }
            }
            else
            {
                //get the card                
                var storedCard = GetUserCreditCard(username, card.UserCreditCardId);
                
                if (storedCard == null)
                {
                    var orderStatus = GetOrderForCheckout(username, orderId);

                    orderStatus.StatusCode = 500;
                    orderStatus.Errors = new ValidationResult[] {
                        new ValidationResult("Failed to locate credit card on file: payment was not processed", new string[] { "Order" })
                    };

                    return orderStatus;
                }

                //update the billing address if anything changed
                if (storedCard.Address1 != card.Address1 ||
                    storedCard.Address2 != card.Address2 ||
                    storedCard.City != card.City ||
                    storedCard.FirstName != card.FirstName ||
                    storedCard.LastName != card.LastName ||
                    storedCard.State != card.State ||
                    storedCard.Zip != card.Zip)
                {
                    storedCard.Address1 = card.Address1;
                    storedCard.Address2 = card.Address2;
                    storedCard.City = card.City;
                    storedCard.FirstName = card.FirstName;
                    storedCard.LastName = card.LastName;
                    storedCard.State = card.State;
                    storedCard.Zip = card.Zip;

                    var updateStatus = UpdateUserCreditCard(username, storedCard);
                    
                    if (updateStatus.StatusCode != 200)
                    {
                        var orderStatus = GetOrderForCheckout(username, orderId);

                        orderStatus.StatusCode = 500;
                        orderStatus.Errors = new ValidationResult[] {
                            new ValidationResult("Failed to update card address: payment was not processed", new string[] { "Order" })
                        };

                        return orderStatus;
                    }
                }

                card = storedCard;
            }

            //grab the order
            var order = GetOrderForCheckout(username, orderId);

            //attach the credit card to the order for our records            
            order.Result.UserCreditCardId = card.UserCreditCardId;            
                        
            if (order.StatusCode != 200)
                return order;

            //let's pay for stuff!

            CardPaymentResult result;

            if (App.StorePricing == "Penny")
                result = manager.AuthorizeCreditCardPayment(card, 0.01m);
            else
                result = manager.AuthorizeCreditCardPayment(card, order.Result.OrderTotal);

            //did it work?
            if (result.Approved)
            {
                order.Result.OrderStatus = OrderStatus.Succeeded;                
            }
            else
            {                
                order.StatusCode = 500;

                // the payment method used is not valid so it should not be attached to
                // the order. Clear it here and the change will be persisted later
                order.Result.UserCreditCardId = null; 

                //gateway might be down
                if (result.ServiceUnavailable)
                {
                    order.Result.OrderStatus = OrderStatus.ServiceUnavailable;
                    order.Errors = new ValidationResult[] {
                        new ValidationResult("Payment service is unavailable. Please try again later.", new string[] { "Order" })
                    };					
                }
                //or it was declined
                else
                {
                    order.Result.OrderStatus = OrderStatus.CardDeclined;
                    order.Errors = new ValidationResult[] {
                        new ValidationResult("Credit Card was declined", new string[] { "Order" })
                    };
                }                
            }

            //update the order status
            using (var context = new RentlerContext())
            {

                var toUpdate = (from o in context.Orders
                                    .Include("Building")                                    
                                where o.OrderId == order.Result.OrderId
                                select o).SingleOrDefault();

                toUpdate.OrderStatus = order.Result.OrderStatus;                
				toUpdate.UserCreditCardId = order.Result.UserCreditCardId;

                //remove temporary order if we're good
                if (order.Result.OrderStatus == OrderStatus.Succeeded)
                {
                    toUpdate.Building.TemporaryOrderId = null;

                    // allow credit card to be used again if requested
                    if (isNewCard && saveCard)
                    {
                        context.Entry(toUpdate).Reference(o => o.UserCreditCard).Load();
                        toUpdate.UserCreditCard.IsDeleted = false;
                    }
                }

                context.SaveChanges();
            }

            // send receipt only if order was successful
            if (order.Result.OrderStatus == OrderStatus.Succeeded)
            {
                //send a receipt
                EmailOrderReceiptModel model = new EmailOrderReceiptModel()
                {
                    To = order.Result.User.Email,
                    Name = string.Format("{0} {1}", order.Result.User.FirstName, order.Result.User.LastName),
                    BuildingId = (order.Result.BuildingId.HasValue) ? order.Result.BuildingId.Value : 0,
                    OrderItems = order.Result.OrderItems.ToList(),
                    OrderTotal = order.Result.OrderTotal
                };
                mailer.Receipt(model);
            }

            return order;
        }

        public UserCreditCard GetUserCreditCard(string username, int cardId)
        {
            using (var context = new RentlerContext())
            {
                var result = (from u in context.UserCreditCards
                              where !u.IsDeleted &&
                              (u.User.Username == username ||
                               u.User.Email == username) &&
                               u.UserCreditCardId == cardId
                              select u).SingleOrDefault();

                return result;
            }
        }

        public Status<bool> FulfillCompletedOrder(string username, int orderId)
        {
            using (var context = new RentlerContext())
            {
                var order = (from o in context.Orders
                                 .Include("OrderItems")
                                 .Include("Building")
                             where o.OrderId == orderId &&
                                   (o.User.Username == username || o.User.Email == username)
                             select o).SingleOrDefault();

                if (order == null)
                    Status.NotFound<bool>();

                foreach (var item in order.OrderItems)
                {
                    var p = Rentler.Configuration.Products
                        .GetProduct(item.ProductId)
                        .FromOrderItem(item);

                    p.ExecuteOnComplete(order);
                }

                context.SaveChanges();

                return Status.OK(true);
            }
        }
    }
}
