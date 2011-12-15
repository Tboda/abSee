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
            ABTester.Start();
            return View();
        }

        public ActionResult Results()
        {
            return View();
        }

        public ActionResult ExampleTest1()
        {
            return View();
        }

        public ActionResult ExampleTest2()
        {
            return View();
        }

        public ActionResult ExampleTest3()
        {
            return View();
        }

        public ActionResult ConvertTest1()
        {
            //Tell the ABTester this was the "downloaded" event
            ABTester.Convert("exampletest1");

            return RedirectToAction("Results");
        }

        public ActionResult ConvertTest2()
        {
            //Tell the ABTester this was the "downloaded" event
            ABTester.Convert("exampletest2");

            return RedirectToAction("Results");
        }

        public ActionResult ConvertTest3()
        {
            //Tell the ABTester this was the "downloaded" event
            ABTester.Convert("exampletest3");

            return RedirectToAction("Results");
        }

    }
}
