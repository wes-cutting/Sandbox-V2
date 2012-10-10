using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;

namespace Rentler.Facades
{
    public interface IUserFacade
    {
        Status<PaginatedList<Common.User>> SearchForUsers(string query, int? page);
    }

    public class UserFacade : IUserFacade
    {
        IDataServiceFactory factory;

        public UserFacade(IDataServiceFactory factory)
        {
            this.factory = factory;
        }

        public Status<PaginatedList<Common.User>> SearchForUsers(string query, int? page)
        {
            int pageNumber = page.HasValue ? page.Value : 1;

            using(var data = this.factory.Get())
            {
                var results = data.User.SearchUsers(query, pageNumber, 100);
                return Status.OK<PaginatedList<Common.User>>(results);
            }
        }
    }
}
