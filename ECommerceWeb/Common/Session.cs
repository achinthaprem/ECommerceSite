using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ECommerce.Tables.Active.HR;

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
		}

		public static void Destroy()
		{
			HttpContext.Current.Session.Contents.RemoveAll();
			HttpContext.Current.Session.Abandon();
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