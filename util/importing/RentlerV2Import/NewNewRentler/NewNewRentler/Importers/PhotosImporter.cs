using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NewNewRentler.Importers
{
	public class PhotosImporter : IRentlerImporter
	{
		public void Import()
		{
			var oldPhotos = new List<Old.Photo>();

			List<long> buildingIds;
			using(var old = new Old.RentlerNewEntities())
				buildingIds = old.Buildings.Select(s => s.BuildingId).ToList();

			using(var old = new Old.RentlerNewEntities())
				oldPhotos = (from p in old.Photos
							 select p).ToList();

			using(var context = new New.RentlerEntities())
			{
				var newPhotos = new List<New.Photo>();

				foreach(var p in oldPhotos)
					if(buildingIds.Contains(p.BuildingId))
						newPhotos.Add(new New.Photo
						{
							BuildingId = p.BuildingId,
							CreateDateUtc = p.CreateDate,
							CreatedBy = p.CreatedBy,
							Extension = p.Extension,
							IsPrimary = p.IsPrimary,
							PhotoId = p.PhotoId,
							SortOrder = p.SortOrder,
							UpdateDateUtc = p.UpdateDate.HasValue ? p.UpdateDate.Value : DateTime.UtcNow,
							UpdatedBy = string.IsNullOrWhiteSpace(p.UpdatedBy) ? "linq script" : p.UpdatedBy
						});

				Console.WriteLine("Adding photos...");
				context.BulkInsert(newPhotos, true);
			}
		}
	}
}
