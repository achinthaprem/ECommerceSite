using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_DESCRIPTION, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Description")]
		public string Description { get; set; }
		
		[Required]
		[DataType(DataType.Upload)]
		[Display(Name = "Choose Image")]
		public HttpPostedFileBase Image { get; set; }

		[Required]
		[Display(Name = "Status")]
		public bool Status { get; set; }
	}

	public class ValidateImageFileAttribute : RequiredAttribute
	{
		
		public override bool IsValid(object value)
		{
			bool                result              = false;
			var					image				= value as HttpPostedFile;

			if (image == null)
			{
				if (image.FileName.Length > Constants.DB_LENGTH_IMG_NAME)
				{
					if (image.ContentLength > Constants.UPLOAD_IMAGE_MAX_SIZE * 1024 * 1024)
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