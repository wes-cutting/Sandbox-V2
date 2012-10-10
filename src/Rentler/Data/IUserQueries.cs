using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public interface IUserQueries
    {
        PaginatedList<Rentler.Common.User> SearchUsers(string query, int pageNumber, int pageSize);
    }
}
