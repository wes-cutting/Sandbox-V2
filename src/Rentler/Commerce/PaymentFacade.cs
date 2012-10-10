using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;

namespace Rentler.Commerce
{
	public class PaymentFacade
	{
		PaymentManager manager;

		public PaymentFacade(PaymentManager manager)
		{
			this.manager = manager;
		}

		/// <summary>
		/// Get a user's credit card information.
		/// </summary>
		/// <param name="userId">The user's id.</param>
		/// <param name="userCardId">The unique id of the user's credit card info.</param>
		/// <returns>A UserCreditCard.</returns>
		public UserCreditCard GetUserCreditCard(int userId, int userCardId)
		{
			using(var context = new RentlerContext())
				return (from u in context.UserCreditCards
						where u.UserCreditCardId == userCardId && u.UserId == userId
						select u).SingleOrDefault();
		}

		/// <summary>
		/// Get a user's bank account information.
		/// </summary>
		/// <param name="userId">The user's id.</param>
		/// <param name="userBankId">The unique id of the user's bank info.</param>
		/// <returns>A UserCreditCard.</returns>
		public List<UserCreditCard> GetUserCreditCards(int userId)
		{
			using(var context = new RentlerContext())
				return (from u in context.UserCreditCards
						where u.UserId == userId
						select u).ToList();
		}

		/// <summary>
		///	Gets all of the user's bank accounts.
		/// </summary>
		/// <param name="userId">The user's id.</param>
		/// <returns>A List of the user's bank accounts.</returns>
		public UserBank GetUserBank(int userId, int userBankId)
		{
			using(var context = new RentlerContext())
				return (from u in context.UserBanks
						where u.UserBankId == userBankId && u.UserId == userId
						select u).SingleOrDefault();
		}

		/// <summary>
		/// Gets all of the user's credit cards on file.
		/// </summary>
		/// <param name="userId">The user's id.</param>
		/// <returns>A lsit of the user's credit cards.</returns>
		public List<UserBank> GetUserBanks(int userId)
		{
			using(var context = new RentlerContext())
				return (from u in context.UserBanks
						where u.UserId == userId
						select u).ToList();
		}

		public UserCreditCard CreateCreditCardForUser(UserCreditCard card)
		{
			card = manager.CreateCustomerCard(card);

			if(card == null)
				return null;

			using(var context = new RentlerContext())
			{
				context.UserCreditCards.Add(card);
				context.SaveChanges();
			}

			return card;
		}

		/// <summary>
		/// Takes care of storing the user's credit card information.
		/// If it is a new card, it will create a new entry in the payment system
		/// and in our own storage. If it is just an update (e.g. the user changing their
		/// address information), it will act accordingly.
		/// </summary>
		/// <param name="card">The user's credit card.</param>
		public void UpdateCreditCardForUser(UserCreditCard card)
		{
			card = manager.UpdateCustomerCard(card);

			if(card.UserCreditCardId == 0)
				CreateCreditCardForUser(card);
			else
			{
				UserCreditCard toUpdate = null;

				using(var context = new RentlerContext())
				{
					toUpdate = (from u in context.UserCreditCards
								where u.UserId == card.UserId &&
									  u.UserCreditCardId == card.UserCreditCardId
								select u).SingleOrDefault();

					if(toUpdate == null)
						throw new ArgumentNullException();

					toUpdate.Alias = card.Alias;
					toUpdate.AccountReference = card.AccountReference;
					toUpdate.CardName = card.CardName;
					toUpdate.CardNumber = card.CardNumber;
					toUpdate.ExpirationMonth = card.ExpirationMonth;
					toUpdate.ExpirationYear = card.ExpirationYear;
					toUpdate.Address1 = card.Address1;
					toUpdate.Address2 = card.Address2;
					toUpdate.City = card.City;
					toUpdate.Email = card.Email;
					toUpdate.FirstName = card.FirstName;
					toUpdate.LastName = card.LastName;
					toUpdate.Phone = card.Phone;
					toUpdate.State = card.State;
					toUpdate.Zip = card.Zip;
					toUpdate.IsDeleted = card.IsDeleted;

					context.SaveChanges();
				}
			}
		}

