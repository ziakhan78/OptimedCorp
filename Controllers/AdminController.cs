using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OptimedCorporation.Models;

namespace OptimedCorporation.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();

        // GET: Admin

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
        public ActionResult Login()
        {
            return PartialView();
        }

        public ActionResult ChangePassword(Admin admin)
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout(Admin admin)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
            //return PartialView("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetLogin(Admin admin)
        {
            //if (ModelState.IsValid)
            //{
            string userName = "";
            string password = "";
            if (admin.Username != null)
            {
                userName = admin.Username;
                password = admin.Password;

                var list = db.Admins.Where(x => x.Username == userName && x.Password == password).ToList();
                if (list.Count > 0)
                {
                    FormsAuthentication.SetAuthCookie(admin.Username, true);
                    Session["Admin"] = userName;
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["Admin"] = null;
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.Status = "Invalid User";
                Session["Admin"] = null;
                return PartialView("Login");
            }
            //}

            //return RedirectToAction("Login");

            //Session["Admin"] = null;
            //}
            //return PartialView("Login");
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Admins.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //public ActionResult Index()
        //{
        //    return View(db.Admin.ToList());
        //}

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.DateAdded = DateTime.Now;
                admin.Ipaddress = GetIpaddress();
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin admin)
        {
            if (ModelState.IsValid)
            {

                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
