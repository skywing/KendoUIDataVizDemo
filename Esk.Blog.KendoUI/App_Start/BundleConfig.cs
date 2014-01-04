using System.Web;
using System.Web.Optimization;

namespace Esk.Blog.KendoUI
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			//blog demo site assets.
			bundles.Add(new ScriptBundle("~/bundles/dashboard-core").Include(
						"~/Scripts/dashboard/dashboard-core.js"));

			bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
						"~/Scripts/dashboard/dashboard-chart.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

			//Kendo Dataviz assets. Not included in the source since it is not open source.
			bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
					  "~/Scripts/kendo/kendo.dataviz.min.js"));

			bundles.Add(new StyleBundle("~/Content/kendo").Include(
					  "~/Content/kendo/kendo.dataviz.min.css"));

		}
	}
}
