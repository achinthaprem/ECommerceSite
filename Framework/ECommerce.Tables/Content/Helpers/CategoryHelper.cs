using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
