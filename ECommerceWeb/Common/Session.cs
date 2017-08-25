using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerceWeb.Models;

namespace ECommerceWeb.Common
{
	public static class Session
	{
		public static int UserID
		{
			get
			{
				int			result				= -1;
				if (Authorized)
				{
					result                      = int.Parse(HttpContext.Current.Session[Constants.USER_ID].ToString());
				}
				return result;
			}
		}

		public static string UserName
		{
			get
			{
				string      result              = null;
				if (Authorized)
				{
					result                      = HttpContext.Current.Session[Constants.USER_NAME].ToString();
				}
				return result;
			}
		}

		public static int UserRole
		{
			get
			{
				int         result              = -1;
				if (Authorized)
				{
					result                      = int.Parse(HttpContext.Current.Session[Constants.USER_ID].ToString());
				}
				return result;
			}
		}

		public static bool Authorized
		{
			get
			{
				bool            result              = false;

				if (HttpContext.Current.Session[Constants.USER_ID] != null)
				{
					result                          = true;
				}

				return result;
			}
		}

		public static void Start(Account obj)
		{
			HttpContext.Current.Session[Constants.USER_ID]          = obj.ID.ToString();
			HttpContext.Current.Session[Constants.USER_NAME]        = obj.first_name.ToString() + " " + obj.last_name.ToString();
			HttpContext.Current.Session[Constants.USER_ROLE]        = obj.ID.ToString();
		}

		public static void Destroy()
		{
			HttpContext.Current.Session.Contents.RemoveAll();
			HttpContext.Current.Session.Abandon();
		}
	}
}