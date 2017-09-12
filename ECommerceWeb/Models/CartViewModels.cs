using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Cart
{
	public class OrderViewModel
	{
		public int ID { get; set; }

		public int AccountID { get; set; }

		public DateTime DateCreated { get; set; }

		public int Status { get; set; }

		public int PaymentMethod { get; set; }

		public decimal TotalAmount { get; set; }

		public List<OrderItem> OrderItems { get; set; }
	}
}