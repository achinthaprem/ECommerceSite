using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Mvc;
using ECommerce.Tables.Content;

namespace ECommerceWeb.Common
{
	public static class Lists
	{

		#region Enum

		/// <summary>
		/// Represents a type of selector for a list.
		/// </summary>
		public enum SelectorType
		{
			/// <summary>
			/// No selector.
			/// </summary>
			None,
			/// <summary>
			/// With a 'select' item.
			/// </summary>
			WithSelect,
			/// <summary>
			/// With an 'all' item.
			/// </summary>
			WithAll
		}

		#endregion

		#region Constants

		private const string            KEY_VALUE_PAIR_KEY              = "Key";
		private const string            KEY_VALUE_PAIR_VALUE            = "Value";

		private const string            DEFAULT_TEXT_SELECT             = "Select";
		private const string            DEFAULT_TEXT_ALL				= "All";

		#endregion

		#region ListMethods

		public static SelectList ListProducts(List<Product> list, SelectorType selector, int? selectedValue)
		{
			SelectList									result              = null;

			List<KeyValuePair<int?, string>>            keyValueList        = new List<KeyValuePair<int?, string>>();

			foreach (Product item in list)
			{
				keyValueList.Add(new KeyValuePair<int?, string>(item.ID, item.Name));
			}

			result                                                          = GetSelectListObject(keyValueList, Constants.DEFAULT_VALUE_INT, selector);

			return result;
		}

		public static SelectList ListCategories(List<Category> list, SelectorType selector, int? selectedValue)
		{
			SelectList									result              = null;

			List<KeyValuePair<int?, string>>            keyValueList        = new List<KeyValuePair<int?, string>>();

			foreach (Category item in list)
			{
				keyValueList.Add(new KeyValuePair<int?, string>(item.ID, item.Name));
			}

			result                                                          = GetSelectListObject(keyValueList, selectedValue, selector);

			return result;
		}

		#endregion

		#region Utility Methods

		/// <summary>
		/// Shuffle the items in the List randomly
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		public static void Shuffle<T>(this IList<T> list)
		{
			RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
			int n = list.Count;
			while (n > 1)
			{
				byte[] box = new byte[1];
				do provider.GetBytes(box);
				while (!(box[0] < n * (Byte.MaxValue / n)));
				int k = (box[0] % n);
				n--;
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

		private static SelectList GetSelectListObject(List<KeyValuePair<int?, string>> list, int? selectedValue, SelectorType selector)
		{
			list                                                        = AddDefaultSelected(list, selector);

			SelectList                  result                          = null;

			if (!selectedValue.HasValue)
			{
				result													= new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE);
			}
			else
			{
				result                                                  = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE, selectedValue.Value);
			}

			return result;
		}

		private static SelectList GetSelectListObject(List<KeyValuePair<int, string>> list, int selectedValue, SelectorType selector)
		{
			list                                                        = AddDefaultSelected(list, selector);

			SelectList                  result                          = null;

			if (selectedValue == Constants.DEFAULT_VALUE_INT)
			{
				result                                                  = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE);
			}
			else
			{
				result                                                  = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE, selectedValue);
			}

			return result;
		}

		private static List<KeyValuePair<int?, string>> AddDefaultSelected(List<KeyValuePair<int?, string>> list, SelectorType selector)
		{
			switch (selector)
			{
				case SelectorType.WithSelect:
					list.Insert(0, new KeyValuePair<int?, string>(null, DEFAULT_TEXT_SELECT));
					break;
				case SelectorType.WithAll:
					list.Insert(0, new KeyValuePair<int?, string>(null, DEFAULT_TEXT_ALL));
					break;
			}

			return list;
		}

		private static List<KeyValuePair<int, string>> AddDefaultSelected(List<KeyValuePair<int, string>> list, SelectorType selector)
		{
			switch (selector)
			{
				case SelectorType.WithSelect:
					list.Insert(0, new KeyValuePair<int, string>(Constants.DEFAULT_VALUE_INT, DEFAULT_TEXT_SELECT));
					break;
				case SelectorType.WithAll:
					list.Insert(0, new KeyValuePair<int, string>(Constants.DEFAULT_VALUE_INT, DEFAULT_TEXT_ALL));
					break;
			}

			return list;
		}

		#endregion

	}
}