using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class UserCreditCardsImporter : IRentlerImporter
	{
		public void Import()
		{
			var oldCards = new List<Old.UserCreditCard>();

			using (var context = new Old.RentlerNewEntities())
				oldCards = context.UserCreditCards.ToList();

			//get the new users; we need the old guid-based userid here
			//to bring the right relationship across.
			var newUsers = new List<New.User>();
			using (var context = new New.RentlerEntities())
				newUsers = context.Users.ToList();

			var usersKeyMap = new Dictionary<Guid, int>();
			using (var newContext = new New.RentlerEntities())
				usersKeyMap = newContext.Users
					.Select(u => new { Key = u.ReferenceId, Value = u.UserId })
					.ToDictionary(x => x.Key, y => y.Value);

			var newCards = new List<New.UserCreditCard>();

			foreach (var item in oldCards)
			{
				newCards.Add(new New.UserCreditCard
				{
					AccountReference = item.AccountReference,
					Address1 = item.AddressLine1,
					Address2 = item.AddressLine2,
					Alias = item.Alias,
					CardName = item.CardName,
					CardNumber = item.CardNumber,
					City = item.City,
					CreateDate = item.CreateDate,
					CreatedBy = item.CreatedBy,
					Email = item.Email,
					ExpirationMonth = item.ExpirationMonth,
					ExpirationYear = item.ExpirationYear,
					FirstName = item.FirstName,
					IsDeleted = item.IsDeleted,
					LastName = item.LastName,
					Phone = item.Phone,
					State = item.State,
					UpdateDate = item.UpdateDate,
					UpdatedBy = item.UpdatedBy,
					UserCreditCardId = item.UserCreditCardId,
					UserId = usersKeyMap[item.UserId],
					Zip = item.Zip
				});
			}

			using (var context = new New.RentlerEntities())
				context.BulkInsert(newCards, true);
		}
	}
}
