using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ECommerceWeb.Models;
using ECommerce.Tables.Active.HR;
using ECommerceWeb.Common.Attributes.Security;
using ECommerceWeb.Common.Parent;
using ECommerceWeb.Models.AccountModels;

namespace ECommerceWeb.Controllers
{
    public class AccountController : ParentAuthenticationController
	{

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		// GET: Account
		public ActionResult Index()
        {
            return View();
        }
    }
}