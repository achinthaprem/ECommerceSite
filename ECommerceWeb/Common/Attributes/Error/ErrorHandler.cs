using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volume.Logger;

namespace ECommerceWeb.Common.Attributes.Error
{
	/*http://stackoverflow.com/questions/22444912/mvc-errorhandling-attribute-returning-json-results-to-ajax-requests-works-only-o*/
	public class ErrorHandler : FilterAttribute, IExceptionFilter
	{

		#region Methods 

		/// <summary>
		/// Store Exception Details 
		/// </summary>
		/// <param name="filterContext"></param>
		public void OnException(ExceptionContext filterContext)
		{
			Log.Error(filterContext.Exception, "Error in processing request");
		}

		#endregion
	}
}