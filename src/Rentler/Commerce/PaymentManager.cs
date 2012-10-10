using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.ExternalServices.Modpay;
using Rentler.ExternalServices.PayForward;
using Rentler.Configuration;

namespace Rentler.Commerce
{
	public class PaymentManager
	{
		long clientId;
		string clientCode;

		ModpayWebIISoapClient modClient;
		PayforwardSoapClient payClient;

		public PaymentManager()
		{
			modClient = new ModpayWebIISoapClient();
			payClient = new PayforwardSoapClient();

			//test account settings
			clientId = long.Parse(App.ModpayClientId);
			clientCode = App.ModpayClientCode;
		}

		public ModpayCustomer GetCustomer(long alias)
		{
			var result = modClient.GetCustomerInfo(this.clientId, this.clientCode, alias);

			//check to make sure it worked
			if(result.returnCode != 1)
				return null;

			return new ModpayCustomer()
			{
				Address = result.address,
				City = result.city,
				CreateDate = DateTime.Parse(result.cDate),
				PayerAlias = alias,
				Email = result.email,
				Fax = result.fax,
				FirstName = result.firstName,
				LastName = result.lastName,
				Phone = result.phone,
				State = result.state,
				Zip = result.zip
			};
		}

		public PaymentInformationResult GetPaymentInfo(long transactionId)
		{
			var result = new PaymentInformationResult();
			var info = modClient.GetPaymentInfo(this.clientId, this.clientCode, transactionId);

			result.Paid = info.paid == 1;
			result.Active = info.active == 1;

			//if we weren't paid and the payment is no longer pending,
			//something bad happened
			if(info.paid == 0 && info.pending == 0)
				result.ErrorMessage = info.errorMessage;

			return result;
		}

		/// <summary>
		/// Creates a customer profile for the user. Note: set
		/// the customer's AccountReference to their UserId; this is an
		/// optional field we can use for reference.
		/// </summary>
		/// <param name="customer"></param>
		/// <returns>A long representing the customer's unique id. Store this as
		/// the user's Alias.</returns>
		long CreatePayerCustomer(ModpayCustomer customer)
		{
			var payerAlias = modClient.CreateCustomer(this.clientId, this.clientCode, customer.AccountReference,
				customer.FirstName, customer.LastName, customer.Address, customer.City,
				customer.State, customer.Zip, customer.Phone, customer.Fax, customer.Email);

			return payerAlias;
		}

		long ModifyPayerCustomer(ModpayCustomer customer)
		{
			var payerAlias = modClient.ModifyCustomer(this.clientId, this.clientCode,
				customer.PayerAlias.Value, customer.AccountReference, customer.FirstName,
				customer.LastName, customer.Address, customer.City, customer.State,
				customer.Zip, customer.Phone, customer.Fax, customer.Email);

			return payerAlias;
		}

		/// <summary>
		/// Creates a customer profile for the user. Note: set
		/// the customer's AccountReference to their UserId; this is an
		/// optional field we can use for reference.
		/// </summary>
		/// <param name="bank"></param>
		/// <returns>A long representing the customer's unique id. Store this as
		/// the user's Alias.</returns>
		long CreatePayeeCustomer(UserBank bank)
		{
			var accType = bank.AccountType == "Checking" ? "1" : "2";

			string addressLine = string.Format("{0} {1}", bank.Address1, bank.Address2);

			var payeeAlias = payClient.CreatePayforwardPayee(
				this.clientId, this.clientCode, bank.UserId.ToString(),
				bank.FirstName, bank.LastName, "", addressLine,
				bank.City, bank.State, bank.Zip,
				"USA", bank.Phone, "",
				bank.Email, bank.RoutingNumber, bank.AccountNumber, bank.AccountName, accType);

			return payeeAlias;
		}

		/// <summary>
		/// Updates a Payforward (payee) customer profile for the user.
		/// </summary>
		/// <param name="bank"></param>
		/// <returns></returns>
		void UpdatePayeeCustomer(UserBank bank)
		{
			string addressLine = string.Format("{0} {1}", bank.Address1, bank.Address2);

			var payeeAlias = payClient.ModifyPayee(
				this.clientId, this.clientCode, bank.UserId.ToString(), bank.PayeeAlias.Value,
				bank.FirstName, bank.LastName, "", addressLine,
				bank.City, bank.State, bank.Zip,
				"USA", bank.Phone, "",
				bank.Email, bank.RoutingNumber, bank.AccountNumber, bank.AccountName, bank.AccountType);
		}

