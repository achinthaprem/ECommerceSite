﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using ECommerce.Tables.Utility.System;
using ECommerceWeb.Common;
using Volume.Toolkit.Paths;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Category
{
	public class CategoryViewModel
	{

		#region Members

		private int                     id                      = Constants.DEFAULT_VALUE_INT;
		private string                  name                    = String.Empty;
		private string                  description             = String.Empty;
		private string                  imageName               = String.Empty;
		private bool                    status                  = Constants.DEFAULT_VALUE_BOOL;
		private ETC.Category            entity                  = null;
		private HttpPostedFileBase      image                   = null;
		private bool                    isEditMode              = false;

		#endregion

		#region Properties

		[Key]
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

		[Display(Name = "Image File")]
		[DataType(DataType.Upload)]
		public string Image
		{
			get { return this.imageName; }
			set { this.imageName = value; }
		}

		[Required]
		[Display(Name = "Status")]
		public bool Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		//public HttpPostedFileBase ImageFile
		//{
		//	get { return this.image; }
		//	set { this.image = value; }
		//}

		#endregion

		#region Helpers

		public bool IsEditMode
		{
			get { return this.isEditMode; }
			set { this.isEditMode = value; }
		}

		#endregion

		#region Constructors

		public CategoryViewModel() { }

		private CategoryViewModel(ETC.Category category)
		{
			this.id                 = category.ID;
			this.name               = category.Name;
			this.description        = category.Description;
			this.imageName          = category.ImageName;
			this.status             = (category.Status == ETC.Category.STATUS_ACTIVE) ? true : false;
			this.entity             = category;

			this.IsEditMode         = true;
		}

		#endregion

		#region Methods

		#region Execute Create

		public static CategoryViewModel ExecuteCreate(int? id)
		{
			CategoryViewModel               result              = null;

			if (id.HasValue)
			{
				ETC.Category                entity              = ETC.Category.ExecuteCreate(id.Value);

				if (entity != null)
				{
					result                                      = new CategoryViewModel(entity);
				}
			}
			else
			{
				result                                          = new CategoryViewModel();
			}

			return result;
		}

		#endregion

		#region List

		public static List<CategoryViewModel> List()
		{
			List<CategoryViewModel>             result                  = new List<CategoryViewModel>();
			List<ETC.Category>                  list                    = ETC.Category.List();

			foreach (ETC.Category item in list)
			{
				result.Add(new CategoryViewModel(item));
			}

			return result;
		}

		#endregion

		#region Validate

		public bool Validate(ModelStateDictionary state)
		{
			bool                    result                  = true;

			if (String.IsNullOrEmpty(this.imageName) ||
				(!this.IsEditMode &&
				this.image == null))
			{
				result                                      &= false;
				state.AddModelError("Image", "Please select an Image to Upload.");
			}

			return result;
		}

		#endregion

		#region Sync

		public void Sync(CategoryViewModel model)
		{
			this.name               = model.name;
			this.description        = model.description;

			if (!String.IsNullOrEmpty(model.Image))
			{
				this.imageName      = model.imageName;
			}

			this.status             = model.status;
		}

		#endregion

		#region Save

		public void Save()
		{
			if (this.id != Constants.DEFAULT_VALUE_INT)
			{
				ETC.Category            category            = this.entity ?? ETC.Category.ExecuteCreate(this.id);

				if (category != null)
				{
					if (this.image != null)
					{
						string          path                = $@"Images\Category\{category.ID}";
						Func.SaveImage(this.image, path, this.imageName);
					}

					category.Update(
						this.name,
						this.description,
						this.imageName,
						this.status ? ETC.Category.STATUS_ACTIVE : ETC.Category.STATUS_INACTIVE,
						Common.Session.Account.ID);
				}
			}
			else
			{
				ETC.Category            category            = ETC.Category.ExecuteCreate(
																this.name,
																this.description,
																this.imageName,
																this.status ? ETC.Category.STATUS_ACTIVE : ETC.Category.STATUS_INACTIVE,
																Common.Session.Account.ID,
																Common.Session.Account.ID);
				category.Insert();

				if (this.image != null)
				{
					string				path                = $@"Images\Category\{category.ID}";
					Func.SaveImage(this.image, path, this.imageName);
				}
			}
		}

		#endregion

		#region Delete

		public void Delete()
		{
			if (this.entity != null)
			{
				this.entity.Delete();
			}
		}

		#endregion

		#region Upload

		public void Upload(HttpPostedFileBase file)
		{
			this.image                      = file;
		}

		#endregion

		#region Get Image Url

		/// <summary>
		/// Get Image Url of the instance
		/// </summary>
		/// <returns></returns>
		public string GetImageSrc()
		{
			string              result              = Constants.DEFAULT_IMAGE_URL;

			if (this.entity != null)
			{
				result                              = PathUtility.CombineUrls(
														Config.StorageUrl,
														$@"Images/Category/{this.entity.ID}/{this.entity.ImageName}");
			}

			return result;
		}

		#endregion

		#endregion

	}
}