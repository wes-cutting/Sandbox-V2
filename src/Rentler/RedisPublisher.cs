using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
	public static class RedisPublisher
	{
		public static void Publish(string channel, string message)
		{
			//disabled for now, it's for debuggin.
			//var connection = Rentler.Redis.ConnectionGateway.Current.GetConnection();
			//connection.Publish(channel, message);
		}
	}
}
