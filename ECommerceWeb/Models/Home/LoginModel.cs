using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models.Home
{
	public class LoginModel
	{
		public LoginModel()
		{

		}

		[Required]
		[EmailAddress]
		[Display(Name = "Email", Prompt = "Please enter the email address associated with your account.")]
		public string EmailAddress
		{
			get;
			set;
		}

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password", Prompt = "Please enter the password that you were given when your account was created.")]
		public string Password
		{
			get;
			set;
		}
	}
}