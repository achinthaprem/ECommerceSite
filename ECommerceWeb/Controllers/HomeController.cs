using System.Net;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Home;

namespace ECommerceWeb.Controllers
{
	public class HomeController : Controller
	{

		#region Home Page

		// GET: Home/Index
		[VerifyUser]
		public ActionResult Index(int? TopSellingFilterBy, int? SelectedProduct)
		{
			if (Common.Session.IsAdmin)
			{
				return View(new ReportViewModel(TopSellingFilterBy, SelectedProduct));
			}
			else
			{
				return RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_SHOP);
			}
		}

		#endregion

		#region Export Excel

		[HttpPost]
		[VerifyAdmin]
		[ValidateAntiForgeryToken]
		public ActionResult ExportToExcel(int? reportMode, int? TopSellingFilterBy, int? SelectedProduct)
		{
			ReportViewModel			result			= new ReportViewModel(TopSellingFilterBy, SelectedProduct);

			if (result != null)
			{
				result.GenerateReport(reportMode, HttpContext.ApplicationInstance);
			}
			else
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return View(result);
		}

		#endregion

		#region About

		public ActionResult About()
		{
			return View();
		}

		#endregion

		#region Contact
		
		public ActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Contact(CreateContactFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Save())
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
				return View(model);
			}

			return Redirect(Request.UrlReferrer.ToString());
		}

		#endregion
		
	}
}