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
    public class teachersController : ApiController
    {
        private SchoolAdminAPIC_dbEntities db = new SchoolAdminAPIC_dbEntities();

        // GET: api/teachers
        public IQueryable<teacher> Getteacher()
        {
            return db.teacher;
        }

        // GET: api/teachers/5
        [ResponseType(typeof(teacher))]
        public IHttpActionResult Getteacher(int id)
        {
            teacher teacher = db.teacher.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // PUT: api/teachers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putteacher(int id, teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.id)
            {
                return BadRequest();
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!teacherExists(id))
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

        // POST: api/teachers
        [ResponseType(typeof(teacher))]
        public IHttpActionResult Postteacher(teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.teacher.Add(teacher);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teacher.id }, teacher);
        }

        // DELETE: api/teachers/5
        [ResponseType(typeof(teacher))]
        public IHttpActionResult Deleteteacher(int id)
        {
            teacher teacher = db.teacher.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.teacher.Remove(teacher);
            db.SaveChanges();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool teacherExists(int id)
        {
            return db.teacher.Count(e => e.id == id) > 0;
        }
    }
}