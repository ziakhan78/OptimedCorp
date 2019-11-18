using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OptimedCorporation.Models;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace OptimedCorporation.Controllers
{
    public class HomeController : Controller
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();


        public ActionResult Index()
        {
            //List<SelectListItem> state = db.States.ToList();
            //List<SelectListItem> city = db.Cities.ToList();

            //Web api call its working
            //var client = new HttpClient();
            //client.BaseAddress = new Uri("https://optimedcorp.com/");
            //HttpResponseMessage resp = client.GetAsync("api/Employees").Result;
            //var result = resp.Content.ReadAsAsync<IEnumerable<Feedback>>().Result;
            //ViewBag.api = result;

            BindStateFromSL();

            ViewBag.State = new SelectList(db.States, "StateId", "StateName");
            db.Banners.ToList();
            db.News.ToList();
            db.Events.ToList();
            db.EventImages.ToList();
            db.StoreLocators.ToList();
            db.Feedback.ToList();
            db.Careers.ToList();
            db.States.ToList();
            db.Cities.ToList();
            db.Admins.ToList();

            string menu = "Home";
            return View(db.Banners.Where(x => x.Menu == menu && x.Status == true).ToList());
            

            //return View();
        }

        [NonAction]
        public void BindStateFromSL()
        {
            var stateFromSL = (from d in db.StoreLocators select d.State).Distinct();
            var state1 = db.States.ToList();

            var stateList = db.States.Where(f => stateFromSL.Contains(f.StateName)).ToList();
            ViewBag.StateList1 = stateList;
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult OurStory()
        {
            return View();
        }

        public ActionResult Brands()
        {
            return View();
        }

        public ActionResult Events()
        {
           
            //EventsVM eventVm = new EventsVM();
            //eventVm.Events=db.Events.ToList();
            //eventVm.EventImages = db.EventImages.ToList();
            //return View(eventVm);

           return View(db.Events.ToList());
            //return View();
        }

        public ActionResult Newsletter()
        {
            return View();
        }

        public ActionResult StoreLocator()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Careers()
        {
            return View();
        }

        public ActionResult Sitemap()
        {
            return View();
        }

        public ActionResult BindStoreLocator(FormCollection form)
        {
            TempData["State"] = form["state"].ToString();
            TempData["City"] = form["city"];
            TempData["Pin"] = form["pin"];

            return RedirectToAction("Index", "StoreLocators");
        }       
    }
}