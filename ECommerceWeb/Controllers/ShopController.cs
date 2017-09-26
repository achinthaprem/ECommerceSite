using ECommerceWeb.Common;
using ECommerceWeb.Models.Shop;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	public class ShopController : Controller
	{

		#region Index

		// GET: Shop/Index
		public async Task<ActionResult> Index(int? filterBy)
		{
			ActionResult                result                  = null;

			if (Common.Session.Authorized)
			{
				result                                          = View(await ProductViewModel.List(filterBy ?? 0));
			}
			else
			{
				result                                          = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Product View

		// GET: Shop/Product
		public ActionResult ProductView(int? ID)
		{
			ActionResult                        result                  = View();

			if (Common.Session.Authorized)
			{
				if (ID != null)
				{
					ProductViewModel            model                   = new ProductViewModel(ID ?? default (int));

					if (model.ID != Constants.DEFAULT_VALUE_INT)
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
				result                                                  = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Internal Methods

		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}

		#endregion

	}
}