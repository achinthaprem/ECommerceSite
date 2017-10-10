using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ECommerce.Tables.Utility.System;
using ECommerceWeb.Common;
using Volume.Toolkit.Paths;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Home
{
	public class SalesViewModel
	{

		#region Members

		private int             id              = Constants.DEFAULT_VALUE_INT;
		private string          name            = String.Empty;
		private string          description     = String.Empty;
		private decimal         price           = Constants.DEFAULT_VALUE_DECIMAL;
		private string          imageName       = String.Empty;
		private string          imageSrc        = String.Empty;
		private string          category        = String.Empty;
		private int             categoryID      = Constants.DEFAULT_VALUE_INT;
		private bool            status          = Constants.DEFAULT_VALUE_BOOL;
		private int             sellings        = Constants.DEFAULT_VALUE_INT;

		#endregion

		#region Properties

		[Display(Name = "ID")]
		public int ID
		{
			get { return this.id; }
			set { this.id = value; }
		}

		[Display(Name = "Name")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		[Display(Name = "Description")]
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Display(Name = "Price")]
		public decimal Price
		{
			get { return this.price; }
			set { this.price = value; }
		}

		[Display(Name = "Image")]
		public string ImageSrc
		{
			get { return this.imageSrc; }
			set { this.imageSrc = value; }
		}

		[Display(Name = "Category")]
		public string Category
		{
			get { return this.category; }
			set { this.category = value; }
		}

		[Display(Name = "Category ID")]
		public int CategoryID
		{
			get { return this.categoryID; }
			set { this.categoryID = value; }
		}

		[Display(Name = "Status")]
		public bool Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		[Display(Name = "Sellings")]
		public int Sellings
		{
			get { return this.sellings; }
			set { this.sellings = value; }
		}

		public string ImageName
		{
			get { return this.imageName; }
			set { this.imageName = value; }
		}

		#endregion

		#region Constructors

		public SalesViewModel() { }

		private SalesViewModel(ETC.Product product)
		{
			this.id                                             = product.ID;
			this.name                                           = product.Name;
			this.description                                    = product.Description;
			this.price                                          = product.Price;
			this.imageName                                      = product.ImageName;
			this.imageSrc                                       = PathUtility.CombineUrls(Config.StorageUrlProduct, product.ID.ToString(), product.ImageName);
			this.category                                       = product.ExecuteCreateCategoryByCategoryID().Name;
			this.categoryID                                     = product.CategoryID;
			this.status                                         = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
			this.sellings                                       = CountSells(product);
		}

		#endregion

		#region Execute Create

		public static SalesViewModel ExecuteCreate(ETC.Product product)
		{
			SalesViewModel          result          = null;

			if (product != null)
			{
				result                              = new SalesViewModel(product);
			}

			return result;
		}

		#endregion

		#region Method

		public static List<SalesViewModel> List(int? FilterBy, bool IncludeNotSold)
		{
			List<SalesViewModel>        result                  = new List<SalesViewModel>();
			List<ETC.Product>           productList             = null;

			if (!FilterBy.HasValue || FilterBy == Constants.DEFAULT_VALUE_INT)
			{
				productList                                     = ETC.Product.List();
			}
			else
			{
				productList                                     = ETC.Product.ListByCategoryID(FilterBy.Value);
			}

			foreach (ETC.Product item in productList)
			{
				SalesViewModel          model                   = ExecuteCreate(item);

				if (model.Sellings == 0 && !IncludeNotSold) { continue; }

				result.Add(model);
			}

			return result.OrderByDescending(o => o.Sellings).ToList();
		}

		#endregion

		#region Utility Methods

		private int CountSells(ETC.Product product)
		{
			List<ETC.OrderItem>         orderItemList           = ETC.OrderItem.ListByProductID(product.ID);
			int                         sellings                = 0;

			foreach (ETC.OrderItem item in orderItemList)
			{
				if (item.ExecuteCreateOrderByOrderID().Status == ETC.Order.STATUS_COMPLETED)
				{
					sellings                                    += item.Quantity;
				}
			}

			return sellings;
		}

		#endregion

	}
}