using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ECommerceWeb.Common;
using System.Web.Mvc;
using ECommerceWeb.Models.Category;

namespace ECommerceWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Add()
        {
			ActionResult					result						= null;

			if (Common.Session.Authorized)
			{
				result													= View();
			}
			else
			{
				result													= GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}


            return result;
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(AddCategoryViewModel model)
		{
			ActionResult					result						= null;



			return result;
		}
		
		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}