		public UserCreditCard CreateCustomerCard(UserCreditCard card)
		{
			string addressLine = string.Format("{0} {1}", card.Address1, card.Address2);

			card.AccountReference = Guid.NewGuid();

			var customer = new ModpayCustomer()
			{
				AccountReference = card.AccountReference.ToString(),
				Address = addressLine,
				City = card.City,
				PayerAlias = card.Alias,
				FirstName = card.FirstName,
				LastName = card.LastName,
				Phone = card.Phone,
				Email = card.Email,
				State = card.State,
				Zip = card.Zip
			};

			try
			{
				//store the customer info
				customer.PayerAlias = CreatePayerCustomer(customer);
				card.Alias = customer.PayerAlias;


				//and then store the card
				modClient.ModifyCustomerCreditCard(
					clientId, clientCode, card.Alias.Value,
					card.CardName, card.CardNumber, card.ExpirationMonth, card.ExpirationYear);

				//mask account info
				card.CardNumber = this.MaskNumber(card.CardNumber);
			}
			catch(Exception exc)
			{
				return null;
			}

			return card;
		}

		/// <summary>
		/// In order to allow a user to store multiple credit cards,
		/// we create a 'customer' for each card. This will create the
		/// customer and store the card information.
		/// </summary>
		/// <param name="card"></param>
		/// <returns>The UserCreditCard. If it is a new entry, the object will be 
		/// returned with the Alias created for the card.</returns>
		public UserCreditCard UpdateCustomerCard(UserCreditCard card)
		{
			//if they haven't stored this card before,
			//create a customer
			string addressLine = string.Format("{0} {1}", card.Address1, card.Address2);

			var customer = new ModpayCustomer()
			{
				AccountReference = card.UserId.ToString(),
				Address = addressLine,
				City = card.City,
				PayerAlias = card.Alias,
				FirstName = card.FirstName,
				LastName = card.LastName,
				Phone = card.Phone,
				Email = card.Email,
				State = card.State,
				Zip = card.Zip
			};

			//store the customer info
			if(!card.Alias.HasValue)
			{
				customer.PayerAlias = CreatePayerCustomer(customer);
				card.Alias = customer.PayerAlias;
			}
			else
				ModifyPayerCustomer(customer);

			//if or when we add support to modify the card
			////and then store the card
			//modClient.ModifyCustomerCreditCard(
			//    clientId, clientCode, card.Alias.Value,
			//    card.CardName, card.CardNumber, card.ExpirationMonth, card.ExpirationYear);

			////mask account info
			//card.CardNumber = this.MaskNumber(card.CardNumber);

			return card;
		}

		/// <summary>
		/// In order to allow a user to store multiple bank accounts,
		/// we create a 'customer' for each account. This will create the
		/// customer and store the bank account information.
		/// </summary>
		/// <param name="bank"></param>
		/// <returns>The UserBank. If it is a new entry, the object will be 
		/// returned with the Alias created for the bank account.</returns>
		public UserBank UpdateCustomerBank(UserBank bank)
		{
			//if they haven't stored this account before,
			//create a customer
			if(!bank.PayerAlias.HasValue)
			{
				string addressLine = string.Format("{0} {1}", bank.Address1, bank.Address2);

				var customer = new ModpayCustomer()
				{
					AccountReference = bank.UserId.ToString(),
					Address = addressLine,
					City = bank.City,
					PayerAlias = bank.PayerAlias,
					FirstName = bank.FirstName,
					LastName = bank.LastName,
					Phone = bank.Phone,
					Email = bank.Email,
					State = bank.State,
					Zip = bank.Zip
				};

				//store the customer info
				customer.PayerAlias = CreatePayerCustomer(customer);
				bank.PayerAlias = customer.PayerAlias;
			}

			//and then store the payer bank account
			modClient.ModifyCustomerBankAcct(
				this.clientId, this.clientCode, bank.PayerAlias.Value,
				bank.AccountName, bank.RoutingNumber, bank.AccountNumber,
				bank.AccountType);

			//also create or modify the payee account, if they don't have one.
			if(!bank.PayeeAlias.HasValue)
				bank.PayeeAlias = CreatePayeeCustomer(bank);
			else
				UpdatePayeeCustomer(bank);

			//mask account info
			bank.AccountNumber = this.MaskNumber(bank.AccountNumber);
			bank.RoutingNumber = this.MaskNumber(bank.RoutingNumber);

			return bank;
		}

