using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace NewNewRentler
{

	/// <summary>
	/// Wrapper for strongly-typed application settings defined
	/// in the app and web.configs.
	/// </summary>
	public class NewApp : IApp
	{
		private string redisHost;
		/// <summary>
		/// Gets the redis hostname.
		/// </summary>
		public string RedisHost
		{
			get
			{
				return "rentlerredis.cloudapp.net";
			}
		}

		private int? redisPort;
		/// <summary>
		/// Gets the redis port.
		/// </summary>
		public int RedisPort
		{
			get
			{
				return 6380;
			}
		}

		private string redisAuth;
		/// <summary>
		/// Gets the redis auth token.
		/// </summary>
		public string RedisAuth
		{
			get
			{
				return "fckgwrhqq2yxrkt8tg6w2b7q8T33mp4ss";
			}
		}

		private int? redisDatabase;
		/// <summary>
		/// Gets the redis database number used.
		/// </summary>
		public int RedisDatabase
		{
			get
			{
				return 1;
			}
		}

		private int? redisCacheDatabase;
		public int RedisCacheDatabase
		{
			get
			{
				return 2;
			}
		}
	}
}
