using System.ComponentModel.DataAnnotations;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Category
{
	public class DeleteCategoryViewModel
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

		public DeleteCategoryViewModel() { }

		private DeleteCategoryViewModel(ETC.Category category)
		{
			this.ID                         = category.ID;
			this.Name                       = category.Name;
			this.Description                = category.Description;
			this.ImageSrc                   = $@"~/Filestore/Images/Category/{category.ID}/{category.ImageName}";
			this.Status                     = (category.Status == ETC.Category.STATUS_ACTIVE) ? true : false;
		}

		#endregion

		#region Methods

		public static DeleteCategoryViewModel ExecuteCreate(int categoryID)
		{
			ETC.Category                category                = ETC.Category.ExecuteCreate(categoryID);

			return (category != null) ? new DeleteCategoryViewModel(category) : null;
		}

		public bool Delete()
		{
			bool                        result                  = false;

			ETC.Category.ExecuteCreate(this.ID).Delete();
			result                                              = true;

			return result;
		}

		#endregion

	}
}