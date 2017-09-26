using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace ECommerceWeb.Common
{
	public class ValidateImageFileAttribute : ValidationAttribute
	{
		/// <summary>
		/// Validates the Image File
		/// </summary>
		/// <param name="value">Uploaded Image File</param>
		/// <returns></returns>
		public override bool IsValid(object value)
		{
			bool                result              = false;
			var                 image               = value as HttpPostedFileBase;

			if (image != null)
			{
				if (image.FileName.Length < 500) // Checks for file name length with database limits
				{
					if (image.ContentLength < 5 * 1024 * 1024) // Checks for file size (5MB max)
					{
						try
						{
							using (var img = Image.FromStream(image.InputStream))
							{
								// Checks for Image format (Only JPG/JPEG or PNG are accepted)
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