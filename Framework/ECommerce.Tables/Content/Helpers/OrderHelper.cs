using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Tables.Content.Helpers
{
	public class OrderHelper
	{
		#region Constructors

		public OrderHelper() { }

		#endregion

		#region Order Helper Public Methods

		/// <summary>
		/// Get Order List
		/// </summary>
		/// <returns></returns>
		public Task<List<Order>> GetOrdersAsync()
		{
			return Task.Run(() =>
			{
				return Order.List();
			});
		}

		/// <summary>
		/// Get Order by ID
		/// </summary>
		/// <param name="ID">ID of the Order</param>
		/// <returns></returns>
		public Task<Order> GetOrderAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Order.ExecuteCreate(ID);
			});
		}

		/// <summary>
		/// Get Order List by Status
		/// </summary>
		/// <param name="Status">Status of Orders</param>
		/// <returns></returns>
		public Task<List<Order>> GetOrdersByStatusAsync(int Status)
		{
			return Task.Run(() =>
			{
				return Order.ListByStatus(Status);
			});
		}

		/// <summary>
		/// Get Order List by Account ID
		/// </summary>
		/// <param name="AccountID">Account ID of Orders</param>
		/// <returns></returns>
		public Task<List<Order>> GetOrdersByAccountAsync(int AccountID)
		{
			return Task.Run(() =>
			{
				return Order.ListByAccountID(AccountID);
			});
		}

		/// <summary>
		/// Create new Order
		/// </summary>
		/// <param name="AccountID"></param>
		/// <param name="Status"></param>
		/// <param name="PaymentMethod"></param>
		/// <param name="TotalAmount"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Update existing Order
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Status"></param>
		/// <param name="PaymentMethod"></param>
		/// <param name="TotalAmount"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Delete an Order by ID
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Get OrderItem List
		/// </summary>
		/// <returns></returns>
		public Task<List<OrderItem>> GetOrderItemsAsync()
		{
			return Task.Run(() =>
			{
				return OrderItem.List();
			});
		}

		/// <summary>
		/// Get OrderItem by ID
		/// </summary>
		/// <param name="OrderItemID">OrderItem ID</param>
		/// <returns></returns>
		public Task<OrderItem> GetOrderItemAsync(int OrderItemID)
		{
			return Task.Run(() =>
			{
				return OrderItem.ExecuteCreate(OrderItemID);
			});
		}

		/// <summary>
		/// Get OrderItem List by Order ID
		/// </summary>
		/// <param name="OrderID">Order ID</param>
		/// <returns></returns>
		public Task<List<OrderItem>> GetOrderItemsByOrderIDAsync(int OrderID)
		{
			return Task.Run(() =>
			{
				return OrderItem.ListByOrderID(OrderID);
			});
		}

		/// <summary>
		/// Get OrderItem by ProductID
		/// </summary>
		/// <param name="ProductID">Product ID</param>
		/// <returns></returns>
		public Task<List<OrderItem>> GetOrderItemsByProductIDAsync(int ProductID)
		{
			return Task.Run(() =>
			{
				return OrderItem.ListByProductID(ProductID);
			});
		}

		/// <summary>
		/// Create an OrderItem
		/// </summary>
		/// <param name="OrderID"></param>
		/// <param name="ProductID"></param>
		/// <param name="Quantity"></param>
		/// <param name="UnitCost"></param>
		/// <param name="Subtotal"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Update an Existing OrderItem
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Quantity"></param>
		/// <param name="UnitCost"></param>
		/// <param name="Subtotal"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Delete an OrderItem
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Updates the Total Amount by Order ID
		/// </summary>
		/// <param name="OrderID">Order ID</param>
		/// <returns></returns>
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
