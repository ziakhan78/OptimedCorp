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

namespace OptimedCorporation.Controllers
{
    public class EventImagesController : Controller
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();

        // GET: EventImages
        public ActionResult Index(int? id)
        {
            var eventImages = db.EventImages.Include(e => e.Events).Where(x=>x.EventId==id);
            return View(eventImages.ToList());

            //EventImages evImg = db.EventImages.Find(id);
            //return View(evImg);
        }

        public ActionResult ViewEventDetails(int id)
        {
            EventImages evImg = db.EventImages.Find(id);
            return View(evImg);
        }

        // GET: EventImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventImages eventImages = db.EventImages.Find(id);
            if (eventImages == null)
            {
                return HttpNotFound();
            }
            return View(eventImages);
        }

        // GET: EventImages/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title");
            //ViewBag.EventId = new SelectList(db.Events, "Id", "Title");
            return View();
        }

        // POST: EventImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,EventId,Images,DateAdded")] EventImages eventImages)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.EventImages.Add(eventImages);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EventId = new SelectList(db.Events, "EventId", "Title", eventImages.EventId);
        //    return View(eventImages);
        //}

        public ActionResult Create(EventImages eventImages)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in eventImages.EventsImages)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/EventsNews/EventsImages"), fileName);
                        file.SaveAs(path);

                        eventImages.Images = fileName;
                        eventImages.DateAdded = DateTime.Now;
                        db.EventImages.Add(eventImages);
                        db.SaveChanges();
                    }
                }

                //ModelState.Clear();
                //ViewBag.Message = "Event Images Uploaded Successfully.";
                //return View(new EventImages());

                return RedirectToAction("Index");
            }

          //  ViewBag.EventId = new SelectList(db.Events, "Id", "Title", eventImages.Events.EventId);
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title", eventImages.Events.EventId);

            return View(eventImages);
        }

        // GET: EventImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventImages eventImages = db.EventImages.Find(id);
            if (eventImages == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title", eventImages.Events.EventId);
            return View(eventImages);
        }

        // POST: EventImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventId,Images,DateAdded")] EventImages eventImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title", eventImages.EventId);
            return View(eventImages);
        }

        // GET: EventImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventImages eventImages = db.EventImages.Find(id);
            if (eventImages == null)
            {
                return HttpNotFound();
            }
            return View(eventImages);
        }

        // POST: EventImages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventImages eventImages = db.EventImages.Find(id);
            db.EventImages.Remove(eventImages);
            db.SaveChanges();
            return RedirectToAction("Index");
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
