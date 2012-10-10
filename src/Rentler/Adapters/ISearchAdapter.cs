using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using Rentler.Redis;
using Rentler.Configuration;

namespace Rentler.Adapters
{
	/// <summary>
	/// Adapter for searching for buildings for the client.
	/// </summary>
	public interface ISearchAdapter
	{
		/// <summary>
		/// Searches the specified search.
		/// </summary>
		/// <param name="search">The search you want.</param>
		/// <returns>A search with the results.</returns>
		Status<Search> Search(Search search);

		/// <summary>
		/// Searches using only a geocoded location.
		/// </summary>
		/// <param name="lat">The latitude</param>
		/// <param name="lng">The longitude</param>
		/// <returns>A search with the results.</returns>
		Status<Search> SearchLocation(float lat, float lng);

		Status<BoundBoxSearch> SearchLocation(float lat, float lng, float miles);

		/// <summary>
		/// Gets a single property for a user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="buildingId">The id of the building</param>
		/// <returns>A BuildingPreview of the Building.</returns>
		Status<BuildingPreview> GetUserProperty(string username, long buildingId);

		/// <summary>
		/// For KSL stuff.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="search"></param>
		/// <returns></returns>
		Status<Rentler.Common.KslPropertySearch> SearchUserProperties(string username, Rentler.Common.KslPropertySearch search);

		/// <summary>
		/// Gets a list of property types and how many are active in Rentler.
		/// </summary>
		/// <returns></returns>
		Status<ListingCountModel> GetListingCounts();
	}

	/// <summary>
	/// Concrete search adapter for searching for listings.
	/// </summary>
	public class SearchAdapter : ISearchAdapter
	{
		IFriendlyZipAdapter zipAdapter;
		IFeaturedAdapter featuredAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="SearchAdapter"/> class.
		/// </summary>
		/// <param name="zipAdapter">The zip adapter.</param>
		public SearchAdapter(
			IFriendlyZipAdapter zipAdapter,
			IFeaturedAdapter featuredAdapter)
		{
			this.zipAdapter = zipAdapter;
			this.featuredAdapter = featuredAdapter;
		}

