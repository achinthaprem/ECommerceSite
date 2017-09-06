using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceWeb.Common;
using System.Drawing;
using System.Drawing.Imaging;

namespace ECommerceWeb.Models.Category
{
	public class AddCategoryViewModel
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

		[Required(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[ValidateImageFile(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[Display(Name = "Choose Image")]
		public HttpPostedFileBase Image { get; set; }

		[Required]
		[Display(Name = "Status")]
		public bool Status { get; set; }
	}

	public class EditCategoryViewModel
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

		public string ImageSrc { get; set; }
		
		[ValidateImageFile(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB or Leave field empty to keep the file in databse.")]
		[Display(Name = "Choose Image")]
		public HttpPostedFileBase Image { get; set; }

		[Required]
		[Display(Name = "Status")]
		public bool Status { get; set; }
	}

	public class ListCategoryViewModel
	{
		[Display(Name = "ID")]
		public int ID { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Image")]
		public string ImageSrc { get; set; }

		[Display(Name = "Status")]
		public bool Status { get; set; }
	}

	public class ValidateImageFileAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			bool                result              = false;
			var					image				= value as HttpPostedFileBase;

			if (image != null)
			{
				if (image.FileName.Length < 500)
				{
					if (image.ContentLength < 5 * 1024 * 1024)
					{
						try
						{
							using (var img = Image.FromStream(image.InputStream))
							{
								result				= img.RawFormat.Equals(ImageFormat.Png) || img.RawFormat.Equals(ImageFormat.Jpeg);
							}
						}
						catch
						{
						}
					}
				}
			}
			
			return result;
		}
	}
}