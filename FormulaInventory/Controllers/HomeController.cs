using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormulaInventory.Models;

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

        public ActionResult EngineView()
        {
            return View("AddEngineView");
        }

        public ActionResult WheelView()
        {
            return View("AddWheelView");
        }

        public ActionResult AddToInventory()
        {
            return View("AddToInventory");
        }

        public ActionResult AddNewEngine(Engine newEngine, int EngineID, float Price)
        {
            Formula1Entities FOrm = new Formula1Entities();
            FOrm.Engines.Add(newEngine);
            FOrm.SaveChanges();

            ViewBag.EngineID = EngineID;
            ViewBag.ECost = Price;

            return View("EngineReview");
        }

        public ActionResult AddNewWheel(Wheel newWheel, int WheelID, float Price)
        {
            Formula1Entities FOrm = new Formula1Entities();
            FOrm.Wheels.Add(newWheel);
            FOrm.SaveChanges();

            ViewBag.WheelID = WheelID;
            ViewBag.WCost = Price;

            return View("WheelReview");
        }

        public ActionResult InventoryView()
        {

            return View("InventoryView");
        }

        public ActionResult EngineInventory()
        {
            Formula1Entities FOrm = new Formula1Entities();

            ViewBag.EngineList = FOrm.Engines.ToList();

            return View("EngineInventoryView");
        }

        public ActionResult WheelInventory()
        {
            Formula1Entities FOrm = new Formula1Entities();

            ViewBag.WheelList = FOrm.Wheels.ToList();

            return View("WheelInventoryView");
        }

    }
}