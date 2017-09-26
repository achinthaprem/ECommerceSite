using ECommerceWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Category
{
	public class AddCategoryViewModel
	{
		#region Members

		private string              name                = String.Empty;
		private string              description         = String.Empty;
		private HttpPostedFileBase  image               = null;
		private bool                status              = false;

		#endregion

		#region Properties

		[Required]
		[DataType(DataType.Text)]
		[StringLength(Constants.DB_LENGTH_NAME, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Name")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		[Required]
		[DataType(DataType.MultilineText)]
		[StringLength(Constants.DB_LENGTH_DESCRIPTION, ErrorMessage = "Maximum allowed length is {1} characters")]
		[Display(Name = "Description")]
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Required(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[ValidateImageFile(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[Display(Name = "Choose Image")]
		public HttpPostedFileBase Image
		{
			get { return this.image; }
			set { this.image = value; }
		}

		[Required]
		[Display(Name = "Status")]
		public bool Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		#endregion

		#region Constructors

		public AddCategoryViewModel()
		{
			this.status                     = true;
		}

		#endregion

		#region Methods

		public bool Save()
		{
			ETC.Category            category                = ETC.Category.ExecuteCreate(
																this.name, 
																this.description, 
																this.image.FileName, 
																Status ? ETC.Category.STATUS_ACTIVE : ETC.Category.STATUS_INACTIVE,
																Common.Session.Account.ID,
																Common.Session.Account.ID);
			category.Insert();

			string					path                    = $@"Images\Category\{category.ID}";
			Func.SaveImage(this.image, path, this.image.FileName);
			
			return (category.ID != -1) ? true : false;
		}

		#endregion

	}
}