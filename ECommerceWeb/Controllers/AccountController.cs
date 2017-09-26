using ECommerce.Tables.Active.HR;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Account;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	public class AccountController : Controller
	{

		#region Login

		// GET : Account/Login
		/// <summary>
		/// Log in page
		/// </summary>
		/// <param name="returnUrl">After Successful login redirect to</param>
		/// <returns></returns>
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl                               = returnUrl;
			return View();
		}

		// POST : Account/Login
		/// <summary>
		/// Log in page
		/// </summary>
		/// <param name="model"></param>
		/// <param name="returnUrl"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			ActionResult            result                  = null;

			if (!ModelState.IsValid)
			{
				result                                      = View(model);
			}
			else
			{
				switch (await model.ValidateLogin())
				{
					case SignInStatus.Deactivated:

						// TODO: Do something for deactivated(inactive) accounts
						break;

					case SignInStatus.Success:

						Common.Session.Start(Account.ExecuteCreateByEmail(model.Email));

						if (Url.IsLocalUrl(returnUrl)) // If there is a Return Url
						{
							result                          = Redirect(returnUrl);
						}
						else // Default page after Log in
						{
							if (Common.Session.IsAdmin) // For Admin User redirects to Admin home page
							{
								result                      = RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_HOME);
							}
							else // For Normal User redirects to product listing page (Shop/Index)
							{
								result                      = RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_SHOP);
							}
						}
						break;

					case SignInStatus.Failure:
					default:

						ModelState.AddModelError("", Constants.MSG_LOGIN_FAIL_USR_PSW);
						break;
				}
			}

			return result;
		}

		#endregion

		#region Register

		// GET : Account/Register
		/// <summary>
		/// New User Registration Page
		/// </summary>
		/// <returns></returns>
		public ActionResult Register()
		{
			return View();
		}

		// POST : Account/Register
		/// <summary>
		/// New User Registration Page
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			ActionResult            result                      = null;

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (await model.Save())
			{
				result                                          = RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);
				TempData[Constants.CONST_TMP_REG_SUCESS]        = model.successMsg;
			}
			else
			{
				ModelState.AddModelError("", model.modelError);
				result                                          = View(model);
			}

			return result;
		}

		#endregion

		#region Logout

		// POST : Account/Logout
		/// <summary>
		/// Logout the current User
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Logout()
		{
			ActionResult            result                  = RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);

			if (Common.Session.Authorized)
			{
				// Logout the User if only Logged in
				Common.Session.Destroy();
			}

			return result;
		}

		#endregion

		#region Manage

		// GET : Account/Manage
		/// <summary>
		/// To Manage Current user's account information
		/// </summary>
		/// <returns></returns>
		public ActionResult Manage()
		{
			ActionResult            result                  = null;

			if (Common.Session.Authorized)
			{
				result                                      = View(new EditViewModel(Common.Session.Account));
			}
			else
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}
			return result;
		}

		// POST : Account/Manage
		/// <summary>
		/// To Manage Current user's account information
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Manage(EditViewModel model)
		{
			ActionResult            result                  = View(model);

			if (Common.Session.Authorized) // If logged in
			{
				if (await model.Save())
				{
					Common.Session.Start(Account.ExecuteCreate(model.ID));
					ViewBag.Success                         = model.successMsg;
				}
				else
				{
					ModelState.AddModelError("", model.modelError);
				}
			}
			else // Not logged in
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Change Password

		// GET : Account/ChangePassword
		/// <summary>
		/// Change password Page
		/// </summary>
		/// <returns></returns>
		public ActionResult ChangePassword()
		{
			ActionResult            result                  = null;

			if (Common.Session.Authorized)
			{
				result                                      = View(new ChangePasswordViewModel(Common.Session.Account.ID));
			}
			else
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		// POST : Account/ChangePassword
		/// <summary>
		/// Change Password Page
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			ActionResult            result                  = View(model);

			if (Common.Session.Authorized) // If logged in
			{
				int                 ID                      = model.ID;

				if (await model.Save())
				{
					ViewBag.Success                         = Constants.MSG_CHANGE_PSW_SUCCESS;
				}
				else
				{
					ModelState.AddModelError("", Constants.MSG_CHANGE_PSW_FAIL);
				}
			}
			else // If not logged in
			{
				result                                      = GetAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		#endregion

		#region Common

		/// <summary>
		/// Redirects User to login page with  return Url
		/// </summary>
		/// <param name="returnUrl"></param>
		/// <returns></returns>
		private ActionResult GetAuthorizeRedirect(string returnUrl)
		{
			// GetAuthorizeRedirect(Request.Url.PathAndQuery);
			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}

		#endregion

	}
}
