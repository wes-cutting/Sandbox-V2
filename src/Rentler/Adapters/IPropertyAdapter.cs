using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using Rentler.Redis;
using Rentler.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Net;
using System.Xml.Linq;
using Rentler.Web.Email;
using Rentler.Auth;
using Rentler.Common;

namespace Rentler.Adapters
{
	public interface IPropertyAdapter
	{
		/// <summary>
		/// Get Property by its Id and owner's username
		/// </summary>
		/// <param name="buildingId">the Id of the property</param>
		/// <param name="username">the username of the property owner</param>
		/// <returns>the property</returns>
		Status<Building> GetProperty(long buildingId, string username);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="buildingId"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		Status<Building> GetPropertyForManagement(long buildingId, string username);

		/// <summary>
		/// Gets prepopulated listing information along with the building
		/// itself.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="username">The username.</param>
		/// <returns></returns>
		Status<Building> GetPropertyListingInfo(long buildingId, string username);

		/// <summary>
		/// Gets the property that includes all of the temporary ordering
		/// information that is associated.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="username">The username.</param>
		/// <returns>A building with all the temporary ordering information.</returns>
		Status<Building> GetPropertyPromoteInfo(long buildingId, string username);

		/// <summary>
		/// Get number of times listing has been viewed
		/// </summary>
		/// <param name="buildingId">the listing identifier</param>
		/// <returns>the number of times this listing has been viewed</returns>
		Status<long> GetListingViews(long buildingId);

		/// <summary>
		/// Get number of times listing has been returned from a search
		/// </summary>
		/// <param name="buildingId">the listing identifier</param>
		/// <returns>the number of times this listing has been viewed</returns>
		Status<long> GetListingSearchViews(long buildingId);

		/// <summary>
		/// Gets basic building information for use with creating
		/// a new property within the application.
		/// </summary>
		/// <param name="username">The username of the user to generate
		/// property information from.</param>
		/// <returns>A building with pre-populated information based on the user.</returns>
		Status<Building> GetInfoForNewProperty(string username);

		/// <summary>
		/// Deactivates the building.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="apiKey">The API key.</param>
		/// <returns></returns>
		Status<Building> DeactivateBuilding(long buildingId, Guid apiKey);

		/// <summary>
		/// Deactivate the building
		/// </summary>
		/// <param name="buildingId">building identifier</param>
		/// <param name="username">owning user identifier</param>
		/// <returns>status with the deactivated building</returns>
		Status<Building> DeactivateBuilding(long buildingId, string username);

		/// <summary>
		/// Activates the building via the api.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="apiKey">The API key.</param>
		/// <returns></returns>
		Status<Building> ActivateBuilding(long buildingId, Guid apiKey);

		/// <summary>
		/// Activate the building
		/// </summary>
		/// <param name="buildingId">building identifier</param>
		/// <param name="username">owning user identifier</param>
		/// <returns>status with the activated building</returns>
		Status<Building> ActivateBuilding(long buildingId, string username);

		/// <summary>
		/// Removed the requested building
		/// </summary>
		/// <param name="buildingId">building identifier</param>
		/// <param name="username">owner of the building</param>
		/// <returns>status indicating whether building was successfully removed</returns>
		Status<bool> DeleteBuilding(long buildingId, string username);

		/// <summary>
		/// Creates the building.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="building">The building.</param>
		/// <returns></returns>
		Status<Building> CreateBuilding(string username, Building building);

        /// <summary>
        /// Creates the building with property information only
        /// </summary>
        /// <param name="username"></param>
        /// <param name="building"></param>
        /// <returns></returns>
        Status<Building> CreateProperty(Building building);

        /// <summary>
        /// Updates the building's property information (type and address)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="building"></param>
        /// <returns></returns>
        Status<Building> UpdateProperty(Building building);

		/// <summary>
		/// Updates the listing information specific fields on Building
		/// </summary>
		/// <param name="username">owner of the building</param>
		/// <param name="building">the building</param>
		/// <returns>Status with the updated building</returns>
		Status<Building> UpdatePropertyListingInfo(Listing building);

		/// <summary>
		/// Updates the terms specific fields on Building
		/// </summary>
		/// <param name="username">owner of the building</param>
		/// <param name="building">the building</param>
		/// <returns>Status with the updated building</returns>
		Status<Building> UpdatePropertyTerms(string username, Building building);

		/// <summary>
		/// Updates the promotional specific fields on Building
		/// </summary>
		/// <param name="username">owner of the building</param>
		/// <param name="building">the building</param>
		/// <returns>Status with the updated building</returns>
		Status<Building> UpdatePropertyPromotions(string username, Building building,
			string ribbonId, IEnumerable<DateTime> featuredDates, string priorityListing);

		/// <summary>
		/// Gets the property for checkout.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="buildingId">The building id.</param>
		/// <returns></returns>
		Status<Building> GetPropertyForCheckout(string username, long buildingId);