		/// <summary>
		/// Searches the specified search.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <returns>
		/// A paginated list of search results.
		/// </returns>
		public Status<Search> Search(Search search)
		{
			// if it is null create a new one
			if (search == null)
				search = new Search();

			if (search.Page < 1)
				search.Page = 1;
			if (search.ResultsPerPage < 5)
				search.ResultsPerPage = 30;

			PaginatedList<BuildingPreview> results = null;

			// get the zip codes for the given location

			string[] codes = this.zipAdapter.GetZipCodesFromLocation(search.Location);

			var featured = featuredAdapter.GetFeatured(codes);

			search.ResultsPerPage = search.ResultsPerPage - featured.Result.Count;

			// find all the buildings
			using (var context = new RentlerContext())
			{
				var listings = from b in context.Buildings
							   where b.IsActive &&
							   b.IsDeleted == false &&
							   b.IsRemovedByAdmin == false
							   select b;

				// Location is defined
				if (codes.Length > 0)
				{
					listings = from b in listings
							   where codes.Contains(b.Zip)
							   select b;
				}

				// Property Type is defined
				if (search.PropertyType != PropertyType.Undefined)
				{
					listings = from b in listings
							   where b.PropertyTypeCode == search.PropertyTypeCode
							   select b;
				}

				// Minimum Price defined
				if (search.MinPrice.HasValue)
				{
					listings = from b in listings
							   where b.Price >= search.MinPrice
							   select b;
				}

				// Maximum Price defined
				if (search.MaxPrice.HasValue)
				{
					listings = from b in listings
							   where b.Price <= search.MaxPrice
							   select b;
				}

				// start of advanced

				// Bedrooms is defined
				if (search.Bedrooms.HasValue)
				{
					listings = from b in listings
							   where b.Bedrooms >= search.Bedrooms
							   select b;
				}

				// Bathrooms is defined
				if (search.Bathrooms.HasValue)
				{
					listings = from b in listings
							   where b.Bathrooms >= search.Bathrooms
							   select b;
				}

				// Minimum Square Footage is defined
				if (search.MinSquareFootage.HasValue)
				{
					listings = from b in listings
							   where b.SquareFeet >= search.MinSquareFootage
							   select b;
				}

				// Maximum Square Footage is defined
				if (search.MaxSquareFootage.HasValue)
				{
					listings = from b in listings
							   where b.SquareFeet <= search.MaxSquareFootage
							   select b;
				}

				// Year Built Minimum is defined
				if (search.YearBuiltMin.HasValue)
				{
					listings = from b in listings
							   where b.YearBuilt >= search.YearBuiltMin
							   select b;
				}

				// Year Built Maximum is defined
				if (search.YearBuiltMax.HasValue)
				{
					listings = from b in listings
							   where b.YearBuilt <= search.YearBuiltMax
							   select b;
				}

				// Amenities are defined
				if (search.Amenities != null && search.Amenities.Length > 0)
				{
					listings = from b in listings
							   let ba = b.BuildingAmenities.Select(x => x.AmenityId)
							   where search.Amenities.All(a => ba.Contains(a))
							   select b;
				}

				// Seller Type is defined
				if (search.SellerType != ContactInfoType.Undefined)
				{
					listings = from b in listings
							   where b.ContactInfo.ContactInfoTypeCode == search.SellerTypeCode
							   select b;
				}

				// Terms
				if (search.Terms != null)
				{
					// pet friendly
					if (search.Terms.Contains("petfriendly"))
					{
						listings = from b in listings
								   where b.ArePetsAllowed == true
								   select b;
					}

					// smoking allowed
					if (search.Terms.Contains("smokingallowed"))
					{
						listings = from b in listings
								   where b.IsSmokingAllowed == true
								   select b;
					}
				}

				// Terms Lease Length
				if (search.LeaseLength != LeaseLength.Undefined)
				{
					listings = from b in listings
							   where b.LeaseLengthCode == search.LeaseLengthCode
							   select b;
				}

				// photos only
				if (search.PhotosOnly)
				{
					// if it has a primary photo then it has at least 1 photo
					listings = from b in listings
							   where b.PrimaryPhotoId.HasValue
							   select b;
				}

				// keywords
				if (!string.IsNullOrWhiteSpace(search.Keywords))
				{
					List<string> keywords = new List<string>();

					// add words
					keywords.AddRange(
						search.Keywords.Split(
							new char[0],
							StringSplitOptions.RemoveEmptyEntries
						)
					);

					// add the whole phrase by default if more
					// than 1 word
					if (keywords.Count > 1)
						keywords.Add(search.Keywords);

					// replace commas
					for (int i = 0; i < keywords.Count; ++i)
						keywords[i] = keywords[i].Replace(",", "");

					// apply to Title, Description and Custom Amenities                    
					listings = from b in listings
							   let ca = b.CustomAmenities.Select(a => a.Name)
							   where keywords.Any(k => b.Title.Contains(k)) ||
							   keywords.Any(k => b.Description.Contains(k)) ||
							   keywords.Any(k => ca.Any(a => a.Contains(k)))
							   select b;
				}

				// end of advanced

				// apply default ordering
				switch (search.OrderBy)
				{
					case "NewOld":
						//order by for priority listings, as well as date activated
						listings = listings.OrderByDescending(l => l.HasPriority)
										   .ThenByDescending(l => l.DateActivatedUtc);
						break;
					case "OldNew":
						listings = listings.OrderBy(m => m.DateActivatedUtc);
						break;
					case "PriceHighLow":
						listings = listings.OrderByDescending(m => m.Price);
						break;
					case "PriceLowHigh":
						listings = listings.OrderBy(m => m.Price);
						break;
					case "DateAvailable":
						break;
					default:
						listings = listings.OrderByDescending(l => l.HasPriority)
										   .ThenByDescending(l => l.DateActivatedUtc);
						break;
				}

				// convert to building preview
				var final = from b in listings
							select new BuildingPreview()
							{
								Address1 = b.Address1,
								Address2 = b.Address2,
								Bathrooms = b.Bathrooms.Value,
								Bedrooms = b.Bedrooms.Value,
								BuildingId = b.BuildingId,
								RibbonId = b.RibbonId,
								City = b.City,
								IsFeatured = false,
								Price = b.Price,
								PrimaryPhotoExtension = b.PrimaryPhotoExtension,
								PrimaryPhotoId = b.PrimaryPhotoId,
								State = b.State,
								Title = b.Title,
								IsActive = b.IsActive,
								Latitude = b.Latitude,
								Longitude = b.Longitude,
								HasPriority = b.HasPriority,
								DatePrioritized = b.DatePrioritized,
								Zip = b.Zip
							};

#if DEBUG
				Tracer.OutputQuery<BuildingPreview>(final);
#endif
				// get the results to show
				results = new PaginatedList<BuildingPreview>(
					final,
					search.Page,
					search.ResultsPerPage
				);

				//grab featured items if we have any results
				if (results.Count > 0)
					results.InsertRange(0, featured.Result);
			}

			// increment search views for each listing
			IncrementSearchViews(results.Select(m => m.BuildingId).ToArray());

			search.Results = results;

			search.HasNextPage = results.HasNextPage;
			search.HasPreviousPage = results.HasPreviousPage;

			return Status.OK<Search>(search);
		}

