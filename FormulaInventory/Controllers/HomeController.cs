using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormulaInventory.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddNewEngine()
        { 
            return View("AddEngineView");
        }

        public ActionResult AddNewWheel()
        {
            return View("AddWheelView");
        }

        public ActionResult AddToInventory()
        {
            return View("AddToInventory");
        }
    }
}