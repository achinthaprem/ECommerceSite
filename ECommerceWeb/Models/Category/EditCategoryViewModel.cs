using ECommerceWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Category
{
	public class EditCategoryViewModel
	{

		#region Members

		private int                     id                      = Constants.DEFAULT_VALUE_INT;
		private string                  name                    = String.Empty;
		private string                  description             = String.Empty;
		private string                  imageSrc                = String.Empty;
		private HttpPostedFileBase      image                   = null;
		private bool                    status                  = Constants.DEFAULT_VALUE_BOOL;

		#endregion

		#region Properties

		[HiddenInput(DisplayValue = false)]
		public int ID
		{
			get { return this.id; }
			set { this.id = value; }
		}

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

		public string ImageSrc
		{
			get { return this.imageSrc; }
			set { this.imageSrc = value; }
		}

		[ValidateImageFile(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB or Leave field empty to keep the file in databse.")]
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

		public EditCategoryViewModel() { }

		private EditCategoryViewModel(ETC.Category category)
		{
			this.id                 = category.ID;
			this.name               = category.Name;
			this.description        = category.Description;
			this.ImageSrc			= $@"~/Filestore/Images/Category/{category.ID}/{category.ImageName}";
			this.status             = (category.Status == ETC.Category.STATUS_ACTIVE) ? true : false;
		}

		#endregion

		#region Methods

		public static EditCategoryViewModel ExecuteCreate(int categoryID)
		{
			ETC.Category                category                = ETC.Category.ExecuteCreate(categoryID);

			return (category != null) ? new EditCategoryViewModel(category) : null;
		}

		public bool Save()
		{
			bool                        result                  = false;
			ETC.Category                category                = ETC.Category.ExecuteCreate(this.id);

			if (category != null)
			{
				string                  imageName               = String.Empty;

				if (Image == null)
				{
					imageName                                   = category.ImageName;
				}
				else
				{
					imageName                                   = this.image.FileName;

					string				path					= $@"Images\Category\{category.ID}";
					Func.SaveImage(this.image, path, imageName);
				}

				category.Update(
					this.name, 
					this.description, 
					imageName, 
					((Status == true) ? ETC.Category.STATUS_ACTIVE : ETC.Category.STATUS_INACTIVE), 
					Common.Session.Account.ID);
			}

			return result;
		}

		#endregion

	}
}