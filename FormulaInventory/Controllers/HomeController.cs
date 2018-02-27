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

        public ActionResult UpdateEngineById(int EngineID)
        {
            //1. orm
            Formula1Entities FOrm = new Formula1Entities();

            //find
            Engine ToBeUpdated = FOrm.Engines.Find(EngineID);

            ViewBag.EngineToBeUpdated = ToBeUpdated;

            return View("UpdateEngineView");
        }

        public ActionResult SaveEngineChanges(Engine UpdatedEngine)
        {
            //0. validation
            if (!ModelState.IsValid)
            {


                //over errors for each field in mobdel
                foreach (ModelState S in ModelState.Values)
                {
                    //individual errors for each fields
                }

                //Request.UserHostAddress
                return View("../Shared/Error"); //error page
            }

            //1. orm
            Formula1Entities FOrm = new Formula1Entities();

            //find
            FOrm.Entry(FOrm.Engines.Find(UpdatedEngine.EngineID)).CurrentValues.SetValues(UpdatedEngine);

            //save
            FOrm.SaveChanges();

            //go to customer view (refresh customer data)
            return RedirectToAction("EngineInventory");
        }

        public ActionResult UpdateWheelById(int WheelID)
        {
            //1. orm
            Formula1Entities FOrm = new Formula1Entities();

            //find
            Wheel ToBeUpdated = FOrm.Wheels.Find(WheelID);

            ViewBag.WheelToBeUpdated = ToBeUpdated;

            return View("UpdateWheelView");
        }

        public ActionResult SaveWheelChanges(Wheel UpdatedWheel)
        {
            //0. validation
            if (!ModelState.IsValid)
            {


                //over errors for each field in mobdel
                foreach (ModelState S in ModelState.Values)
                {
                    //individual errors for each fields
                }

                //Request.UserHostAddress
                return View("../Shared/Error"); //error page
            }

            //1. orm
            Formula1Entities FOrm = new Formula1Entities();

            //find
            FOrm.Entry(FOrm.Wheels.Find(UpdatedWheel.WheelID)).CurrentValues.SetValues(UpdatedWheel);

            //save
            FOrm.SaveChanges();

            //go to customer view (refresh customer data)
            return RedirectToAction("WheelInventory");
        }

        public ActionResult DeleteEngineById(int EngineID)
        {
            //1. orm
            Formula1Entities FOrm = new Formula1Entities();

            //2. find what you're looking for THEN remove from list
            //if record has dependencis then delete that first!
            FOrm.Engines.Remove((FOrm.Engines.Find(EngineID)));

            //save changes
            FOrm.SaveChanges();

            //show new refreshed database
            return RedirectToAction("EngineInventory");
        }

        public ActionResult DeleteWheelById(int WheelID)
        {
            //1. orm
            Formula1Entities FOrm = new Formula1Entities();

            //2. find what you're looking for THEN remove from list
            //if record has dependencis then delete that first!
            FOrm.Wheels.Remove((FOrm.Wheels.Find(WheelID)));

            //save changes
            FOrm.SaveChanges();

            //show new refreshed database
            return RedirectToAction("WheelInventory");
        }
    }
}