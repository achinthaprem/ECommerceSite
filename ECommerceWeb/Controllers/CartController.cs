using ECommerce.Tables.Content;
using ECommerce.Tables.Content.Helpers;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Cart;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	public class CartController : Controller
	{
		ProductHelper                       ProductHelper                   = new ProductHelper();
		OrderHelper                         OrderHelper                     = new OrderHelper();

		// GET: Cart
		new public async Task<ActionResult> View()
		{
			ActionResult                            result                              = null;

			if (Common.Session.Authorized)
			{
				OrderViewModel                      model                               = new OrderViewModel();
				await CheckPendingOrders();

				Order								order								= await OrderHelper.GetOrderAsync(Common.Session.CurrentOrderID ?? default(int));

				model.ID																= order.ID;
				model.AccountID															= order.AccountID;
				model.DateCreated                                                       = order.DateCreated;
				model.Status                                                            = order.Status;
				model.PaymentMethod                                                     = order.PaymentMethod;
				model.TotalAmount                                                       = order.TotalAmount;
				model.OrderItems                                                        = await OrderHelper.GetOrderItemsByOrderIDAsync(order.ID);

				// TODO: Complete here
				
				result                                                                  = View("View", model);
			}
			else
			{
				result                                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		public async Task<ActionResult> Add(int? ProductID, int? Quantity)
		{
			ActionResult                            result                              = null;

			if (Common.Session.Authorized)
			{
				if (ProductID != null && Quantity != null && Quantity > 0)
				{
					Product                         product                             = await ProductHelper.GetProductAsync(ProductID ?? default(int));

					if (product != null)
					{
						await CheckPendingOrders();

						if (Common.Session.CurrentOrderID != null)
						{
							bool                    completed                           = false;
							OrderItem               existingOrderItem                   = await CheckPendingOrderItems(ProductID ?? default(int));

							if (existingOrderItem != null)
							{
								int                 newQuantity                         = existingOrderItem.Quantity + (Quantity ?? default(int));

								completed                                               = await OrderHelper.UpdateOrderItemAsync(
																								existingOrderItem.ID,
																								newQuantity,
																								product.Price,
																								(product.Price * newQuantity));
							}
							else
							{
								completed                                               = await OrderHelper.CreateOrderItemAsync(
																								Common.Session.CurrentOrderID ?? default(int),
																								product.ID, Quantity ?? default(int),
																								product.Price,
																								((Quantity ?? default(int)) * product.Price));
							}

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
							result                                                      = new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);
						}
					}
					else
					{
						result                                                          = new HttpNotFoundResult();
					}
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

		public async Task<ActionResult> RemoveItem(int? OrderItemID)
		{
			ActionResult                            result                              = null;

			if (Common.Session.Authorized)
			{
				if (OrderItemID != null)
				{
					OrderItem                       orderItem                           = await OrderHelper.GetOrderItemAsync(OrderItemID ?? default(int));

					if (orderItem != null)
					{
						orderItem.Delete();

						orderItem                                                       = await OrderHelper.GetOrderItemAsync(OrderItemID ?? default(int));

						if (orderItem == null)
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
						result                                                          = new HttpNotFoundResult();
					}
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

		public async Task<ActionResult> RemoveOrder(int? OrderID)
		{
			ActionResult                            result                              = null;

			if (Common.Session.Authorized)
			{
				if (OrderID != null)
				{
					Order                           order                               = await OrderHelper.GetOrderAsync(OrderID ?? default(int));

					if (order != null)
					{
						order.Delete();

						order                                                           = await OrderHelper.GetOrderAsync(OrderID ?? default(int));

						if (order == null)
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
						result                                                          = new HttpNotFoundResult();
					}
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

		private async Task<OrderItem> CheckPendingOrderItems(int ProductID)
		{
			int                         OrderID                             = Common.Session.CurrentOrderID ?? default(int);
			List<OrderItem>             list                                = await OrderHelper.GetOrderItemsByOrderIDAsync(OrderID);
			OrderItem                   result                              = null;

			foreach (OrderItem item in list)
			{
				if (item.ProductID == ProductID)
				{
					result                                                  = item;
					break;
				}
			}

			return result;
		}

		private async Task CheckPendingOrders()
		{
			List<Order>                 orders                              = await OrderHelper.GetOrdersByAccountAsync(Common.Session.Account.ID);
			Order                       order                               = null;

			foreach (Order _order in orders)
			{
				if (_order.Status == Order.STATUS_PENDING)
				{
					order                                                   = _order;
					break;
				}
			}

			if (order == null)
			{
				order                                                       = Order.ExecuteCreate(
																						Common.Session.Account.ID,
																						Order.STATUS_PENDING,
																						Order.PAYMENT_METHOD_DEFAULT,
																						0);
				order.Insert();
			}

			Common.Session.CurrentOrderID                                   = order.ID;
		}

		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}