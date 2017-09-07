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

namespace ECommerceWeb.Controllers
{
	public class ProductController : Controller
	{
		CategoryHelper						CategoryHelper                  = new CategoryHelper();
		ProductHelper                       ProductHelper                   = new ProductHelper();

		// GET: Product/Add
		public async Task<ActionResult> Add()
		{
			ActionResult                    result                          = null;

			if (Common.Session.IsAdmin)
			{
				AddProductViewModel         temp                            = new AddProductViewModel();
				temp.Status                                                 = true;
				temp.CategoryList                                           = ConvertToSelectList(await CategoryHelper.GetCategoryListAsync());

				result                                                      = View(temp);
			}
			else
			{
				result														= GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// POST: Product/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(AddProductViewModel model)
		{
			ActionResult                    result							= null;

			if (Common.Session.IsAdmin)
			{
				if (ModelState.IsValid)
				{
					if (await ProductHelper.CreateProductAsync(
						model.CategoryID,
						model.Name,
						model.Description,
						model.Price,
						model.Image,
						model.Status,
						Common.Session.Account.ID))
					{
						result												= View(model);
						ViewBag.Message										= "Category added successfully!";
					}
				}
				else
				{
					result													= View(model);
				}
			}
			else
			{
				result														= GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// GET: Product/List
		public async Task<ActionResult> List()
		{
			ActionResult                                    result                              = null;

			if (Common.Session.IsAdmin)
			{
				List<ListProductViewModel>					list                                = new List<ListProductViewModel>();
				List<Product>								products							= await ProductHelper.GetProductListAsync();

				if (products.Count > 0)
				{
					foreach (Product product in products)
					{
						ListProductViewModel                element								= new ListProductViewModel();
						element.ID                                                              = product.ID;
						element.Name                                                            = product.Name;
						element.Description                                                     = product.Description;
						element.Price                                                           = product.Price;
						element.ImageSrc                                                        = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
						element.Category                                                        = (await CategoryHelper.GetCategoryAsync(product.CategoryID)).Name;
						element.Status                                                          = (product.Status == Product.STATUS_ACTIVE) ? true : false;

						list.Add(element);
					}

					result                                                                      = View(list);
				}
				else
				{
					// TODO: If no products
				}
			}
			else
			{
				result                                                                          = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}
		
		// GET: Product/Edit
		public async Task<ActionResult> Edit(int ID = -1)
		{
			ActionResult                        result                  = View();

			if (Common.Session.IsAdmin)
			{
				if (ID != -1)
				{
					Product						product					= await ProductHelper.GetProductAsync(ID);

					if (product != null)
					{
						EditProductViewModel	view                    = new EditProductViewModel();

						view.ID                                         = product.ID;
						view.Name                                       = product.Name;
						view.Description                                = product.Description;
						view.Price                                      = product.Price;
						view.ImageSrc                                   = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
						view.CategoryList                               = ConvertToSelectList(await CategoryHelper.GetCategoryListAsync());
						view.CategoryID                                 = product.CategoryID;
						view.Status                                     = (product.Status == Product.STATUS_ACTIVE) ? true : false;

						result                                          = View(view);
					}
					else
					{
						ViewBag.MessageError                            = "Oh Snap! Something went wrong :/";
					}
				}
				else
				{
					ViewBag.MessageError                                = "Oh Snap! Something went wrong :/";
				}
			}
			else
			{
				result                                                  = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// POST: Product/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(EditProductViewModel model)
		{
			ActionResult                    result                      = null;

			if (Common.Session.IsAdmin)
			{
				if (model.Image == null)
				{
					ModelState.Remove("Image");
				}

				if (ModelState.IsValid)
				{
					if (await ProductHelper.UpdateProductAsync(
													model.ID,
													model.CategoryID,
													model.Name,
													model.Description,
													model.Price,
													model.Image,
													model.Status,
													Common.Session.Account.ID))
					{
						ViewBag.MessageSuccess                          = "Product updated successfully!";
						result                                          = View(model);
					}
					else
					{
						ViewBag.MessageSuccess                          = "Something went wrong! Try again later.";
						result                                          = View(model);
					}
				}
				else
				{
					ModelState.AddModelError("", "Please fix the errors below and try again.");
					result                                              = View(model);
				}
			}
			else
			{
				result                                                  = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		private List<SelectListItem> ConvertToSelectList(List<Category> list)
		{
			List<SelectListItem>            result                          = new List<SelectListItem>();

			foreach (Category category in list)
			{
				result.Add(new SelectListItem { Value = category.ID.ToString(), Text = category.Name });
			}

			return result;
		}

		private ActionResult GetAdminAuthorizeRedirect(string returnUrl)
		{
			TempData[Constants.CONST_ADMIN_ONLY_LOGIN]                  = Constants.MSG_ADMIN_ONLY_LOGIN;

			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}