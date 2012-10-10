using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookSleeve;
using System.Net.Sockets;
using Rentler.Configuration;

namespace Rentler.Redis
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

        private RedisConnection connection;
        private RedisConnection readConnection;

        private static volatile ConnectionGateway instance;

        private static object syncLock = new object();
        private static object syncConnectionLock = new object();
        private static object syncReadConnectionLock = new object();

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ConnectionGateway"/> is reclaimed by garbage collection.
        /// </summary>
        ~ConnectionGateway()
        {
            if (connection != null)
                connection.Close(false);

            if (readConnection != null)
                readConnection.Close(false);
        }

        /// <summary>
        /// Prevents a default instance of the 
        /// <see cref="ConnectionGateway"/> class from being created.
        /// </summary>
        private ConnectionGateway()
        {
            connection = getNewConnection();
            readConnection = getNewConnection();
        }

        /// <summary>
        /// Generates a new Booksleve connection to redis.
        /// </summary>
        /// <returns>The connection created.</returns>
        private static RedisConnection getNewConnection()
        {
            if (!string.IsNullOrEmpty(App.RedisAuth))
                return new RedisConnection(App.RedisHost, port: App.RedisPort, password: App.RedisAuth);
            else
                return new RedisConnection(App.RedisHost, port: App.RedisPort);
        }

        /// <summary>
        /// Gets the read connection and opens the connection
        /// to the redis server.
        /// </summary>
        /// <returns>An opened redis connection ready to
        /// accept a read.</returns>
        /// <exception cref="System.Exception"></exception>
        public RedisConnection GetReadConnection()
        {
            lock (syncReadConnectionLock)
            {
                // no connection
                if (readConnection == null)
                    readConnection = getNewConnection();

                // already opening
                if (readConnection.State == RedisConnectionBase.ConnectionState.Opening)
                    return readConnection;

                // in the middle of closing
                if (readConnection.State == RedisConnectionBase.ConnectionState.Closing ||
                    readConnection.State == RedisConnectionBase.ConnectionState.Closed)
                {
                    try
                    {
                        readConnection = getNewConnection();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(RedisConnectionFailed, ex);
                    }
                }
                // just created, but not open
                if (readConnection.State == RedisConnectionBase.ConnectionState.Shiny)
                {
                    try
                    {
                        var openAsync = readConnection.Open();
                        readConnection.Wait(openAsync);
                    }
                    catch (SocketException ex)
                    {
                        throw new Exception(RedisConnectionFailed, ex);
                    }
                }

                return readConnection;
            }
        }

        /// <summary>
        /// Gets either the current or a new connection based on
        /// the status of the connection. DO NOT WRAP IN A USING STATEMENT.
        /// </summary>
        /// <returns>A booksleve connection to redis that is thread-safe
        /// and universal.</returns>
        public RedisConnection GetWriteConnection()
        {
            lock (syncConnectionLock)
            {
                // no connection
                if (connection == null)
                    connection = getNewConnection();

                // already opening
                if (connection.State == RedisConnectionBase.ConnectionState.Opening)
                    return connection;

                // in the middle of closing
                if (connection.State == RedisConnectionBase.ConnectionState.Closing || 
                    connection.State == RedisConnectionBase.ConnectionState.Closed)
                {
                    try
                    {
                        connection = getNewConnection();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(RedisConnectionFailed, ex);
                    }
                }
                // just created, but not open
                if (connection.State == RedisConnectionBase.ConnectionState.Shiny)
                {
                    try
                    {
                        var openAsync = connection.Open();
                        connection.Wait(openAsync);
                    }
                    catch (SocketException ex)
                    {
                        throw new Exception(RedisConnectionFailed, ex);
                    }
                }

                return connection;
            }
        }

        /// <summary>
        /// Gets the current instantiation from the singleton
        /// implementation.
        /// </summary>
        public static ConnectionGateway Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new ConnectionGateway();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
