using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Redis;
using Rentler.Configuration;
using Rentler.Data;
using Rentler.Web.Email;
using Newtonsoft.Json;
using Rentler.Auth;

namespace Rentler.Adapters
{
	/// <summary>
	/// Adapter for general listing management.
	/// </summary>
	public interface IListingAdapter
	{
		/// <summary>
		/// Gets the building for the requested listing.
		/// </summary>
		/// <param name="listingId">The listing id.</param>
		/// <returns>A status with the building.</returns>
		Status<Building> GetListing(long listingId);

		/// <summary>
		/// Gets all active buildings.
		/// </summary>
		/// <returns></returns>
		Status<List<Building>> GetListings();

		/// <summary>
		/// Get number of times this listing has been viewed
		/// </summary>
		/// <param name="listingId">the listing identifier</param>
		/// <returns>the number</returns>
		Status<long> GetListingViews(long listingId);

		Status<BuildingPreview> GenerateBuildingPreview(Building building);

		/// <summary>
		/// Gets a total list of views for all listings
		/// </summary>
		/// <returns>List of views for all listings.</returns>
		Status<long> GetTotalListingViews();

		/// <summary>
		/// Gets the saved listings for user.
		/// </summary>
		/// <param name="username">The username to get the saved listings for.</param>
		/// <returns>A list of saved listings for a user.</returns>
		Status<PaginatedList<BuildingPreview>> GetFavoritesForUser(string username, int? pageNumber, int? pageSize);

		/// Determine whether this listing was saved by this user
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="username">user's username (unique)</param>
		/// <returns>A status with the indicator</returns>
		Status<bool> ListingWasSavedBy(long listingId, string username);

		/// <summary>
		/// Saves a Listing for a particular User
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="username">user identifier</param>
		/// <returns>A status with the saved building</returns>
		Status<SavedBuilding> CreateSavedBuilding(long listingId, string username);

		/// <summary>
		/// Removes a Saved Listing for a particular User
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="username">user identifier</param>
		/// <returns>A status with the saved building</returns>
		Status<bool> DeleteSavedBuilding(long listingId, string username);

		/// <summary>
		/// Reports a listing
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="reportedText">reason for reporting this listing</param>
		/// <returns>status with reported listing</returns>
		Status<bool> ReportListing(long listingId, string reportedText);

		/// <summary>
		/// Creates the interest in building from the user to the current
		/// listing.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="listingId">The listing id.</param>
		/// <returns>A status of whether or not the interest was created.</returns>
		Status<UserInterest> CreateInterestInBuilding(string username, long listingId, string message);

		Status<IEnumerable<long>> GetAllListingIds();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="listingId"></param>
		/// <returns></returns>
		void IncrementListingViews(long listingId);
	}

	/// <summary>
	/// Concrete implementation of the listing adapter
	/// with the items in it.
	/// </summary>
	public class ListingAdapter : IListingAdapter
	{
		IFriendlyZipAdapter zipAdapter;
		IListingMailer mailer;

		public ListingAdapter(IFriendlyZipAdapter zipAdapter, IListingMailer mailer)
		{
			this.zipAdapter = zipAdapter;
			this.mailer = mailer;
		}

		public Status<IEnumerable<long>> GetAllListingIds()
		{
			using (var context = new RentlerContext())
			{
				var listings = (from b in context.Buildings
								where b.IsActive && !b.IsDeleted &&
								!b.IsRemovedByAdmin
								select b.BuildingId).ToList();
				return Status.OK<IEnumerable<long>>(listings);
			}
		}

