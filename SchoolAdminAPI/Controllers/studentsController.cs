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
    public class studentsController : ApiController
    {
        private SchoolAdminAPIC_dbEntities db = new SchoolAdminAPIC_dbEntities();

        // GET: api/students
        public IQueryable<student> Getstudent()
        {
            return db.student;
        }

        // GET: api/students/5
        [ResponseType(typeof(student))]
        public IHttpActionResult Getstudent(int id)
        {
            student student = db.student.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstudent(int id, student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.id)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!studentExists(id))
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

        // POST: api/students
        [ResponseType(typeof(student))]
        public IHttpActionResult Poststudent(student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.student.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.id }, student);
        }

        // DELETE: api/students/5
        [ResponseType(typeof(student))]
        public IHttpActionResult Deletestudent(int id)
        {
            student student = db.student.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.student.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool studentExists(int id)
        {
            return db.student.Count(e => e.id == id) > 0;
        }
    }
}