using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace ECommerceWeb.Common
{
	public class CustomValidations
	{
	}

	public class ValidateImageFileAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			bool                result              = false;
			var                 image               = value as HttpPostedFileBase;

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
								result              = img.RawFormat.Equals(ImageFormat.Png) || img.RawFormat.Equals(ImageFormat.Jpeg);
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