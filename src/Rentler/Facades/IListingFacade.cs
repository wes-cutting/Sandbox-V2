using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Common;
using Rentler.Data;
using Rentler.Redis;
using Rentler.Configuration;

namespace Rentler.Facades
{
    public interface IListingFacade
    {
        Status<PaginatedList<ReportedListing>> GetReportedListings(int? page);

        Status<bool> RemoveFlag(int buildingId);

        Status<bool> RemoveFlagAndDeactivate(int buildingId);

        Status<bool> RemoveByAdmin(int buildingId);
    }

    public class ListingFacade : IListingFacade
    {
        IDataServiceFactory dataFactory;

        public ListingFacade(IDataServiceFactory dataFactory)
        {
            this.dataFactory = dataFactory;
        }

        public Status<PaginatedList<ReportedListing>> GetReportedListings(int? page)
        {
            int pageNumber = page.HasValue ? page.Value : 1;

            using (var data = this.dataFactory.Get())
            {
                var result = data.Listing.GetReported(pageNumber, 50);
                return Status.OK<PaginatedList<ReportedListing>>(result);
            }
        }

        public Status<bool> RemoveFlag(int buildingId)
        {
            using (var data = this.dataFactory.Get())
            {
                var listing = data.Listing.RemoveFlag(buildingId);
                if (listing == null)
                    return Status.NotFound<bool>();
                InvalidateCache(buildingId);
                return Status.OK<bool>(true);
            }
        }


        public Status<bool> RemoveFlagAndDeactivate(int buildingId)
        {
            using (var data = this.dataFactory.Get())
            {
                var listing = data.Listing.RemoveFlagAndDeactivate(buildingId);
                if (listing == null)
                    return Status.NotFound<bool>();
                InvalidateCache(buildingId);
                return Status.OK<bool>(true);
            }
        }

        public Status<bool> RemoveByAdmin(int buildingId)
        {
            using (var data = this.dataFactory.Get())
            {
                var listing = data.Listing.AddIsRemovedByAdmin(buildingId);
                if (listing == null)
                    return Status.NotFound<bool>();
                InvalidateCache(buildingId);
                return Status.OK<bool>(true);
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