		/// <summary>
		/// Gets the building for the requested listing.
		/// </summary>
		/// <param name="listingId">The listing id.</param>
		/// <returns>
		/// A status with the building.
		/// </returns>
		public Status<Building> GetListing(long listingId)
		{
			if (listingId == 0)
				return Status.ValidationError<Building>(null, "listingId", "Listing ID is required");

			Building listing = null;

			//L2
			var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
			var redisConnection = ConnectionGateway.Current.GetReadConnection();
            var listingTask = redisConnection.Strings.GetString(App.RedisCacheDatabase, CacheKeys.LISTING + listingId);
            var result = redisConnection.Wait(listingTask);

			if (!string.IsNullOrWhiteSpace(result))
				listing = JsonConvert.DeserializeObject<Building>(result, settings);

			if (listing != null)
				return Status.OK<Building>(listing);

			//L3
			using (var context = new RentlerContext())
			{
				context.Configuration.ProxyCreationEnabled = false;

				listing = (from b in context.Buildings
								.Include("Photos")
								.Include("BuildingAmenities")
								.Include("CustomAmenities")
								.Include("User")
								.Include("ContactInfo")
						   where b.IsDeleted == false &&
                           b.IsRemovedByAdmin == false &&
						   b.IsActive &&
						   b.BuildingId == listingId
						   select b).SingleOrDefault();

				if (listing == null)
					return Status.NotFound<Building>();

				//store in L2
				var connection = ConnectionGateway.Current.GetWriteConnection();
				connection.Strings.Set(App.RedisCacheDatabase, CacheKeys.LISTING + listingId,
					JsonConvert.SerializeObject(listing, settings), 14400);

				return Status.OK<Building>(listing);
			}
		}

		/// <summary>
		/// Gets the building for the requested listing.
		/// </summary>
		/// <param name="listingId">The listing id.</param>
		/// <returns>
		/// A status with the building.
		/// </returns>
		public Status<List<Building>> GetListings()
		{
			//L3
			using (var context = new RentlerContext())
			{
				context.Configuration.ProxyCreationEnabled = false;

				var listing = (from b in context.Buildings
								.Include("Photos")
								.Include("BuildingAmenities")
								.Include("CustomAmenities")
								.Include("User")
								.Include("ContactInfo")
							   where !b.IsDeleted &&
							   b.IsActive
							   select b).ToList();

				if (listing == null)
					return Status.NotFound<List<Building>>();

				return Status.OK<List<Building>>(listing);
			}
		}

		/// <summary>
		/// Get number of times this listing has been viewed
		/// </summary>
		/// <param name="listingId">the listing identifier</param>
		/// <returns>
		/// the number
		/// </returns>
		public Status<long> GetListingViews(long listingId)
		{
            var connection = ConnectionGateway.Current.GetReadConnection();
            
            try
            {
                var task = connection.Hashes.GetString(App.RedisDatabase, CacheKeys.LISTING_VIEWS, listingId.ToString());
                string result = connection.Wait(task);

                if (string.IsNullOrEmpty(result))
                    return Status.OK<long>(0);

                return Status.OK<long>(long.Parse(result));
            }
            catch (Exception)
            {
                return Status.OK<long>(0);
            }
            
		}

		/// <summary>
		/// Gets a total list of views for all listings
		/// </summary>
		/// <returns>
		/// List of views for all listings.
		/// </returns>
		public Status<long> GetTotalListingViews()
		{
            var connection = ConnectionGateway.Current.GetReadConnection();
            
            try
            {
                var task = connection.Strings.GetString(App.RedisDatabase, CacheKeys.TOTAL_LISTING_VIEWS);
                string result = connection.Wait(task);

                if (string.IsNullOrEmpty(result))
                    return Status.OK<long>(0);

                return Status.OK<long>(long.Parse(result));
            }
            catch (Exception)
            {
                return Status.OK<long>(0);
            }
            
		}


