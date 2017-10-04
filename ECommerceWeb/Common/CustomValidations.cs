using System;
using System.Diagnostics.Contracts;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWeb.Common
{

	#region Custom Authentication Attributes

	/// <summary>
	/// To verify Normal User
	/// </summary>
	public class VerifyUserAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!Session.Authorized && !SkipVerification(filterContext))
			{
				filterContext.Result            = new RedirectResult(
													string.Format(
														"~/Account/Login?level={0}&returnUrl={1}",
														Constants.ACCESS_LEVEL_USER,
														HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.AbsolutePath)));
			}
		}

		/// <summary>
		/// Checks for the AllowAny attribute's presence 
		/// </summary>
		/// <param name="filterContext"></param>
		/// <returns></returns>
		private static bool SkipVerification(ActionExecutingContext filterContext)
		{
			Contract.Assert(filterContext != null);

			return filterContext.ActionDescriptor.IsDefined(typeof(AllowAnyAttribute), inherit: true) ||
				filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnyAttribute), inherit: true);
		}
	}

	/// <summary>
	/// To verify Admin Users
	/// </summary>
	public class VerifyAdminAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!Session.IsAdmin)
			{
				filterContext.Result            = new RedirectResult(
													string.Format(
														"~/Account/Login?level={0}&returnUrl={1}",
														Constants.ACCESS_LEVEL_ADMIN,
														HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.AbsolutePath)));
			}
		}
	}

	/// <summary>
	/// To allow Anyone
	/// </summary>
	public sealed class AllowAnyAttribute : Attribute {	}

	#endregion

}