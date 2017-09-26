using ECommerceWeb.Common;
using ECommerceWeb.Models.Cart;
using ECommerceWeb.Models.Shop;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	public class CartController : Controller
	{

		#region Cart View

		// GET: Cart/View
		/// <summary>
		/// Cart view page
		/// </summary>
		/// <returns></returns>
		new public async Task<ActionResult> View()
		{
			ActionResult                            result                              = null;

			if (Common.Session.Authorized) // If logged in
			{
				result                                                                  = View("View", new OrderViewModel());
			}
			else
			{
				result                                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Checkout

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Checkout(OrderViewModel model)
		{
			ActionResult                            result                              = null;

			if (Common.Session.Authorized)
			{
				if (model != null)
				{
					if (await model.CheckOut())
					{
						Common.Session.CountItemsInCart();
						TempData["alert-success"]                                       = "Order placed successfully!";

						// TODO: Create new shipping info record for the order
					}
					else
					{
						Common.Session.CountItemsInCart();
						TempData["alert-fail"]                                          = "Error occured while placing the order!";
					}

					result                                                              = Redirect(Request.UrlReferrer.ToString());
				}
				else
				{
					result                                                              = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Add to Cart

		// GET: Cart/Add
		[HttpPost]
		public async Task<ActionResult> Add(int? ProductID, int? Quantity)
		{
			ActionResult                    result                          = null;

			if (Common.Session.Authorized)
			{
				bool                    completed                           = await ProductViewModel.AddToCart(ProductID, Quantity ?? 1);

				if (completed)
				{
					TempData["alert-success"]                               = "Item(s) added to the cart successfully!";

					result                                                  = Redirect(Request.UrlReferrer.ToString());
				}
				else
				{
					TempData["alert-fail"]                                  = "Failed to add Item(s) to the cart!";

					result                                                  = Redirect(Request.UrlReferrer.ToString());
				}

				Common.Session.CountItemsInCart();
			}
			else
			{
				result                                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Remove Item from Cart

		[HttpPost]
		public async Task<ActionResult> RemoveItem(int? OrderItemID)
		{
			ActionResult                        result                          = null;

			if (Common.Session.Authorized)
			{
				if (await ProductViewModel.RemoveFromCart(OrderItemID))
				{
					TempData["alert-success"]                                   = "Item(s) removed from the cart successfully!";

					result                                                      = Redirect(Request.UrlReferrer.ToString());
				}
				else
				{
					TempData["alert-fail"]                                      = "Failed to remove Item(s) from the cart!";

					result                                                      = Redirect(Request.UrlReferrer.ToString());
				}

				Common.Session.CountItemsInCart();
			}
			else
			{
				result                                                          = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Remove Order

		public async Task<ActionResult> RemoveOrder(int? orderID)
		{
			ActionResult                        result                          = null;

			if (Common.Session.Authorized)
			{
				if (await OrderViewModel.RemoveOrder(orderID))
				{
					TempData["alert-success"]                                   = "The Order cancelled successfully!";

					result                                                      = Redirect(Request.UrlReferrer.ToString());
				}
				else
				{
					TempData["alert-fail"]                                      = "Failed to cancel the order!";

					result                                                      = Redirect(Request.UrlReferrer.ToString());
				}

				Common.Session.CountItemsInCart();
			}
			else
			{
				result                                                          = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Internal Methods

		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}

		#endregion
	}
}