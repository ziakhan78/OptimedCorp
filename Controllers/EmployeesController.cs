using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OptimedCorporation.Models;



namespace OptimedCorporation.Controllers
{
    
    public class EmployeesController : ApiController
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();

        // GET: api/Employees
        public IQueryable<Feedback> GetFeedback()
        {
            return db.Feedback;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult GetFeedback(int id)
        {
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeedback(int id, Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedback.Id)
            {
                return BadRequest();
            }

            db.Entry(feedback).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult PostFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Feedback.Add(feedback);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feedback.Id }, feedback);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult DeleteFeedback(int id)
        {
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            db.Feedback.Remove(feedback);
            db.SaveChanges();

            return Ok(feedback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackExists(int id)
        {
            return db.Feedback.Count(e => e.Id == id) > 0;
        }
    }
}