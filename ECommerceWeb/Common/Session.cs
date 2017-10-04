using ECommerce.Tables.Active.HR;
using ECommerce.Tables.Content;
using System.Collections.Generic;
using System.Web;

namespace ECommerceWeb.Common
{
	public static class Session
	{
		/// <summary>
		/// Current logged in User
		/// </summary>
		public static Account Account
		{
			get
			{
				return HttpContext.Current.Session[Constants.SESSION_CURRENT_ACCOUNT] as Account;
			}
			set
			{
				HttpContext.Current.Session[Constants.SESSION_CURRENT_ACCOUNT]              = value;
			}
		}

		/// <summary>
		/// If any user is logged in
		/// </summary>
		public static bool Authorized
		{
			get
			{
				bool            result              = false;

				if (Account != null)
				{
					result                          = true;
				}

				return result;
			}
		}

		/// <summary>
		/// If admin user is logged in
		/// </summary>
		public static bool IsAdmin
		{
			get
			{
				bool            result              = false;

				if (Authorized && (Account.Role == (int)Account.RoleCode.Admin))
				{
					result                          = true;
				}

				return result;
			}
		}

		/// <summary>
		/// Current Order ID
		/// </summary>
		public static int? CurrentOrderID
		{
			get
			{
				return HttpContext.Current.Session[Constants.SESSION_CURRENT_ORDER_ID] as int?;
			}
			set
			{
				HttpContext.Current.Session[Constants.SESSION_CURRENT_ORDER_ID]         = value;
			}
		}

		/// <summary>
		/// Count of Pending OrderItems (Total Quantity)
		/// </summary>
		public static int? PendingOrderItems
		{
			get
			{
				return HttpContext.Current.Session[Constants.SESSION_PENDING_ORDER_ITEMS] as int?;
			}
			set
			{
				HttpContext.Current.Session[Constants.SESSION_PENDING_ORDER_ITEMS]      = value;
			}
		}

		/// <summary>
		/// Current User's Full name (First Name + Last Name)
		/// </summary>
		public static string FullName
		{
			get
			{
				string      result                  = null;
				if (Authorized)
				{
					Account         account         = Account;
					result                          = account.FirstName + " " + account.LastName;
				}
				return result;
			}
		}

		/// <summary>
		/// Starts the Session
		/// </summary>
		/// <param name="account">Logged in User</param>
		public static void Start(Account account)
		{
			Session.Account							= account;
			CountItemsInCart();
		}

		/// <summary>
		/// End the Current Session
		/// </summary>
		public static void Destroy()
		{
			HttpContext.Current.Session.Contents.RemoveAll();
			HttpContext.Current.Session.Abandon();
		}

		/// <summary>
		/// Count Total Quantity in the Cart and Update PendingOrderItems
		/// </summary>
		public static void CountItemsInCart()
		{
			List<Order>					orders                      = Order.ListByAccountID(Account.ID);
			Order						order                       = null;

			foreach (Order _order in orders) // For all the Orders from Current User
			{
				if (_order.Status == Order.STATUS_PENDING) // If any Pending Order
				{
					order											= _order;
					break;
				}
			}

			int							count                       = 0;

			if (order != null) // If there is an Pending Order
			{
				List<OrderItem>         items                       = OrderItem.ListByOrderID(order.ID);

				foreach (OrderItem item in items) // For all the OrderItems in the Pending Order
				{
					count                                           += item.Quantity;
				}
			}

			PendingOrderItems										= count;
		}
	}
}