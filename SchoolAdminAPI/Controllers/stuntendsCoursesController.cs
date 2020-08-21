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
    public class stuntendsCoursesController : ApiController
    {
        private SchoolAdminAPIC_dbEntities db = new SchoolAdminAPIC_dbEntities();

        // GET: api/stuntendsCourses
        public IQueryable<stuntendsCourse> GetstuntendsCourse()
        {
            return db.stuntendsCourse;
        }

        // GET: api/stuntendsCourses/5
        [ResponseType(typeof(stuntendsCourse))]
        public IHttpActionResult GetstuntendsCourse(int id)
        {
            stuntendsCourse stuntendsCourse = db.stuntendsCourse.Find(id);
            if (stuntendsCourse == null)
            {
                return NotFound();
            }

            return Ok(stuntendsCourse);
        }

        // PUT: api/stuntendsCourses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutstuntendsCourse(int id, stuntendsCourse stuntendsCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stuntendsCourse.id)
            {
                return BadRequest();
            }

            db.Entry(stuntendsCourse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stuntendsCourseExists(id))
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

        // POST: api/stuntendsCourses
        [ResponseType(typeof(stuntendsCourse))]
        public IHttpActionResult PoststuntendsCourse(stuntendsCourse stuntendsCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.stuntendsCourse.Add(stuntendsCourse);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stuntendsCourse.id }, stuntendsCourse);
        }

        // DELETE: api/stuntendsCourses/5
        [ResponseType(typeof(stuntendsCourse))]
        public IHttpActionResult DeletestuntendsCourse(int id)
        {
            stuntendsCourse stuntendsCourse = db.stuntendsCourse.Find(id);
            if (stuntendsCourse == null)
            {
                return NotFound();
            }

            db.stuntendsCourse.Remove(stuntendsCourse);
            db.SaveChanges();

            return Ok(stuntendsCourse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool stuntendsCourseExists(int id)
        {
            return db.stuntendsCourse.Count(e => e.id == id) > 0;
        }
    }
}