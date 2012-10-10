using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public partial class SqlDataService : IBuildingQueries
    {
        public PaginatedList<Common.PropertyPreview> SearchUserBuildings(int userId, Common.PropertySearch search)
        {
            // base query
            var query = from u in this.context.Buildings
                        where u.UserId == userId && !u.IsDeleted
                        select u;

            // querying for keywords
            if (!string.IsNullOrWhiteSpace(search.Keywords))
                query = from u in query
                        where
                            u.Address1.Contains(search.Keywords) ||
                            u.Address2.Contains(search.Keywords) ||
                            u.City.Contains(search.Keywords) ||
                            u.State.Contains(search.Keywords)
                        select u;

            // ordering
            if (search.OrderBy.ToLower() == "createdate")
                query = from u in query
                        orderby u.CreateDateUtc
                        descending
                        select u;
            else
                query = from u in query
                        orderby u.IsActive
                        descending
                        select u;

            var final = from u in query
                        select u;

            var list1 = new PaginatedList<Building>(final, search.Page, search.ResultsPerPage);

            var move = (from m in list1 select m.ToBuildingPreview()).AsQueryable();
            return new PaginatedList<Common.PropertyPreview>(move, search.Page, search.ResultsPerPage, list1.TotalCount);
        }


        public Common.PropertyPreview GetBuildingPreviewById(long buildingId)
        {
            var result = (from u in this.context.Buildings
                          where u.BuildingId == buildingId
                          select u).SingleOrDefault();

            if (result == null)
                return null;
            
            return result.ToBuildingPreview();
        }

        public void DeleteBuilding(long buildingId)
        {
            var result = (from u in this.context.Buildings
                          where u.BuildingId == buildingId
                          select u).SingleOrDefault();

            if (result != null)
                result.IsDeleted = true;
        }
    }
}
