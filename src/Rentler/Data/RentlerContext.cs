using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Rentler.Data
{
	/// <summary>
	/// Entity framework context for the Sql server.
	/// </summary>
	public class RentlerContext : DbContext
	{
		public RentlerContext()
		{
		}

		public RentlerContext(bool enableProxyCreation)
		{
			this.Configuration.ProxyCreationEnabled = enableProxyCreation;
		}

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		/// <value>
		/// The users.
		/// </value>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Gets or sets the buildings.
		/// </summary>
		/// <value>
		/// The buildings.
		/// </value>
		public DbSet<Building> Buildings { get; set; }

		/// <summary>
		/// Gets or sets the FeaturedListings.
		/// <value>
		/// The featured listings.
		/// </value>
		/// </summary>
		public DbSet<FeaturedListing> FeaturedListings { get; set; }

		/// <summary>
		/// Gets or sets the photos.
		/// </summary>
		/// <value>
		/// The photos.
		/// </value>
		public DbSet<Photo> Photos { get; set; }

		/// <summary>
		/// Gets or sets the contact infos.
		/// </summary>
		/// <value>
		/// The contact infos.
		/// </value>
		public DbSet<ContactInfo> ContactInfos { get; set; }

		/// <summary>
		/// Gets or sets the custom amenities.
		/// </summary>
		/// <value>
		/// The custom amenities.
		/// </value>
		public DbSet<CustomAmenity> CustomAmenities { get; set; }

		/// <summary>
		/// Gets or sets the building amenities.
		/// </summary>
		/// <value>
		/// The building amenities.
		/// </value>
		public DbSet<BuildingAmenity> BuildingAmenities { get; set; }

		/// <summary>
		/// Gets or sets the saved buildings.
		/// </summary>
		/// <value>
		/// The saved buildings.
		/// </value>
		public DbSet<SavedBuilding> SavedBuildings { get; set; }

		/// <summary>
		/// Gets or sets the zip infos.
		/// </summary>
		/// <value>
		/// The zip infos.
		/// </value>
		public DbSet<ZipInfo> ZipInfos { get; set; }

		/// <summary>
		/// Gets or sets the user applications.
		/// </summary>
		/// <value>
		/// The user applications.
		/// </value>
		public DbSet<UserApplication> UserApplications { get; set; }

		/// <summary>
		/// Gets or sets the API keys.
		/// </summary>
		/// <value>
		/// The API keys.
		/// </value>
		public DbSet<ApiKey> ApiKeys { get; set; }

		/// <summary>
		/// Gets or sets the affiliate users.
		/// </summary>
		/// <value>
		/// The affiliate users.
		/// </value>
		public DbSet<AffiliateUser> AffiliateUsers { get; set; }

		/// <summary>
		/// Gets or sets the user's credit cards
		/// </summary>
		public DbSet<UserCreditCard> UserCreditCards { get; set; }

		/// <summary>
		/// Gets or sets the user's orders
		/// </summary>
		public DbSet<Order> Orders { get; set; }

		/// <summary>
		/// Gets or sets the order's order items
		/// </summary>
		public DbSet<OrderItem> OrderItems { get; set; }

		/// <summary>
		/// Get's a user's banks.
		/// </summary>
		public DbSet<UserBank> UserBanks { get; set; }

		/// <summary>
		/// Get's user interests
		/// </summary>
		public DbSet<UserInterest> UserInterests { get; set; }

		/// <summary>
		/// Gets Roles.
		/// </summary>
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Gets Users in a given Role.
		/// </summary>
		public DbSet<RoleUser> RoleUsers { get; set; }

		/// <summary>
		/// Called when [model creating].
		/// </summary>
		/// <param name="modelBuilder">The model builder.</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>()
                        .HasOptional<ContactInfo>(m => m.ContactInfo)
                        .WithMany(m => m.Buildings)
                        .HasForeignKey(m => m.ContactInfoId)
                        .WillCascadeOnDelete(false);            

            modelBuilder.Entity<CustomAmenity>()
                        .HasKey(k => new { k.BuildingId, k.Name })
                        .HasRequired(m => m.Building)
                        .WithMany(m => m.CustomAmenities)
                        .HasForeignKey(m => m.BuildingId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<BuildingAmenity>()
                        .HasKey(k => new { k.BuildingId, k.AmenityId })
                        .HasRequired(m => m.Building)
                        .WithMany(m => m.BuildingAmenities)
                        .HasForeignKey(m => m.BuildingId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<SavedBuilding>()
                        .HasKey(k => new { k.UserId, k.BuildingId })
                        .HasRequired(m => m.User)
                        .WithMany(m => m.SavedBuildings)
                        .HasForeignKey(m => m.UserId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<SavedBuilding>()
                        .HasKey(k => new { k.UserId, k.BuildingId })
                        .HasRequired(m => m.Building)
                        .WithMany(m => m.SavedBuildings)
                        .HasForeignKey(m => m.BuildingId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserApplication>()
                        .HasKey(k => new { k.UserId })
                        .HasRequired(k => k.User)
                        .WithOptional(k => k.UserApplication)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>()                
                        .HasOptional<UserCreditCard>(m => m.UserCreditCard)
                        .WithMany(m => m.Orders)
                        .HasForeignKey(m => m.UserCreditCardId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                        .HasOptional<Building>(m => m.Building)
                        .WithMany(m => m.Orders)
                        .HasForeignKey(m => m.BuildingId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Building>()
                        .HasOptional<Order>(m => m.TemporaryOrder)
                        .WithMany()
                        .HasForeignKey(m => m.TemporaryOrderId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInterest>()
                        .HasRequired<Building>(m => m.Building)
                        .WithMany(m => m.Leads)
                        .HasForeignKey(m => m.BuildingId)
                        .WillCascadeOnDelete(false);

			modelBuilder.Entity<RoleUser>()
						.HasRequired<Role>(m => m.Role)
						.WithMany()
						.HasForeignKey(m => m.RoleName)
						.WillCascadeOnDelete(true);

			modelBuilder.Entity<RoleUser>()
						.HasRequired<User>(m => m.User);

			modelBuilder.Entity<RoleUser>()
						.HasKey(m => new { m.RoleName, m.UserId });
		}
	}
}
