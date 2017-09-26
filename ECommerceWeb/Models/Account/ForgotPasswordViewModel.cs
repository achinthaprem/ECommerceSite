using ECommerceWeb.Common;
using System.ComponentModel.DataAnnotations;

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
}