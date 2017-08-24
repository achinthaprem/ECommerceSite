using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

using ECommerce.Tables.Active.HR;
using ECommerceWeb.Common.Attributes.Error;

namespace ECommerceWeb.Common.Parent
{
	[ErrorHandler]
	public class ParentController : Controller
	{
		#region Members

		private Account account         = null;

		#endregion

		#region Properties

		/// <summary>
		/// Logged in Users Account 
		/// </summary>
		public Account LoggedInAccount
		{
			get
			{
				if (this.account == null &&
					this.User != null &&
					this.User.Identity.IsAuthenticated &&
					this.User.Identity != null &&
					!String.IsNullOrEmpty(this.User.Identity.Name))
				{
					this.account                    = Account.ExecuteCreate(this.User.Identity.Name);
				}

				return this.account;
			}
		}

		#endregion

		#region  Action Methods 

		/// <summary>
		/// Redirects to local url 
		/// </summary>
		/// <param name="returnUrl"></param>
		/// <returns></returns>
		protected ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}



		#endregion

		#region  Methods


		/// <summary>
		/// Create the string for view
		/// </summary>
		/// <param name="viewName"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		protected string RenderRazorViewToString(string viewName, object model)
		{
			ViewData.Model                        = model;
			using (var sw = new StringWriter())
			{
				var             viewResult          = ViewEngines.Engines.FindPartialView(ControllerContext,
																						viewName);
				var             viewContext         = new ViewContext(ControllerContext,
																	viewResult.View,
																	ViewData,
																	TempData,
																	sw);
				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
				return sw.GetStringBuilder().ToString();
			}
		}

		#endregion

	}
}