		/// <summary>
		/// Gets the saved listings for user.
		/// </summary>
		/// <param name="username">The username to get the saved listings for.</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <returns>
		/// A list of saved listings for a user.
		/// </returns>
		public Status<PaginatedList<BuildingPreview>> GetFavoritesForUser(
			string username, int? pageNumber, int? pageSize)
		{
            var identity = CustomAuthentication.GetIdentity();

            if (!identity.IsAuthenticated)
                return Status.UnAuthorized<PaginatedList<BuildingPreview>>();
			
            if (!pageNumber.HasValue)
				pageNumber = 0;
			if (!pageSize.HasValue || pageSize.Value > 100)
				pageSize = 25;

			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<PaginatedList<BuildingPreview>>(null, "username", "username is required");            

			using (var context = new RentlerContext())
			{				
                var props = (from sb in context.SavedBuildings
                             join b in context.Buildings on sb.BuildingId equals b.BuildingId
                             where sb.UserId == identity.UserId &&
                             b.IsActive == true &&
                             b.IsRemovedByAdmin == false
                             orderby b.CreateDateUtc descending
                             select b).ToList();                					

                var queryableProps = props.Select(b => new BuildingPreview()
                {
                    Bathrooms = b.Bathrooms.Value,
                    Bedrooms = b.Bedrooms.Value,
                    BuildingId = b.BuildingId,
                    City = b.City,
                    IsFeatured = false,
                    Price = b.Price,
                    PrimaryPhotoExtension = b.PrimaryPhotoExtension,
                    PrimaryPhotoId = b.PrimaryPhotoId,
                    State = b.State,
                    Title = string.IsNullOrWhiteSpace(b.Title) ? b.Address1 : b.Title,
                    Address1 = b.Address1
                }).AsQueryable<BuildingPreview>();

				return Status.OK<PaginatedList<BuildingPreview>>(
					new PaginatedList<BuildingPreview>(queryableProps, pageNumber.Value, pageSize.Value));
			}
		}

		/// <summary>
		/// Determine whether this listing was saved by this user
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="username">user's username (unique)</param>
		/// <returns>
		/// A status with the indicator
		/// </returns>
		public Status<bool> ListingWasSavedBy(long listingId, string username)
		{
			if (listingId == 0)
				return Status.ValidationError<bool>(false, "listingId", "listingId is required");

			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<bool>(false, "username", "username is required");

			using (var context = new RentlerContext())
			{
				if (context.SavedBuildings
					.Include("User")
					.Any(s => s.User.Username == username && s.BuildingId == listingId))
				{
					return Status.OK<bool>(true);
				}
			}

			return Status.OK<bool>(false);
		}


		/// <summary>
		/// Saves a Listing for a particular User
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="username">user identifier</param>
		/// <returns>
		/// A status with the saved building
		/// </returns>
		public Status<SavedBuilding> CreateSavedBuilding(long listingId, string username)
		{
			if (listingId == 0)
				return Status.ValidationError<SavedBuilding>(null, "listingId", "listingId is required");

			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<SavedBuilding>(null, "username", "username is required");

			using (var context = new RentlerContext())
			{
				var user = (from u in context.Users.Include("SavedBuildings")
							where u.IsDeleted == false && (u.Username == username || u.Email == username)
							select u).SingleOrDefault();

				if (user == null)
					return Status.NotFound<SavedBuilding>();

				// see if user already saved this building so we don't try to 
				// save it again
				if (user.SavedBuildings.Any(s => s.BuildingId == listingId))
					return Status.Error<SavedBuilding>("User already saved this building", null);

				SavedBuilding newSave = new SavedBuilding()
				{
					BuildingId = listingId,
					UserId = user.UserId,
					CreateDateUtc = DateTime.UtcNow,
					CreatedBy = "ListingAdapter"
				};

				user.SavedBuildings.Add(newSave);

				try
				{
					context.SaveChanges();

					InvalidateCache(newSave.BuildingId);

					newSave.User = null;
					return Status.OK<SavedBuilding>(newSave);
				}
				catch (Exception ex)
				{
					return Status.Error<SavedBuilding>(
						ex.Message,
						null
					);
				}
			}
		}

		/// <summary>
		/// Removes a Saved Listing for a particular User
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="username">user identifier</param>
		/// <returns>
		/// A status with the saved building
		/// </returns>
		public Status<bool> DeleteSavedBuilding(long listingId, string username)
		{
            var identity = CustomAuthentication.GetIdentity();
            
            if (!identity.IsAuthenticated)
                return Status.UnAuthorized<bool>();

			if (listingId == 0)
				return Status.ValidationError<bool>(false, "listingId", "listingId is required");

			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<bool>(false, "username", "username is required");

			using (var context = new RentlerContext())
			{
                try
				{
                    SavedBuilding save = (from s in context.SavedBuildings
                                          where s.BuildingId == listingId &&
                                          s.UserId == identity.UserId
                                          select s).SingleOrDefault();

                    if (save == null)
                        return Status.NotFound<bool>();

				    context.SavedBuildings.Remove(save);				
					context.SaveChanges();

					InvalidateCache(save.BuildingId);

					return Status.OK<bool>(true);
				}
				catch (Exception ex)
				{
					return Status.Error<bool>(ex.Message, false);
				}
			}
		}

