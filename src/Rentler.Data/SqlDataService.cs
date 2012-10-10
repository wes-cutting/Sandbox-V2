using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public partial class SqlDataService : IDataService
    {
        RentlerEntityContext context;

        public SqlDataService()
        {
            this.context = new RentlerEntityContext();
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public IListingQueries Listing
        {
            get { return this; }
        }

        public IBuildingQueries Building
        {
            get { return this; }
        }

        public IUserQueries User
        {
            get { return this; }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
