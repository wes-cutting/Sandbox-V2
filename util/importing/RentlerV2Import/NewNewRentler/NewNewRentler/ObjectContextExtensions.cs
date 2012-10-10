using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Microsoft.Practices.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;
using System.Transactions;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Metadata.Edm;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace NewNewRentler
{
	public static class ObjectContextExtensions
	{
		/// <summary>
		/// Uses a reliable connection retry policy and transaction 
		/// scoping to add and submit a set of EntityObjects.
		/// </summary>
		/// <typeparam name="T">Must derive from <typeparamref name="EntityObject"/>.</typeparam>
		/// <param name="context">Any ObjectContext</param>
		/// <param name="objects">A set of <typeparamref name="EntityObject"/>s to save.</param>
		public static void AddObjectsAndSave<T>(this ObjectContext context, IEnumerable<T> objects)
			where T : EntityObject
		{
			if(!objects.Any())
				return;

			var policy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>
				(10, TimeSpan.FromSeconds(10));
			var tso = new TransactionOptions();
			tso.IsolationLevel = IsolationLevel.ReadCommitted;

			var name = context.GetTableName<T>();

			foreach(var item in objects)
				context.AddObject(name, item);


			policy.ExecuteAction(() =>
			{
				using(TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tso))
				{
					context.SaveChanges();

					ts.Complete();
				}
			});
		}

		public static void AddObjectsAndSaveWithIdentityInsert<T>(
			this ObjectContext context, IEnumerable<T> objects)
			where T : EntityObject
		{
			if(!objects.Any())
				return;

			Action<IEnumerable<T>> exec = (obj) =>
			{
				var policy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>
					(10, TimeSpan.FromSeconds(10));

				var name = context.GetTableName<T>();

				foreach(var item in obj)
					context.AddObject(name, item);

				policy.ExecuteAction(() =>
				{
					context.Connection.Open();
					var trans = context.Connection.BeginTransaction();

					context.ExecuteStoreCommand("SET IDENTITY_INSERT " + context.GetTableName<T>() + " ON");
					context.SaveChanges();
					context.ExecuteStoreCommand("SET IDENTITY_INSERT " + context.GetTableName<T>() + " OFF");

					trans.Commit();
					context.AcceptAllChanges();
					context.Connection.Close();
				});
			};

			context.AddObjectsAndSave(objects, doThisInstead: exec);
		}

		/// <summary>
		/// Uses a reliable connection retry policy and transaction 
		/// scoping to add and submit a set of EntityObjects. Submits items
		/// a page at a time.
		/// </summary>
		/// <typeparam name="T">Must derive from <typeparamref name="EntityObject"/>.</typeparam>
		/// <param name="context">Any ObjectContext</param>
		/// <param name="objects">A set of <typeparamref name="EntityObject"/>s to save.</param>
		/// <param name="pageSize">The number of items to submit at one time.</param>
		public static void AddObjectsAndSave<T>(
			this ObjectContext context, IEnumerable<T> objects, int pageSize = 1000, Action<IEnumerable<T>> doThisInstead = null)
			where T : EntityObject
		{
			if(!objects.Any())
				return;

			var name = context.GetTableName<T>();

			var watch = new Stopwatch();
			var times = new List<TimeSpan>();

			int page = 1;
			PaginatedList<T> items = null;
			var hasNextPage = true;

			while(hasNextPage)
			{
				//reset watch
				watch.Reset();
				watch.Start();

				//get page of items and save them
				items = new PaginatedList<T>(objects.AsQueryable(), page, pageSize);
				hasNextPage = items.HasNextPage;

				if(doThisInstead != null)
					doThisInstead(items);
				else
					context.AddObjectsAndSave(items);

				page++;

				//record time
				watch.Stop();
				times.Add(watch.Elapsed);

				Console.Write("{0}: {1} rows in {2}.",
					name,
					items.Count,
					watch.Elapsed.ToPrettyString());
				Console.WriteLine(" Page {0}/{1}, {2} complete,  ETA: {3}.",
					items.PageNumber,
					items.TotalPages,
					((double)items.PageNumber / (double)items.TotalPages).ToString("P"),
					DateTime.Now.AddSeconds(
						times.Average((ts) => ts.TotalSeconds) *
						(items.TotalPages - page)).ToLongTimeString());
			}
			Console.WriteLine("{0} done! Took {1}.",
				name,
				TimeSpan.FromSeconds(times.Sum((ts) => ts.TotalSeconds)).ToPrettyString());
		}

		public static string GetTableName<T>(this ObjectContext context) where T : EntityObject
		{
			string className = typeof(T).Name;

			var container =
			   context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);

			return (from meta in container.BaseEntitySets
					where meta.ElementType.Name == className
					select meta.Name).First();
		}


		public static void BulkInsert<T>(this ObjectContext context, List<T> data, bool keepIdentity = false) where T : EntityObject
		{
			Console.WriteLine("Inserting {0}'s, {1} rows...", context.GetTableName<T>(), data.Count);

			//set up datasets
			var ds = data.ToDataSet(context.GetTableName<T>());
			var source = ds.Tables[context.GetTableName<T>()];

			//execute bulk copy
			using(SqlConnection conn = new SqlConnection((context.Connection as EntityConnection).StoreConnection.ConnectionString))
			{
				conn.Open();

				string[] columns = source.Columns.Cast<System.Data.DataColumn>().Select(s => s.ColumnName).ToArray();


				using(SqlBulkCopy copy = keepIdentity ? new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, null) :
														new SqlBulkCopy(conn))
				{
					copy.DestinationTableName = context.GetTableName<T>();
					copy.WriteToServer(source);
				}

				conn.Close();
			}
		}
	}
}
