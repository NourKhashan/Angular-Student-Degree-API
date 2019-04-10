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
using StudentAPI.Models;
using System.Web.Http.Cors;

namespace StudentAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]

    public class StudentSubjectsController : ApiController
    {
        private StudentModels db = new StudentModels();

        // GET: api/StudentSubjects
        public IQueryable<StudentSubject> GetStudentSubject()
        {
            return db.StudentSubject;
        }

        // GET: api/StudentSubjects/5
        [ResponseType(typeof(StudentSubject))]
        public IHttpActionResult GetStudentSubject(int id)
        {
            StudentSubject studentSubject = db.StudentSubject.Find(id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return Ok(studentSubject);
        }
        
        // Update Degree For All Students
        public IHttpActionResult PutStudentSubject(StudentSubject[] studentSubjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            StudentSubject studentSubject;
            foreach (var item in studentSubjects)
            {
                studentSubject = db.StudentSubject.Where(std=>std.StdId== item.StdId).Where(subj=>subj.SubId== item.SubId).Select(m=>m).FirstOrDefault();
                studentSubject.Degree = item.Degree;
            }
            db.SaveChanges();
            

            return StatusCode(HttpStatusCode.NoContent);
        }


        // PUT: api/StudentSubjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentSubject(int id, StudentSubject studentSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentSubject.StdId)
            {
                return BadRequest();
            }

            db.Entry(studentSubject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentSubjectExists(id))
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

        // POST: api/StudentSubjects
        // POST: api/StudentSubjects
        [ResponseType(typeof(StudentSubject))]
        public IHttpActionResult PostStudentSubject(StudentSubject studentSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentSubject.Add(studentSubject);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentSubjectExists(studentSubject.StdId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentSubject.StdId }, studentSubject);
        }

        // DELETE: api/StudentSubjects/5
        [ResponseType(typeof(StudentSubject))]
        public IHttpActionResult DeleteStudentSubject(int id)
        {
            StudentSubject studentSubject = db.StudentSubject.Find(id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            db.StudentSubject.Remove(studentSubject);
            db.SaveChanges();

            return Ok(studentSubject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentSubjectExists(int id)
        {
            return db.StudentSubject.Count(e => e.StdId == id) > 0;
        }
    }
}