		/// <summary>
		/// Get photos for building
		/// </summary>
		/// <param name="username">owner of the building</param>
		/// <param name="buildingId">the building whose photos are being requested</param>
		/// <returns>photos attached to building identified by buildingId</returns>
		Status<Photo[]> GetPhotos(string username, long buildingId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="userInterestId"></param>
		/// <returns></returns>
		Status<UserInterest> GetUserInterest(string username, int userInterestId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="userInterestId"></param>
		/// <returns></returns>
		Status<UserInterest> GetUserInterestWithApplication(string username, int userInterestId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userInterestId"></param>
		/// <param name="response"></param>
		/// <returns></returns>
		Status<UserInterest> SendUserResponse(string username, int userInterestId, string response);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userInterestId"></param>
		/// <returns></returns>
		Status<bool> DeleteUserInterest(string username, int userInterestId);

        Status<List<UserInterest>> GetLeadsForProperty(long buildingId, string username);
	}

	/// <summary>
	/// Implementation of the property adapter utilizing
	/// entity framework for primary data storage.
	/// </summary>
	public class PropertyAdapter : IPropertyAdapter
	{
		IAuthAdapter authAdapter;
		IOrderAdapter orderAdapter;
		IPropertyMailer mailer;

		/// <summary>
		/// Initializes a new instance of the <see cref="PropertyAdapter"/> class.
		/// </summary>
		/// <param name="authAdapter">The auth adapter.</param>
		/// <param name="orderAdapter">The order adapter.</param>
		public PropertyAdapter(IAuthAdapter authAdapter, IOrderAdapter orderAdapter, IPropertyMailer mailer)
		{
			this.authAdapter = authAdapter;
			this.orderAdapter = orderAdapter;
			this.mailer = mailer;
		}

		/// <summary>
		/// Get Property by its Id and owner's username
		/// </summary>
		/// <param name="buildingId">the Id of the property</param>
		/// <param name="username">the username of the property owner</param>
		/// <returns>
		/// the property
		/// </returns>
		public Status<Building> GetPropertyForManagement(long buildingId, string username)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "The username is required");

			using (var context = new RentlerContext())
			{
				if (!context.Users.Any(u => u.IsDeleted == false && u.Username == username))
					return Status.NotFound<Building>();

				var building = (from b in context.Buildings.Include("Leads.User.UserApplication")
								where b.BuildingId == buildingId &&
								b.User.Username == username &&
								b.IsDeleted == false
								select b).SingleOrDefault();

				if (building == null)
					return Status.NotFound<Building>();

				return Status.OK<Building>(building);
			}
		}

        public Status<List<UserInterest>> GetLeadsForProperty(long buildingId, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Status.ValidationError<List<UserInterest>>(null, "username", "The username is required");

            using(var context = new RentlerContext())
            {
                var leads = (from ui in context.UserInterests.Include("User")
                             where ui.BuildingId == buildingId && 
                             ui.Building.User.Username == username &&
                             ui.ApplicationSubmitted
                             select ui).ToList();

                return Status.OK<List<UserInterest>>(leads);
            }
        }

		/// <summary>
		/// Get Property by its Id and owner's username
		/// </summary>
		/// <param name="buildingId">the Id of the property</param>
		/// <param name="username">the username of the property owner</param>
		/// <returns>
		/// the property
		/// </returns>
		public Status<Building> GetProperty(long buildingId, string username)
		{            
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "The username is required");

			using (var context = new RentlerContext())
			{
				if (!context.Users.Any(u => u.IsDeleted == false && u.Username == username))
					return Status.NotFound<Building>();

				var building = (from b in context.Buildings
								where b.BuildingId == buildingId &&
								b.User.Username == username &&
								b.IsDeleted == false
								select b).SingleOrDefault();

				if (building == null)
					return Status.NotFound<Building>();

				return Status.OK<Building>(building);
			}
		}

		/// <summary>
		/// Gets the property listing info.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="username">The username.</param>
		/// <returns></returns>
		public Status<Building> GetPropertyListingInfo(long buildingId, string username)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "The username is required");