		public Status<Rentler.Common.KslPropertySearch> SearchUserProperties(string username, Rentler.Common.KslPropertySearch search)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<Rentler.Common.KslPropertySearch>(null, "username", "The username is required");

			// if it is null create a new one
			if (search == null)
				search = new Rentler.Common.KslPropertySearch();
			if (search.Page < 1)
				search.Page = 1;
			if (search.ResultsPerPage < 5)
				search.ResultsPerPage = 25;
			if (string.IsNullOrEmpty(search.OrderBy))
				search.OrderBy = "CreateDate";

			// get the user
			using (var context = new RentlerContext())
			{
				var user = (from u in context.Users
							where !u.IsDeleted && (u.Username == username || u.Email == username)
							select u).FirstOrDefault();

				if (user == null)
					return Status.NotFound<Rentler.Common.KslPropertySearch>();

				var props = from b in context.Buildings
							where b.UserId == user.UserId && !b.IsDeleted
							select b;

				// keyword search
				if (!string.IsNullOrEmpty(search.Keywords))
				{
					props = from b in props
							where b.Address1.Contains(search.Keywords) ||
							b.City.Contains(search.Keywords)
							select b;
				}

				// ordering
				switch (search.OrderBy.ToLower())
				{
					case "islisted":
						props = from b in props
								orderby b.IsActive descending
								select b;
						break;
					default:
						props = from b in props
								orderby b.CreateDateUtc descending
								select b;
						break;
				}


				var final = from b in props
							select new BuildingPreview()
							{
								Bathrooms = b.Bathrooms ?? 0,
								Bedrooms = b.Bedrooms ?? 0,
								BuildingId = b.BuildingId,
								City = b.City,
								IsFeatured = false,
								Price = b.Price,
								PrimaryPhotoExtension = b.PrimaryPhotoExtension,
								PrimaryPhotoId = b.PrimaryPhotoId,
								State = b.State,
								Title = b.Title,
								IsRemovedByAdmin = b.IsRemovedByAdmin,
								Address1 = b.Address1,
								IsActive = b.IsActive
							};

				var results = final.ToList();
				search.Results = results;

				return Status.OK<Rentler.Common.KslPropertySearch>(search);
			}
		}

		/// <summary>
		/// Increments the search views in the cache.
		/// </summary>
		/// <param name="listings">The listings.</param>
		private void IncrementSearchViews(long[] listings)
		{
			listings = listings.Distinct().ToArray();
			foreach (long item in listings)
			{
				var connection = ConnectionGateway.Current.GetWriteConnection();
				connection.Hashes.Increment(App.RedisDatabase, CacheKeys.LISTING_SEARCH_VIEWS, item.ToString());
				connection.Strings.Increment(App.RedisDatabase, CacheKeys.TOTAL_SEARCH_VIEWS);
			}
		}

