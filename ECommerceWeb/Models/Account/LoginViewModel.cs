using System;
using System.ComponentModel.DataAnnotations;
using ECommerceWeb.Common;
using ETAH = ECommerce.Tables.Active.HR;

namespace ECommerceWeb.Models.Account
{
	public enum SignInStatus
	{
		//
		// Summary:
		//     Sign in was successful
		Success = 0,
		//
		// Summary:
		//     Account is deactivated
		Deactivated = 1,
		//
		// Summary:
		//     Sign in failed
		Failure = 2
	}

	public class LoginViewModel
	{

		#region Members

		public string               email                   = String.Empty;
		public string               password                = String.Empty;
		public bool                 rememberMe              = true;

		#endregion

		#region Properties

		[Required]
		[EmailAddress]
		[StringLength(Constants.DB_LENGTH_EMAIL, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Email")]
		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		[Required]
		[DataType(DataType.Password)]
		[StringLength(Constants.DB_LENGTH_PASSWORD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Password")]
		public string Password
		{
			get { return this.password; }
			set { this.password = value; }
		}

		[Display(Name = "Remember me?")]
		public bool RememberMe
		{
			get { return this.rememberMe; }
			set { this.rememberMe = value; }
		}

		#endregion

		#region Methods

		public SignInStatus ValidateLogin()
		{
			SignInStatus               result                   = SignInStatus.Failure;

			ETAH.Account                account                 = ETAH.Account.ExecuteCreateByEmail(this.email);

			if (account != null)
			{
				if (account.Status == ETAH.Account.STATUS_ACTIVE)
				{
					if (account.Password.Equals(ETAH.AccountPasswordHasher.HashPassword(this.password, account.Salt)))
					{
						result                                  = SignInStatus.Success;
					}
					else
					{
						result                                  = SignInStatus.Failure;
					}
				}
				else
				{
					result                                      = SignInStatus.Deactivated;
				}
			}
			else
			{
				result                                          = SignInStatus.Failure;
			}

			return result;
		}

		#endregion

	}
}