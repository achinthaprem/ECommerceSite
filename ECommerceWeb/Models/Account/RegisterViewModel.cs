using ECommerceWeb.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ETAH = ECommerce.Tables.Active.HR;

namespace ECommerceWeb.Models.Account
{
	public class RegisterViewModel
	{

		#region Constants

		public const int                    SELF_REGISTERED         = -1;

		#endregion

		#region Members

		public string               firstName               = String.Empty;
		public string               lastName                = String.Empty;
		public string               email                   = String.Empty;
		public string               password                = String.Empty;
		public string               confirmPassword         = String.Empty;
		public string               contactNo               = String.Empty;
		public string               shippingAddress         = String.Empty;
		public string               country                 = String.Empty;
		
		public string               modelError              = null;
		public string               successMsg              = null;

		#endregion

		#region Properties

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_FNAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "First Name")]
		public string FirstName
		{
			get { return this.firstName; }
			set { this.firstName = value; }
		}

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_LNAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Last Name")]
		public string LastName
		{
			get { return this.lastName; }
			set { this.lastName = value; }
		}

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

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		[Display(Name = "Confirm password")]
		public string ConfirmPassword
		{
			get { return this.confirmPassword; }
			set { this.confirmPassword = value; }
		}

		[Required]
		[DataType(DataType.PhoneNumber)]
		[StringLength(Constants.DB_LENGTH_CONTACT_NO, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Contact No")]
		public string ContactNo
		{
			get { return this.contactNo; }
			set { this.contactNo = value; }
		}

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(Constants.DB_LENGTH_SHIP_ADD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Shipping Address")]
		public string ShippingAddress
		{
			get { return this.shippingAddress; }
			set { this.shippingAddress = value; }
		}

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_COUNTRY, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Country")]
		public string Country
		{
			get { return this.country; }
			set { this.country = value; }
		}

		#endregion

		#region Methods

		public Task<bool> Save()
		{
			return Task.Run(() =>
			{
				bool                    result                      = false;

				if (CheckEmailAsync(this.email))
				{
					string				salt						= String.Empty;
					string				hPassword					= ETAH.AccountPasswordHasher.CreatePassword(Password, out salt);

					ETAH.Account        newAccount					= ETAH.Account.ExecuteCreate(
																						this.firstName,
																						this.lastName,
																						this.email,
																						hPassword,
																						salt,
																						this.contactNo,
																						this.shippingAddress,
																						this.country,
																						ETAH.Account.STATUS_ACTIVE,
																						(int)ETAH.Account.RoleCode.All,
																						SELF_REGISTERED,
																						SELF_REGISTERED);

					newAccount.Insert();

					if (newAccount.ID != -1)
					{
						this.successMsg                             = Constants.MSG_REG_SUCCESS;
						result                                      = true;
					}
				}
				else
				{
					this.modelError                                 = Constants.MSG_REG_FAIL_EMAIL_EXIST;
				}

				return result;
			});
		}

		/// <summary>
		/// Check Email existance in database
		/// </summary>
		/// <param name="Email">Email about to check</param>
		/// <returns></returns>
		public bool CheckEmailAsync(string Email)
		{
			bool                        result                  = false;

			if (ETAH.Account.ExecuteCreateByEmail(Email) == null)
			{
				result                                          = true;
			}

			return result;
		}

		#endregion

	}
}