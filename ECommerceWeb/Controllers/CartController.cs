using System.Net;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Cart;
using ECommerceWeb.Models.Shop;

namespace ECommerceWeb.Controllers
{
	[VerifyUser]
	public class CartController : Controller
	{

		#region Cart View

		// GET: Cart/View
		/// <summary>
		/// Cart view page
		/// </summary>
		/// <returns></returns>
		new public ActionResult View()
		{
			return View("View", new OrderViewModel());
		}

		#endregion

		#region Checkout

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Checkout(OrderViewModel model)
		{
			if (model != null)
			{
				if (model.CheckOut())
				{
					Common.Session.CountItemsInCart();
					TempData[Constants.ALERT_SUCCESS]                       = "Order placed successfully!";

					// TODO: Create new shipping info record for the order
				}
				else
				{
					Common.Session.CountItemsInCart();
					TempData[Constants.ALERT_FAIL]                          = "Error occured while placing the order!";
				}

				return Redirect(Request.UrlReferrer.ToString());
			}
			else
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
		}

		#endregion

		#region Add to Cart

		// GET: Cart/Add
		[HttpPost]
		public ActionResult Add(int? ProductID, int? Quantity)
		{
			bool                    completed                   = ShopViewModel.AddToCart(ProductID, Quantity ?? 1);
			
			if (completed)
			{
				TempData[Constants.ALERT_SUCCESS]               = "Item(s) added to the cart successfully!";
			}
			else
			{
				TempData[Constants.ALERT_FAIL]                  = "Failed to add Item(s) to the cart!";
			}

			Common.Session.CountItemsInCart();

			return Redirect(Request.UrlReferrer.ToString());
		}

		#endregion

		#region Remove Item from Cart

		[HttpPost]
		public ActionResult RemoveItem(int? OrderItemID)
		{
			if (ShopViewModel.RemoveFromCart(OrderItemID))
			{
				TempData[Constants.ALERT_SUCCESS]               = "Item(s) removed from the cart successfully!";
			}
			else
			{
				TempData[Constants.ALERT_FAIL]                  = "Failed to remove Item(s) from the cart!";
			}

			Common.Session.CountItemsInCart();

			return Redirect(Request.UrlReferrer.ToString());
		}

		#endregion

		#region Remove Order

		public ActionResult RemoveOrder(int? orderID)
		{
			if (OrderViewModel.RemoveOrder(orderID))
			{
				TempData[Constants.ALERT_SUCCESS]               = "The Order cancelled successfully!";
			}
			else
			{
				TempData[Constants.ALERT_FAIL]					= "Failed to cancel the order!";
			}

			Common.Session.CountItemsInCart();

			return Redirect(Request.UrlReferrer.ToString());
		}

		#endregion
		
	}
}