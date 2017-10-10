using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerce.Tables.Utility.System;
using ECommerceWeb.Common;
using Volume.Toolkit.Paths;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Shop
{
	public class ShopViewModel
	{

		#region Members

		private int             id              = Constants.DEFAULT_VALUE_INT;
		private string          name            = String.Empty;
		private string          description     = String.Empty;
		private decimal         price           = Constants.DEFAULT_VALUE_DECIMAL;
		private string          imageSrc        = String.Empty;
		private string          category        = String.Empty;
		private int             categoryID      = Constants.DEFAULT_VALUE_INT;
		private bool            status          = Constants.DEFAULT_VALUE_BOOL;
		private int             quantity        = Constants.DEFAULT_VALUE_INT;

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
			get { return this.CategoryID; }
			set { this.categoryID = value; }
		}

		[Display(Name = "Status")]
		public bool Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		[Required]
		[Range(1, int.MaxValue)]
		[Display(Name = "Quantity")]
		public int Quantity
		{
			get { return this.quantity; }
			set { this.quantity = value; }
		}

		#endregion

		#region Constructors

		public ShopViewModel() { }

		public ShopViewModel(int productID)
		{
			ETC.Product         product         = ETC.Product.ExecuteCreate(productID);

			if (product != null)
			{
				this.id                         = product.ID;
				this.name                       = product.Name;
				this.description                = product.Description;
				this.price                      = product.Price;
				this.imageSrc                   = PathUtility.CombineUrls(Config.StorageUrlProduct, product.ID.ToString(), product.ImageName);
				this.category                   = product.ExecuteCreateCategoryByCategoryID().Name;
				this.categoryID                 = product.CategoryID;
				this.status                     = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
				this.quantity                   = 1;
			}
		}

		public ShopViewModel(ETC.Product product)
		{
			if (product != null)
			{
				this.id                         = product.ID;
				this.name                       = product.Name;
				this.description                = product.Description;
				this.price                      = product.Price;
				this.imageSrc                   = PathUtility.CombineUrls(Config.StorageUrlProduct, product.ID.ToString(), product.ImageName);
				this.category                   = product.ExecuteCreateCategoryByCategoryID().Name;
				this.categoryID                 = product.CategoryID;
				this.status                     = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
				this.quantity                   = 1;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets ProductViewModel list by Category, leave parameter empty to get products from all categories
		/// Return null if no products
		/// </summary>
		/// <param name="categoryID"></param>
		/// <returns></returns>
		public static List<ShopViewModel> List(int categoryID)
		{
			List<ShopViewModel>				result          = null;

			List<ETC.Product>               products        = (categoryID == 0) ? ETC.Product.List() : ETC.Product.ListByCategoryID(categoryID);

			if (products.Count > 0)
			{
				result                                      = new List<ShopViewModel>();

				foreach (ETC.Product product in products)
				{
					if (product.Status == ETC.Product.STATUS_ACTIVE)
					{
						result.Add(new ShopViewModel(product));
					}
				}

				result.Shuffle();
			}

			return result;
		}

		public static bool AddToCart(int? productID, int quantity)
		{
			bool                    result                          = false;

			if (productID.HasValue)
			{
				CheckPendingOrders();

				ShopViewModel           model                       = new ShopViewModel(productID.Value);
				ETC.OrderItem           orderItem                   = CheckPendingOrderItems(model.ID);

				if (orderItem != null)
				{
					int                 newQuantity                 = orderItem.Quantity + quantity;

					orderItem.Update(newQuantity, model.Price, (model.Price * newQuantity));
					UpdateTotalAmountInOrder(Common.Session.CurrentOrderID ?? default(int));

					result                                          = true;
				}
				else
				{
					orderItem                                       = ETC.OrderItem.ExecuteCreate(
																		Common.Session.CurrentOrderID ?? default(int),
																		model.ID,
																		quantity,
																		model.Price,
																		(model.Price * quantity));
					orderItem.Insert();
					UpdateTotalAmountInOrder(Common.Session.CurrentOrderID ?? default(int));

					result                                          = true;
				}
			}

			return result;
		}

		public static bool RemoveFromCart(int? orderItemID)
		{
			CheckPendingOrders();

			bool                        result              = false;

			if (orderItemID.HasValue && Common.Session.CurrentOrderID.HasValue)
			{
				ETC.OrderItem           item                = ETC.OrderItem.ExecuteCreate(orderItemID.Value);
				item.Delete();

				UpdateTotalAmountInOrder(Common.Session.CurrentOrderID.Value);

				result                                      = true;
			}

			return result;
		}

		private static ETC.OrderItem CheckPendingOrderItems(int ProductID)
		{
			ETC.OrderItem               result                      = null;

			if (Common.Session.CurrentOrderID.HasValue)
			{
				List<ETC.OrderItem>         list                    = ETC.OrderItem.ListByOrderID(Common.Session.CurrentOrderID.Value);

				foreach (ETC.OrderItem item in list)
				{
					if (item.ProductID == ProductID)
					{
						result                                      = item;
						break;
					}
				}
			}

			return result;
		}

		private static void CheckPendingOrders()
		{
			List<ETC.Order>                 orders                      = ETC.Order.ListByAccountID(Common.Session.Account.ID);
			ETC.Order                       order                       = null;

			foreach (ETC.Order item in orders)
			{
				if (item.Status == ETC.Order.STATUS_PENDING)
				{
					order                                               = item;
					break;
				}
			}

			if (order == null)
			{
				ETC.Order                   newOrder                    = ETC.Order.ExecuteCreate(Common.Session.Account.ID, ETC.Order.STATUS_PENDING, ETC.Order.PAYMENT_METHOD_DEFAULT, 0.00m);
				newOrder.Insert();
				order                                                   = newOrder;
			}

			Common.Session.CurrentOrderID                               = order.ID;
		}

		public static void UpdateTotalAmountInOrder(int OrderID)
		{
			ETC.Order                   order               = ETC.Order.ExecuteCreate(OrderID);
			List<ETC.OrderItem>         list                = ETC.OrderItem.ListByOrderID(OrderID);
			decimal                     total               = 0.00m;

			foreach (ETC.OrderItem item in list)
			{
				total                                       += item.Subtotal;
			}

			order.Update(DateTime.Now, order.Status, order.PaymentMethod, total);
		}

		#endregion

	}
}