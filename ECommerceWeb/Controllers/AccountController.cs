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
		private AccountService AccountService = new AccountService();

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}

			ActionResult            result                  = View(user);

			Account                 obj                     = Account.ExecuteCreateByEmail(user.Email);
			SignInStatus            status                  = await AccountService.SignInUser(obj, user.Password);

			switch (status)
			{
				case SignInStatus.Deactivated:

					// TODO: Do something for deactivated(inactive) accounts
					break;

				case SignInStatus.Success:

					Common.Session.Start(obj);
					result                                  = RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_HOME);
					break;

				case SignInStatus.Failure:
				default:

					ModelState.AddModelError("", "Invalid login attempt.");
					break;
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
		public async Task<ActionResult> Register(RegisterViewModel user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}

			ActionResult            result                  = RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);

			if (!await AccountService.CreateAccount(user.FirstName,
				user.LastName,
				user.Email,
				user.Password,
				user.ContactNo,
				user.ShippingAddress,
				user.Country,
				(int)Account.RoleCode.All,
				AccountService.SELF_REGISTERED))
			{
				ModelState.AddModelError("", "Registration failed.");
				result                                      = View(user);
			}

			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Logout()
		{
			ActionResult            result                  = RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);

			Common.Session.Destroy();

			return result;
		}
	}
}
