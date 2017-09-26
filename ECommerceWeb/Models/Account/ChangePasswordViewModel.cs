using ECommerce.Tables.Active.HR;
using ECommerceWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ETAH = ECommerce.Tables.Active.HR;

namespace ECommerceWeb.Models.Account
{
	public class ChangePasswordViewModel
	{

		#region Members

		private string              currentPassword                 = String.Empty;
		private string              newPassword                     = String.Empty;
		private string              confirmPassword                 = String.Empty;
		private int                 id                              = Constants.DEFAULT_VALUE_INT;

		#endregion

		#region Properties

		[Required]
		[DataType(DataType.Password)]
		[StringLength(Constants.DB_LENGTH_PASSWORD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Current Password")]
		public string CurrentPassword
		{
			get { return this.currentPassword; }
			set { this.currentPassword = value; }
		}

		[Required]
		[DataType(DataType.Password)]
		[StringLength(Constants.DB_LENGTH_PASSWORD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "New Password")]
		public string NewPassword
		{
			get { return this.newPassword; }
			set { this.newPassword = value; }
		}

		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
		[Display(Name = "Confirm password")]
		public string ConfirmPassword
		{
			get { return this.confirmPassword; }
			set { this.confirmPassword = value; }
		}

		// Use full namespace reference here, because there is another ambiguous reference
		[System.Web.Mvc.HiddenInput(DisplayValue = false)]
		public int ID
		{
			get { return this.id; }
			set { this.id = value; }
		}

		#endregion

		#region Constructors

		public ChangePasswordViewModel(int ID)
		{
			this.id                 = ID;
		}

		#endregion

		#region Methods

		public Task<bool> Save()
		{
			return Task.Run(() =>
			{
				bool                        result                  = false;

				if (this.id != Constants.DEFAULT_VALUE_INT)
				{
					ETAH.Account            account                 = ETAH.Account.ExecuteCreate(this.id);
					string                  password                = AccountPasswordHasher.HashPassword(this.currentPassword, account.Salt);

					if (account.Password.Equals(password)) // If current password correct
					{
						string              salt                    = String.Empty;
						string              hPassword               = AccountPasswordHasher.CreatePassword(this.newPassword, out salt);

						account.UpdatePasswordSalt(hPassword, salt, Common.Session.Account.ID);

						result                                      = true;
					}
				}

				return result;
			});
		}

		#endregion

	}
}