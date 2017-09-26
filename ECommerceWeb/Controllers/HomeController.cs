using ECommerceWeb.Common;
using ECommerceWeb.Models.Home;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	public class HomeController : Controller
	{

		#region Home Page

		// GET: Home/Index
		public async Task<ActionResult> Index(int? TopSellingFilterBy, int? SelectedProduct)
		{
			ActionResult            result                  = null;

			if (Common.Session.IsAdmin)
			{
				result                                      = View("Index", new ReportViewModel(TopSellingFilterBy, SelectedProduct));
			}
			else if (Common.Session.Authorized)
			{
				// TODO: new view/model for user
				result                                      = View();
			}
			else
			{
				result                                      = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Export Excel

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ExportToExcel(int? reportMode, int? TopSellingFilterBy, int? SelectedProduct)
		{
			ActionResult                result                  = null;

			if (Common.Session.IsAdmin)
			{
				ReportViewModel         model                   = new ReportViewModel(TopSellingFilterBy, SelectedProduct);

				if (model != null)
				{
					model.GenerateReport(reportMode, HttpContext.ApplicationInstance);
				}
				else
				{
					result                                      = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                          = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
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
		public async Task<ActionResult> Contact(CreateContactFormViewModel model)
		{
			ActionResult                    result                      = Redirect(Request.UrlReferrer.ToString());

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
				result                                                  = View(model);
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