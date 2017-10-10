using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Product;

namespace ECommerceWeb.Controllers
{
	[VerifyAdmin]
	public class ProductController : Controller
	{
		
		#region Properties

		public ProductViewModel TempSession
		{
			get
			{
				ProductViewModel            result              = null;

				if (Session[Constants.SESSION_TEMP_PRODUCT] != null)
				{
					result                                      = Session[Constants.SESSION_TEMP_PRODUCT] as ProductViewModel;
				}

				return result;
			}

			set
			{
				Session[Constants.SESSION_TEMP_PRODUCT]         = value;
			}
		}

		#endregion

		#region Index

		// GET: Product/List
		/// <summary>
		/// List All Products
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View(ProductViewModel.List());
		}

		#endregion

		#region Editor

		// GET: Product/Edit
		/// <summary>
		/// Add or Edit by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Editor(int? id)
		{
			ProductViewModel					result              = null;

			if (id.HasValue)
			{
				if (this.TempSession != null &&
					this.TempSession.ID == id.Value)
				{
					result                                          = this.TempSession;
				}
				else
				{
					Session.Remove(Constants.SESSION_TEMP_PRODUCT);
					result                                          = ProductViewModel.ExecuteCreate(id.Value);
					this.TempSession                                = result;
				}

				if (result == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                              = new ProductViewModel();
				this.TempSession                                    = result;
			}

			return View(result);
		}

		// POST: Product/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Editor(ProductViewModel model)
		{
			if (this.TempSession != null &&
				model.ID == this.TempSession.ID &&
				ModelState.IsValid)
			{
				this.TempSession.Sync(model);

				if (this.TempSession.Validate(ModelState))
				{
					this.TempSession.Save();
					Session.Remove(Constants.SESSION_TEMP_PRODUCT);
					TempData[Constants.ALERT_SUCCESS]				= "Product saved successfully!";

					return RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_PRODUCT);
				}
			}

			return View(this.TempSession);
		}

		#endregion

		#region Upload Image

		// POST: Product/UploadImage
		/// <summary>
		/// Upload Image to current Product (Sessions)
		/// </summary>
		/// <returns></returns>
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

		// POST: Product/Delete
		/// <summary>
		/// Delete Product by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int? id)
		{
			bool                    result              = false;

			if (id.HasValue)
			{
				ProductViewModel	model               = ProductViewModel.ExecuteCreate(id.Value);

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