using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SchoolAdminAPI.Models;

namespace SchoolAdminAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class coursesController : ApiController
    {
        private SchoolAdminAPIC_dbEntities db = new SchoolAdminAPIC_dbEntities();

        // GET: api/courses
        public IQueryable<course> Getcourse()
        {
            return db.course;
        }

        // GET: api/courses/5
        [ResponseType(typeof(course))]
        public IHttpActionResult Getcourse(int id)
        {
            course course = db.course.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcourse(int id, course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.id)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseExists(id))
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

        // POST: api/courses
        [ResponseType(typeof(course))]
        public IHttpActionResult Postcourse(course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.course.Add(course);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.id }, course);
        }

        // DELETE: api/courses/5
        [ResponseType(typeof(course))]
        public IHttpActionResult Deletecourse(int id)
        {
            course course = db.course.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.course.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool courseExists(int id)
        {
            return db.course.Count(e => e.id == id) > 0;
        }
    }
}