using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public partial class SqlDataService : IUserQueries
    {
        public PaginatedList<Common.User> SearchUsers(string query, int pageNumber, int pageSize)
        {
            var finalQuery = (from u in this.context.Users
                         select u);

            if (!string.IsNullOrWhiteSpace(query))
            {
                finalQuery = (from u in finalQuery
                              where u.Username.Contains(query) ||
                              u.Email.Contains(query) ||
                              u.FirstName.Contains(query) ||
                              u.LastName.Contains(query)
                              orderby u.CreateDateUtc descending
                              select u);
            }
            else
            {
                finalQuery = (from u in finalQuery
                              orderby u.CreateDateUtc descending
                              select u);
            }

            // convert to business paginated list with entity framework
            var list1 = new PaginatedList<User>(finalQuery, pageNumber, pageSize);
            var move = (from m in list1 select m.ToBusinessUser()).AsQueryable();
            return new PaginatedList<Common.User>(move, pageNumber, pageSize, list1.TotalCount);
        }
    }
}
