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
		/// <summary>
		/// Admin Homepage & Reports
		/// </summary>
		/// <param name="TopSellingFilterBy"></param>
		/// <param name="SelectedProduct"></param>
		/// <returns></returns>
		[VerifyUser]
		public ActionResult Index(int? TopSellingFilterBy, int? SelectedProduct)
		{
			if (Common.Session.IsAdmin)
			{
				ReportViewModel         result          = ReportViewModel.ExecuteCreate(TopSellingFilterBy, SelectedProduct);

				if (result != null)
				{
					return View(result);
				}
				else
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				return RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_SHOP);
			}
		}

		#endregion

		#region Export Excel

		/// <summary>
		/// Export to Excel report
		/// </summary>
		/// <param name="reportMode"></param>
		/// <param name="TopSellingFilterBy"></param>
		/// <param name="SelectedProduct"></param>
		/// <returns></returns>
		[HttpPost]
		[VerifyAdmin]
		[ValidateAntiForgeryToken]
		public ActionResult ExportToExcel(int? reportMode, int? TopSellingFilterBy, int? SelectedProduct)
		{
			ReportViewModel			result			= ReportViewModel.ExecuteCreate(TopSellingFilterBy, SelectedProduct);

			if (result != null)
			{
				result.GenerateReport(reportMode, TopSellingFilterBy, HttpContext.ApplicationInstance);
			}
			else
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return View(result);
		}

		#endregion

		#region About

		/// <summary>
		/// About page
		/// </summary>
		/// <returns></returns>
		public ActionResult About()
		{
			return View();
		}

		#endregion

		#region Contact
		
		/// <summary>
		/// Contact page
		/// </summary>
		/// <returns></returns>
		public ActionResult Contact()
		{
			return View();
		}

		/// <summary>
		/// Contact form submission
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Contact(CreateContactFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Save())
				{
					TempData[Constants.ALERT_SUCCESS]                           = "Thank you for contacting us, we'll be in touch :)";
				}
				else
				{
					TempData[Constants.ALERT_FAIL]                              = "Oh Snap! Something went wrong, try again later :/";
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