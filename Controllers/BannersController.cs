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
    [Authorize]
    public class BannersController : Controller
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();

        // GET: Banners

        [NonAction]
        public string GetIpaddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }

        // GET: Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Banner banner)
        {
            if (ModelState.IsValid)
            {
                string imgPath = "";

                if (banner.MenuBanner.FileName != "")
                {
                    imgPath = Path.GetFileName(banner.MenuBanner.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/BannerImages"), imgPath);
                    banner.MenuBanner.SaveAs(path1);
                }

                banner.BannerImage = imgPath;

                banner.DateAdded = DateTime.Now;
                banner.Ipaddress = GetIpaddress();

                db.Banners.Add(banner);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Banner Added Successfully.";
                return View(new Banner());
            }

            return View(banner);
        }

        // GET: Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                string imgPath = "";

                if (banner.MenuBanner.FileName != "")
                {
                    imgPath = Path.GetFileName(banner.MenuBanner.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/BannerImages"), imgPath);
                    banner.MenuBanner.SaveAs(path1);
                }

                banner.BannerImage = imgPath;

                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Banner Updated Successfully.";
                return View(new Banner());

            }
            return View(banner);
        }

        // GET: Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
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