		public UserBank CreateBankForUser(UserBank bank)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Takes care of storing the user's bank account information.
		/// If it is a new card, it will create a new entry in the payment system
		/// and in our own storage. If it is just an update (e.g. the user changing their
		/// address information), it will act accordingly.
		/// </summary>
		/// <param name="bank">The user's bank account.</param>
		public void UpdateBankForUser(UserBank bank)
		{
			bank = manager.UpdateCustomerBank(bank);

			if(bank.UserBankId == 0)
				CreateBankForUser(bank);
			else
			{
				using(var context = new RentlerContext())
				{
					var toUpdate = (from u in context.UserBanks
									where u.UserId == bank.UserId &&
									u.UserBankId == bank.UserBankId
									select u).SingleOrDefault();

					if(toUpdate == null)
						throw new ArgumentNullException();

					toUpdate.AccountName = bank.AccountName;
					toUpdate.AccountNumber = bank.AccountNumber;
					toUpdate.AccountType = bank.AccountType;
					toUpdate.PayerAlias = bank.PayerAlias;
					toUpdate.PayeeAlias = bank.PayeeAlias;
					toUpdate.RoutingNumber = bank.RoutingNumber;
					toUpdate.Address1 = bank.Address1;
					toUpdate.Address2 = bank.Address2;
					toUpdate.City = bank.City;
					toUpdate.Email = bank.Email;
					toUpdate.FirstName = bank.FirstName;
					toUpdate.LastName = bank.LastName;
					toUpdate.Phone = bank.Phone;
					toUpdate.State = bank.State;
					toUpdate.Zip = bank.Zip;
					toUpdate.IsDeleted = bank.IsDeleted;

					context.SaveChanges();
				}
			}
		}

		public void DeleteUserCreditCard(UserCreditCard card)
		{
			card.IsDeleted = true;

			manager.UpdateCustomerCard(card);
			UpdateCreditCardForUser(card);
		}

		public void DeleteUserBank(UserBank bank)
		{
			bank.IsDeleted = true;

			manager.UpdateCustomerBank(bank);
			UpdateBankForUser(bank);
		}

		/// <summary>
		/// Charges a customer's credit card for the amount, immediately.
		/// Use this for subscription/fee's.
		/// </summary>
		/// <param name="card">The user's credit card.</param>
		/// <param name="amount">The amount to charge the user.</param>
		/// <returns>A SinglePaymentResult, stating whether the card was approved and
		/// if not, why.</returns>
		public CardPaymentResult AuthorizePayment(UserCreditCard card, decimal amount)
		{
			return manager.AuthorizeCreditCardPayment(card, amount);
		}

		/// <summary>
		/// Checks the status of a single payment (usually Ach).
		/// </summary>
		/// <param name="transactionId">The unique id of the transaction.</param>
		/// <returns>A PaymentInformationResult.</returns>
		public PaymentInformationResult GetPaymentInfo(long transactionId)
		{
			return manager.GetPaymentInfo(transactionId);
		}

		/// <summary>
		/// Creates a one-time payment, to be processed on a specific date.
		/// Use this method for move in/out stuff like security deposits, pro-rated rent, etc.
		/// </summary>
		/// <param name="card">The user's credit card.</param>
		/// <param name="amount">The amount to charge the user.</param>
		/// <param name="payDate">The date to process the transaction.</param>
		public SinglePaymentResult CreateSinglePayment(UserCreditCard card, decimal amount, DateTime payDate)
		{
			if(card.Alias == null)
				throw new ArgumentNullException("Credit Card alias is null.");

			return manager.CreateSinglePayment(card.Alias.Value, amount, payDate, PaymentType.CreditCard);
		}

		/// <summary>
		/// Creates a one-time payment, to be processed on a specific date.
		/// Use this method for move in/out stuff like security deposits, pro-rated rent, etc.
		/// </summary>
		/// <param name="bank">The user's bank account.</param>
		/// <param name="amount">The amount to charge the user.</param>
		/// <param name="payDate">The date to process the transaction.</param>
		/// <returns>The result of the payment type. If it failed, the ErrorMessage property
		/// will explain what happened.</returns>
		public SinglePaymentResult CreateSinglePayment(UserBank bank, decimal amount, DateTime payDate)
		{
			if(bank.PayerAlias == null)
				throw new ArgumentNullException("Bank account alias is null.");

			return manager.CreateSinglePayment(bank.PayerAlias.Value, amount, payDate, PaymentType.ACH);
		}

		/// <summary>
		/// Creates a one-time credit, most likely to a landlord, on a specific date.
		/// </summary>
		/// <param name="bank">The user's bank account.</param>
		/// <param name="amount">The amount to credit the user's account.</param>
		/// <param name="payDate">The date to process the transaction.</param>
		/// <returns>The result of the payment type. If it failed, the ErrorMessage property
		/// will explain what happened.</returns>
		public SinglePayeeCreditResult CreateSinglePayeeCredit(UserBank bank, decimal amount, DateTime payDate)
		{
			if(bank.PayeeAlias == null)
				throw new ArgumentNullException("Bank account alias is null.");

			var payType = bank.AccountType == "Checking" ?
				PayforwardPaymentType.Checking : PayforwardPaymentType.Savings;

			return manager.CreateSinglePayeeCredit(bank.PayeeAlias.Value, amount, payDate, payType);
		}
	}
}
