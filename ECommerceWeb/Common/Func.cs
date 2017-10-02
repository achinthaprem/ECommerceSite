using ECommerce.Tables.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Web.Mvc;
using Volume.Toolkit.Paths;
using ECommerce.Tables.Utility.System;

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

		/// <summary>
		/// Converts List<Category> to List<SelectListItem>
		/// </summary>
		/// <param name="activeList"></param>
		/// <returns></returns>
		public static List<SelectListItem> FilterActiveCategoryList(List<Category> activeList)
		{
			List<SelectListItem>            result                      = new List<SelectListItem>();

			result.Add(new SelectListItem { Value = "0", Text = "All Categories" });

			foreach (Category category in activeList)
			{
				result.Add(new SelectListItem { Value = category.ID.ToString(), Text = category.Name });
			}

			return result;
		}

		/// <summary>
		/// Convert List<Category> to List<SelectListItem>
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static List<SelectListItem> ConvertCategoriesToSelectList(List<Category> list)
		{
			List<SelectListItem>            result                          = new List<SelectListItem>();

			foreach (Category category in list)
			{
				result.Add(new SelectListItem { Value = category.ID.ToString(), Text = category.Name });
			}

			return result;
		}

		/// <summary>
		/// Saves the Image to the given Path
		/// </summary>
		/// <param name="Image">Uploaded Image file</param>
		/// <param name="Path">Path to the file</param>
		/// <param name="FileName">Image file name</param>
		public static void SaveImage(System.Web.HttpPostedFileBase Image, string Path, string FileName)
		{
			Directory.CreateDirectory(PathUtility.CombinePaths(Config.StoragePath, Path));

			Image.SaveAs(PathUtility.CombinePaths(Config.StoragePath, Path, FileName));
		}

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
	}
}