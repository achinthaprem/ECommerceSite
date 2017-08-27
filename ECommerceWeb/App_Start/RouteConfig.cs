using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ECommerceWeb.Common;

namespace ECommerceWeb
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new {
					controller		= Constants.CONTROLLER_HOME,
					action			= Constants.ACTION_INDEX,
					id				= UrlParameter.Optional
				}
			);
		}
	}
}
