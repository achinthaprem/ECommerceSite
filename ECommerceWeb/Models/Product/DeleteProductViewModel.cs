using System.ComponentModel.DataAnnotations;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Product
{
	public class DeleteProductViewModel
	{

		#region Properties

		[Display(Name = "ID")]
		public int ID { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Price")]
		public decimal Price { get; set; }

		[Display(Name = "Image")]
		public string ImageSrc { get; set; }

		[Display(Name = "Category")]
		public string Category { get; set; }

		[Display(Name = "Status")]
		public bool Status { get; set; }

		#endregion

		#region Constructors

		public DeleteProductViewModel() { }

		private DeleteProductViewModel(ETC.Product product)
		{
			this.ID                             = product.ID;
			this.Name                           = product.Name;
			this.Description                    = product.Description;
			this.Price							= product.Price;
			this.ImageSrc                       = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
			this.Category                       = product.ExecuteCreateCategoryByCategoryID().Name;
			this.Status                         = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
		}

		#endregion

		#region Methods

		public static DeleteProductViewModel ExecuteCreate(int productID)
		{
			ETC.Product             product             = ETC.Product.ExecuteCreate(productID);

			return (product != null) ? new DeleteProductViewModel(product) : null;
		}

		public bool Delete()
		{
			bool                    result              = false;

			ETC.Product.ExecuteCreate(this.ID).Delete();
			result                                      = true;

			return result;
		}

		#endregion

	}
}