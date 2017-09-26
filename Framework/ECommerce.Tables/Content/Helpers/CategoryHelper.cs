using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace ECommerce.Tables.Content.Helpers
{
	public class CategoryHelper
	{

		#region Constructors

		public CategoryHelper() { }

		#endregion

		#region Public Access Methods

		/// <summary>
		/// Get Category by ID
		/// </summary>
		/// <param name="ID">ID of the Category</param>
		/// <returns></returns>
		public Task<Category> GetCategoryAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Category.ExecuteCreate(ID);
			});
		}

		/// <summary>
		/// Create Category
		/// </summary>
		/// <param name="Name">Name</param>
		/// <param name="Description">Description</param>
		/// <param name="Image">Image</param>
		/// <param name="Status">Status</param>
		/// <param name="CreatingAccountID">Account ID of whose creating</param>
		/// <returns></returns>
		public Task<bool> CreateCategoryAsync(
			string Name,
			string Description,
			HttpPostedFileBase Image,
			bool Status,
			int CreatingAccountID)
		{
			return Task.Run(() =>
			{
				Category            category                = Category.ExecuteCreate(
																				Name,
																				Description,
																				Image.FileName,
																				((Status)?Category.STATUS_ACTIVE:Category.STATUS_INACTIVE),
																				CreatingAccountID,
																				CreatingAccountID);
				category.Insert();

				string              path                    = $@"Images\Category\{category.ID}";
				CommonHelper.SaveImage(Image, path, Image.FileName);

				return (category.ID != -1) ? true : false;
			});
		}

		/// <summary>
		/// Update Category
		/// </summary>
		/// <param name="ID">Category ID about to Update</param>
		/// <param name="Name">New Name</param>
		/// <param name="Description">New Description</param>
		/// <param name="Image">New Image</param>
		/// <param name="Status">New Status</param>
		/// <param name="ModifyingAccountID">Account ID of whose modifying</param>
		/// <returns></returns>
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

					category.Update(
								Name,
								Description,
								imageName,
								((Status == true) ? Category.STATUS_ACTIVE : Category.STATUS_INACTIVE),
								ModifyingAccountID);

					result                                  = true;
				}

				return result;
			});
		}
		/// <summary>
		/// Delete Category
		/// </summary>
		/// <param name="ID">Category ID about to Delete</param>
		/// <returns></returns>
		public Task<bool> DeleteCategoryAsync(int ID)
		{
			return Task.Run(() =>
			{
				Category            category                = Category.ExecuteCreate(ID);
				category.Delete();

				category                                    = Category.ExecuteCreate(ID);

				return (category == null) ? true : false;
			});
		}

		/// <summary>
		/// Get Category List
		/// </summary>
		/// <returns></returns>
		public Task<List<Category>> GetCategoryListAsync()
		{
			return Task.Run(() =>
			{
				return Category.List();
			});
		}

		#endregion

	}
}
