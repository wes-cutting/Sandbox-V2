using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class UserInterestsImporter : IRentlerImporter
	{
		public void Import()
		{
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

			var oldInterests = new List<Old.UserInterest>();

			using (var context = new Old.RentlerNewEntities())
				oldInterests = context.UserInterests.ToList();

			var newInterests = new List<New.UserInterest>();

			foreach (var item in oldInterests)
			{
				newInterests.Add(new New.UserInterest
				{
					BuildingId = item.BuildingId,
					Message = item.Message,
					ResponseMessage = item.ResponseMessage,
					UserId = usersKeyMap[item.UserId],
					UserInterestId = item.UserInterestId,
					ApplicationSubmitted = item.ShowApplicationInfo
				});
			}

			using (var context = new New.RentlerEntities())
				context.BulkInsert(newInterests, true);
		}
	}
}
