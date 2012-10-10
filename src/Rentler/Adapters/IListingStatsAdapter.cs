using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Redis;
using Rentler.Configuration;

namespace Rentler.Adapters
{
	public interface IListingStatsAdapter
	{
		Status<long> GetListingSearchStat(long buildingId);
		Status<long> GetListingStat(long buildingId);
		void IncrementListingStat(long buildingId);
		void IncrementSearchStats(long[] buildingIds);
	}

    public class ListingStatsAdapter : IListingStatsAdapter
    {
        public void IncrementSearchStats(long[] buildingIds)
        {
            buildingIds = buildingIds.Distinct().ToArray();
            var connection = ConnectionGateway.Current.GetWriteConnection();
            for (int x = 0; x < buildingIds.Length; ++x)
                connection.Hashes.Increment(App.RedisDatabase, CacheKeys.LISTING_SEARCH_VIEWS, buildingIds[x].ToString());
        }

        public void IncrementListingStat(long buildingId)
        {
            var connection = ConnectionGateway.Current.GetWriteConnection();
            connection.Hashes.Increment(App.RedisDatabase, CacheKeys.LISTING_VIEWS, buildingId.ToString());
        }

        public Status<long> GetListingStat(long buildingId)
        {
			var connection = ConnectionGateway.Current.GetReadConnection();
			var resultTask = connection.Hashes.GetString(App.RedisDatabase, CacheKeys.LISTING_VIEWS, buildingId.ToString());
			string result = connection.Wait(resultTask);
			if (string.IsNullOrEmpty(result))
				return Status.NotFound<long>();

			return Status.OK<long>(long.Parse(result));
        }

        public Status<long> GetListingSearchStat(long buildingId)
        {
            var connection = ConnectionGateway.Current.GetReadConnection();
			
			var resultTask = connection.Hashes.GetString(App.RedisDatabase, CacheKeys.LISTING_SEARCH_VIEWS, buildingId.ToString());
			string result = connection.Wait(resultTask);
			if (string.IsNullOrEmpty(result))
				return Status.NotFound<long>();

			return Status.OK<long>(long.Parse(result));
        }
    }
}
