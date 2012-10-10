using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class SavedBuildingsImporter : IRentlerImporter
	{
		public void Import()
		{
			//get the new users; we need the old guid-based userid here
			//to bring the right relationship across.
			var newUsers = new List<New.User>();
			using(var context = new New.RentlerEntities())
				newUsers = context.Users.ToList();

			var usersKeyMap = new Dictionary<Guid, int>();
			using(var newContext = new New.RentlerEntities())
				usersKeyMap = newContext.Users
					.Select(u => new { Key = u.ReferenceId, Value = u.UserId })
					.ToDictionary(x => x.Key, y => y.Value);

			//get the old saved buildings
			var oldSaves = new List<Old.SavedBuilding>();
			using(var context = new Old.RentlerNewEntities())
				oldSaves = context.SavedBuildings.ToList();

			//create new saved buildings, using the new int based userId
			var newSaves = new List<New.SavedBuilding>();
			foreach(var item in oldSaves)
			{
				newSaves.Add(new New.SavedBuilding
				{
					BuildingId = item.BuildingId,
					CreateDateUtc = item.CreateDate,
					CreatedBy = item.CreatedBy,
					UserId = usersKeyMap[item.UserId]
				});
			}

			//finally, add them to the db.
			using(var context = new New.RentlerEntities())
				context.BulkInsert(newSaves);
		}
	}
}
