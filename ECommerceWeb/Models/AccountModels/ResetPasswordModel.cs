using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using ECommerce.Tables.Active.HR;

namespace ECommerceWeb.Models.AccountModels
{
	public class ResetPasswordModel
	{
		#region Properties

		[Required(ErrorMessage = "Email field is required")]
		[EmailAddress(ErrorMessage = "Invalid email address provided")]
		[Display(Name = "Email*")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password field is required")]
		[StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password*")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password*")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public string Code { get; set; }

		public int Purpose { get; set; }

		public int UserID { get; set; }

		public string UserName
		{
			get
			{
				string              result          = String.Empty;

				Account             account         = Account.ExecuteCreate(this.UserID);

				if (account != null)
				{
					result                          = account.FullName;
				}

				return result;
			}
		}


		#endregion

		#region Constructor

		public ResetPasswordModel()
		{

		}

		public ResetPasswordModel(string code, int userID, int purpose)
		{
			this.Code               = code;
			this.UserID             = userID;
			this.Purpose            = purpose;
		}

		#endregion


	}
}