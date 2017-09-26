using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace ECommerce.Tables.Content.Helpers
{
	public class ProductHelper
	{

		#region Constructors

		public ProductHelper() { }

		#endregion

		#region Public Access Methods

		/// <summary>
		/// Get Product by ID
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public Task<Product> GetProductAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Product.ExecuteCreate(ID);
			});
		}

		/// <summary>
		/// Create new Product
		/// </summary>
		/// <param name="CategoryID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="Price"></param>
		/// <param name="Image"></param>
		/// <param name="Status"></param>
		/// <param name="CreatingAccountID"></param>
		/// <returns></returns>
		public Task<bool> CreateProductAsync(
			int CategoryID,
			string Name,
			string Description,
			decimal Price,
			HttpPostedFileBase Image,
			bool Status,
			int CreatingAccountID)
		{
			return Task.Run(() =>
			{
				Product             product                 = Product.ExecuteCreate(
																				CategoryID,
																				Name,
																				Description,
																				Price,
																				Image.FileName,
																				((Status) ? Product.STATUS_ACTIVE : Product.STATUS_INACTIVE),
																				CreatingAccountID,
																				CreatingAccountID);
				product.Insert();

				string              path                    = $@"Images\Product\{product.ID}";
				CommonHelper.SaveImage(Image, path, Image.FileName);

				return (product.ID != -1) ? true : false;
			});
		}

		/// <summary>
		/// Update an Existing Product
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="CategoryID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="Price"></param>
		/// <param name="Image"></param>
		/// <param name="Status"></param>
		/// <param name="ModifingAccountID"></param>
		/// <returns></returns>
		public Task<bool> UpdateProductAsync(
			int ID,
			int CategoryID,
			string Name,
			string Description,
			decimal Price,
			HttpPostedFileBase Image,
			bool Status,
			int ModifingAccountID)
		{
			return Task.Run(() =>
			{
				Product             product                 = Product.ExecuteCreate(ID);
				bool                result                  = false;

				if (product != null)
				{
					string          imageName               = String.Empty;

					if (Image == null)
					{
						imageName                           = product.ImageName;
					}
					else
					{
						imageName                           = Image.FileName;

						string      path                    = $@"Images\Product\{product.ID}";
						CommonHelper.SaveImage(Image, path, imageName);
					}

					product.Update(
						CategoryID,
						Name,
						Description,
						Price,
						imageName,
						((Status == true) ? Product.STATUS_ACTIVE : Product.STATUS_INACTIVE),
						ModifingAccountID);

					result                                  = true;
				}

				return result;
			});
		}

		/// <summary>
		/// Delete an Product
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public Task<bool> DeleteProductAsync(int ID)
		{
			return Task.Run(() =>
			{
				Product             product                 = Product.ExecuteCreate(ID);
				product.Delete();

				product                                     = Product.ExecuteCreate(ID);

				return (product == null) ? true : false;
			});
		}

		/// <summary>
		/// Get Product List
		/// </summary>
		/// <returns></returns>
		public Task<List<Product>> GetProductListAsync()
		{
			return Task.Run(() =>
			{
				return Product.List();
			});
		}

		/// <summary>
		/// Get Product List by Category ID
		/// </summary>
		/// <param name="CategoryID"></param>
		/// <returns></returns>
		public Task<List<Product>> GetProductListByCategoryAsync(int CategoryID)
		{
			return Task.Run(() =>
			{
				return Product.ListByCategoryID(CategoryID);
			});
		}

		#endregion

	}
}