		public Status<BuildingPreview> GetUserProperty(string username, long buildingId)
		{
			BuildingPreview building = null;

			using (var context = new RentlerContext())
				building = (from b in context.Buildings
							where b.BuildingId == buildingId &&
								 (b.User.Username == username || b.User.Email == username)
							select new BuildingPreview()
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
								Title = b.Title,
								IsRemovedByAdmin = b.IsRemovedByAdmin,
								Address1 = b.Address1,
								IsActive = b.IsActive
							}).SingleOrDefault();

			if (building == null)
				return Status.NotFound<BuildingPreview>();

			return Status.OK(building);
		}

		public Status<Search> SearchLocation(float lat, float lng)
		{
			//first, resolve lat/lng to a zip
			var zips = zipAdapter.GetZipCodesFromLocation(lat, lng);
			var search = new BoundBoxSearch()
			{
				ResultsPerPage = int.MaxValue,
				Location = zips.Any() ? zips.Select(i => i).Aggregate((i, j) => i + " " + j) : "",
			};

			return Search(search);
		}

		public Status<BoundBoxSearch> SearchLocation(float lat, float lng, float miles)
		{
			var bounds = Haversine.GetBoundingBox(lat, lng, miles);
			PaginatedList<BuildingPreview> results = null;

			using (var context = new RentlerContext())
			{
				var final = from b in context.Buildings
							where b.IsActive &&
									!b.IsDeleted &&
									!b.IsRemovedByAdmin &&
									b.Latitude >= bounds.MinLat &&
									b.Latitude <= bounds.MaxLat &&
									b.Longitude >= bounds.MinLng &&
									b.Longitude <= bounds.MaxLng
							orderby b.BuildingId
							select new BuildingPreview()
							{
								Address1 = b.Address1,
								Address2 = b.Address2,
								Bathrooms = b.Bathrooms.Value,
								Bedrooms = b.Bedrooms.Value,
								BuildingId = b.BuildingId,
								RibbonId = b.RibbonId,
								City = b.City,
								IsFeatured = false,
								Price = b.Price,
								PrimaryPhotoExtension = b.PrimaryPhotoExtension,
								PrimaryPhotoId = b.PrimaryPhotoId,
								State = b.State,
								Title = b.Title,
								IsActive = b.IsActive,
								Latitude = b.Latitude,
								Longitude = b.Longitude,
								Zip = b.Zip
							};
				// get the results to show
				results = new PaginatedList<BuildingPreview>(final, 1, int.MaxValue);
			}

			results = new PaginatedList<BuildingPreview>(results
				.Select(z => new KeyValuePair<double, BuildingPreview>(
					Haversine.GetDistance(lat, lng, z.Latitude, z.Longitude), z))
				.OrderBy(z => z.Key).Select(s => s.Value).AsQueryable(), 1, int.MaxValue);

			return Status.OK(new BoundBoxSearch
			{
				Results = results,
				Bounds = bounds,
				ResultsPerPage = int.MaxValue,
				Page = 1
			});
		}

		public Status<ListingCountModel> GetListingCounts()
		{
			var result = new ListingCountModel();

			using (var context = new RentlerContext())
			{
				var counts = (from b in context.Buildings
							  where b.IsActive && !b.IsDeleted
							  group b by b.PropertyTypeCode into grouped
							  select new
							  {
								  PropertyTypeCode = grouped.Key,
								  Count = grouped.Count()
							  }).ToList();

				foreach (var item in counts)
					switch ((PropertyType)item.PropertyTypeCode)
					{
						case PropertyType.SingleFamilyHome:
							result.SingleFamilyHome = item.Count;
							break;
						case PropertyType.Apartment:
							result.Apartment = item.Count;
							break;
						case PropertyType.CondoTownhome:
							result.CondoTownhome = item.Count;
							break;
						case PropertyType.MultiFamilyHome:
							result.MultiFamilyHome = item.Count;
							break;
						case PropertyType.ManufacturedHome:
							result.ManufacturedHome = item.Count;
							break;
						case PropertyType.HorseLivestock:
							result.HorseLivestock = item.Count;
							break;
						case PropertyType.SingleRoom:
							result.SingleRoom = item.Count;
							break;
						default:
							break;
					}
			}

			return Status.OK(result);
		}
	}
}
