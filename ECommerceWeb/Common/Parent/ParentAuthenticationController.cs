using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using ECommerce.Tables.Active.HR;

namespace ECommerceWeb.Common.Parent
{
	public class ParentAuthenticationController : ParentController
	{
		#region Properties

		/// <summary>
		/// User manager 
		/// </summary>
		public UserManager<Account> UserManager { get; protected set; }

		#endregion

		#region Constructor

		public ParentAuthenticationController() : base()
		{
			this.UserManager                = new UserManager<Account>(new AccountService() { });
			this.UserManager.PasswordHasher = new AccountPasswordHasher();

			// Configure validation logic for passwords
			UserManager.PasswordValidator   = new PasswordValidator
			{
				RequiredLength = 9,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			this.UserManager.UserTokenProvider              = new AccountTokenProvider<Account>();
		}


		#endregion

	}
}