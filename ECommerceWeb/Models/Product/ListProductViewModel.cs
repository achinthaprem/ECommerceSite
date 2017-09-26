using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Product
{
	public class ListProductViewModel
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

		public ListProductViewModel() { }

		private ListProductViewModel(ETC.Product product)
		{
			this.ID                                 = product.ID;
			this.Name                               = product.Name;
			this.Description                        = product.Description;
			this.Price                              = product.Price;
			this.ImageSrc                           = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
			this.Category                           = product.ExecuteCreateCategoryByCategoryID().Name;
			this.Status                             = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
		}

		#endregion

		#region Methods

		public static List<ListProductViewModel> GetList()
		{
			List<ListProductViewModel>          result                  = new List<ListProductViewModel>();
			List<ETC.Product>                   products                = ETC.Product.List();

			foreach(ETC.Product product in products)
			{
				result.Add(new ListProductViewModel(product));
			}

			return result;
		}

		#endregion

	}
}