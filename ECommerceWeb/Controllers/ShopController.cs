using System.Net;
using System.Web.Mvc;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Shop;

namespace ECommerceWeb.Controllers
{
	[VerifyUser]
	public class ShopController : Controller
	{

		#region Index

		// GET: Shop/Index
		/// <summary>
		/// List down the products which, the status is active
		/// </summary>
		/// <param name="filterBy"></param>
		/// <returns></returns>
		public ActionResult Index(int? filterBy)
		{
			return View(ShopViewModel.List(filterBy ?? 0));
		}

		#endregion

		#region Product View

		// GET: Shop/Product
		/// <summary>
		/// Opens a new page to show an product
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public ActionResult ProductView(int? ID)
		{
			ShopViewModel				result              = null;

			if (ID.HasValue)
			{
				result										= new ShopViewModel(ID.Value);

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