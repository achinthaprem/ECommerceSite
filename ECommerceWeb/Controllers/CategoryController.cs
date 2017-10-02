using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Category;

namespace ECommerceWeb.Controllers
{
	[VerifyAdmin]
	public class CategoryController : Controller
	{

		#region Properties

		public CategoryViewModel TempSession
		{
			get
			{
				CategoryViewModel               result              = null;

				if (Session[Constants.SESSION_TEMP_CATEGORY] != null)
				{
					result                                          = Session[Constants.SESSION_TEMP_CATEGORY] as CategoryViewModel;
				}

				return result;
			}

			set
			{
				Session[Constants.SESSION_TEMP_CATEGORY]            = value;
			}
		}

		#endregion

		#region List

		// GET: Category/List
		public ActionResult List()
		{
			return View(CategoryViewModel.List());
		}

		#endregion

		#region Editor

		// GET: Category/Editor
		public ActionResult Editor(int? id)
		{
			CategoryViewModel                   result              = null;

			if (id.HasValue)
			{
				if (this.TempSession != null &&
					this.TempSession.ID == id.Value)
				{
					result                                          = this.TempSession;
				}
				else
				{
					Session.Remove(Constants.SESSION_TEMP_CATEGORY);
					result                                          = CategoryViewModel.ExecuteCreate(id.Value);
					this.TempSession                                = result;
				}

				if (result == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                              = new CategoryViewModel();
				this.TempSession                                    = result;
			}

			return View(result);
		}

		// POST: Category/Editor
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Editor(CategoryViewModel model)
		{
			if (this.TempSession != null &&
				model.ID == this.TempSession.ID &&
				ModelState.IsValid)
			{
				this.TempSession.Sync(model);

				if (this.TempSession.Validate(ModelState))
				{
					this.TempSession.Save();
					Session.Remove(Constants.SESSION_TEMP_CATEGORY);
					TempData["alert-success"]                       = "Category saved successfully!";

					return RedirectToAction(Constants.ACTION_LIST, Constants.CONTROLLER_CATEGORY);
				}
			}

			return View(this.TempSession);
		}

		#endregion

		#region Upload Image

		// POST: Category/UploadImage
		[HttpPost]
		public JsonResult UploadImage()
		{
			foreach (string fileObject in Request.Files)
			{
				HttpPostedFileBase          file            = Request.Files[fileObject] as HttpPostedFileBase;

				if (this.TempSession != null)
				{
					this.TempSession.Upload(file);
				}
			}

			return Json("File Uploaded Successfully!");
		}

		#endregion

		#region Delete

		// POST: Category/Delete
		[HttpPost]
		public ActionResult Delete(int? id)
		{
			bool					result              = false;

			if (id.HasValue)
			{
				CategoryViewModel	model				= CategoryViewModel.ExecuteCreate(id.Value);

				if (model != null)
				{
					model.Delete();
					result                              = true;
				}
			}

			return Json(new { Success = result }, JsonRequestBehavior.AllowGet);
		}

		#endregion

	}
}