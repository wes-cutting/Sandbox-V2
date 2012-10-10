using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Rentler.Redis;
using Rentler.Data;
using Rentler.Configuration;
using System.Globalization;
using System.Threading;
using ProtoBuf;
using System.IO;
using Rentler.Extensions;

namespace Rentler.Adapters
{
	public interface IFriendlyZipAdapter
	{
		string[] GetZipCodesFromLocation(string location);
		string[] GetZipCodesFromLocation(float lat, float lng);
		List<ZipInfoPreview> GetZipInfo(string[] zips);
	}

	public class FriendlyZipAdapter : IFriendlyZipAdapter
	{
		public string[] GetZipCodesFromLocation(string location)
		{
			if (string.IsNullOrWhiteSpace(location))
				return new string[0];

			location = location.Replace(" ", ",").Replace(",,", ",").ToLower();

			List<ZipInfoPreview> zips = null;
			var states = States.Current.StatesListForComparisons;

			//split up the search params
			var search = location.Split(',').ToList();

			//any full state names? replace them with state codes
			var comparer = StringComparer.Create(CultureInfo.CurrentCulture, true);
			for (int i = 0; i < search.Count; i++)
				foreach (var s in states)
					if (comparer.Compare(search[i], s.Key) == 0)
						search[i] = s.Value.ToLower();

			//get the zip infos
			zips = GetZips();

			//any zip codes?
			var anyZips = (from z in zips
						   where search.Any(i => z.ZipCode.ToString() == i)
						   select z.ZipCode.ToString()).Distinct().ToArray();

			//if we found any, just return
			if (anyZips.Length > 0)
				return anyZips;

			List<ZipInfoPreview> results = null;

			//look for friendly name matches
			results = (from z in zips
					   where search.All(i => z.FriendlyName.Contains(i))
					   select z).ToList();

			//any state codes? look for them
			if (search.Any(i => states.ContainsValue(i.ToUpper())))
				results = (from r in results
						   where search.Any(i => r.StateCode.Contains(i))
						   select r).ToList();

			return results.Distinct().Select(f => f.ZipCode.ToString()).ToArray();
		}

		public string[] GetZipCodesFromLocation(float lat, float lng)
		{
			var zips = GetZips();

			//filter out duplicate zip codes (since we only care about
			//their location, not friendly/slang names for anything)
			zips = zips.DistinctBy(z => z.ZipCode).ToList();

			var zipRange = zips
				.Select(z => new KeyValuePair<double, ZipInfoPreview>(
					Haversine.GetDistance(lat, lng, z.Latitude, z.Longitude), z))
				.OrderBy(z => z.Key).ToList();

			//grab two miles worth
			var result = zipRange.Where(z => z.Key <= 2.0).ToList();

			//none that close? grab the closest five.
			if (result.Count == 0)
				result = zipRange.Take(5).ToList();

			return result.Select(r => r.Value.ZipCode.ToString()).ToArray();
		}

		public List<ZipInfoPreview> GetZipInfo(string[] zips)
		{
			return GetZips().Where(z => zips.Contains(z.ZipCode.ToString())).ToList();
		}

		List<ZipInfoPreview> GetZips()
		{
			var zips = new List<ZipInfoPreview>();

			//L1
			var cache = HttpContext.Current.Cache;
			zips = cache[CacheKeys.ZIP_INFOS] as List<ZipInfoPreview>;
			if (zips != null)
				return zips;

			//L2
			//try to get from redis

			//leave this connection as is; it is rarely hit, thanks to the L1 cache,
			//and zip infos should be loaded as fast as possible, since it is vital for search.
			var connection = ConnectionGateway.Current.GetWriteConnection();
			var zipTask = connection.Strings.Get(App.RedisDatabase, CacheKeys.ZIP_INFOS);
			var result = connection.Wait(zipTask);

			if (result != null && result.Length > 0)
			{
				using (var stream = new MemoryStream(result))
					zips = Serializer.Deserialize<List<ZipInfoPreview>>(stream);

				//add to L1
				cache[CacheKeys.ZIP_INFOS] = zips;

				return zips;
			}


			using (var context = new RentlerContext())
			{
				//get from db
				zips = (from z in context.ZipInfos
						select new ZipInfoPreview
						{
							ZipCode = z.ZipCode,
							FriendlyName = (z.City + " " + z.CityAliasName + " " + z.StateCode).ToLower(),
							StateCode = z.StateCode.ToLower(),
							Latitude = z.Latitude,
							Longitude = z.Longitude
						}).ToList();
			}

			//add to L2
			var con = ConnectionGateway.Current.GetWriteConnection();
			con.Strings.Set(App.RedisDatabase, CacheKeys.ZIP_INFOS, zips.ToBinaryArray());

			//add to L1
			cache[CacheKeys.ZIP_INFOS] = zips;

			return zips;
		}
	}
}
