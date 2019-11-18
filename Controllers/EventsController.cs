using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OptimedCorporation.Models;
using PagedList;

namespace OptimedCorporation.Controllers
{
    public class EventsController : Controller
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();

        [NonAction]
        public string GetIpaddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }


        [HttpPost]
        public JsonResult AjaxMethod1(int? id)
        {
            EventImages evImg = db.EventImages.Find(id);
            return Json(evImg);
        }

        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
            Events person = new Events
            {
                Title = name,
                Location = DateTime.Now.ToString()
            };
            return Json(person);
        }

        [HttpPost]
        public ActionResult DisplayEmployees()
        {
            var query = from ee in db.Events
                        select ee;
            return PartialView("Test", query.ToList());
        }

       

        public ActionResult IndexOld()
        {
           

            //EventsVM objP = new EventsVM();

            //List<EventsVM> pe = new List<EventsVM>();

            var events = (from n in db.Events
                   join c in db.EventImages on n.EventId equals c.EventId                  
                  
            select new EventsDetails {EventId= n.EventId,Description= n.Description, Location= n.Location, Title= n.Title, EventDate= n.EventDate,Thumbnail= n.Thumbnail, EventsImages= c.Images }).ToList();
                        

           // pe.Add(objP);

            //var events = db.Events.OrderByDescending(x => x.EventId).ToList();
            //ViewBag.Events = events;
            return View(events);
        }

        public ActionResult Index()
        {

            var events1 = db.Events.OrderByDescending(x => x.EventId).ToList();
            ViewBag.Events = events1;

            //var events = db.Events.OrderByDescending(x => x.EventId).ToList();
            //return View(events);

            var events = (from n in db.Events
                          join c in db.EventImages on n.EventId equals c.EventId

                          select new EventsDetails { EventId = n.EventId, Description = n.Description, Location = n.Location, Title = n.Title, EventDate = n.EventDate, Thumbnail = n.Thumbnail, EventsImages = c.Images }).ToList();
            return View(events);
        }

        public ActionResult TestEvents()
        {
            EventsVM objP = new EventsVM();
            List<EventsVM> pe = new List<EventsVM>();
            pe.Add(objP);

            var events = db.Events.OrderByDescending(x => x.EventId).ToList();
            ViewBag.Events = events;
            return View(events);
        }

        public ActionResult ViewPopup()
        {
            var events1 = db.Events.OrderByDescending(x => x.EventId).ToList();
            ViewBag.Events = events1;

            //var events = db.Events.OrderByDescending(x => x.EventId).ToList();
            //return View(events);

            var events = (from n in db.Events
                          join c in db.EventImages on n.EventId equals c.EventId

                          select new EventsDetails { EventId = n.EventId, Description = n.Description, Location = n.Location, Title = n.Title, EventDate = n.EventDate, Thumbnail = n.Thumbnail, EventsImages = c.Images }).ToList();
            return View(events);
        }


        public ActionResult Test(string customerId)
        {
            int id = int.Parse(customerId);
            var evImg = db.EventImages.Where(x => x.EventId == id).ToList();
            return PartialView("Test", evImg);

            //OptimedCorporationContext entities = new OptimedCorporationContext();
            //return PartialView("Test", entities.EventImages.Find(customerId));
        }


        public ActionResult EventDetails(string customerId)
        {
            int id = int.Parse(customerId);
            var evImg = db.EventImages.Where(x => x.EventId == id).ToList();

            return PartialView(evImg);

            //OptimedCorporationContext entities = new OptimedCorporationContext();
            //return PartialView("Test", entities.EventImages.Find(customerId));
        }


        public ActionResult ViewEvents(int? page)
        {
            var events = db.Events.OrderByDescending(x => x.EventId).ToList();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(events.ToPagedList(pageNumber, pageSize));
        }
        

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Events events)
        {
            if (ModelState.IsValid)
            {
                string imgPath = "";               

                if (events.EventThumbnail.FileName != "")
                {
                    imgPath = Path.GetFileName(events.EventThumbnail.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/EventsNews/EventsThumbnails"), imgPath);
                    events.EventThumbnail.SaveAs(path1);
                }                

                events.Thumbnail = imgPath;              

                events.DateAdded = DateTime.Now;
                events.Ipaddress = GetIpaddress();

                db.Events.Add(events);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "Event Added Successfully.";
                return View(new Events());              
            }

            return View(events);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,Title,EventDate,Location,Description,Thumbnail,Images,Status,Ipaddress,DateAdded")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewEvents");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events events = db.Events.Find(id);
            db.Events.Remove(events);
            db.SaveChanges();
            return RedirectToAction("ViewEvents");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
