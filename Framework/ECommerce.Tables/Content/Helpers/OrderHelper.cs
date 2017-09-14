using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Tables.Content.Helpers
{
	public class OrderHelper
	{
		#region Constructors

		public OrderHelper() { }

		#endregion

		#region Order helper methods

		public Task<List<Order>> GetOrdersAsync()
		{
			return Task.Run(() =>
			{
				return Order.List();
			});
		}

		public Task<Order> GetOrderAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Order.ExecuteCreate(ID);
			});
		}

		public Task<List<Order>> GetOrdersByStatusAsync(int Status)
		{
			return Task.Run(() =>
			{
				return Order.ListByStatus(Status);
			});
		}

		public Task<List<Order>> GetOrdersByAccountAsync(int AccountID)
		{
			return Task.Run(() =>
			{
				return Order.ListByAccountID(AccountID);
			});
		}

		public Task<bool> CreateOrderAsync(
			int AccountID,
			int Status,
			int PaymentMethod,
			decimal TotalAmount)
		{
			return Task.Run(() =>
			{
				bool                result              = false;
				Order               order               = Order.ExecuteCreate(AccountID, Status, PaymentMethod, TotalAmount);
				order.Insert();
				result                                  = true;
				return result;
			});
		}

		public Task<bool> UpdateOrderAsync(
			int ID,
			int Status,
			int PaymentMethod,
			decimal TotalAmount)
		{
			return Task.Run(() =>
			{
				bool                result              = false;
				Order               order               = Order.ExecuteCreate(ID);

				if (order != null)
				{
					order.Update(Status, PaymentMethod, TotalAmount);

					result                              = true;
				}

				return result;
			});
		}

		public Task<bool> DeleteOrderAsync(int ID)
		{
			return Task.Run(() =>
			{
				Order               order               = Order.ExecuteCreate(ID);
				order.Delete();

				order                                   = Order.ExecuteCreate(ID);

				return (order == null) ? true : false;
			});
		}

		#endregion

		#region OrderItem helper methods

		public Task<List<OrderItem>> GetOrderItemsAsync()
		{
			return Task.Run(() =>
			{
				return OrderItem.List();
			});
		}

		public Task<OrderItem> GetOrderItemAsync(int OrderItemID)
		{
			return Task.Run(() =>
			{
				return OrderItem.ExecuteCreate(OrderItemID);
			});
		}

		public Task<List<OrderItem>> GetOrderItemsByOrderIDAsync(int OrderID)
		{
			return Task.Run(() =>
			{
				return OrderItem.ListByOrderID(OrderID);
			});
		}

		public Task<List<OrderItem>> GetOrderItemsByProductIDAsync(int ProductID)
		{
			return Task.Run(() =>
			{
				return OrderItem.ListByProductID(ProductID);
			});
		}

		public Task<bool> CreateOrderItemAsync(
			int OrderID,
			int ProductID,
			int Quantity,
			decimal UnitCost,
			decimal Subtotal)
		{
			return Task.Run(async () =>
			{
				bool                result              = false;
				OrderItem           orderItem           = OrderItem.ExecuteCreate(OrderID, ProductID, Quantity, UnitCost, Subtotal);
				orderItem.Insert();

				await UpdateTotalOfOrder(orderItem.OrderID);

				result                                  = true;
				return result;
			});
		}

		public Task<bool> UpdateOrderItemAsync(
			int ID,
			int Quantity,
			decimal UnitCost,
			decimal Subtotal)
		{
			return Task.Run(async () =>
			{
				bool                result              = false;
				OrderItem           orderItem           = OrderItem.ExecuteCreate(ID);

				if (orderItem != null)
				{
					orderItem.Update(Quantity, UnitCost, Subtotal);

					result                              = true;

					await UpdateTotalOfOrder(orderItem.OrderID);
				}

				return result;
			});
		}

		public Task<bool> DeleteOrderItemAsync(int ID)
		{
			return Task.Run(async () =>
			{
				OrderItem           orderItem           = OrderItem.ExecuteCreate(ID);
				orderItem.Delete();

				await UpdateTotalOfOrder(orderItem.OrderID);

				orderItem                               = OrderItem.ExecuteCreate(ID);

				return (orderItem == null) ? true : false;
			});
		}

		#endregion

		#region Common helper methods

		public async Task UpdateTotalOfOrder(int OrderID)
		{
			Order                   order               = await GetOrderAsync(OrderID);
			List<OrderItem>         list                = await GetOrderItemsByOrderIDAsync(OrderID);
			decimal                 total               = 0.00m;

			foreach (OrderItem item in list)
			{
				total                                   += item.Subtotal;
			}

			order.Update(order.Status, order.PaymentMethod, total);
		}

		#endregion
	}
}
