using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECommerceWeb.Common;

namespace ECommerceWeb.Models.Account
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[StringLength(Constants.DB_LENGTH_EMAIL, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}

	public class ChangePasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		[StringLength(Constants.DB_LENGTH_PASSWORD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Current Password")]
		public string CurrentPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(Constants.DB_LENGTH_PASSWORD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "New Password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
		[Display(Name = "Confirm password")]
		public string ConfirmPassword { get; set; }

		[System.Web.Mvc.HiddenInput(DisplayValue = false)]
		public string ID { get; set; }
	}

	public class LoginViewModel
	{
		[Required]
		[EmailAddress]
		[StringLength(Constants.DB_LENGTH_EMAIL, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(Constants.DB_LENGTH_PASSWORD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_FNAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_LNAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(Constants.DB_LENGTH_EMAIL, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(Constants.DB_LENGTH_PASSWORD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		[Display(Name = "Confirm password")]
		public string ConfirmPassword { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[StringLength(Constants.DB_LENGTH_CONTACT_NO, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Contact No")]
		public string ContactNo { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(Constants.DB_LENGTH_SHIP_ADD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Shipping Address")]
		public string ShippingAddress { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_COUNTRY, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Country")]
		public string Country { get; set; }
	}

	public class EditViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_FNAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_LNAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(Constants.DB_LENGTH_EMAIL, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Email")]
		public string Email { get; set; }
		
		[Required]
		[DataType(DataType.PhoneNumber)]
		[StringLength(Constants.DB_LENGTH_CONTACT_NO, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Contact No")]
		public string ContactNo { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(Constants.DB_LENGTH_SHIP_ADD, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Shipping Address")]
		public string ShippingAddress { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_COUNTRY, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Country")]
		public string Country { get; set; }

		[System.Web.Mvc.HiddenInput(DisplayValue = false)]
		public string ID { get; set; }
	}
}