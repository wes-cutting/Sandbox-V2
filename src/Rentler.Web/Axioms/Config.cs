using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Rentler.Web
{
	public static class Config
	{
		private static string addThisPubId;
		private static string hostname;
		private static string kslDomain;
		private static string kslListingPath;
		private static string kslSearchPath;
		private static string kslLoginPath;

		public static string Hostname
		{
			get
			{
				if (string.IsNullOrWhiteSpace(hostname))
					hostname = ConfigurationManager.AppSettings["Hostname"];

				return hostname;
			}
		}

		public static string AddThisPubId
		{
			get
			{
				if (string.IsNullOrEmpty(addThisPubId))
					addThisPubId = ConfigurationManager.AppSettings["AddThisPubId"];

				return addThisPubId;
			}
		}

		public static string KslDomain
		{
			get
			{
				if (string.IsNullOrEmpty(kslDomain))
					kslDomain = ConfigurationManager.AppSettings["KslUrl"];

				return kslDomain;
			}
		}

		public static string KslListingPath
		{
			get
			{
				if (string.IsNullOrEmpty(kslListingPath))
					kslListingPath = ConfigurationManager.AppSettings["KslListingPath"];

				return kslListingPath;
			}
		}

		public static string KslSearchPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(kslSearchPath))
					kslSearchPath = ConfigurationManager.AppSettings["KslSearchPath"];

				return kslSearchPath;
			}
		}

		public static string KslListingUrl
		{
			get
			{
				return KslDomain + KslListingPath;
			}
		}

		public static string KslLoginPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(kslLoginPath))
					kslLoginPath = ConfigurationManager.AppSettings["KslLoginPath"];
				return kslLoginPath;
			}
		}
	}
}