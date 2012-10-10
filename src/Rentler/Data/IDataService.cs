using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    /// <summary>
    /// Encapsulates all database queries for the application.
    /// This data service is instantiated by the DataServiceFactory.
    /// This allows you to get a service factory injected into
    /// an adapter and then you can use a using statement around
    /// the Get method to retain disposing and have access
    /// to the entire data layer.
    /// </summary>
    public interface IDataService : IDisposable
    {
        void Save();

        IListingQueries Listing { get; }

        IBuildingQueries Building { get; }

        IUserQueries User { get; }
    }
}
