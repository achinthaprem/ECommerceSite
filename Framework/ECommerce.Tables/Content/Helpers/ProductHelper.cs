using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ECommerce.Tables.Content.Helpers
{
	public class ProductHelper
	{
		public ProductHelper()
		{
		}

		public Task<Product> GetProductAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Product.ExecuteCreate(ID);
			});
		}

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

		public Task<bool> DeleteProductAsync(int ID)
		{
			return Task.Run(() =>
			{
				Product				product					= Product.ExecuteCreate(ID);
				product.Delete();

				product										= Product.ExecuteCreate(ID);

				return (product == null) ? true : false;
			});
		}

		public Task<List<Product>> GetProductListAsync()
		{
			return Task.Run(() =>
			{
				return Product.List();
			});
		}
	}
}
