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
using CaptchaMvc.HtmlHelpers;


namespace OptimedCorporation.Controllers
{
    [Authorize]
    public class FeedbacksController : Controller
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

        // GET: Feedbacks
        [AllowAnonymous]
        [Route("Enquiry")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewFeedback()
        {
            return View(db.Feedback.OrderByDescending(x => x.Id).ToList());
        }

        [AllowAnonymous]
        [NonAction]
        public void SendEmail(Feedback obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ReadFileName = "";
                    MailMessage mail = new MailMessage();

                    mail.To.Add(obj.Email);
                    mail.To.Add("marketing@optimedcorp.com");
                    mail.Bcc.Add("zia@goradiainfotech.com");
                    mail.From = new MailAddress("mail@optimedcorp.com");
                    mail.Subject = "Optimed Corporation Feedback Enquiry";

                    mail.IsBodyHtml = true;
                    string PathVal = Server.MapPath("~");

                    ReadFileName = PathVal + "/mail/feedback.htm";

                    string strMessage = "";
                    StreamReader sr1 = new StreamReader(ReadFileName);

                    strMessage = sr1.ReadToEnd();

                    strMessage = strMessage.Replace("XXXname", obj.Name);
                    strMessage = strMessage.Replace("XXXmobile", obj.Phone);
                    strMessage = strMessage.Replace("XXXemail", obj.Email);
                    strMessage = strMessage.Replace("XXXmessage", obj.Message);
                    strMessage = strMessage.Replace("XXXaddress", obj.Address+" "+ obj.City + " " + obj.Pin + " " + obj.State);
                    strMessage = strMessage.Replace("XXXtype", obj.Type);

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
                catch (Exception Ex)
                {
                    ViewBag.Status = "Problem while sending email, Please check details.";
                }
            }
            // return View();
            //return RedirectToAction("Contact");
            //return View("Feedbacks", obj);
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.DateAdded = DateTime.Now;
                feedback.Ipaddress = GetIpaddress();

                db.Feedback.Add(feedback);
                db.SaveChanges();
                SendEmail(feedback);
                ModelState.Clear();
                ViewBag.Message = "Your enquiry has been submited successfully.";
                return View(new Feedback());
            }

            return View(feedback);
        }

        [AllowAnonymous]
        public ActionResult Enquiry()
        {
            return View("Index");
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enquiry(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                // Code for validating the CAPTCHA  
                if (this.IsCaptchaValid("Captcha is not valid"))
                {
                    feedback.DateAdded = DateTime.Now;
                    feedback.Ipaddress = GetIpaddress();

                    db.Feedback.Add(feedback);
                    db.SaveChanges();
                    SendEmail(feedback);
                    ModelState.Clear();
                    ViewBag.Message = "Your enquiry has been submited successfully.";
                    //return View(new Feedback());
                    return View("Index");
                }


                ViewBag.ErrMessage = "Error: captcha is not valid.";
                return View("Index");
            }
            return View("Index");
        }



        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,Address,City,State,Pin,Message,Type,Ipaddress,DateAdded")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedback.Find(id);
            db.Feedback.Remove(feedback);
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
