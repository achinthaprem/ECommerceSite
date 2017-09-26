using ECommerceWeb.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Product
{
	public class EditProductViewModel
	{

		#region Members

		private int						id						= Constants.DEFAULT_VALUE_INT;
		private string					name					= String.Empty;
		private string					description				= String.Empty;
		private decimal					price					= Constants.DEFAULT_VALUE_DECIMAL;
		private string                  imageSrc                = String.Empty;
		private HttpPostedFileBase      image                   = null;
		private List<SelectListItem>    categoryList            = null;
		private int                     categoryID              = Constants.DEFAULT_VALUE_INT;
		private bool					status                  = Constants.DEFAULT_VALUE_BOOL;

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

		[Required]
		[DataType(DataType.Currency)]
		[RegularExpression(@"^(\d{2,16})(.\d{2})?$", ErrorMessage = "Enter Price with two decimal points.")]
		[Display(Name = "Price")]
		public decimal Price
		{
			get { return this.price; }
			set { this.price = value; }
		}

		public string ImageSrc
		{
			get { return this.imageSrc; }
			set { this.imageSrc = value; }
		}

		[ValidateImageFile(ErrorMessage = "Please select a PNG or JPG file smaller than 5MB.")]
		[Display(Name = "Choose Image")]
		public HttpPostedFileBase Image
		{
			get { return this.image; }
			set { this.image = value; }
		}

		public List<SelectListItem> CategoryList
		{
			get { return this.categoryList; }
			set { this.categoryList = value; }
		}

		[Required]
		[Display(Name = "Category")]
		public int CategoryID
		{
			get { return this.categoryID; }
			set { this.categoryID = value; }
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

		public EditProductViewModel() { }

		private EditProductViewModel(ETC.Product product)
		{
			this.id                                         = product.ID;
			this.name                                       = product.Name;
			this.description                                = product.Description;
			this.price                                      = product.Price;
			this.imageSrc                                   = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
			this.categoryList                               = Func.ConvertCategoriesToSelectList(ETC.Category.ListByStatus(ETC.Category.STATUS_ACTIVE));
			this.categoryID                                 = product.CategoryID;
			this.status                                     = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
		}

		#endregion

		#region Methods

		public static EditProductViewModel ExecuteCreate (int productID)
		{
			ETC.Product                 product                 = ETC.Product.ExecuteCreate(productID);

			return (product != null) ? new EditProductViewModel(product) : null;
		}

		public bool Save()
		{
			bool                        result                  = false;

			ETC.Product                 product                 = ETC.Product.ExecuteCreate(this.id);

			if (product != null)
			{
				string					imageName               = String.Empty;

				if (Image == null)
				{
					imageName									= product.ImageName;
				}
				else
				{
					imageName									= Image.FileName;

					string				path                    = $@"Images\Product\{product.ID}";
					Func.SaveImage(Image, path, imageName);
				}

				product.Update(
					this.categoryID, 
					this.name,
					this.description, 
					this.price, imageName, 
					((Status == true) ? ETC.Product.STATUS_ACTIVE : ETC.Product.STATUS_INACTIVE), 
					Common.Session.Account.ID);

				result                                          = true;
			}

			return result;
		}

		#endregion

	}
}