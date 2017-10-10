using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ECommerce.Tables.Utility.System;
using ECommerceWeb.Common;
using Volume.Toolkit.Paths;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Product
{
	public class ProductViewModel
	{

		#region Members

		private int                     id                      = Constants.DEFAULT_VALUE_INT;
		private string                  name                    = String.Empty;
		private string                  description             = String.Empty;
		private decimal                 price                   = Constants.DEFAULT_VALUE_DECIMAL;
		private string                  imageName               = String.Empty;
		private bool                    status                  = Constants.DEFAULT_VALUE_BOOL;
		private int                     categoryID              = Constants.DEFAULT_VALUE_INT;
		private ETC.Product             entity                  = null;
		private HttpPostedFileBase      image                   = null;
		private ETC.Category            category                = null;
		private SelectList				categoryList            = null;
		private bool                    isEditMode              = false;
		private string                  tempFolder              = String.Empty;

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

		[Required]
		[DataType(DataType.Currency)]
		[RegularExpression(@"^(\d{2,16})(.\d{2})?$", ErrorMessage = "Enter Price with two decimal points.")]
		[Display(Name = "Price")]
		public decimal Price
		{
			get { return this.price; }
			set { this.price = value; }
		}

		[Display(Name = "Image File")]
		[DataType(DataType.Upload)]
		public string Image
		{
			get { return this.imageName; }
			set { this.imageName = value; }
		}

		[Required]
		[Display(Name = "Category")]
		public int Category
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

		#region Helpers

		public bool IsEditMode
		{
			get { return this.isEditMode; }
			set { this.isEditMode = value; }
		}

		public SelectList CategoryList
		{
			get { return this.categoryList; }
			set { this.categoryList = value; }
		}

		public string CategoryName
		{
			get { return this.category.Name ?? null; }
		}

		private string TempFolderPath
		{
			get { return PathUtility.CombinePaths(Config.StoragePathTemp, this.tempFolder); }
		}

		private string TempFolderUrl
		{
			get { return PathUtility.CombineUrls(Config.StorageUrlTemp, this.tempFolder); }
		}

		private string FolderPath
		{
			get { return PathUtility.CombinePaths(Config.StoragePathProduct, this.id.ToString()); }
		}

		private string FolderUrl
		{
			get { return PathUtility.CombineUrls(Config.StorageUrlProduct, this.id.ToString()); }
		}

		#endregion

		#endregion

		#region Constructors

		public ProductViewModel()
		{
			this.categoryList                               = Lists.ListCategories(ETC.Category.ListByStatus(ETC.Category.STATUS_ACTIVE), Lists.SelectorType.WithSelect, null);
		}

		private ProductViewModel(ETC.Product product)
		{
			this.id                                         = product.ID;
			this.name                                       = product.Name;
			this.description                                = product.Description;
			this.price                                      = product.Price;
			this.imageName                                  = product.ImageName;
			this.status                                     = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
			this.categoryID                                 = product.CategoryID;
			this.entity                                     = product;
			this.category                                   = product.ExecuteCreateCategoryByCategoryID();
			this.categoryList                               = Lists.ListCategories(ETC.Category.ListByStatus(ETC.Category.STATUS_ACTIVE), Lists.SelectorType.WithSelect, null);

			this.isEditMode                                 = true;
		}

		#endregion

		#region Execute Create

		public static ProductViewModel ExecuteCreate(int? id)
		{
			ProductViewModel            result                  = null;

			if (id.HasValue)
			{
				ETC.Product             entity                  = ETC.Product.ExecuteCreate(id.Value);

				if (entity != null)
				{
					result                                      = new ProductViewModel(entity);
				}
			}
			else
			{
				result                                          = new ProductViewModel();
			}

			return result;
		}

		#endregion

		#region Methods

		#region List

		public static List<ProductViewModel> List()
		{
			List<ProductViewModel>          result              = new List<ProductViewModel>();
			List<ETC.Product>               list                = ETC.Product.List();

			foreach (ETC.Product item in list)
			{
				result.Add(new ProductViewModel(item));
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

		public void Sync(ProductViewModel model)
		{
			this.name               = model.name;
			this.description        = model.description;
			this.price              = model.price;

			if (!String.IsNullOrEmpty(model.Image))
			{
				this.imageName      = model.imageName;
			}

			this.status             = model.status;
			this.categoryID         = model.categoryID;
		}

		#endregion

		#region Save

		public void Save()
		{
			if (this.id != Constants.DEFAULT_VALUE_INT)
			{
				ETC.Product             product             = this.entity ?? ETC.Product.ExecuteCreate(this.id);

				if (product != null)
				{
					if (this.image != null)
					{
						SaveImg();
					}

					product.Update(
						this.categoryID,
						this.name,
						this.description,
						this.price,
						this.imageName,
						this.status ? ETC.Product.STATUS_ACTIVE : ETC.Product.STATUS_INACTIVE,
						Common.Session.Account.ID);
				}
			}
			else
			{
				ETC.Product             product             = ETC.Product.ExecuteCreate(
																this.categoryID,
																this.name,
																this.description,
																this.price,
																this.imageName,
																this.status ? ETC.Product.STATUS_ACTIVE : ETC.Product.STATUS_INACTIVE,
																Common.Session.Account.ID,
																Common.Session.Account.ID);
				product.Insert();
				this.id                                     = product.ID;

				if (this.image != null)
				{
					SaveImg();
				}
			}

			if (!String.IsNullOrEmpty(this.TempFolderPath) && Directory.Exists(this.TempFolderPath))
			{
				Directory.Delete(this.TempFolderPath, true);
			}
		}

		private void SaveImg()
		{
			if (!Directory.Exists(this.FolderPath))
			{
				Directory.CreateDirectory(this.FolderPath);
			}

			File.Copy(PathUtility.CombinePaths(this.TempFolderPath, this.imageName),
					  PathUtility.CombinePaths(this.FolderPath, this.imageName));
		}

		#endregion

		#region Delete

		public void Delete()
		{
			if (this.entity != null)
			{
				this.entity.Delete();

				Directory.Delete(this.FolderPath, true);
			}
		}

		#endregion

		#region Upload

		public void Upload(HttpPostedFileBase file)
		{
			if (String.IsNullOrEmpty(this.tempFolder))
			{
				this.tempFolder                 = Guid.NewGuid().ToString();
			}

			if (!Directory.Exists(this.TempFolderPath))
			{
				Directory.CreateDirectory(this.TempFolderPath);
			}

			this.image                          = file;
			this.imageName                      = file.FileName;

			file.SaveAs(PathUtility.CombinePaths(this.TempFolderPath, this.imageName));
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
				result                              = PathUtility.CombineUrls(this.FolderUrl, this.imageName);
			}
			else if (this.image != null)
			{
				result                              = PathUtility.CombineUrls(this.TempFolderUrl, this.imageName);
			}

			return result;
		}

		#endregion

		#endregion

	}
}