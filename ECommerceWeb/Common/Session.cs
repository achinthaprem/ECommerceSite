using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Tables.Active.HR;

namespace ECommerceWeb.Common
{
	public static class Session
	{
		public static Account Account
		{
			get {
				return HttpContext.Current.Session[Constants.CURRENT_ACCOUNT] as Account;
			}
		}

		public static bool Authorized
		{
			get
			{
				bool            result              = false;

				if (HttpContext.Current.Session[Constants.CURRENT_ACCOUNT] != null)
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
				string      result					= null;
				if (Authorized)
				{
					Account         account         = HttpContext.Current.Session[Constants.CURRENT_ACCOUNT] as Account;
					result							= account.FirstName + " " + account.LastName;
				}
				return result;
			}
		}

		public static void Start(Account account)
		{
			HttpContext.Current.Session[Constants.CURRENT_ACCOUNT]			= account;
		}

		public static void Destroy()
		{
			HttpContext.Current.Session.Contents.RemoveAll();
			HttpContext.Current.Session.Abandon();
		}
	}
}