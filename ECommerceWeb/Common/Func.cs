using System;

namespace ECommerceWeb.Common
{
	public static class Func
	{
		/// <summary>
		/// Convert decimal value to a string with leading Currency format
		/// </summary>
		/// <param name="value">Price in decimal</param>
		/// <returns></returns>
		public static string Currencyfy(decimal value)
		{
			return String.Format("Rs. {0:n}", value);
		}
		
	}
}