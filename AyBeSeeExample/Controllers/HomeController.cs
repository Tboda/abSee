using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abSee;

namespace AyBeSeeExample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Convert(string submit)
		{
            //Tell the ABTester this was the "downloaded" event
			ABTester.Convert(submit);

			TempData["Message"] = "Thanks mate!";

			return RedirectToAction("Index");
		}

    }
}
