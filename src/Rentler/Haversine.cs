using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
	public static class Haversine
	{
		const double MIN_LAT = -(Math.PI / 2);
		const double MAX_LAT = (Math.PI / 2);
		const double MIN_LNG = -Math.PI;
		const double MAX_LNG = Math.PI;

		/// <summary>
		/// Gets the distance between two lat/lng's in miles.
		/// </summary>
		/// <param name="lat1">The lat1.</param>
		/// <param name="lon1">The lon1.</param>
		/// <param name="lat2">The lat2.</param>
		/// <param name="lon2">The lon2.</param>
		/// <remarks>Uses the Haversine "as the crow flies" formula.</remarks>
		/// <returns>The distance in miles.</returns>
		public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
		{
			double theta = lon1 - lon2;
			double dist =
				Math.Sin(DegToRad(lat1)) * Math.Sin(DegToRad(lat2)) +
				Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) *
				Math.Cos(DegToRad(theta));

			dist = Math.Acos(dist);
			dist = RadToDeg(dist);
			dist = dist * 60 * 1.1515;

			return (dist);
		}

		public static LocationBounds GetBoundingBox(double lat, double lng, double miles)
		{
			//radius of the earth in miles
			var radius = 3963.1676;

			//convert to radians
			lat = DegToRad(lat);
			lng = DegToRad(lng);

			//angular distance in radians on a great circle
			double radDist = miles / radius;

			double minLat = lat - radDist;
			double maxLat = lat + radDist;

			double minLng, maxLng;

			if (minLat > MIN_LAT && maxLat < MAX_LAT)
			{
				double deltaLng = Math.Asin(Math.Sin(radDist) / Math.Cos(lat));

				minLng = lng - deltaLng;
				if (lng < MIN_LNG)
					minLng += 2d * Math.PI;

				maxLng = lng + deltaLng;
				if (maxLng > MAX_LNG)
					maxLng -= 2d * Math.PI;
			}
			else
			{
				// a pole is within the distance
				minLat = Math.Max(minLat, MIN_LAT);
				maxLat = Math.Min(maxLat, MAX_LAT);
				minLng = MIN_LNG;
				maxLng = MAX_LNG;
			}

			return new LocationBounds
			{
				MinLat = RadToDeg(minLat),
				MaxLat = RadToDeg(maxLat),
				MinLng = RadToDeg(minLng),
				MaxLng = RadToDeg(maxLng)
			};
		}

		static double DegToRad(double deg)
		{
			return (deg * Math.PI / 180.0);
		}

		static double RadToDeg(double rad)
		{
			return (rad / Math.PI * 180.0);
		}
	}
}
