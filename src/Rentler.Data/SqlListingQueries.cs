using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public partial class SqlDataService : IListingQueries
    {
        public Common.Listing GetListingById(long listingId)
        {
            var listing = (from m in this.context.Buildings
                           where m.BuildingId == listingId
                           select m).SingleOrDefault();

            if (listing == null)
                return null;
            
            return listing.ToListing();
        }


        public PaginatedList<Common.ReportedListing> GetReported(int pageNumber, int pageSize)
        {
            var query = (from u in this.context.Buildings
                         where u.IsReported && !u.IsRemovedByAdmin && !u.IsDeleted && u.IsActive
                         orderby u.CreateDateUtc
                         select u);

            // convert to business paginated list with entity framework
            var list1 = new PaginatedList<Building>(query, pageNumber, pageSize);
            var move = (from m in list1 select m.ToReportedListing()).AsQueryable();
            return new PaginatedList<Common.ReportedListing>(move, pageNumber, pageSize, list1.TotalCount);
        }


        public Common.Listing RemoveFlag(int buildingId)
        {
            var listing = (from u in this.context.Buildings
                           where u.BuildingId == buildingId
                           select u).SingleOrDefault();

            if (listing == null)
                return null;

            listing.IsReported = false;
            this.context.SaveChanges();
            return listing.ToListing();
        }


        public Common.Listing RemoveFlagAndDeactivate(int buildingId)
        {
            var listing = (from u in this.context.Buildings
                           where u.BuildingId == buildingId
                           select u).SingleOrDefault();

            if (listing == null)
                return null;

            listing.IsReported = false;
            listing.IsActive = false;
            this.context.SaveChanges();
            return listing.ToListing();
        }


        public Common.Listing AddIsRemovedByAdmin(int buildingId)
        {
            var listing = (from u in this.context.Buildings
                           where u.BuildingId == buildingId
                           select u).SingleOrDefault();

            if (listing == null)
                return null;

            listing.IsRemovedByAdmin = true;
            listing.IsActive = false;

            this.context.SaveChanges();
            return listing.ToListing();
        }
    }
}
