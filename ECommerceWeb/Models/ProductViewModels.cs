using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using ECommerceWeb.Common;

namespace ECommerceWeb.Models.Product
{
	public class AddProductViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_NAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(Constants.DB_LENGTH_DESCRIPTION, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[RegularExpression(@"^(\d{2,16})(.\d{2})?$", ErrorMessage = "Enter Price with two decimal points.")]
		[Display(Name = "Price")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[ValidateImageFile(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[Display(Name = "Choose Image")]
		public HttpPostedFileBase Image { get; set; }

		public List<SelectListItem> CategoryList { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int CategoryID { get; set; }

		[Required]
		[Display(Name = "Status")]
		public bool Status { get; set; }
	}

	public class EditProductViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int ID { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_NAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(Constants.DB_LENGTH_DESCRIPTION, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[RegularExpression(@"^(\d{2,16})(.\d{2})?$", ErrorMessage = "Enter Price with two decimal points.")]
		[Display(Name = "Price")]
		public decimal Price { get; set; }

		public string ImageSrc { get; set; }

		[ValidateImageFile(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[Display(Name = "Choose Image")]
		public HttpPostedFileBase Image { get; set; }

		public List<SelectListItem> CategoryList { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int CategoryID { get; set; }

		[Required]
		[Display(Name = "Status")]
		public bool Status { get; set; }
	}

	public class ListProductViewModel
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

		[Display(Name = "Status")]
		public bool Status { get; set; }
	}

	public class DeleteProductViewModel
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

		[Display(Name = "Status")]
		public bool Status { get; set; }
	}
}