		/// <summary>
		/// Looks up bank information, using a routing number.
		/// </summary>
		/// <param name="routingNumber"></param>
		/// <returns></returns>
		public BankInfo GetBankInfo(string routingNumber)
		{
			var result = modClient.GetBankInfo(routingNumber);

			return new BankInfo()
			{
				RoutingNumber = routingNumber,
				Address = result.address,
				City = result.city,
				Name = result.name,
				Phone = result.phone,
				State = result.state,
				Zip = result.zipCode
			};
		}

		public SinglePaymentResult CreateSinglePayment(long alias, decimal amount, DateTime payDate,
										PaymentType payType)
		{
			var paymentResult = new SinglePaymentResult();

			try
			{
				var result = modClient.CreatePayment(
					this.clientId, this.clientCode, alias,
					payDate, (int)payType, amount);

				paymentResult.TransactionId = result;
				switch(result)
				{
					case -1:
						paymentResult.ErrorMessage = "Authentication with Modpay failed";
						break;
					case -2:
						paymentResult.ErrorMessage = "Customer alias not found.";
						break;
					case -3:
						paymentResult.ErrorMessage = "An ACH payment was specified, but the customer has no bank account information.";
						break;
					case -4:
						paymentResult.ErrorMessage = "A Credit Card payment was specified, but the customer record contains no credit card.";
						break;
					case -5:
						paymentResult.ErrorMessage = "Customer's credit card number is not a known or supported type.";
						break;
					case -6:
						paymentResult.ErrorMessage = "Invalid payment type.";
						break;
					//worked! The value is the transactionId.
					default:
						paymentResult.Accepted = true;
						break;
				}
			}
			catch(Exception exc)
			{
				paymentResult = new SinglePaymentResult()
				{
					Accepted = false,
					ServiceUnavailable = true,
					ErrorMessage = exc.Message,
				};
			}

			return paymentResult;
		}

		public SinglePayeeCreditResult CreateSinglePayeeCredit(long alias, decimal amount, DateTime payDate,
											  PayforwardPaymentType payType)
		{
			var result = payClient.CreatePayforwardPayment(
				this.clientId, this.clientCode, alias,
				payDate, Convert.ToDouble(amount), (int)payType);

			var paymentResult = new SinglePayeeCreditResult();
			paymentResult.TransactionId = result;
			switch(result)
			{
				case -1:
					paymentResult.ErrorMessage = "Authentication with Payforward failed";
					break;
				case -2:
					paymentResult.ErrorMessage = "Customer alias not found";
					break;
				case -3:
					paymentResult.ErrorMessage = "An ACH payment was specified, but the customer has no bank account information.";
					break;
				case -6:
					paymentResult.ErrorMessage = "Invalid payment type";
					break;
				case -7:
					paymentResult.ErrorMessage = "Duplicate payment";
					paymentResult.DuplicateTransaction = true;
					break;
				default:
					paymentResult.Accepted = true;
					break;
			}

			return paymentResult;
		}

		public void CreateRecurringPayment(long alias, PaymentType payType,
											DateTime startDate, int numberOfPayments, decimal installmentAmount)
		{
			var result = modClient.CreateRecurringPaymentSchedule(
				this.clientId, this.clientCode, alias,
				(int)payType, startDate, numberOfPayments, installmentAmount);
		}

		public CardPaymentResult AuthorizeCreditCardPayment(UserCreditCard card, decimal amount)
		{
			var payResult = new CardPaymentResult();

			try
			{
				var result = modClient.AuthorizeCreditCardPayment(
					this.clientId, this.clientCode, card.Alias.Value, amount);

				payResult = new CardPaymentResult()
				{
					TransactionId = result.transId.ToString(),
					Approved = result.approved,
					AuthString = result.authString,
					MessageText = result.messageText,
				};
			}
			catch(Exception exc)
			{
				payResult = new CardPaymentResult()
				{
					Approved = false,
					ServiceUnavailable = true,
					MessageText = exc.Message
				};
			}

			return payResult;
		}

		public string MaskNumber(string number)
		{
			return number.Substring(number.Length - 4).PadLeft(number.Length, '*');
		}
	}
}
