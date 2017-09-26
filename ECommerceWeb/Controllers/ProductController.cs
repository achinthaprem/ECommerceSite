using ECommerceWeb.Common;
using ECommerceWeb.Models.Product;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	public class ProductController : Controller
	{
		
		#region Add

		// GET: Product/Add
		public async Task<ActionResult> Add()
		{
			ActionResult                    result                          = null;

			if (Common.Session.IsAdmin)
			{
				result                                                      = View(new AddProductViewModel());
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
					if (model.Save())
					{
						TempData["alert-success"]                           = "Category added successfully!";
					}
					else
					{
						TempData["alert-fail"]                              = "Failed to add new product!";
					}

					result													= RedirectToAction(Constants.ACTION_LIST, Constants.CONTROLLER_PRODUCT);
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

		#endregion

		#region List

		// GET: Product/List
		public async Task<ActionResult> List()
		{
			ActionResult					result							= null;

			if (Common.Session.IsAdmin)
			{
				result                                                      = View(ListProductViewModel.GetList());
			}
			else
			{
				result                                                      = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Edit

		// GET: Product/Edit
		public async Task<ActionResult> Edit(int? ID)
		{
			ActionResult                        result                  = View();

			if (Common.Session.IsAdmin)
			{
				if (ID != null)
				{
					EditProductViewModel		model                   = EditProductViewModel.ExecuteCreate(ID ?? -1);

					if (model != null)
					{
						result                                          = View(model);
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
					if (model.Save())
					{
						TempData["alert-success"]                       = "Product updated successfully!";
					}
					else
					{
						TempData["alert-fail"]                          = "Something went wrong! Try again later.";
					}

					result                                              = RedirectToAction(Constants.ACTION_LIST, Constants.CONTROLLER_PRODUCT);
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

		// GET: Product/Delete
		public async Task<ActionResult> Delete(int? ID)
		{
			ActionResult                        result                  = null;

			if (Common.Session.IsAdmin)
			{
				if (ID != null)
				{
					DeleteProductViewModel		model                   = DeleteProductViewModel.ExecuteCreate(ID ?? -1);

					if (model != null)
					{
						result                                          = View(model);
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

		// POST: Product/Delete
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(DeleteProductViewModel model)
		{
			ActionResult                        result                  = View();

			if (Common.Session.IsAdmin)
			{
				if (model.Delete())
				{
					TempData["alert-success"]                           = "Product Deleted successfully!";
				}
				else
				{
					TempData["alert-fail"]                              = "Something went wrong! Try again later.";
				}

				result                                                  = RedirectToAction(Constants.ACTION_LIST, Constants.CONTROLLER_PRODUCT);
			}
			else
			{
				result                                                  = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Internal Methods

		private ActionResult GetAdminAuthorizeRedirect(string returnUrl)
		{
			TempData[Constants.CONST_ADMIN_ONLY_LOGIN]                  = Constants.MSG_ADMIN_ONLY_LOGIN;

			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}

		#endregion

	}
}