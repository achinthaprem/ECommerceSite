using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using ECommerceWeb.Common;


namespace ECommerceWeb.Models.Shop
{
	public class ProductViewModel
	{
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

		[Display(Name = "Category ID")]
		public string CategoryID { get; set; }

		[Display(Name = "Status")]
		public bool Status { get; set; }
	}
}