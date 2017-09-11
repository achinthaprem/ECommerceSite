using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECommerce.Tables.Active.HR;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Account;

namespace ECommerceWeb.Controllers
{
	public class AccountController : Controller
	{
		private AccountService AccountService = new AccountService();

		public ActionResult Login(string returnUrl)
		{
			// TODO: if (!loggedIn) Then validate cookies

			ViewBag.ReturnUrl                               = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			ActionResult            result                  = View(model);

			Account                 account                 = null;// Common.Session.ValidateCookie(Request);

			//if (account == null)
			//{
			account                                     = await AccountService.GetAccountAsync(model.Email);

			//	Response.Cookies.Add(Common.Session.CreateCookie(account, model.RememberMe));
			//}

			SignInStatus            status                  = await AccountService.SignInUserAsync(account, model.Password);

			switch (status)
			{
				case SignInStatus.Deactivated:

					// TODO: Do something for deactivated(inactive) accounts
					break;

				case SignInStatus.Success:

					Common.Session.Start(account);

					if (Url.IsLocalUrl(returnUrl))
					{
						result                              = Redirect(returnUrl);
					}
					else
					{
						if (Common.Session.IsAdmin)
						{
							result                          = RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_HOME);
						}
						else
						{
							result                          = RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_SHOP);
						}
					}
					break;

				case SignInStatus.Failure:
				default:

					ModelState.AddModelError("", Constants.MSG_LOGIN_FAIL_USR_PSW);
					break;
			}

			return result;
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			ActionResult            result                      = null;

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (await AccountService.CheckEmailAsync(model.Email))
			{
				if (await AccountService.CreateAccountAsync(model.FirstName,
				model.LastName,
				model.Email,
				model.Password,
				model.ContactNo,
				model.ShippingAddress,
				model.Country,
				(int)Account.RoleCode.All,
				AccountService.SELF_REGISTERED))
				{
					result                                      = RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);
					TempData[Constants.CONST_TMP_REG_SUCESS]    = Constants.MSG_REG_SUCCESS;
				}
				else
				{
					ModelState.AddModelError("", Constants.MSG_REG_FAIL);
					result                                      = View(model);
				}
			}
			else
			{
				ModelState.AddModelError("", Constants.MSG_REG_FAIL_EMAIL_EXIST);
				result                                          = View(model);
			}

			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Logout()
		{
			ActionResult            result                  = RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);

			if (Common.Session.Authorized)
			{
				Common.Session.Destroy();
			}
			else
			{
				result                                      = RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);
			}

			return result;
		}

		public ActionResult Manage()
		{
			ActionResult            result                  = null;

			if (Common.Session.Authorized)
			{
				Account             account                 = Common.Session.Account;

				EditViewModel       editView                = new EditViewModel();

				editView.ID                                 = account.ID.ToString();
				editView.FirstName                          = account.FirstName;
				editView.LastName                           = account.LastName;
				editView.Email                              = account.Email;
				editView.ContactNo                          = account.ContactNo;
				editView.ShippingAddress                    = account.ShippingAddress;
				editView.Country                            = account.Country;

				result                                      = View(editView);
			}
			else
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}
			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Manage(EditViewModel model)
		{
			ActionResult            result                  = View(model);

			if (Common.Session.Authorized)
			{
				int                 ID                      = int.Parse(model.ID);

				if (await AccountService.CheckEmailAsync(ID, model.Email))
				{
					if (await AccountService.UpdateAccountAsync(
					ID,
					model.FirstName,
					model.LastName,
					model.Email,
					model.ContactNo,
					model.ShippingAddress,
					model.Country,
					Account.STATUS_ACTIVE,
					(int)Account.RoleCode.All,
					ID))
					{
						ViewBag.ID                          = ID;
						ViewBag.Success                     = Constants.MSG_MANAGE_SUCCESS;

						Common.Session.Start(Account.ExecuteCreate(ID));
					}
					else
					{
						ModelState.AddModelError("", Constants.MSG_MANAGE_FAIL);
					}
				}
				else
				{
					ModelState.AddModelError("", Constants.MSG_MANAGE_FAIL_EMAIL_EXIST);
				}
			}
			else
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		public ActionResult ChangePassword()
		{
			ActionResult            result                  = null;

			if (Common.Session.Authorized)
			{
				ChangePasswordViewModel view                = new ChangePasswordViewModel();
				view.ID                                     = Common.Session.Account.ID.ToString();
				result                                      = View(view);
			}
			else
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}
			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			ActionResult            result                  = View(model);

			if (Common.Session.Authorized)
			{
				int                 ID                      = int.Parse(model.ID);

				if (await AccountService.ValidatePasswordAsync(ID, model.CurrentPassword))
				{
					if (await AccountService.ChangePasswordAsync(ID, model.NewPassword, Common.Session.Account.ID))
					{
						ViewBag.Success                     = Constants.MSG_CHANGE_PSW_SUCCESS;
					}
					else
					{
						ModelState.AddModelError("", Constants.MSG_CHANGE_PSW_FAIL);
					}
				}
				else
				{
					ModelState.AddModelError("", Constants.MSG_CHANGE_PSW_INVALID_PSW);
				}
			}
			else
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			// GetAuthorizeRedirect(Request.Url.PathAndQuery);
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}
