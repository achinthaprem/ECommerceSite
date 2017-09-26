using ECommerceWeb.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Home
{
	public class CreateContactFormViewModel
	{

		#region Members

		private string              name                = String.Empty;
		private string              email               = String.Empty;
		private string              contactNo           = String.Empty;
		private string              subject             = String.Empty;
		private string              message             = String.Empty;


		#endregion

		#region Properties

		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Name")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		[Required]
		[DataType(DataType.EmailAddress)]
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
		[DataType(DataType.Text)]
		[StringLength(50, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Subject")]
		public string Subject
		{
			get { return this.subject; }
			set { this.subject = value; }
		}

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(250, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Message")]
		public string Message
		{
			get { return this.message; }
			set { this.message = value; }
		}

		#endregion

		#region Constructors

		public CreateContactFormViewModel() { }

		#endregion

		#region Methods

		public bool Save()
		{
			bool                    result                  = false;
			ETC.Contact             contact                 = ETC.Contact.ExecuteCreate(
																this.name, 
																this.email, 
																this.contactNo, 
																this.subject, 
																this.message, 
																ETC.Contact.STATUS_UNREAD);
			contact.Insert();
			result                                          = true;
			return result;
		}

		#endregion

	}
}