using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Category
{
	public class ListCategoryViewModel
	{
		#region Properties

		[Display(Name = "ID")]
		public int ID { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Image")]
		public string ImageSrc { get; set; }

		[Display(Name = "Status")]
		public bool Status { get; set; }

		#endregion

		#region Constructors

		public ListCategoryViewModel() { }

		private ListCategoryViewModel(ETC.Category category)
		{
			if (category != null)
			{
				this.ID                 = category.ID;
				this.Name               = category.Name;
				this.Description        = category.Description;
				this.ImageSrc           = $@"~/Filestore/Images/Category/{category.ID}/{category.ImageName}";
				this.Status             = (category.Status == ETC.Category.STATUS_ACTIVE) ? true : false;
			}
		}

		#endregion

		#region Methods

		public static List<ListCategoryViewModel> GetList()
		{
			List<ListCategoryViewModel>             result				= new List<ListCategoryViewModel>();
			List<ETC.Category>                      list                = ETC.Category.List();

			foreach (ETC.Category category in list)
			{
				result.Add(new ListCategoryViewModel(category));
			}

			return result;
		}

		#endregion

	}
}