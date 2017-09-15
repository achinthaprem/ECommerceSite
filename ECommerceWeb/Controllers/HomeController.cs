using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Home;
using ECommerce.Tables.Content;
using ECommerce.Tables.Content.Helpers;

namespace ECommerceWeb.Controllers
{
	public class HomeController : Controller
	{
		CategoryHelper              CategoryHelper          = new CategoryHelper();
		ProductHelper               ProductHelper           = new ProductHelper();
		OrderHelper                 OrderHelper             = new OrderHelper();
		ContactHelper               ContactHelper           = new ContactHelper();

		public async Task<ActionResult> Index(int? TopSellingFilterBy, int? SelectedProduct)
		{
			ActionResult            result                  = null;

			if (Common.Session.IsAdmin)
			{
				ViewBag.FilterList                          = ShopController.FilterActiveCategoryList(await CategoryHelper.GetCategoryListAsync());
				ViewBag.IsAdmin                             = true;

				var                 list                    = await GetTopSellingProductsAsync(TopSellingFilterBy, false);
				ViewBag.listCount                           = list.Count;

				ViewBag.selectedCategoryID                  = (TopSellingFilterBy == null) ? 0 : TopSellingFilterBy;

				ViewData["ProductSearchList"]               = ProductSelectList(await GetTopSellingProductsAsync(null, true));

				ViewData["SelectedProduct"]                 = await SearchProduct(SelectedProduct);

				result                                      = View(list);
			}
			else if (Common.Session.Authorized)
			{
				ViewBag.IsAdmin                             = false;
				result                                      = View();
			}
			else
			{
				result                                      = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Contact(CreateContactFormViewModel model)
		{
			ActionResult                    result                      = Redirect(Request.UrlReferrer.ToString());

			if (ModelState.IsValid)
			{
				if (await ContactHelper.CreateContactAsync(
							model.Name,
							model.Email,
							model.ContactNo,
							model.Subject,
							model.Message,
							ECommerce.Tables.Content.Contact.STATUS_UNREAD))
				{
					TempData["alert-success"]                           = "Thank you for contacting us, we'll be in touch :)";
				}
				else
				{
					TempData["alert-fail"]                              = "Oh Snap! Something went wrong, try again later :/";
				}
			}
			else
			{
				result                                                  = View(model);
			}

			return result;
		}

		private async Task<TopSellingProductViewModel> SearchProduct(int? selectedProduct)
		{
			TopSellingProductViewModel      result                      = null;

			if (selectedProduct != null)
			{
				Product                     product                     = await ProductHelper.GetProductAsync(selectedProduct ?? default (int));

				if (product != null)
				{
					result                                              = new TopSellingProductViewModel();
					result.ID                                           = product.ID;
					result.Name                                         = product.Name;
					result.Description                                  = product.Description;
					result.Price                                        = product.Price;
					result.ImageSrc										= $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
					result.Category                                     = product.ExecuteCreateCategoryByCategoryID().Name;
					result.CategoryID                                   = product.CategoryID;
					result.Status                                       = (product.Status == Product.STATUS_ACTIVE) ? true : false;
					result.Sellings                                     = await CountSellsAsync(product);
				}
			}

			return result;
		}

		private List<SelectListItem> ProductSelectList(List<TopSellingProductViewModel> modelList)
		{
			List<SelectListItem>            result                      = new List<SelectListItem>();

			foreach (TopSellingProductViewModel model in modelList)
			{
				result.Add(new SelectListItem { Value = model.ID.ToString(), Text = model.Name });
			}
			
			return result;
		}

		private async Task<List<TopSellingProductViewModel>> GetTopSellingProductsAsync(int? FilterBy, bool IncludeNotSold)
		{
			List<TopSellingProductViewModel>        result                  = new List<TopSellingProductViewModel>();
			List<Product>                           productList             = null;

			if (FilterBy == null || FilterBy == 0)
			{
				productList                                                 = await ProductHelper.GetProductListAsync();
			}
			else
			{
				productList                                                 = await ProductHelper.GetProductListByCategoryAsync(FilterBy ?? default(int));
			}

			foreach (Product product in productList)
			{
				int                                 sellings                = await CountSellsAsync(product);
				
				if (sellings == 0 && !IncludeNotSold)
				{
					continue;
				}

				TopSellingProductViewModel          model                   = new TopSellingProductViewModel();
				model.ID                                                    = product.ID;
				model.Name                                                  = product.Name;
				model.Description                                           = product.Description;
				model.Price                                                 = product.Price;
				model.ImageSrc                                              = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
				model.Category                                              = product.ExecuteCreateCategoryByCategoryID().Name;
				model.CategoryID                                            = product.CategoryID;
				model.Status                                                = (product.Status == Product.STATUS_ACTIVE) ? true : false;
				model.Sellings                                              = sellings;

				result.Add(model);
			}

			result                                                          = result.OrderByDescending(o => o.Sellings).ToList();

			return result;
		}

		private async Task<int> CountSellsAsync (Product product)
		{
			List<OrderItem>							orderItemList           = await OrderHelper.GetOrderItemsByProductIDAsync(product.ID);
			int										sellings                = 0;

			foreach (OrderItem item in orderItemList)
			{
				if (item.ExecuteCreateOrderByOrderID().Status == Order.STATUS_COMPLETED)
				{
					sellings												+= item.Quantity;
				}
			}

			return sellings;
		}

		private ActionResult GetAdminAuthorizeRedirect(string returnUrl)
		{
			TempData[Constants.CONST_ADMIN_ONLY_LOGIN]                  = Constants.MSG_ADMIN_ONLY_LOGIN;

			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}