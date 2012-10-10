using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class ApiKeysImporter : IRentlerImporter
	{
		public void Import()
		{
			Console.WriteLine("Getting api keys...");
			var oldKeys = new List<Old.ApiKey>();
			using(var context = new Old.RentlerNewEntities())
				oldKeys = context.ApiKeys.ToList();

			var newKeys = new List<New.ApiKey>();

			Console.WriteLine("Making new ones...");
			foreach(var item in oldKeys)
			{
				newKeys.Add(new New.ApiKey
				{
					ApiKeyId = item.ApiKeyId,
					Name = item.Name
				});
			}

			Console.WriteLine("Adding them to the new stuff!");
			using(var context = new New.RentlerEntities())
				context.BulkInsert(newKeys);
		}
	}
}
