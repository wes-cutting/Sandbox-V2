using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using NewRentlerImport.Extensions;
using System.IO;

namespace NewRentlerImport
{
	public class BuildingAmenitiesWithOptions
	{
		public void LetsDoThis()
		{
			//contexts
			var old = new RentlerBackupEntities();
			var db = new New.RentlerNewEntities();
			var withoutTracing = new New.RentlerNewEntities();

			//diagnostics
			Stopwatch watch = new Stopwatch();

			var map = (from b in old.Buildings
					   where b.PropertyInfoId != null
					   select new
					   {
						   BuildingId = b.BuildingId,
						   PropertyInfoId = b.PropertyInfoId.Value
					   }).ToList();

			Dictionary<int, int> dict = new Dictionary<int, int>();
			foreach(var item in map)
				dict[item.PropertyInfoId] = item.BuildingId;

			//save the endangered RAMS
			map = null;

			var amen = old.PropertyInfoAmenitiesWithOptions.ToList();
			var newamen = new List<New.BuildingAmenitiesWithOption>();

			foreach(var item in amen)
			{
				if(dict.ContainsKey(item.PropertyInfoId))
					newamen.Add(new New.BuildingAmenitiesWithOption
					{
						AmenityId = item.AmenityId,
						CreateDate = item.CreateDate,
						CreatedBy = item.CreatedBy,
						UpdateDate = item.UpdateDate,
						UpdatedBy = item.UpdatedBy,
						IsDeleted = item.IsDeleted,
						BuildingId = dict[item.PropertyInfoId],
						Option = item.Option
					});
			}

			//more endangered RAMS
			amen = null;

			StringBuilder script = new StringBuilder();

			var updated = DateTime.UtcNow.ToString();
			script.AppendLine("use RentlerNew;");
			int count = 0;
			foreach(var item in newamen)
			{
				if(count == 100)
				{
					count = 0;
					script.AppendLine("GO");
				}
				script.AppendLine(string.Format(
					"insert into BuildingAmenitiesWithOptions values({0}, {1}, {2}, \'{3}\', \'{4}\', \'{5}\', \'{6}\', {7})",
					item.AmenityId, item.BuildingId, item.Option, updated,
					"weblinq", item.CreateDate.ToString(), item.CreatedBy.ToString(),
					item.IsDeleted ? "1" : "0"));
				count++;
			}

			var file = File.CreateText("buildingamenitieswithoptions.sql");
			file.Write(script.ToString());
			file.Flush();
			file.Close();
			Console.WriteLine("Done generating script; pause here and run it. File: buildingamenitieswithoptions.sql");
			Console.ReadLine();
		}
	}
}
