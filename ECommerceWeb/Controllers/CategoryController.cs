using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading.Tasks;
using ECommerceWeb.Common;
using System.Web.Mvc;
using ECommerceWeb.Models.Category;
using ECommerce.Tables.Content.Helpers;
using ECommerce.Tables.Content;

namespace ECommerceWeb.Controllers
{
	public class CategoryController : Controller
	{
		CategoryHelper                  CategoryHelper                  = new CategoryHelper();

		// GET: Category
		public ActionResult Add()
		{
			ActionResult                    result                      = null;

			if (Common.Session.Authorized)
			{
				AddCategoryViewModel        temp                        = new AddCategoryViewModel();
				temp.Status = true;

				result                                                  = View(temp);
			}
			else
			{
				result                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}


			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(AddCategoryViewModel model)
		{
			ActionResult                    result                      = null;

			if (ModelState.IsValid && Common.Session.Authorized)
			{
				if (await CategoryHelper.CreateCategoryAsync(
												model.Name,
												model.Description,
												model.Image,
												model.Status,
												Common.Session.Account.ID))
				{
					result                                              = View(model);
					ViewBag.Message                                     = "Category added successfully!";
				}
			}
			else
			{
				result                                                  = View(model);
			}

			return result;
		}

		public async Task<ActionResult> List()
		{
			ActionResult									result								= null;

			if (Common.Session.Authorized)
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

				}
			}
			else
			{
				result																			= GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// GET: Category/Edit
		public async Task<ActionResult> Edit(int ID = -1)
		{
			ActionResult                        result                  = View();

			if (Common.Session.Authorized)
			{
				if (ID != -1)
				{
					Category                    category                = await CategoryHelper.GetCategoryAsync(ID);

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
						ViewBag.MessageError                            = "Oh Snap! Something went wrong :/";
					}
				}
				else
				{
					ViewBag.MessageError								= "Oh Snap! Something went wrong :/";
				}
			}
			else
			{
				result                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(EditCategoryViewModel model)
		{
			ActionResult                    result                      = null;
			
			if (Common.Session.Authorized)
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
				result                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}