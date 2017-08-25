using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceWeb.Models;
using ECommerceWeb.Common;

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
		public ActionResult Login(Account user)
		{
			ActionResult            result                  = View(user);

			if (ModelState.IsValid)
			{
				using (DBEntities db = new DBEntities())
				{
					var             obj                     = db.Accounts.Where(a => a.email.Equals(user.email) && a.password.Equals(user.password)).FirstOrDefault();

					if (obj != null)
					{
						Common.Session.Start(obj);

						result                              = RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_HOME);
					}
				}
			}

			return result;
		}

		public ActionResult Register()
		{
			ActionResult            result                  = View();

			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(Account user)
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
