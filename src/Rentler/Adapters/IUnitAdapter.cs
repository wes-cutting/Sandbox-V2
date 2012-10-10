using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;

namespace Rentler.Adapters
{
    public interface IUnitAdapter
    {
        Status<Building> GetFullBuilding(string username, long buildingId);

        /// <summary>
        /// Get number of times listing has been viewed
        /// </summary>
        /// <param name="buildingId">the listing identifier</param>
        /// <returns>the number of times this listing has been viewed</returns>
        Status<long> GetListingViews(long buildingId);

        /// <summary>
        /// Get number of times listing has been returned from a search
        /// </summary>
        /// <param name="buildingId">the listing identifier</param>
        /// <returns>the number of times this listing has been viewed</returns>
        Status<long> GetListingSearchViews(long buildingId);        
    }

    public class UnitAdapter : IUnitAdapter
    {
        IPropertyAdapter propertyAdapter;

        public UnitAdapter(IPropertyAdapter propertyAdapter)
        {
            this.propertyAdapter = propertyAdapter;
        }

        public Status<Building> GetFullBuilding(string username, long buildingId)
        {
            return this.propertyAdapter.GetProperty(buildingId, username);
        }

        public Status<long> GetListingViews(long buildingId)
        {
            return this.propertyAdapter.GetListingViews(buildingId);
        }

        public Status<long> GetListingSearchViews(long buildingId)
        {
            return this.propertyAdapter.GetListingSearchViews(buildingId);
        }
    }
}
