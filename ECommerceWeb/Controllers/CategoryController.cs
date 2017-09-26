using ECommerceWeb.Common;
using ECommerceWeb.Models.Category;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	public class CategoryController : Controller
	{

		#region Add

		// GET: Category/Add
		public ActionResult Add()
		{
			ActionResult                    result                      = null;

			if (Common.Session.IsAdmin)
			{
				result                                                  = View(new AddCategoryViewModel());
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
					if (model.Save())
					{
						TempData["alert-success"]                       = "Category added successfully!";
					}
					else
					{
						TempData["alert-fail"]                          = "Failed to add new category!";
					}

					result												= RedirectToAction(Constants.ACTION_LIST, Constants.CONTROLLER_CATEGORY);
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

		#endregion

		#region List

		// GET: Category/List
		public async Task<ActionResult> List()
		{
			ActionResult				result					= null;

			if (Common.Session.IsAdmin)
			{
				result                                          = View(ListCategoryViewModel.GetList());
			}
			else
			{
				result											= GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Edit

		// GET: Category/Edit
		public async Task<ActionResult> Edit(int? ID)
		{
			ActionResult                        result				= View();

			if (Common.Session.IsAdmin)
			{
				if (ID != null)
				{
					EditCategoryViewModel		model               = EditCategoryViewModel.ExecuteCreate(ID ?? -1);

					if (model != null)
					{
						result                                      = View(model);
					}
					else
					{
						result                                      = new HttpNotFoundResult();
					}
				}
				else
				{
					result											= new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                              = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
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
					if (model.Save())
					{
						TempData["alert-success"]						= "Category updated successfully!";
					}
					else
					{
						TempData["alert-fail"]                          = "Something went wrong! Try again later.";
					}

					result                                              = RedirectToAction(Constants.ACTION_LIST, Constants.CONTROLLER_CATEGORY);
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

		#endregion

		#region Delete

		// GET: Category/Delete
		public async Task<ActionResult> Delete(int? ID)
		{
			ActionResult                        result              = null;

			if (Common.Session.IsAdmin)
			{
				if (ID != null)
				{
					DeleteCategoryViewModel     model				= DeleteCategoryViewModel.ExecuteCreate(ID ?? 0);

					if (model != null)
					{
						result                                      = View(model);
					}
					else
					{
						result                                      = new HttpNotFoundResult();
					}
				}
				else
				{
					result                                          = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                              = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// POST: Category/Delete
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(DeleteCategoryViewModel model)
		{
			ActionResult                    result                  = View();

			if (Common.Session.IsAdmin)
			{
				if (model.Delete())
				{
					TempData["alert-success"]						= "Category Deleted successfully!";
				}
				else
				{
					TempData["alert-fail"]                          = "Something went wrong! Try again later.";
				}

				result                                              = RedirectToAction(Constants.ACTION_LIST, Constants.CONTROLLER_CATEGORY);
			}
			else
			{
				result                                              = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Internal Methods

		private ActionResult GetAdminAuthorizeRedirect(string returnUrl)
		{
			TempData[Constants.CONST_ADMIN_ONLY_LOGIN]					= Constants.MSG_ADMIN_ONLY_LOGIN;

			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}

		#endregion
	}
}