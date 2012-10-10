using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Common;

namespace Rentler.Data
{
    public interface IListingQueries
    {
        Listing GetListingById(long listingId);

		PaginatedList<Common.ReportedListing> GetReported(int pageNumber, int pageSize);

        Listing RemoveFlag(int buildingId);

        Listing RemoveFlagAndDeactivate(int buildingId);

        Listing AddIsRemovedByAdmin(int buildingId);
    }
}
