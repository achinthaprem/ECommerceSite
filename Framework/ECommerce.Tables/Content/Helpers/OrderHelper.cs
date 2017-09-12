using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tables.Content.Helpers
{
	public class OrderHelper
	{
		public OrderHelper()
		{
		}

		public Task<List<Order>> GetOrdersAsync()
		{
			return Task.Run(() =>
			{
				return Order.List();
			});
		}

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

		public Task<bool> CreateOrderItemAsync(
			int OrderID,
			int ProductID,
			int Quantity,
			decimal UnitCost,
			decimal Subtotal)
		{
			return Task.Run(() =>
			{
				bool                result              = false;
				OrderItem           orderItem           = OrderItem.ExecuteCreate(OrderID, ProductID, Quantity, UnitCost, Subtotal);
				orderItem.Insert();
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

		public Task<bool> UpdateOrderItemAsync(
			int ID,
			int Quantity,
			decimal UnitCost,
			decimal Subtotal)
		{
			return Task.Run(() =>
			{
				bool                result              = false;
				OrderItem           orderItem           = OrderItem.ExecuteCreate(ID);

				if (orderItem != null)
				{
					orderItem.Update(Quantity, UnitCost, Subtotal);

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

		public Task<bool> DeleteOrderItemAsync(int ID)
		{
			return Task.Run(() =>
			{
				OrderItem           orderItem           = OrderItem.ExecuteCreate(ID);
				orderItem.Delete();

				orderItem                               = OrderItem.ExecuteCreate(ID);

				return (orderItem == null) ? true : false;
			});
		}

		public async Task UpdateTotalOfOrder(int OrderID)
		{
			// TODO: Update total amount of Order when any change happen to Order Item
		}
	}
}
