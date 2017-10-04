using System;
using System.ComponentModel.DataAnnotations;
using ECommerceWeb.Common;
using ETAH = ECommerce.Tables.Active.HR;

namespace ECommerceWeb.Models.Account
{
	public class EditViewModel
	{

		#region Members

		public string           firstName               = String.Empty;
		public string           lastName                = String.Empty;
		public string           email                   = String.Empty;
		public string           contactNo               = String.Empty;
		public string           shippingAddress         = String.Empty;
		public string           country                 = String.Empty;
		public int              id                      = Constants.DEFAULT_VALUE_INT;

		public string           modelError              = null;
		public string           successMsg              = null;

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

		[System.Web.Mvc.HiddenInput(DisplayValue = false)]
		public int ID
		{
			get { return this.id; }
			set { this.id = value; }
		}

		#endregion

		#region Constructors

		public EditViewModel(ETAH.Account account)
		{
			this.firstName                          = account.FirstName;
			this.lastName                           = account.LastName;
			this.email                              = account.Email;
			this.contactNo                          = account.ContactNo;
			this.shippingAddress                    = account.ShippingAddress;
			this.country                            = account.Country;
			this.id                                 = account.ID;
		}

		#endregion

		#region Methods

		public bool Save()
		{
			bool                        result                  = false;

			ETAH.Account                account                 = ETAH.Account.ExecuteCreate(ID);
			ETAH.Account                accByEmail              = ETAH.Account.ExecuteCreateByEmail(Email);

			if (accByEmail == null || (account.Email == accByEmail.Email))
			{
				if (account != null)
				{
					account.Update(
						this.firstName,
						this.lastName,
						this.email,
						this.contactNo,
						this.shippingAddress,
						this.country,
						Common.Session.Account.Status,
						Common.Session.Account.Role,
						this.id);

					this.successMsg                             = Constants.MSG_MANAGE_SUCCESS;
					result                                      = true;
				}
				else
				{
					this.modelError                             = Constants.MSG_MANAGE_FAIL;
				}
			}
			else
			{
				this.modelError                                 = Constants.MSG_MANAGE_FAIL_EMAIL_EXIST;
			}

			return result;
		}

		#endregion

	}
}