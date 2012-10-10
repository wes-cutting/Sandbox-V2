using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler
{
	public interface IApp
	{
		string RedisHost { get; }
		int RedisPort { get; }
		string RedisAuth { get; }
		int RedisDatabase { get; }
		int RedisCacheDatabase { get; }
	}
}
