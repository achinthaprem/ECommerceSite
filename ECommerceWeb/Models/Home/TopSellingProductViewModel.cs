using ECommerceWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models.Home
{
	public class TopSellingProductViewModel
	{

		#region Members

		private int             id              = Constants.DEFAULT_VALUE_INT;
		private string          name            = String.Empty;
		private string          description     = String.Empty;
		private decimal         price           = Constants.DEFAULT_VALUE_DECIMAL;
		private string          imageSrc        = String.Empty;
		private string          category        = String.Empty;
		private int             categoryID      = Constants.DEFAULT_VALUE_INT;
		private bool            status          = Constants.DEFAULT_VALUE_BOOL;
		private int             sellings        = Constants.DEFAULT_VALUE_INT;

		#endregion

		#region Properties

		[Display(Name = "ID")]
		public int ID
		{
			get { return this.id; }
			set { this.id = value; }
		}

		[Display(Name = "Name")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		[Display(Name = "Description")]
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Display(Name = "Price")]
		public decimal Price
		{
			get { return this.price; }
			set { this.price = value; }
		}

		[Display(Name = "Image")]
		public string ImageSrc
		{
			get { return this.imageSrc; }
			set { this.imageSrc = value; }
		}

		[Display(Name = "Category")]
		public string Category
		{
			get { return this.category; }
			set { this.category = value; }
		}

		[Display(Name = "Category ID")]
		public int CategoryID
		{
			get { return this.categoryID; }
			set { this.categoryID = value; }
		}

		[Display(Name = "Status")]
		public bool Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		[Display(Name = "Sellings")]
		public int Sellings
		{
			get { return this.sellings; }
			set { this.sellings = value; }
		}

		#endregion

		#region Constructors

		public TopSellingProductViewModel()	{ }

		#endregion

		#region Methods



		#endregion

	}
}