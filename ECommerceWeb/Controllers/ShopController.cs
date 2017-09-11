using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Product;
using ECommerce.Tables.Content.Helpers;
using ECommerce.Tables.Content;
using System.Net;


namespace ECommerceWeb.Controllers
{
    public class ShopController : Controller
	{
		CategoryHelper                      CategoryHelper                  = new CategoryHelper();
		ProductHelper                       ProductHelper                   = new ProductHelper();

		// GET: Shop
		public async Task<ActionResult> Index(int filterBy = 0)
		{
			ActionResult                                    result                              = null;

			if (Common.Session.Authorized)
			{
				List<ListProductViewModel>                  list                                = new List<ListProductViewModel>();
				List<Product>                               products                            = await ProductHelper.GetProductListAsync();

				if (products.Count > 0)
				{
					foreach (Product product in products)
					{
						if (product.Status == Product.STATUS_INACTIVE)
						{
							continue;
						}

						if (product.CategoryID != filterBy && filterBy != 0)
						{
							continue;
						}

						ListProductViewModel                element                             = new ListProductViewModel();
						element.ID                                                              = product.ID;
						element.Name                                                            = product.Name;
						element.Description                                                     = product.Description;
						element.Price                                                           = product.Price;
						element.ImageSrc                                                        = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
						element.Category                                                        = (await CategoryHelper.GetCategoryAsync(product.CategoryID)).Name;
						element.Status                                                          = (product.Status == Product.STATUS_ACTIVE) ? true : false;

						list.Add(element);
					}

					ViewBag.FilterList                                                          = FilterActiveCategoryList(await CategoryHelper.GetCategoryListAsync());
					result                                                                      = View(list);
				}
				else
				{
					// TODO: If no products
				}
			}
			else
			{
				result                                                                          = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		private List<SelectListItem> FilterActiveCategoryList(List<Category> list)
		{
			List<SelectListItem>            result                          = new List<SelectListItem>();

			result.Add(new SelectListItem { Value = "0", Text = "All Categories" });

			foreach (Category category in list)
			{
				if (category.Status == Category.STATUS_INACTIVE)
				{
					continue;
				}

				result.Add(new SelectListItem { Value = category.ID.ToString(), Text = category.Name });
			}

			return result;
		}

		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}