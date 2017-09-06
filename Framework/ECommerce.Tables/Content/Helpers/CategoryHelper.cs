using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ECommerce.Tables.Content.Helpers
{
	public class CategoryHelper
	{
		public CategoryHelper()
		{
		}

		public Task<Category> GetCategoryAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Category.ExecuteCreate(ID);
			});
		}

		public Task<bool> CreateCategoryAsync(
			string Name,
			string Description,
			HttpPostedFileBase Image,
			bool Status,
			int CreatingAccountID)
		{
			return Task.Run(() =>
			{
				Category            category                = Category.ExecuteCreate(Name, Description, Image.FileName, ((Status)?Category.STATUS_ACTIVE:Category.STATUS_INACTIVE), CreatingAccountID, CreatingAccountID);
				category.Insert();

				string              path                    = $@"Images\Category\{category.ID}";
				CommonHelper.SaveImage(Image, path, Image.FileName);

				return (category.ID != -1) ? true : false;
			});
		}

		public Task<bool> UpdateCategoryAsync(
			int ID,
			string Name,
			string Description,
			HttpPostedFileBase Image,
			bool Status,
			int ModifyingAccountID
			)
		{
			return Task.Run(() =>
			{
				Category            category                = Category.ExecuteCreate(ID);
				bool                result                  = false;

				if (category != null)
				{
					string          imageName               = String.Empty;

					if (Image == null)
					{
						imageName                           = category.ImageName;
					}
					else
					{
						imageName                           = Image.FileName;

						string      path                    = $@"Images\Category\{category.ID}";
						CommonHelper.SaveImage(Image, path, imageName);
					}

					category.Update(Name, Description, imageName, ((Status == true) ? Category.STATUS_ACTIVE : Category.STATUS_INACTIVE), ModifyingAccountID);

					result                                  = true;
				}

				return result;
			});
		}

		public Task<List<Category>> GetCategoryListAsync()
		{
			return Task.Run(() =>
			{
				return Category.List();
			});
		}
	}
}
