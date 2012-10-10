using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewRentlerImport.Extensions
{
	public static class OldAmenitiesExtensions
	{
		public static New.Amenity ToNewAmenity(this Amenity amen)
		{
			return new New.Amenity
			{
				AmenityId = amen.AmenityId,
				Name = amen.Name,
				Category = amen.Category,
				Description = amen.Description,
				CreateDate = amen.CreateDate,
				CreatedBy = amen.CreatedBy,
				IsDeleted = amen.IsDeleted,
				UpdateDate = amen.UpdateDate,
				UpdatedBy = amen.UpdatedBy
			};
		}

		public static New.AmenitiesWithOption ToNewAmenityWithOptions(this AmenitiesWithOption amen)
		{
			var withOptions = new New.AmenitiesWithOption
			{
				AmenityId = amen.AmenityId,
				Name = amen.Name,
				Category = amen.Category,
				Description = amen.Description,
				CreateDate = amen.CreateDate,
				CreatedBy = amen.CreatedBy,
				IsDeleted = amen.IsDeleted,
				UpdateDate = amen.UpdateDate,
				UpdatedBy = amen.UpdatedBy
			};

			foreach(var item in amen.AmenityOptions)
			{
				withOptions.AmenityOptions.Add(new New.AmenityOption
				{
					AmenityId = item.AmenityId,
					CreateDate = amen.CreateDate,
					CreatedBy = amen.CreatedBy,
					IsDeleted = amen.IsDeleted,
					UpdateDate = amen.UpdateDate,
					UpdatedBy = amen.UpdatedBy,
					Option = item.Option,
					SortOrder = item.SortOrder
				});
			}

			return withOptions;
		}

		public static New.CustomAmenity ToNewCustomAmenity(this CustomAmenity amen, long buildingId)
		{
			return new New.CustomAmenity
			{
				AmenityId = amen.AmenityId,
				CreateDate = amen.CreateDate,
				CreatedBy = amen.CreatedBy,
				IsDeleted = amen.IsDeleted,
				UpdateDate = amen.UpdateDate,
				UpdatedBy = amen.UpdatedBy,
				BuildingId = buildingId,
				Name = amen.Name
			};
		}
	}
}