		/// <summary>
		/// Reports a listing
		/// </summary>
		/// <param name="listingId">listing identifier</param>
		/// <param name="reportedText">reason for reporting this listing</param>
		/// <returns>
		/// status with reported listing
		/// </returns>
		public Status<bool> ReportListing(long listingId, string reportedText)
		{
			if (listingId == 0)
				return Status.ValidationError<bool>(false, "listingId", "listingId is required");
			if (string.IsNullOrWhiteSpace(reportedText))
				return Status.ValidationError<bool>(false, "reportedText", "reportedText is required");

			using (var context = new RentlerContext())
			{
				var building = (from b in context.Buildings
								where b.IsDeleted == false && b.BuildingId == listingId
								select b).SingleOrDefault();

				if (building == null)
					return Status.NotFound<bool>();

				building.IsReported = true;
				building.ReportedText = reportedText;

				try
				{
					context.SaveChanges();
					return Status.OK<bool>(true);
				}
				catch (Exception ex)
				{
					return Status.Error<bool>(ex.Message, false);
				}
			}
		}


		/// <summary>
		/// Creates the interest in building from the user to the current
		/// listing.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="listingId">The listing id.</param>
		/// <returns>
		/// A status of whether or not the interest was created.
		/// </returns>
		public Status<UserInterest> CreateInterestInBuilding(string username, long listingId, string message)
		{
			if (listingId == 0)
				return Status.ValidationError<UserInterest>(null, "listingId", "listingId is required");

			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<UserInterest>(null, "username", "username is required");

			using (var context = new RentlerContext())
			{
				try
				{
					User user = (from u in context.Users
								 where u.Username == username
								 select u).SingleOrDefault();

					if (user == null)
						return Status.NotFound<UserInterest>();

					UserInterest newInterest = new UserInterest()
					{
						BuildingId = listingId,
						UserId = user.UserId,
						Message = message
					};

					context.UserInterests.Add(newInterest);
					context.SaveChanges();

					// loads the building, which generated this interest, into newInterest
					context.Entry(newInterest).Reference(e => e.Building).Load();

					// notify landlord of interest
					// TODO: if we are unable to send this email we need a way to allow the user
					// to re-notify the Landlord without requiring them to continue to create
					// interests
					EmailListingInterestedModel model = new EmailListingInterestedModel(newInterest);
					mailer.Interested(model);

					return Status.OK<UserInterest>(newInterest);
				}
				catch (Exception ex)
				{
					// log exception
					return Status.Error<UserInterest>("System was unable to create interest", null);
				}
			}
		}


		public Status<BuildingPreview> GenerateBuildingPreview(Building building)
		{
			if (building == null)
				return Status.ValidationError<BuildingPreview>(null, "building", "The building cannot be null");

			var preview = new BuildingPreview()
			{
				Address1 = building.Address1,
				Address2 = building.Address2,
				Bathrooms = building.Bathrooms.Value,
				Bedrooms = building.Bedrooms.Value,
				BuildingId = building.BuildingId,
				City = building.City,
				IsActive = building.IsActive,
				IsRemovedByAdmin = building.IsRemovedByAdmin,
				Latitude = building.Latitude,
				Longitude = building.Longitude,
				Price = building.Price,
				PrimaryPhotoExtension = building.PrimaryPhotoExtension,
				PrimaryPhotoId = building.PrimaryPhotoId,
				RibbonId = building.RibbonId,
				State = building.State,
				Title = building.Title,
				Zip = building.Zip
			};

			return Status.OK<BuildingPreview>(preview);
		}

		/// <summary>
		/// Increments the listing views in the cache.
		/// </summary>
		/// <param name="listings">The listings.</param>
		public void IncrementListingViews(long listingId)
		{
			try
			{
				var connection = ConnectionGateway.Current.GetWriteConnection();
				connection.Hashes.Increment(App.RedisDatabase, CacheKeys.LISTING_VIEWS, listingId.ToString());
			}
			catch (Exception ex)
			{
				// TODO: log exception                
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