			using (var context = new RentlerContext(false))
			{
				try
				{
					var building = (from b in context.Buildings
										.Include("BuildingAmenities")
										.Include("CustomAmenities")
										.Include("User.ContactInfos")
										.Include("ContactInfo")
									where b.IsDeleted == false &&
									b.BuildingId == buildingId &&
									b.User.IsDeleted == false &&
									b.User.Username == username
									select b).SingleOrDefault();

					// invalid building id
					if (building == null)
						return Status.NotFound<Building>();

					return Status.OK<Building>(building);
				}
				catch (Exception ex)
				{
					return Status.Error<Building>(ex.Message, null);
				}
			}
		}

		/// <summary>
		/// Gets the property that includes all of the temporary ordering
		/// information that is associated.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="username">The username.</param>
		/// <returns>
		/// A building with all the temporary ordering information.
		/// </returns>
		public Status<Building> GetPropertyPromoteInfo(long buildingId, string username)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "The username is required");

			using (var context = new RentlerContext(false))
			{
				try
				{
					var building = (from b in context.Buildings
										.Include("TemporaryOrder.OrderItems")
									where b.IsDeleted == false &&
									b.BuildingId == buildingId &&
									b.User.IsDeleted == false &&
									b.User.Username == username
									select b).SingleOrDefault();

					// invalid building id
					if (building == null)
						return Status.NotFound<Building>();

					// see if temporary order is still valid
					if (building.TemporaryOrder != null)
					{
						bool hasChanges = false;

						// we only care about featured date order items
						var featuredOrderItems = building.TemporaryOrder.OrderItems.Where(i => i.ProductId == "featureddate");
						if (featuredOrderItems != null)
						{
							for (int i = 0; i < featuredOrderItems.Count(); ++i)
							{
								var orderItem = featuredOrderItems.ElementAt(i);

								// get date as is in db
								DateTime featureUtc = DateTime.Parse(orderItem.ProductOption);
								if (featureUtc.Date < DateTime.UtcNow.Date)
								{
									// if the featured date has passed its invalid so remove it
									context.OrderItems.Remove(orderItem);
									--i;
									hasChanges = true;
								}
							}
						}

						if (building.TemporaryOrder.OrderItems.Count == 0)
						{
							context.Orders.Remove(building.TemporaryOrder);
							// redundant but its here just in case we get a messed
							// up temporary order with no order items
							hasChanges = true;
						}

						if (hasChanges)
							context.SaveChanges();
					}

					return Status.OK<Building>(building);
				}
				catch (Exception ex)
				{
					return Status.Error<Building>(ex.Message, null);
				}
			}
		}

		/// <summary>
		/// Get number of times listing has been viewed
		/// </summary>
		/// <param name="buildingId">the listing identifier</param>
		/// <returns>
		/// the number of times this listing has been viewed
		/// </returns>
		public Status<long> GetListingViews(long buildingId)
		{
			var connection = ConnectionGateway.Current.GetReadConnection();
			var task = connection.Hashes.GetString(App.RedisDatabase, CacheKeys.LISTING_VIEWS, buildingId.ToString());
			string result = connection.Wait(task);

			if (string.IsNullOrEmpty(result))
				return Status.OK<long>(0);

			return Status.OK<long>(long.Parse(result));
		}

		/// <summary>
		/// Get number of times listing has been returned from a search
		/// </summary>
		/// <param name="buildingId">the listing identifier</param>
		/// <returns>
		/// the number of times this listing has been viewed
		/// </returns>
		public Status<long> GetListingSearchViews(long buildingId)
		{
            var connection = ConnectionGateway.Current.GetReadConnection();
			
			var task = connection.Hashes.GetString(App.RedisDatabase, CacheKeys.LISTING_SEARCH_VIEWS, buildingId.ToString());
			string result = connection.Wait(task);

			if (string.IsNullOrEmpty(result))
				return Status.OK<long>(0);

			return Status.OK<long>(long.Parse(result));
			
		}

		/// <summary>
		/// Gets basic building information for use with creating
		/// a new property within the application.
		/// </summary>
		/// <param name="username">The username of the user to generate
		/// property information from.</param>
		/// <returns>
		/// A building with pre-populated information based on the user.
		/// </returns>
		public Status<Building> GetInfoForNewProperty(string username)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "The username is required");

			using (var context = new RentlerContext())
			{
				// get the user contact info
				var user = (from i in context.Users.Include("ContactInfos")
							where i.Username.ToLower() == username.ToLower() &&
							!i.IsDeleted
							select i).FirstOrDefault();

				if (user == null)
					return Status.NotFound<Building>();

				Building b = new Building();
				b.User = user;
				b.BuildingAmenities = new List<BuildingAmenity>();
				b.CustomAmenities = new List<CustomAmenity>();

				// Give them a contact info to work with
				if (user.ContactInfos.Count < 1)
					b.ContactInfo = new ContactInfo();
				else
					b.ContactInfo = user.ContactInfos.First();

				return Status.OK<Building>(b);
			}
		}


		/// <summary>
		/// Removed the requested building
		/// </summary>
		/// <param name="buildingId">building identifier</param>
		/// <param name="username">owner of the building</param>
		/// <returns>
		/// status indicating whether building was successfully removed
		/// </returns>
		public Status<bool> DeleteBuilding(long buildingId, string username)
		{
			if (buildingId == 0)
				return Status.ValidationError<bool>(false, "buildingId", "buildingId is required");

			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<bool>(false, "username", "username is required");

			using (var context = new RentlerContext())
			{
				var building = (from b in context.Buildings.Include("User")
								where b.IsDeleted == false &&
								b.BuildingId == buildingId &&
								b.User.Username == username
								select b).SingleOrDefault();

				if (building == null)
					return Status.NotFound<bool>();

				// soft delete building
				building.IsDeleted = true;

				try
				{
					context.SaveChanges();

					InvalidateCache(building.BuildingId);

					return Status.OK<bool>(true);
				}
				catch (Exception ex)
				{
					return Status.Error<bool>(
						ex.Message,
						false
					);
				}
			}
		}

		/// <summary>
		/// Deactivates the building.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="apiKey">The API key.</param>
		/// <returns></returns>
		public Status<Building> DeactivateBuilding(long buildingId, Guid apiKey)
		{
			// validate the api key if it exists
			if (apiKey != null)
			{
				var valKey = this.authAdapter.ValidateApiKey(apiKey);
				if (valKey.StatusCode != 200)
					return Status.UnAuthorized<Building>();
			}

			// get the building
			using (var context = new RentlerContext())
			{
				Building building = GetBuilding(context, buildingId);
				return DeactivateBuilding(context, building);
			}
		}

		/// <summary>
		/// Deactivates the building.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="building">The building.</param>
		/// <returns></returns>
		private Status<Building> DeactivateBuilding(RentlerContext context, Building building)
		{
			if (building == null)
				return Status.NotFound<Building>();

			building.IsActive = false;

			try
			{
				context.SaveChanges();

				InvalidateCache(building.BuildingId);

				return Status.OK<Building>(building);
			}
			catch (Exception ex)
			{
				return Status.Error<Building>(ex.Message, building);
			}
		}

		/// <summary>
		/// Deactivate the building
		/// </summary>
		/// <param name="buildingId">building identifier</param>
		/// <param name="username">owning user identifier</param>
		/// <returns>
		/// status with the deactivated building
		/// </returns>
		public Status<Building> DeactivateBuilding(long buildingId, string username)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "username is required");

			using (var context = new RentlerContext())
			{
				Building building = GetBuilding(context, buildingId);
				if (building == null)
					return Status.NotFound<Building>();
				if (building.User.Username.ToLower() != username.ToLower())
					return Status.NotFound<Building>();
				return DeactivateBuilding(context, building);
			}
		}

		/// <summary>
		/// Gets the building used for basic building controls.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="buildingId">The building id.</param>
		/// <returns></returns>
		private Building GetBuilding(RentlerContext context, long buildingId)
		{
			return (from b in context.Buildings.Include("User")
					where b.IsDeleted == false &&
					b.BuildingId == buildingId
					select b).SingleOrDefault();
		}

		/// <summary>
		/// Activates the building via the api.
		/// </summary>
		/// <param name="buildingId">The building id.</param>
		/// <param name="apiKey">The API key.</param>
		/// <returns></returns>
		public Status<Building> ActivateBuilding(long buildingId, Guid apiKey)
		{
			// validate the api key if it exists
			if (apiKey != null)
			{
				var valKey = this.authAdapter.ValidateApiKey(apiKey);
				if (valKey.StatusCode != 200)
					return Status.UnAuthorized<Building>();
			}

			// get the building
			using (var context = new RentlerContext())
			{
				Building building = GetBuilding(context, buildingId);
				return ActivateBuilding(context, building);
			}
		}

		/// <summary>
		/// Activate the building.
		/// </summary>
		/// <param name="buildingId">building identifier</param>
		/// <param name="username">owning user identifier</param>
		/// <returns>
		/// status with the activated building
		/// </returns>
		public Status<Building> ActivateBuilding(long buildingId, string username)
		{
			// if there is not a username
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "username is required");

			using (var context = new RentlerContext())
			{
				Building building = GetBuilding(context, buildingId);
				if (building == null)
					return Status.NotFound<Building>();
				if (building.User.Username.ToLower() != username.ToLower())
					return Status.NotFound<Building>();
				return ActivateBuilding(context, building);
			}
		}

		/// <summary>
		/// Activates the building.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="building">The building.</param>
		/// <returns></returns>
		private Status<Building> ActivateBuilding(RentlerContext context, Building building)
		{
			if (building == null)
				return Status.NotFound<Building>();

			if (!building.Bedrooms.HasValue)
				return Status.Error("Listing is not complete", building);
			////confirm this is a valid listing
			//var listing = new Listing(building);
			//if (!listing.IsValidListing)
			//    return Status.Error("Listing is not complete", building);

			// set active
			building.IsActive = true;

			// if date activated is already set, only reset to now if it is more than 14 days old
			if (building.DateActivatedUtc.HasValue)
			{
				if (building.DateActivatedUtc <= DateTime.UtcNow.AddDays(-14))
					building.DateActivatedUtc = DateTime.UtcNow;
			}
			else
				building.DateActivatedUtc = DateTime.UtcNow;

			try
			{
				context.SaveChanges();

				InvalidateCache(building.BuildingId);

				return Status.OK<Building>(building);
			}
			catch (Exception ex)
			{
				return Status.Error<Building>(ex.Message, building);
			}
		}

		/// <summary>
		/// Creates the building.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="building">The building.</param>
		/// <returns></returns>
		public Status<Building> CreateBuilding(string username, Building building)
		{
			// validate the input
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "username is required");
			if (building == null)
				return Status.ValidationError<Building>(null, "building", "building is null");

			// validate the building
			var validation = Status.Validatate<Building>(building);
			if (validation.StatusCode != 200)
				return validation;

			// normalize standard amenities
			if (building.BuildingAmenities != null)
			{
				var amenities = building.BuildingAmenities.Distinct().ToList();
				for (int x = 0; x < amenities.Count; ++x)
				{
					if (!Amenities.Current.IsValidAmenity(amenities[x].AmenityId))
					{
						amenities.RemoveAt(x);
						--x;
					}
				}
				building.BuildingAmenities = amenities;
			}

			// normalize custom amenities
			if (building.CustomAmenities != null)
			{
				var custom = building.CustomAmenities.Distinct().ToList();
				// pascal case custom amenities
				for (int x = 0; x < custom.Count; ++x)
				{
					if (string.IsNullOrEmpty(custom[x].Name))
					{
						custom.RemoveAt(x);
						--x;
						continue;
					}
					custom[x].Name = char.ToUpper(custom[x].Name[0]) + custom[x].Name.Substring(1);
				}
				building.CustomAmenities = custom;
			}

			//get the lat/lng location of the building
			Geocode(building);

			// add it
			using (RentlerContext context = new RentlerContext())
			{
				var user = (from u in context.Users.Include("ContactInfos")
							where u.Username == username && !u.IsDeleted
							select u).FirstOrDefault();

				if (user == null)
					return Status.ValidationError<Building>(null, "username", "User doesn't exist");

				// try to make one
				if (building.ContactInfo == null)
					return Status.ValidationError<Building>(null, "contactinfo", "No contact information specified");

				// validate the contactinfo
				var contactValidation = Status.Validatate<ContactInfo>(building.ContactInfo);
				if (contactValidation.StatusCode != 200)
					return Status.ValidationError<Building>(null, "contactinfo", "The contact information is not valid");

				// if the contactinfoid isn't set
				if (building.ContactInfoId == 0)
				{
					// add it
					ContactInfo finalContact = building.ContactInfo;
					user.ContactInfos.Add(finalContact);
					context.SaveChanges();

					// add it to the building
					building.ContactInfoId = finalContact.ContactInfoId;
				}
				else
				{
					var contact = user.ContactInfos.Where(u => u.ContactInfoId == building.ContactInfoId).FirstOrDefault();
					if (contact == null)
						return Status.ValidationError<Building>(null, "contactinfoid", "ContactInfoId is invalid");

					// update the contact info
					contact.CompanyName = building.ContactInfo.CompanyName;
					contact.ContactInfoTypeCode = building.ContactInfo.ContactInfoTypeCode;
					contact.Email = building.ContactInfo.Email;
					contact.Name = building.ContactInfo.Name;
					contact.PhoneNumber = building.ContactInfo.PhoneNumber;
					contact.ShowEmailAddress = building.ContactInfo.ShowEmailAddress;
					contact.ShowPhoneNumber = building.ContactInfo.ShowPhoneNumber;
				}

				// don't allow them to pass complex object. We handled it above.
				building.ContactInfo = null;

				// set defaults
				building.CreateDateUtc = DateTime.UtcNow;
				building.CreatedBy = "propertyadapter";
				building.UpdateDateUtc = DateTime.UtcNow;
				building.UpdatedBy = "propertyadapter";

				user.Buildings.Add(building);

				try
				{
					context.SaveChanges();
				}
				catch (Exception e)
				{
					// log exception
					return Status.Error<Building>(
						"An unexpected error occurred while trying to save the listing information. Contact Rentler support for assistance.",
						building
					);
				}

				return Status.OK<Building>(building);
			}
		}

        public Status<Building> CreateProperty(Building building)
        {
            var identity = CustomAuthentication.GetIdentity();
            
            if (!identity.IsAuthenticated)
                return Status.UnAuthorized<Building>();
            
            if (building == null)
                return Status.ValidationError<Building>(null, "building", "building is null");

            //get the lat/lng location of the building
            Geocode(building);

            using (RentlerContext context = new RentlerContext())
            {
                try
                {
                    building.UserId = identity.UserId;                    

                    // set defaults
                    building.CreateDateUtc = DateTime.UtcNow;
                    building.CreatedBy = "propertyadapter.createproperty";
                    
                    context.Buildings.Add(building);
                    context.SaveChanges();

                    return Status.OK<Building>(building);
                }
                catch (Exception ex)
                {
                    // TODO: log exception

                    return Status.Error<Building>("An unexpected error occurred so the property was not created. Contact Rentler Support for assistance.", building);
                }
            }            
        }

		/// <summary>
		/// Updates the terms specific fields on Building
		/// </summary>
		/// <param name="username">owner of the building</param>
		/// <param name="building">the building</param>
		/// <returns>
		/// Status with the updated building
		/// </returns>
		public Status<Building> UpdatePropertyTerms(string username, Building building)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Building>(null, "username", "username is required");

			if (building == null)
				return Status.ValidationError<Building>(null, "building", "building is null");

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					Building current = (from b in context.Buildings.Include("User")
										where b.IsDeleted == false &&
										b.BuildingId == building.BuildingId &&
										b.User.Username == username
										select b).SingleOrDefault();

					if (current == null)
						return Status.NotFound<Building>();

					current.IsBackgroundCheckRequired = building.IsBackgroundCheckRequired;
					current.IsCreditCheckRequired = building.IsCreditCheckRequired;
					current.Price = building.Price;
					current.Deposit = building.Deposit;
					current.RefundableDeposit = building.RefundableDeposit;
					current.DateAvailableUtc = building.DateAvailableUtc;
					current.LeaseLengthCode = building.LeaseLengthCode;
					current.IsSmokingAllowed = building.IsSmokingAllowed;
					current.ArePetsAllowed = building.ArePetsAllowed;

					// pets are not allowed so no fee can be charged
					if (!building.ArePetsAllowed)
						current.PetFee = decimal.Zero;
					else
						current.PetFee = building.PetFee;

					// validate the building
					var validation = Status.Validatate<Building>(current);
					if (validation.StatusCode != 200)
						return validation;

					context.SaveChanges();

					InvalidateCache(building.BuildingId);

					return Status.OK<Building>(current);
				}
				catch (Exception ex)
				{
					// log exception
					return Status.Error<Building>(
						"An unexpected error occurred while trying to update rental terms. Contact Rentler support for assistance.",
						building
					);
				}
			}
		}


		/// <summary>
		/// Updates the promotional specific fields on Building
		/// </summary>
		/// <param name="username">owner of the building</param>
		/// <param name="building">the building</param>
		/// <returns>
		/// Status with the updated building
		/// </returns>
		public Status<Building> UpdatePropertyPromotions(string username, Building building,
			string ribbonId, IEnumerable<DateTime> featuredDates, string priorityListing)
		{
			if (building == null)
				return Status.ValidationError<Building>(null, "building", "building is null");

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					Building current = (from b in context.Buildings
											.Include("User")
											.Include("TemporaryOrder")
										where b.IsDeleted == false &&
										b.BuildingId == building.BuildingId &&
										b.User.Username == username
										select b).SingleOrDefault();

					if (current == null)
						return Status.NotFound<Building>();

					current.Title = building.Title;
					current.Description = building.Description;

					// validate the building
					var validation = Status.Validatate<Building>(current);
					if (validation.StatusCode != 200)
						return validation;

					// delete any old order
					if (current.TemporaryOrder != null)
						context.Orders.Remove(current.TemporaryOrder);

					// add order with ribbon if items
					if (!string.IsNullOrWhiteSpace(ribbonId) || featuredDates != null || !string.IsNullOrWhiteSpace(priorityListing))
					{
						Order order = new Order();
						order.UserId = current.UserId;
						order.BuildingId = current.BuildingId;
						order.CreateDate = DateTime.UtcNow;
						order.CreatedBy = "propertyAdapter";
						order.OrderStatus = OrderStatus.New;

						//calculate order total here
						if (!string.IsNullOrWhiteSpace(ribbonId))
							order.OrderTotal += Configuration.Products.GetProduct("ribbon")
								.ToOrderItem(ribbonId, 1).Price;

						if (featuredDates != null)
							order.OrderTotal += Configuration.Products
								.GetProduct("featureddate").ToOrderItem(
									DateTime.UtcNow.ToString("G"), 1).Price * featuredDates.Count();

						if (!string.IsNullOrWhiteSpace(priorityListing))
							order.OrderTotal += Configuration.Products
								.GetProduct("prioritylisting").ToOrderItem(
									DateTime.UtcNow.ToString("G"), 1).Price;

						context.Orders.Add(order);
						context.SaveChanges();

						current.TemporaryOrder = order;
						current.TemporaryOrderId = order.OrderId;

						// add ribbon
						if (!string.IsNullOrWhiteSpace(ribbonId))
						{
							var ribbonItem = Configuration.Products.GetProduct("ribbon").ToOrderItem(ribbonId, 1);
							ribbonItem.OrderId = order.OrderId;
							context.OrderItems.Add(ribbonItem);
						}

						// add featured dates
						if (featuredDates != null)
						{
							for (int i = 0; i < featuredDates.Count(); ++i)
							{
								// featured dates come through with no time, we we have to add it
								DateTime date = featuredDates.ElementAt(i);
								date = date.Add(DateTime.Now.TimeOfDay);

								DateTime dateUtc = date.ToUniversalTime();
								var featuredItem = Configuration.Products.GetProduct("featureddate").ToOrderItem(dateUtc.ToString("G"), 1);
								featuredItem.OrderId = order.OrderId;
								context.OrderItems.Add(featuredItem);
							}
						}

						// add priority listing
						if (!string.IsNullOrWhiteSpace(priorityListing))
						{
							var priorityItem = 
								Configuration.Products.GetProduct("prioritylisting")
								.ToOrderItem(true.ToString(), 1);
							priorityItem.OrderId = order.OrderId;
							context.OrderItems.Add(priorityItem);
						}
					}

					context.SaveChanges();

					InvalidateCache(building.BuildingId);

					return Status.OK<Building>(current);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					// log exception
					return Status.Error<Building>(
						"An unexpected error occurred while trying to save promotions. Contact Rentler support for assistance.",
						building
					);
				}
			}
		}

        public Status<Building> UpdateProperty(Building building)
        {
            var identity = CustomAuthentication.GetIdentity();

            if (!identity.IsAuthenticated)
                return Status.UnAuthorized<Building>();

            if (building == null)
                return Status.ValidationError<Building>(null, "building", "building is null");

            // address could have changed, if so we need to get new lat lon
            Geocode(building);

			using (RentlerContext context = new RentlerContext())
            {
                try
                {
                    var presentBuilding = (from b in context.Buildings
                                           where b.UserId == identity.UserId &&
                                           b.BuildingId == building.BuildingId
                                           select b).SingleOrDefault();

                    presentBuilding.PropertyTypeCode = building.PropertyTypeCode;
                    presentBuilding.Address1 = building.Address1;
                    presentBuilding.Address2 = building.Address2;
                    presentBuilding.City = building.City;
                    presentBuilding.State = building.State;
                    presentBuilding.Zip = building.Zip;
                    presentBuilding.Latitude = building.Latitude;
                    presentBuilding.Longitude = building.Longitude;

                    presentBuilding.UpdateDateUtc = DateTime.UtcNow;
                    presentBuilding.UpdatedBy = "propertyadapter.updateproperty";

                    context.SaveChanges();
					InvalidateCache(presentBuilding.BuildingId);

                    return Status.OK<Building>(presentBuilding);
                }
                catch (Exception)
                {
                    // log exception
                    return Status.Error<Building>("An unexpected error occurred so the requested property changes were not completed. Contact Rentler Support for assistance.", building);
                }
            }
        }

		/// <summary>
		/// Updates the listing information specific fields on Building
		/// </summary>
		/// <param name="username">owner of the building</param>
		/// <param name="building">the building</param>
		/// <returns>
		/// Status with the updated building
		/// </returns>
		public Status<Building> UpdatePropertyListingInfo(Listing building)
		{
            var identity = CustomAuthentication.GetIdentity();

            if (!identity.IsAuthenticated)
                return Status.UnAuthorized<Building>();

			if (building == null)
				return Status.ValidationError<Building>(null, "building", "building is null");

			// normalize standard amenities
			if (building.BuildingAmenities != null)
			{
				var amenities = building.BuildingAmenities.Distinct().ToList();
				for (int x = 0; x < amenities.Count; ++x)
				{
					if (!Amenities.Current.IsValidAmenity(amenities[x].AmenityId))
					{
						amenities.RemoveAt(x);
						--x;
					}
				}
				building.BuildingAmenities = amenities;
			}

			// normalize custom amenities
			if (building.CustomAmenities != null)
			{
				var custom = building.CustomAmenities.Distinct().ToList();
				// pascal case custom amenities
				for (int x = 0; x < custom.Count; ++x)
				{
					if (string.IsNullOrEmpty(custom[x].Name))
					{
						custom.RemoveAt(x);
						--x;
						continue;
					}
					custom[x].Name = char.ToUpper(custom[x].Name[0]) + custom[x].Name.Substring(1);
				}
				building.CustomAmenities = custom;
			}
			
			// update it
			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					var current = (from b in context.Buildings
										.Include("BuildingAmenities")
										.Include("CustomAmenities")
										.Include("User.ContactInfos")
										.Include("ContactInfo")
								   where b.IsDeleted == false &&
								   b.BuildingId == building.BuildingId &&								   
								   b.UserId == identity.UserId
								   select b).SingleOrDefault();

					if (current == null)
						return Status.NotFound<Building>();

					// update properties					
					current.Acres = building.Acres;
					current.SquareFeet = building.SquareFeet;
					current.YearBuilt = building.YearBuilt;
					current.Bedrooms = building.Bedrooms;
					current.Bathrooms = building.Bathrooms;

                    if (building.BuildingAmenities != null)
                    {
                        // building amenities                    
                        foreach (var a in building.BuildingAmenities)
                        {
                            // has this amenity already been added?
                            if (!current.BuildingAmenities.Any(ba => ba.AmenityId == a.AmenityId))
                                current.BuildingAmenities.Add(a);
                        }

                        for (int i = 0; i < current.BuildingAmenities.Count; ++i)
                        {
                            // make sure each current amenity is still wanted
                            if (!building.BuildingAmenities.Any(ba => ba.AmenityId == current.BuildingAmenities.ElementAt(i).AmenityId))
                            {
                                current.BuildingAmenities.Remove(current.BuildingAmenities.ElementAt(i));
                                --i;
                            }
                        }
                    }

                    if (building.CustomAmenities != null)
                    {
                        // custom amenities                    
                        foreach (var a in building.CustomAmenities)
                        {
                            // has this amenity already been added?
                            if (!current.CustomAmenities.Any(ca => ca.Name == a.Name))
                                current.CustomAmenities.Add(a);
                        }

                        for (int i = 0; i < current.CustomAmenities.Count; ++i)
                        {
                            // make sure each current amenity is still wanted
                            if (!building.CustomAmenities.Any(ca => ca.Name == current.CustomAmenities.ElementAt(i).Name))
                            {
                                current.CustomAmenities.Remove(current.CustomAmenities.ElementAt(i));
                                --i;
                            }
                        }
                    }

					// try to make one
					if (building.ContactInfo == null)
						return Status.ValidationError<Building>(null, "contactinfo", "No contact information specified");

					// validate the contactinfo
					var contactValidation = Status.Validatate<ContactInfo>(building.ContactInfo);
					if (contactValidation.StatusCode != 200)
						return Status.ValidationError<Building>(null, "contactinfo", "The contact information is not valid");

					// if the contactinfoid isn't set
					if (building.ContactInfo.ContactInfoId == 0)
					{
						// add it
						ContactInfo finalContact = building.ContactInfo;
						current.User.ContactInfos.Add(finalContact);
						context.SaveChanges();

						// add it to the building
						current.ContactInfoId = finalContact.ContactInfoId;
					}
					else
					{
						var contact = current.User.ContactInfos.Where(u => u.ContactInfoId == building.ContactInfo.ContactInfoId).FirstOrDefault();
						if (contact == null)
							return Status.ValidationError<Building>(null, "contactinfoid", "ContactInfoId is invalid");

						// update the contact info
						contact.CompanyName = building.ContactInfo.CompanyName;
						contact.ContactInfoTypeCode = building.ContactInfo.ContactInfoTypeCode;
						contact.Email = building.ContactInfo.Email;
						contact.Name = building.ContactInfo.Name;
						contact.PhoneNumber = building.ContactInfo.PhoneNumber;
						contact.ShowEmailAddress = building.ContactInfo.ShowEmailAddress;
						contact.ShowPhoneNumber = building.ContactInfo.ShowPhoneNumber;

						current.ContactInfoId = contact.ContactInfoId;
					}

					// set defaults                
					current.UpdateDateUtc = DateTime.UtcNow;
                    current.UpdatedBy = "propertyadapter.updatepropertylistinginfo";

					//invalidate L2 cache
					InvalidateCache(building.BuildingId);

					context.SaveChanges();

                    return Status.OK<Building>(current);
				}
				catch (Exception e)
				{
                    // TODO: log exception
					return Status.Error<Building>(e.Message, null);
				}				
			}
		}


		/// <summary>
		/// Gets the property for checkout.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="buildingId">The building id.</param>
		/// <returns></returns>
		public Status<Building> GetPropertyForCheckout(string username, long buildingId)
		{
			// add it
			using (RentlerContext context = new RentlerContext(false))
			{
				try
				{
					var user = (from u in context.Users
									   .Include("Buildings")
									   .Include("UserCreditCards")
								where u.IsDeleted == false &&
								u.Username == username
								select u).FirstOrDefault();

					if (user == null)
						return Status.NotFound<Building>();

					var building = context.Buildings.Find(buildingId);

					if (building == null)
						return Status.NotFound<Building>();

					return Status.OK<Building>(building);
				}
				catch (Exception ex)
				{
					return Status.Error<Building>(ex.Message, null);
				}
			}
		}

		public Status<Photo[]> GetPhotos(string username, long buildingId)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Photo[]>(null, "username", "The username is required");


			using (RentlerContext context = new RentlerContext(false))
			{
				try
				{
					var isValid = (from b in context.Buildings
								   where b.User.IsDeleted == false && b.User.Username == username &&
								   b.IsDeleted == false && b.BuildingId == buildingId
								   select b).Any();

					if (!isValid)
						return Status.NotFound<Photo[]>();

					var photos = from p in context.Photos
								 where p.BuildingId == buildingId
								 orderby p.SortOrder
								 select p;

					return Status.OK<Photo[]>(photos.ToArray());
				}
				catch (Exception ex)
				{
					return Status.Error<Photo[]>(ex.Message, null);
				}
			}
		}

		/// <summary>
		/// Geocodes the specified full address, using the Google Maps Api.
		/// </summary>
		/// <param name="fullAddress">The full address.</param>
		/// <returns></returns>
		public void Geocode(Building building)
		{
			var fullAddress = string.Format("{0} {1}, {2}, {3} {4}",
								building.Address1, building.Address2, building.City,
								building.State, building.Zip);

			fullAddress = HttpUtility.UrlEncode(fullAddress);

			string url = string.Format(
				"http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false",
				fullAddress);

			WebClient client = new WebClient();
			var result = XElement.Parse(client.DownloadString(url));

			if (result.Element("status").Value == "OK")
			{
				building.Latitude = float.Parse(result.Element("result").Element("geometry").Element("location").Element("lat").Value);
				building.Longitude = float.Parse(result.Element("result").Element("geometry").Element("location").Element("lng").Value);
			}
		}

		public Status<UserInterest> GetUserInterest(string username, int userInterestId)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<UserInterest>(null, "username", "The username is required");

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					var lead = (from i in context.UserInterests.Include("User").Include("Building")
								where i.Building.User.Username == username && i.UserInterestId == userInterestId
								select i).SingleOrDefault();

					if (lead == null)
						return Status.NotFound<UserInterest>();

					return Status.OK(lead);
				}
				catch (Exception ex)
				{
					// log exception
					return Status.Error<UserInterest>("System was unable to get lead", null);
				}
			}
		}

		public Status<UserInterest> GetUserInterestWithApplication(string username, int userInterestId)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<UserInterest>(null, "username", "The username is required");

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					var lead = (from i in context.UserInterests.Include("User.UserApplication")
								where i.Building.User.Username == username && i.UserInterestId == userInterestId
								select i).SingleOrDefault();

					if (lead == null)
						return Status.NotFound<UserInterest>();

					return Status.OK(lead);
				}
				catch (Exception ex)
				{
					// log exception
					return Status.Error<UserInterest>("System was unable to get lead", null);
				}
			}
		}

		public Status<UserInterest> SendUserResponse(string username, int userInterestId, string response)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<UserInterest>(null, "username", "The username is required");

			if (string.IsNullOrWhiteSpace(response))
				return Status.ValidationError<UserInterest>(null, "response", "Response is required");

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					var lead = (from i in context.UserInterests
								where i.Building.User.Username == username && i.UserInterestId == userInterestId
								select i).SingleOrDefault();

					if (lead == null)
						return Status.NotFound<UserInterest>();

					lead.ResponseMessage = response;
					context.SaveChanges();

					EmailRequestApplicationModel model = new EmailRequestApplicationModel(lead);
					this.mailer.RequestApplication(model);

					return Status.OK(lead);
				}
				catch (Exception ex)
				{
					// log exception
					return Status.Error<UserInterest>("System was unable to get lead", null);
				}
			}
		}

		public Status<bool> DeleteUserInterest(string username, int userInterestId)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<bool>(false, "username", "The username is required");

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					var lead = (from i in context.UserInterests
								where i.Building.User.Username == username && i.UserInterestId == userInterestId
								select i).SingleOrDefault();

					if (lead == null)
						return Status.NotFound<bool>();

					context.UserInterests.Remove(lead);
					context.SaveChanges();

					return Status.OK(true);
				}
				catch (Exception ex)
				{
					// TODO: log exception
					return Status.Error<bool>("System was unable to remove the lead", false);
				}
			}
		}

		void InvalidateCache(long buildingId)
		{
			//invalidate L2 cache
			var connection = ConnectionGateway.Current.GetWriteConnection();
			var task = connection.Keys.Remove(App.RedisCacheDatabase, CacheKeys.LISTING + buildingId);
			connection.Wait(task);
		}
	}
}
