using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceWeb.Common
{
	public static class Func
	{
		public static string Currencyfy(decimal value)
		{
			return String.Format("Rs. {0:n}", value);
		}
	}
}