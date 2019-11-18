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
    [Authorize]
    public class NewsController : Controller
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

        [AllowAnonymous]
        [Route("Newsletter")]
        public ActionResult Index(int? page)
        {
            var students = db.News.OrderByDescending(x => x.NewsDate).ToList();
          
            return View(students);
        }

        [AllowAnonymous]
        public ActionResult ViewNews(int? page)
        {
            var students = db.News.OrderByDescending(x => x.NewsDate).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NewsLis(int? page)
        {
            var newsList = db.News.Where(x => x.Status ==true).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(newsList.ToPagedList(pageNumber, pageSize));
        }

        // GET: News
        //public ActionResult Index()
        //{
        //    return View(db.News.ToList());
        //}

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                string imgPath = "";
                string filePath = "";

                if (news.NewsThumbnail.FileName != "")
                {
                    imgPath = Path.GetFileName(news.NewsThumbnail.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/EventsNews/NewsThumbnails"), imgPath);
                    news.NewsThumbnail.SaveAs(path1);
                }

                if (news.NewsFile.FileName != "")
                {
                    filePath = Path.GetFileName(news.NewsFile.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/EventsNews/NewsFiles"), filePath);
                    news.NewsFile.SaveAs(path1);
                }

                news.Thumbnail = imgPath;
                news.FilePath = filePath;

                news.DateAdded = DateTime.Now;
                news.Ipaddress = GetIpaddress();

                db.News.Add(news);
                db.SaveChanges();

                ModelState.Clear();                
                ViewBag.Message = "News Added Successfully.";
                return View(new News());

                //return RedirectToAction("Index");
            }

            return View(news);
        }
        //public ActionResult Create([Bind(Include = "Id,Heading,NewsDate,Description,Thumbnail,FilePath,Status,Ipaddress,DateAdded")] News news)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.News.Add(news);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(news);
        //}

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                if (news.NewsThumbnail != null)
                {
                    string imgPath = Path.GetFileName(news.NewsThumbnail.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/EventsNews/NewsThumbnails"), imgPath);
                    news.NewsThumbnail.SaveAs(path1);
                    news.Thumbnail = imgPath;
                }

                if (news.NewsFile != null)
                {
                    string filePath = Path.GetFileName(news.NewsFile.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/EventsNews/NewsFiles"), filePath);
                    news.NewsFile.SaveAs(path1);
                    news.FilePath = filePath;
                }                                              

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewNews");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("ViewNews");
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
