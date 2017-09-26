using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWeb.Models.Home
{
	public class ListContactFormViewModel
	{
		[Display(Name = "ID")]
		public int ID { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "Contact No")]
		public string ContactNo { get; set; }

		[Display(Name = "Subject")]
		public string Subject { get; set; }

		[Display(Name = "Message")]
		public string Message { get; set; }

		[Display(Name = "Date")]
		public DateTime Date { get; set; }

		[HiddenInput]
		public int ReadStatus { get; set; }
	}
}