using ECommerceWeb.Common;
using ECommerceWeb.Models.Cart;
using ECommerceWeb.Models.Shop;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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
		public async Task<ActionResult> Checkout(OrderViewModel model)
		{
			if (model != null)
			{
				if (await model.CheckOut())
				{
					Common.Session.CountItemsInCart();
					TempData["alert-success"]                       = "Order placed successfully!";

					// TODO: Create new shipping info record for the order
				}
				else
				{
					Common.Session.CountItemsInCart();
					TempData["alert-fail"]                          = "Error occured while placing the order!";
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
		public async Task<ActionResult> Add(int? ProductID, int? Quantity)
		{
			bool                    completed                           = await ProductViewModel.AddToCart(ProductID, Quantity ?? 1);
			// TODO: Use Ajax
			if (completed)
			{
				TempData["alert-success"]                               = "Item(s) added to the cart successfully!";
			}
			else
			{
				TempData["alert-fail"]                                  = "Failed to add Item(s) to the cart!";
			}

			Common.Session.CountItemsInCart();

			return Redirect(Request.UrlReferrer.ToString());
		}

		#endregion

		#region Remove Item from Cart

		[HttpPost]
		public async Task<ActionResult> RemoveItem(int? OrderItemID)
		{
			if (await ProductViewModel.RemoveFromCart(OrderItemID))
			{
				TempData["alert-success"]               = "Item(s) removed from the cart successfully!";
			}
			else
			{
				TempData["alert-fail"]                  = "Failed to remove Item(s) from the cart!";
			}

			Common.Session.CountItemsInCart();

			return Redirect(Request.UrlReferrer.ToString());
		}

		#endregion

		#region Remove Order

		public async Task<ActionResult> RemoveOrder(int? orderID)
		{
			if (await OrderViewModel.RemoveOrder(orderID))
			{
				TempData["alert-success"]               = "The Order cancelled successfully!";
			}
			else
			{
				TempData["alert-fail"]					= "Failed to cancel the order!";
			}

			Common.Session.CountItemsInCart();

			return Redirect(Request.UrlReferrer.ToString());
		}

		#endregion
		
	}
}