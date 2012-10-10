using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Rentler.Configuration
{
	/// <summary>
	/// Wrapper for strongly-typed application settings defined
	/// in the app and web.configs.
	/// </summary>
	public static class App
	{
		private static bool? l1CacheEnabled;
		public static bool L1CacheEnabled
		{
			get
			{
				if (l1CacheEnabled == null)
					l1CacheEnabled = bool.Parse(ConfigurationManager.AppSettings["L1CacheEnabled"]);
				return l1CacheEnabled.Value;
			}
		}

		private static string redisHost;
		/// <summary>
		/// Gets the redis hostname.
		/// </summary>
		public static string RedisHost
		{
			get
			{
				if (redisHost == null)
					redisHost = ConfigurationManager.AppSettings["RedisHost"];
				return redisHost;
			}
		}

		private static int? redisPort;
		/// <summary>
		/// Gets the redis port.
		/// </summary>
		public static int RedisPort
		{
			get
			{
				if (redisPort == null)
					redisPort = int.Parse(ConfigurationManager.AppSettings["RedisPort"]);
				return redisPort.Value;
			}
		}

		private static string redisAuth;
		/// <summary>
		/// Gets the redis auth token.
		/// </summary>
		public static string RedisAuth
		{
			get
			{
				if (redisAuth == null)
					redisAuth = ConfigurationManager.AppSettings["RedisAuth"];
				return redisAuth;
			}
		}

		private static int? redisDatabase;
		/// <summary>
		/// Gets the redis database number used.
		/// </summary>
		public static int RedisDatabase
		{
			get
			{
				if (redisDatabase == null)
					redisDatabase = int.Parse(ConfigurationManager.AppSettings["RedisDatabase"]);
				return redisDatabase.Value;
			}
		}

		private static int? redisCacheDatabase;
		public static int RedisCacheDatabase
		{
			get
			{
				if (redisCacheDatabase == null)
					redisCacheDatabase = int.Parse(ConfigurationManager.AppSettings["RedisCacheDatabase"]);
				return redisCacheDatabase.Value;
			}
		}

		private static string hostname;

		public static string Hostname
		{
			get
			{
				if (hostname == null)
					hostname = ConfigurationManager.AppSettings["Hostname"];
				return hostname;
			}
		}

		private static string blobPrefix;

		public static string BlobPrefix
		{
			get
			{
				if (blobPrefix == null)
					blobPrefix = ConfigurationManager.AppSettings["BlobPrefix"];
				return blobPrefix;
			}
		}

		private static string photoFolder;

		public static string PhotoFolder
		{
			get
			{
				if (string.IsNullOrEmpty(photoFolder))
				{
					string prefix = App.BlobPrefix;

					// setup the prefix
					if (!string.IsNullOrEmpty(prefix))
						prefix += "-";

					if (prefix.ToLower() == "dev-")
						prefix += System.Environment.MachineName + "-";

					prefix += "userphotos";

					photoFolder = prefix.ToLower();
				}
				return photoFolder;
			}
		}

		private static string blobConnection;

		public static string BlobConnection
		{
			get
			{
				if (blobConnection == null)
					blobConnection = ConfigurationManager.AppSettings["BlobConnection"];
				return blobConnection;
			}
		}

		private static string blobStorageHostname;

		public static string BlobStorageHostname
		{
			get
			{
				if (blobStorageHostname == null)
					blobStorageHostname = ConfigurationManager.AppSettings["BlobStorageHostname"];
				return blobStorageHostname;
			}
		}

		private static string googleCode;
		/// <summary>
		/// Gets the redis hostname.
		/// </summary>
		public static string GoogleCode
		{
			get
			{
				if (googleCode == null)
					googleCode = ConfigurationManager.AppSettings["GoogleCode"];
				return googleCode;
			}
		}

		private static string modpayClientId;
		/// <summary>
		/// Gets the modpay clientId.
		/// </summary>
		public static string ModpayClientId
		{
			get
			{
				if (string.IsNullOrWhiteSpace(modpayClientId))
					modpayClientId = ConfigurationManager.AppSettings["ModpayClientId"];
				return modpayClientId;
			}
		}

		private static string modpayClientCode;

		/// <summary>
		/// Gets the modpay clientCode (password)
		/// </summary>
		public static string ModpayClientCode
		{
			get
			{
				if (string.IsNullOrWhiteSpace(modpayClientCode))
					modpayClientCode = ConfigurationManager.AppSettings["ModpayClientCode"];
				return modpayClientCode;
			}
		}

		private static string kslUrl;

		public static string KslUrl
		{
			get
			{
				if (string.IsNullOrWhiteSpace(kslUrl))
					kslUrl = ConfigurationManager.AppSettings["KslUrl"];
				return kslUrl;
			}
		}

		private static string kslListingPath;

		public static string KslListingPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(kslListingPath))
					kslListingPath = ConfigurationManager.AppSettings["KslListingPath"];
				return kslListingPath;
			}
		}

        private static string kslSearchPath;

        public static string KslSearchPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(kslSearchPath))
                    kslSearchPath = ConfigurationManager.AppSettings["KslSearchPath"];
                return kslSearchPath;
            }
        }

		private static string storePricing;

		public static string StorePricing
		{
			get
			{
				if (string.IsNullOrWhiteSpace(storePricing))
					storePricing = ConfigurationManager.AppSettings["StorePricing"];
				return storePricing;
			}
		}
	}
}
