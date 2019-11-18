using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using OptimedCorporation.Models;

namespace OptimedCorporation.Controllers
{
    [Authorize]
    public class CareersController : Controller
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();


        public ActionResult CareersForm()
        {
            return View("CareersForm");
        }


        // GET: Careers

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
            return View();
        }

        public ActionResult ViewCareers()
        {
            return View(db.Careers.ToList());
        }

        [AllowAnonymous]
        [NonAction]
        public void SendEmail(Careers obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ReadFileName = "";
                    MailMessage mail = new MailMessage();

                    mail.To.Add(obj.Email);
                    mail.To.Add("info@optimedcorp.com");
                    mail.Bcc.Add("zia@goradiainfotech.com");
                    mail.From = new MailAddress("mail@optimedcorp.com");
                    mail.Subject = "Optimed Corporation Careers";

                    mail.IsBodyHtml = true;
                    string PathVal = Server.MapPath("~");

                    ReadFileName = PathVal + "/mail/careers.htm";

                    string strMessage = "";
                    StreamReader sr1 = new StreamReader(ReadFileName);

                    strMessage = sr1.ReadToEnd();

                    strMessage = strMessage.Replace("XXXname", obj.Name);
                    strMessage = strMessage.Replace("XXXmobile", obj.Mobile);
                    strMessage = strMessage.Replace("XXXemail", obj.Email);
                    strMessage = strMessage.Replace("XXXdob", obj.Dob.ToString("dd-MMM-yyyy"));
                    strMessage = strMessage.Replace("XXXexp", obj.Experience.ToString());                  
                    strMessage = strMessage.Replace("XXXqualification", obj.Qualification);
                    strMessage = strMessage.Replace("XXXmessage", obj.Message);


                    mail.Body = strMessage;
                    sr1.Close();


                    //SmtpClient emailClient = new SmtpClient();
                    //emailClient.Credentials = new NetworkCredential("mail@optimedcorp.com", "Tuza802@");
                    //emailClient.Port = 25;
                    //emailClient.Host = "mail.optimedcorp.com";
                    //emailClient.EnableSsl = false;
                    //emailClient.Send(mail);

                    SmtpClient emailClient = new SmtpClient();
                    emailClient.Credentials = new NetworkCredential("mail@optimedcorp.com", "U=s%6wO#7");
                    emailClient.Port = 25;
                    emailClient.Host = "smtp.yandex.com";
                    emailClient.EnableSsl = true;
                    emailClient.Send(mail);


                    ModelState.Clear();

                    ViewBag.Status = "Email Sent Successfully.";

                }
                catch (Exception)
                {
                    ViewBag.Status = "Problem while sending email, Please check details.";
                }
            }
            // return View();
            //return RedirectToAction("Contact");
            //return View("Feedbacks", obj);
        }

        // GET: Careers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Careers careers = db.Careers.Find(id);
            if (careers == null)
            {
                return HttpNotFound();
            }
            return View(careers);
        }

        // GET: Careers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Careers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Careers careers)
        {
            if (ModelState.IsValid)
            {
                string filePath = ""; 

                if (careers.ResumeFile.FileName != "")
                {
                    filePath = Path.GetFileName(careers.ResumeFile.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/Resume"), filePath);
                    careers.ResumeFile.SaveAs(path1);
                }

             
                careers.Resume = filePath;
                careers.DateAdded = DateTime.Now;
                careers.Ipaddress = GetIpaddress();

                db.Careers.Add(careers);
                db.SaveChanges();
                SendEmail(careers);
                ModelState.Clear();
                ViewBag.Message = "Your request has been sent successfully.";
                return View(new Careers());


            }

            return View(careers);
        }

        [AllowAnonymous]
        public ActionResult SendCareersForm()
        {
            return View("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendCareersForm(Careers careers)
        {
            if (ModelState.IsValid)
            {
                string filePath = "";

                if (careers.ResumeFile.FileName != "")
                {
                    filePath = Path.GetFileName(careers.ResumeFile.FileName);
                    string path1 = Path.Combine(Server.MapPath("~/Resume"), filePath);
                    careers.ResumeFile.SaveAs(path1);
                }


                careers.Resume = filePath;
                careers.DateAdded = DateTime.Now;
                careers.Ipaddress = GetIpaddress();

                db.Careers.Add(careers);
                db.SaveChanges();
                SendEmail(careers);
                ModelState.Clear();
                ViewBag.Message = "Your request has been sent successfully.";
                return View("Index");


            }

            return View("Index");
        }

        // GET: Careers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Careers careers = db.Careers.Find(id);
            if (careers == null)
            {
                return HttpNotFound();
            }
            return View(careers);
        }

        // POST: Careers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,Message,Resume,Ipaddress,DateAdded")] Careers careers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(careers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(careers);
        }

        // GET: Careers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Careers careers = db.Careers.Find(id);
            if (careers == null)
            {
                return HttpNotFound();
            }
            return View(careers);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Careers careers = db.Careers.Find(id);
            db.Careers.Remove(careers);
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
