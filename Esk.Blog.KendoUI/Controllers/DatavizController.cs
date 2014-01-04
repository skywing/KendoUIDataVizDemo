using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Esk.Blog.KendoUI.Controllers
{
    public class DatavizController : Controller
    {
        //
        // GET: /Dataviz/
        public ActionResult RealTimeDonutChart()
        {
            return View();
        }

	}
}