using ECommerce.Tables.Content;
using ECommerce.Tables.Content.Helpers;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Shop;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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
				List<ProductViewModel>						list                                = new List<ProductViewModel>();
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

						ProductViewModel					element                             = new ProductViewModel();
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

		// GET: Shop/Product
		public async Task<ActionResult> ProductView(int? ID)
		{
			ActionResult                        result                  = View();

			if (Common.Session.Authorized)
			{
				if (ID != null)
				{
					Product                     product                 = await ProductHelper.GetProductAsync(ID ?? default(int));

					if (product != null)
					{
						ProductViewModel    view						= new ProductViewModel();

						view.ID                                         = product.ID;
						view.Name                                       = product.Name;
						view.Description                                = product.Description;
						view.Price                                      = product.Price;
						view.ImageSrc                                   = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
						view.Category									= (await CategoryHelper.GetCategoryAsync(product.CategoryID)).Name;
						view.CategoryID                                 = product.CategoryID.ToString();
						view.Status                                     = (product.Status == Product.STATUS_ACTIVE) ? true : false;

						result                                          = View(view);
					}
					else
					{
						result                                          = new HttpNotFoundResult();
					}
				}
				else
				{
					result                                              = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
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