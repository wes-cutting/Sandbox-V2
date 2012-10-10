using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class OrdersImporter : IRentlerImporter
	{
		public void Import()
		{
			var newUsers = new List<New.User>();
			using (var context = new New.RentlerEntities())
				newUsers = context.Users.ToList();

			var usersKeyMap = new Dictionary<Guid, int>();
			using (var newContext = new New.RentlerEntities())
				usersKeyMap = newContext.Users
					.Select(u => new { Key = u.ReferenceId, Value = u.UserId })
					.ToDictionary(x => x.Key, y => y.Value);


			var oldOrders = new List<Old.Order>();
			using (var context = new Old.RentlerNewEntities())
				oldOrders = context.Orders.Include("OrderItems").Include("OrderItems.Product").ToList();

			var newOrders = new List<New.Order>();

			foreach (var item in oldOrders)
			{
				newOrders.Add(new New.Order
				{
					CreateDate = item.PurchaseDate,
					CreatedBy = "importer",
					OrderId = (int)item.OrderId,
					OrderStatusCode = New.OrderStatusCodeConverter.Convert(item.Status),
					OrderTotal = item.OrderItems.Sum(o => o.Quantity * (o.ProductId == 2 ? 0.99m : 9.95m)),
					UserCreditCardId = item.UserCreditCardId,
					UserId = usersKeyMap[item.UserId]
				});
			}

			//save orders
			using (var context = new New.RentlerEntities())
				context.BulkInsert(newOrders, true);

			var newOrderItems = new List<New.OrderItem>();

			foreach (var item in oldOrders)
			{
				foreach (var o in item.OrderItems)
				{
					newOrderItems.Add(new New.OrderItem
					{
						OrderId = (int)o.OrderId,
						OrderItemId = (int)o.OrderItemId,
						Price = o.Product.Price,
						ProductId = New.ProductsConverter.Convert(o.ProductId),
						ProductDescription = New.ProductsConverter.Convert(o.ProductId) + " imported",
						ProductOption = New.ProductsConverter.Convert(o.ProductId) + " imported",
						Quantity = 1
					});
				}
			}

			using (var context = new New.RentlerEntities())
				context.BulkInsert(newOrderItems, true);
		}
	}
}
