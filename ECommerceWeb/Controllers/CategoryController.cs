using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading.Tasks;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Category;
using ECommerce.Tables.Content.Helpers;
using ECommerce.Tables.Content;
using System.Net;

namespace ECommerceWeb.Controllers
{
	public class CategoryController : Controller
	{
		CategoryHelper                  CategoryHelper                  = new CategoryHelper();

		// GET: Category/Add
		public ActionResult Add()
		{
			ActionResult                    result                      = null;

			if (Common.Session.IsAdmin)
			{
				AddCategoryViewModel        temp                        = new AddCategoryViewModel();
				temp.Status												= true;

				result                                                  = View(temp);
			}
			else
			{
				result                                                  = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}


			return result;
		}

		// POST: Category/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(AddCategoryViewModel model)
		{
			ActionResult                    result                      = null;

			if (Common.Session.IsAdmin)
			{
				if (ModelState.IsValid)
				{
					if (await CategoryHelper.CreateCategoryAsync(
													model.Name,
													model.Description,
													model.Image,
													model.Status,
													Common.Session.Account.ID))
					{
						result                                          = View(model);
						ViewBag.Message                                 = "Category added successfully!";
					}
				}
				else
				{
					result                                              = View(model);
				}
			}
			else
			{
				result                                                  = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// GET: Category/List
		public async Task<ActionResult> List()
		{
			ActionResult									result								= null;

			if (Common.Session.IsAdmin)
			{
				List<ListCategoryViewModel>                 list                                = new List<ListCategoryViewModel>();
				List<Category>                              categories                          = await CategoryHelper.GetCategoryListAsync();

				if (categories.Count > 0)
				{
					foreach (Category category in categories)
					{
						ListCategoryViewModel                   element                         = new ListCategoryViewModel();
						element.ID                                                              = category.ID;
						element.Name                                                            = category.Name;
						element.Description                                                     = category.Description;
						element.ImageSrc                                                        = $@"~/Filestore/Images/Category/{category.ID}/{category.ImageName}";
						element.Status                                                          = (category.Status == Category.STATUS_ACTIVE) ? true : false;

						list.Add(element);
					}

					result                                                                      = View(list);
				}
				else
				{
					// TODO: If no categories
				}
			}
			else
			{
				result																			= GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// GET: Category/Edit
		public async Task<ActionResult> Edit(int? ID)
		{
			ActionResult                        result                  = View();

			if (Common.Session.IsAdmin)
			{
				if (ID != null)
				{
					Category                    category                = await CategoryHelper.GetCategoryAsync(ID ?? default(int));

					if (category != null)
					{
						EditCategoryViewModel   view                    = new EditCategoryViewModel();

						view.ID                                         = category.ID;
						view.Name                                       = category.Name;
						view.Description                                = category.Description;
						view.ImageSrc                                   = $@"~/Filestore/Images/Category/{category.ID}/{category.ImageName}";
						view.Status                                     = (category.Status == Category.STATUS_ACTIVE) ? true : false;

						result                                          = View(view);
					}
					else
					{
						result                                          = new HttpNotFoundResult();
					}
				}
				else
				{
					result												= new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                                  = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// POST: Category/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(EditCategoryViewModel model)
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
					if (await CategoryHelper.UpdateCategoryAsync(
													model.ID, 
													model.Name, 
													model.Description,
													model.Image,
													model.Status, 
													Common.Session.Account.ID))
					{
						ViewBag.MessageSuccess                          = "Category updated successfully!";
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

		// GET: Category/Delete
		public async Task<ActionResult> Delete(int? ID)
		{
			ActionResult                        result                  = View();

			if (Common.Session.IsAdmin)
			{
				if (ID != null)
				{
					Category                    category                = await CategoryHelper.GetCategoryAsync(ID ?? default(int));

					if (category != null)
					{
						// TODO: Complete the code here and POST method
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
				result                                                  = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		private ActionResult GetAdminAuthorizeRedirect(string returnUrl)
		{
			TempData[Constants.CONST_ADMIN_ONLY_LOGIN]					= Constants.MSG_ADMIN_ONLY_LOGIN;

			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}