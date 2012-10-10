using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Common;
using Rentler.Auth;
using Rentler.Data;
using Rentler.Configuration;
using Rentler.Redis;

namespace Rentler.Facades
{
    public interface IPropertyFacade
    {
        Status<Listing> ManageListingById(long listing);

        Status<PropertySearch> SearchForUserProperty(PropertySearch search);

        Status<bool> DeleteProperty(long buildingId);
    }

    public class PropertyFacade : IPropertyFacade
    {
        IDataServiceFactory service;

        public PropertyFacade(IDataServiceFactory service)
        {
            this.service = service;
        }

        public Status<Listing> ManageListingById(long listingId)
        {
            var ident = CustomAuthentication.GetIdentity();

            if (!ident.IsAuthenticated)
                return Status.UnAuthorized<Listing>();

            using (var data = this.service.Get())
            {
                var result = data.Listing.GetListingById(listingId);

                if (result == null)
                    return Status.NotFound<Listing>();

                if (result.UserId == ident.UserId)
                    result.IsOwnedByCurrentUser = true;

                if (!result.IsOwnedByCurrentUser)
                    return Status.UnAuthorized<Listing>();

                // get the stats
                var connection = ConnectionGateway.Current.GetReadConnection();
                try
                {
                    var listingViewTask = connection.Hashes.GetString(App.RedisDatabase, 
                        CacheKeys.LISTING_VIEWS, listingId.ToString());
                    string listingViewResult = connection.Wait(listingViewTask);
                    if (string.IsNullOrEmpty(listingViewResult))
                        result.PageViews = 0;
                    result.PageViews = long.Parse(listingViewResult);

                    var listingSearchTask = connection.Hashes.GetString(App.RedisDatabase, 
                        CacheKeys.LISTING_SEARCH_VIEWS, listingId.ToString());
                    string listingSearchResult = connection.Wait(listingSearchTask);
                    if (string.IsNullOrEmpty(listingSearchResult))
                        result.SearchViews = 0;
                    result.SearchViews = long.Parse(listingSearchResult);
                }
                catch (Exception)
                {
                    result.PageViews = 0;
                    result.SearchViews = 0;
                }

                return Status.OK<Listing>(result);
            }
        }

        public Status<PropertySearch> SearchForUserProperty(PropertySearch search)
        {
            var ident = CustomAuthentication.GetIdentity();
            if (!ident.IsAuthenticated)
                return Status.UnAuthorized<PropertySearch>();

            // if it is null create a new one
            if (search == null)
                search = new PropertySearch();
            if (search.Page < 1)
                search.Page = 1;
            if (search.ResultsPerPage < 5)
                search.ResultsPerPage = 25;
            if (string.IsNullOrEmpty(search.OrderBy))
                search.OrderBy = "CreateDate";

            using (var data = this.service.Get())
            {
                var result = data.Building.SearchUserBuildings(ident.UserId, search);
                search.Results = result;
                return Status.OK<PropertySearch>(search);
            }
        }

        public Status<bool> DeleteProperty(long buildingId)
        {
            var ident = CustomAuthentication.GetIdentity();
            if (!ident.IsAuthenticated)
                return Status.UnAuthorized<bool>();

            using (var data = this.service.Get())
            {
                // get the building and make sure the user owns it
                var preview = data.Building.GetBuildingPreviewById(buildingId);

                if (preview == null)
                    return Status.NotFound<bool>();
                if (preview.UserId != ident.UserId)
                    return Status.UnAuthorized<bool>();

                // delete the building
                data.Building.DeleteBuilding(buildingId);

                data.Save();

                return Status.OK<bool>(true);
            }
        }
    }
}
