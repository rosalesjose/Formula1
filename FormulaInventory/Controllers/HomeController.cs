using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormulaInventory.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FormulaInventory.Controllers
{
    public class HomeController : Controller
    {
        #region BaseViews
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
        #endregion

        #region AddInventory
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

        //[HttpPost]
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

        //[HttpPost]
        //public ActionResult AddImage(File upload, HttpPostedFileBase file)
        //{
        //    if (file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(Server.MapPath("C:/Users/Skeletor/source/repos/FormulaInventory/FormulaInventory/Img"), fileName);
        //        file.SaveAs(path);

        //        return View();
        //    }

        //    //[HttpPost]
        //    //public ActionResult AddImage(Engine model, HttpPostedFileBase image1)
        //    //{
        //    //    var db = new Formula1Entities();

        //    //    if (image1 != null)
        //    //    {
        //    //        model. = new byte[image1.ContentLength];
        //    //    }



        //    //}

        //    //[HttpPost]
        //    //public ActionResult Index(Engine model, HttpPostedFileBase file)
        //    //{
        //    //    Formula1Entities FOrm = new Formula1Entities();
        //    //    var allowedExtensions = new[] {
        //    //    ".Jpg", ".png", ".jpg", "jpeg"
        //    //};
        //    //    FOrm.Id = model["Id"].ToString();
        //    //    FOrm.Image_url = file.ToString(); //getting complete url  
        //    //    FOrm.Name = fc["Name"].ToString();
        //    //    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
        //    //    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
        //    //    if (allowedExtensions.Contains(ext)) //check what type of extension  
        //    //    {
        //    //        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
        //    //        string myfile = name + "_" + FOrm.Id + ext; //appending the name with id  
        //    //                                                    // store the file inside ~/project folder(Img)  
        //    //        var path = Path.Combine(Server.MapPath("~/Img"), myfile);
        //    //        FOrm.Image_url = path;
        //    //        obj.tbl_details.Add(FOrm);
        //    //        obj.SaveChanges();
        //    //        file.SaveAs(path);
        //    //    }
        //    //    else
        //    //    {
        //    //        ViewBag.message = "Please choose only Image file";
        //    //    }
        //    //    return View();
        //    //}
        //}
        #endregion

        #region ViewInventory
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
        #endregion

        #region UpdateDeleteSave
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
        #endregion

        #region F1 API

        public ActionResult ShowInfoForRace(string season)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp("http://ergast.com/api/f1/" + season + ".json");

                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

                //request headers

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader rd = new StreamReader(response.GetResponseStream());

                    string output = rd.ReadToEnd(); //read all the response back

                    //parsing data
                    JObject JParser = JObject.Parse(output);

                    //ViewBag.RawData = JParser["name"]; //single piece of info

                    ViewBag.Season = JParser["MRData"]["RaceTable"]["season"];

                    ViewBag.RInfo = JParser["MRData"]["RaceTable"]["Races"];

                    return View("SeasonView");
                }
                else
                {
                    return View("../Shared/Error");
                }
            }
            catch (Exception)
            {

                return View("../Shared/Error");
            }
        }

        public ActionResult ShowConstructors()
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp("https://ergast.com/api/f1/2018.json");

                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader rd = new StreamReader(response.GetResponseStream());

                    string output = rd.ReadToEnd();

                    JObject JParser = JObject.Parse(output);

                    ViewBag.Schedule = JParser["MRData"]["RaceTable"]["Races"];

                    return View("CarConstructors");
                }
                else
                {
                    return View("../Shared/Error");
                }
            }
            catch (Exception)
            {

                return View("../Shared/Error");
            }
        }

        #endregion

    }
}