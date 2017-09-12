using ECommerce.Tables.Active.HR;
using ECommerce.Tables.Content;
using ECommerce.Tables.Content.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace ECommerceWeb.Common
{
	public static class Session
	{
		public static Account Account
		{
			get
			{
				return HttpContext.Current.Session[Constants.CURRENT_ACCOUNT] as Account;
			}
		}

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

		public static int? CurrentOrderID
		{
			get
			{
				return HttpContext.Current.Session[Constants.CURRENT_ORDER_ID] as int?;
			}
			set
			{
				HttpContext.Current.Session[Constants.CURRENT_ORDER_ID]         = value;
			}
		}

		public static int? PendingOrderItems
		{
			get
			{
				return HttpContext.Current.Session[Constants.PENDING_ORDER_ITEMS] as int?;
			}
			set
			{
				HttpContext.Current.Session[Constants.PENDING_ORDER_ITEMS]      = value;
			}
		}

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

		public static void Start(Account account)
		{
			HttpContext.Current.Session[Constants.CURRENT_ACCOUNT]          = account;
			CountItemsInCart();
		}

		public static void Destroy()
		{
			HttpContext.Current.Session.Contents.RemoveAll();
			HttpContext.Current.Session.Abandon();
		}

		public static void CountItemsInCart()
		{
			OrderHelper             OrderHelper                 = new OrderHelper();
			List<Order>             orders                      = OrderHelper.GetOrdersByAccountAsync(Account.ID).Result;
			Order                   order                       = null;

			foreach (Order _order in orders)
			{
				if (_order.Status == Order.STATUS_PENDING)
				{
					order                                       = _order;
					break;
				}
			}

			List<OrderItem>         items                       = OrderHelper.GetOrderItemsByOrderIDAsync(order.ID).Result;
			int                     count                       = 0;

			foreach(OrderItem item in items)
			{
				count                                           += item.Quantity;
			}

			PendingOrderItems                                   = count;
		}

		//public static Account ValidateCookie(HttpRequestBase request)
		//{
		//	Account                     result                              = null;

		//	HttpCookie                  cookie                              = request.Cookies[Constants.USER_COOCKIE_NAME];

		//	if (cookie != null)
		//	{
		//		if (bool.Parse(cookie[Constants.USER_REMEMBER_ME].ToString()))
		//		{
		//			result                                                      = ECommerce.Tables.Active.HR.Account.ExecuteCreate(int.Parse(cookie[Constants.USER_ID].ToString()));
		//		}
		//	}

		//	return result;
		//}

		//public static HttpCookie CreateCookie(Account account, bool rememberMe)
		//{
		//	HttpCookie                  userInfo                            = new HttpCookie(Constants.USER_COOCKIE_NAME);

		//	userInfo[Constants.USER_ID]                                     = account.ID.ToString();
		//	userInfo[Constants.USER_NAME]                                   = account.FirstName + " " + account.LastName;
		//	userInfo[Constants.USER_EMAIL]                                  = account.Email;
		//	userInfo[Constants.USER_REMEMBER_ME]                            = rememberMe.ToString();

		//	userInfo.Domain = "localhost";
		//	userInfo.Expires.Add(new TimeSpan(30, 0, 0, 0));

		//	return userInfo;
		//}
	}
}