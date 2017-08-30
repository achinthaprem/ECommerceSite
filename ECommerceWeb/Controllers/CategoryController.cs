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
				AddCategoryViewModel		temp						= new AddCategoryViewModel();
				temp.Status = true;

				result													= View(temp);
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

			if (ModelState.IsValid || true)
			{
				if (model.Status)
				{
					result = Content("<script language='javascript' type='text/javascript'>alert('True');</script>");
				}
				else
				{
					result = Content("<script language='javascript' type='text/javascript'>alert('False');</script>");
				}
			}
			else
			{
				result                                                  = View(model);
			}

			return result;
		}
		
		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}