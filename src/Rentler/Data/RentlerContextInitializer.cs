using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Web.Security;

namespace Rentler.Data
{
	public class RentlerContextInitializer : DropCreateDatabaseIfModelChanges<RentlerContext>
	{
		protected override void Seed(RentlerContext context)
		{
			var user = new User
			{
				Email = "dustin.dahl@gmail.com",
				CreateDateUtc = DateTime.UtcNow,
				FirstName = "Dustin",
				LastName = "Dahl",
				IsDeleted = false,
				PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile("kilondusda", "SHA1"),
				UpdateDateUtc = DateTime.UtcNow,
				UpdatedBy = "seed",
				Username = "dusda"
			};

            context.Users.Add(user);

            var user2 = new User
            {
                Email = "cory.hedges@gmail.com",
                CreateDateUtc = DateTime.UtcNow,
                FirstName = "Cory",
                LastName = "Hedges",
                IsDeleted = false,
                PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile("test123", "SHA1"),
                UpdateDateUtc = DateTime.UtcNow,
                UpdatedBy = "seed",
                Username = "chedges"
            };

            context.Users.Add(user2);
			
			context.SaveChanges();

			var info = new ContactInfo
			{
				ContactInfoType = ContactInfoType.Personal,
				Email = "dustin.dahl@gmail.com",
				Name = "Dustin Dahl",
				PhoneNumber = "801.243.1675",
				ShowEmailAddress = true,
				ShowPhoneNumber = true,
				UserId = user.UserId
			};

			context.ContactInfos.Add(info);
			context.SaveChanges();


			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "650 W S Temple", "Salt Lake City", "84104"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "651 W S Temple", "Salt Lake City", "84104"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "652 W S Temple", "Salt Lake City", "84104"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "653 W S Temple", "Salt Lake City", "84104"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "654 W S Temple", "Salt Lake City", "84104"));

			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "504 W Murray Blvd", "Salt Lake City", "84123"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "505 W Murray Blvd", "Salt Lake City", "84123"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "506 W Murray Blvd", "Salt Lake City", "84123"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "507 W Murray Blvd", "Salt Lake City", "84123"));

			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "9571 S 700 E", "Sandy", "84094"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "9572 S 700 E", "Sandy", "84094"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "9573 S 700 E", "Sandy", "84094"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "9574 S 700 E", "Sandy", "84094"));

			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "5610 Goodway Dr", "Murray", "84123"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "5611 Goodway Dr", "Murray", "84123"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "5612 Goodway Dr", "Murray", "84123"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "5613 Goodway Dr", "Murray", "84123"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "5614 Goodway Dr", "Murray", "84123"));

			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "3475 West 3500 South", "West Valley City", "84119"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "3476 West 3500 South", "West Valley City", "84119"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "3477 West 3500 South", "West Valley City", "84119"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "3478 West 3500 South", "West Valley City", "84119"));
			context.Buildings.Add(GenerateBuilding(user.UserId, info.ContactInfoId, "3479 West 3500 South", "West Valley City", "84119"));

			context.SaveChanges();

			var buildings = context.Buildings
				.Where(b => b.Address1.StartsWith("5210") ||
							b.Address1.StartsWith("650") ||
							b.Address1.StartsWith("504") ||
							b.Address1.StartsWith("9571") ||
							b.Address1.StartsWith("3475")).ToList();

			foreach(var item in buildings)
				context.FeaturedListings.Add(GenerateFeaturedListing(item));

			context.SaveChanges();

			//add Admin Role
			context.Roles.Add(new Role { RoleName = "Admin", Description = "The administration role. Required for access to admin panel" });
			context.SaveChanges();
		}

		FeaturedListing GenerateFeaturedListing(Building building)
		{
			var featured = new FeaturedListing { 
				BuildingId = building.BuildingId,
				 Zip = building.Zip
			};

			//set to proper midnight of the featured day
			var tzi = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
			featured.ScheduledDate = DateTime.UtcNow.Date.AddHours(-tzi.GetUtcOffset(DateTime.UtcNow.Date).Hours);

			return featured;
		}

		Building GenerateBuilding(int userId, int contactInfoId, string address1, string city, string zip)
		{
			return new Building
			{
				Acres = 0,
				Address1 = address1,
				ArePetsAllowed = true,
				Bathrooms = 1,
				Bedrooms = 3,
				City = city,
				State = "UT",
				ContactInfoId = contactInfoId,
				CreateDateUtc = DateTime.UtcNow,
				CreatedBy = "seed",
				DateActivatedUtc = DateTime.UtcNow,
				DateAvailableUtc = DateTime.UtcNow,
				Deposit = 200,
				Description = "Fantastic place!",
				IsActive = true,
				IsBackgroundCheckRequired = true,
				IsCreditCheckRequired = true,
				IsDeleted = false,
				IsRemovedByAdmin = false,
				IsReported = false,
				IsSmokingAllowed = false,
				Latitude = 40.6496620f,
				Longitude = -111.9088020f,
				LeaseLength = LeaseLength.Year,
				PetFee = 12.00m,
				Price = 900,
				PropertyType = PropertyType.SingleFamilyHome,
				RefundableDeposit = 200,
				SquareFeet = 1000,
				UpdateDateUtc = DateTime.UtcNow,
				UpdatedBy = "seed",
				UserId = userId,
				YearBuilt = 1974,
				Zip = zip
			};
		}
	}
}
