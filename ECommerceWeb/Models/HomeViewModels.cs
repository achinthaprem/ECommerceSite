using ECommerceWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ECommerceWeb.Models.Home
{
	public class CreateContactFormViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[StringLength(Constants.DB_LENGTH_EMAIL, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[StringLength(Constants.DB_LENGTH_CONTACT_NO, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Contact No")]
		public string ContactNo { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(50, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Subject")]
		public string Subject { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(250, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Message")]
		public string Message { get; set; }
	}

	public class ListContactFormViewModel
	{
		[Display(Name = "ID")]
		public int ID { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "Contact No")]
		public string ContactNo { get; set; }

		[Display(Name = "Subject")]
		public string Subject { get; set; }

		[Display(Name = "Message")]
		public string Message { get; set; }

		[Display(Name = "Date")]
		public DateTime Date { get; set; }

		[HiddenInput]
		public int ReadStatus { get; set; }
	}

	public class TopSellingProductViewModel
	{
		[Display(Name = "ID")]
		public int ID { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Price")]
		public decimal Price { get; set; }

		[Display(Name = "Image")]
		public string ImageSrc { get; set; }

		[Display(Name = "Category")]
		public string Category { get; set; }

		[Display(Name = "Category ID")]
		public int CategoryID { get; set; }

		[Display(Name = "Status")]
		public bool Status { get; set; }

		[Display(Name = "Sellings")]
		public int Sellings { get; set; }
	}
}