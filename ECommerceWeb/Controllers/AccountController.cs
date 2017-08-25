using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECommerce.Tables.Active.HR;
using ECommerceWeb.Common;
using ECommerceWeb.Models;

namespace ECommerceWeb.Controllers
{
	public class AccountController : Controller
	{
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel user)
		{
			ActionResult            result                  = View(user);

			if (ModelState.IsValid)
			{
				var					obj                     = await Task.Run(() => Account.ExecuteCreateByEmail(user.Email));

				if (obj != null)
				{
					if (obj.Password.Equals(user.Password))
					{
						Common.Session.Start(obj);

						result                              = RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_HOME);
					}
				}
			}
			ViewBag.Message = "";
			return result;
		}

		public ActionResult Register()
		{
			ActionResult            result                  = View();

			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel user)
		{
			ActionResult            result                  = View();

			return result;
		}

		public ActionResult Logout()
		{
			ActionResult            result                  = View();

			return result;
		}
	}
}
