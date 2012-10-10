using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewRentlerImport.Extensions
{
	public static class OldPhotosExtensions
	{
		public static New.Photo ToNewPhoto(this Photo photo)
		{
			return new New.Photo
			{
				BuildingId = photo.BuildingId,
				CreateDate = photo.CreateDate,
				CreatedBy = photo.CreatedBy,
				Extension = photo.Extension,
				IsDeleted = photo.IsDeleted,
				IsPrimary = photo.IsPrimary,
				PhotoId = photo.PhotoId,
				SortOrder = 0,
				UpdateDate = photo.UpdateDate,
				UpdatedBy = photo.UpdatedBy
			};
		}
	}
}
