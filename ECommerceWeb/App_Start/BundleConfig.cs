using System.Web;
using System.Web.Optimization;

namespace ECommerceWeb
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery.validate*",
						"~/Scripts/jquery.unobtrusive-ajax.js"));

			//bundles.Add(new ScriptBundle("~/bundles/toolkit").Include(
			//			"~/Scripts/Volume/Toolkit/*.js"));

			//bundles.Add(new ScriptBundle("~/bundles/toolkit").IncludeDirectory("~/Scripts/Volume/Toolkit", "volume.toolkit.*"));

			bundles.Add(new ScriptBundle("~/bundles/toolkit").Include("~/Scripts/Volume/Toolkit/volume.toolkit.js").IncludeDirectory("~/Scripts/Volume/Toolkit", "*.js"));

			bundles.Add(new ScriptBundle("~/bundles/application").Include(
						"~/Scripts/Volume/app.*",
						"~/Content/scripts/script.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/styles").Include(
					  "~/Content/CSS/*.css"));
		}
	}
}
