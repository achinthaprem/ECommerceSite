using ECommerce.Tables.Active.HR;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Account;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
	[VerifyUser]
	public class AccountController : Controller
	{

		#region Login

		// GET : Account/Login
		/// <summary>
		/// Log in page
		/// </summary>
		/// <param name="returnUrl">After Successful login redirect to</param>
		/// <returns></returns>
		[AllowAny]
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
		[AllowAny]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
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
							return Redirect(returnUrl);
						}
						else // Default page after Log in
						{
							if (Common.Session.IsAdmin) // For Admin User redirects to Admin home page
							{
								return RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_HOME);
							}
							else // For Normal User redirects to product listing page (Shop/Index)
							{
								return RedirectToAction(Constants.ACTION_INDEX, Constants.CONTROLLER_SHOP);
							}
						}

					case SignInStatus.Failure:
					default:

						ModelState.AddModelError("", Constants.MSG_LOGIN_FAIL_USR_PSW);
						break;
				}
			}

			return View(model);
		}

		#endregion

		#region Register

		// GET : Account/Register
		/// <summary>
		/// New User Registration Page
		/// </summary>
		/// <returns></returns>
		[AllowAny]
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
		[AllowAny]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (await model.Save())
			{
				TempData[Constants.CONST_TMP_REG_SUCESS]        = model.successMsg;
				return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);
			}

			ModelState.AddModelError("", model.modelError);

			return View(model);
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
			Common.Session.Destroy();

			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT);
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
			return View(new EditViewModel(Common.Session.Account));
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
			if (await model.Save())
			{
				Common.Session.Start(Account.ExecuteCreate(model.ID));
				ViewBag.Success                         = model.successMsg;
			}
			else
			{
				ModelState.AddModelError("", model.modelError);
			}

			return View(model);
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
			return View(new ChangePasswordViewModel(Common.Session.Account.ID));
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
			int                 ID                      = model.ID;

			if (await model.Save())
			{
				ViewBag.Success                         = Constants.MSG_CHANGE_PSW_SUCCESS;
			}
			else
			{
				ModelState.AddModelError("", Constants.MSG_CHANGE_PSW_FAIL);
			}

			return View(model);
		}

		#endregion
		
	}
}
