using ECommerceWeb.Common;
using ECommerceWeb.Models.Shop;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	[VerifyUser]
	public class ShopController : Controller
	{

		#region Index

		// GET: Shop/Index
		public async Task<ActionResult> Index(int? filterBy)
		{
			return View(await ProductViewModel.List(filterBy ?? 0));
		}

		#endregion

		#region Product View

		// GET: Shop/Product
		public ActionResult ProductView(int? ID)
		{
			ProductViewModel            result              = null;

			if (ID.HasValue)
			{
				result										= new ProductViewModel(ID.Value);

				if (result.ID == Constants.DEFAULT_VALUE_INT)
				{
					return new HttpNotFoundResult();
				}
			}
			else
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return View(result);
		}

		#endregion
		
	}
}