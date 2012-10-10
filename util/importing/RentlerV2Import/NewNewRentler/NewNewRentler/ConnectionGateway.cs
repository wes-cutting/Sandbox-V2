using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookSleeve;
using System.Net.Sockets;

namespace NewNewRentler
{

	/// <summary>
	/// Singleton implemented Booksleve connection to the redis
	/// server. Booksleve is universal, thread-safe, and paralleled so
	/// only one connection needs to be maintined throughout the system. This
	/// class maintains that connection.
	/// </summary>
	public sealed class ConnectionGateway
	{
		private const string RedisConnectionFailed = "Redis connection failed.";
		private RedisConnection _connection;
		private static volatile ConnectionGateway _instance;

		private static object syncLock = new object();
		private static object syncConnectionLock = new object();

		public static IApp App { get; set; }

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="ConnectionGateway"/> is reclaimed by garbage collection.
		/// </summary>
		~ConnectionGateway()
		{
			if (_connection != null)
				_connection.Close(false);
		}

		/// <summary>
		/// Prevents a default instance of the 
		/// <see cref="ConnectionGateway"/> class from being created.
		/// </summary>
		private ConnectionGateway()
		{
			_connection = getNewConnection();
		}

		/// <summary>
		/// Generates a new Booksleve connection to redis.
		/// </summary>
		/// <returns>The connection created.</returns>
		private static RedisConnection getNewConnection()
		{
			if (!string.IsNullOrEmpty(App.RedisAuth))
				return new RedisConnection(App.RedisHost, port: App.RedisPort,
					password: App.RedisAuth, syncTimeout: 50000, ioTimeout: 50000);
			else
				return new RedisConnection(App.RedisHost, port: App.RedisPort,
					syncTimeout: 50000, ioTimeout: 50000);
		}

		/// <summary>
		/// Gets either the current or a new connection based on
		/// the status of the connection.
		/// </summary>
		/// <returns>A booksleve connection to redis that is thread-safe
		/// and universal.</returns>
		public RedisConnection GetConnection()
		{
			lock (syncConnectionLock)
			{
				// no connection
				if (_connection == null)
					_connection = getNewConnection();

				// already opening
				if (_connection.State == RedisConnectionBase.ConnectionState.Opening)
					return _connection;

				// in the middle of closing
				if (_connection.State == RedisConnectionBase.ConnectionState.Closing ||
					_connection.State == RedisConnectionBase.ConnectionState.Closed)
				{
					try
					{
						_connection = getNewConnection();
					}
					catch (Exception ex)
					{
						throw new Exception(RedisConnectionFailed, ex);
					}
				}
				// just created, but not open
				if (_connection.State == RedisConnectionBase.ConnectionState.Shiny)
				{
					try
					{
						var openAsync = _connection.Open();
						_connection.Wait(openAsync);
					}
					catch (SocketException ex)
					{
						throw new Exception(RedisConnectionFailed, ex);
					}
				}

				return _connection;
			}
		}

		public RedisConnection GetNewConnection()
		{
			lock (syncConnectionLock)
				_connection = null;

			return GetConnection();
		}

		/// <summary>
		/// Gets the current instantiation from the singleton
		/// implementation.
		/// </summary>
		public static ConnectionGateway Current
		{
			get
			{
				if (_instance == null)
				{
					lock (syncLock)
					{
						if (_instance == null)
						{
							_instance = new ConnectionGateway();
						}
					}
				}

				return _instance;
			}
		}
	}
}
