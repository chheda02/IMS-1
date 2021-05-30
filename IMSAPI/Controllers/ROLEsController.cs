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
using IMSAPI.Models;

namespace IMSAPI.Controllers
{
    public class ROLEsController : ApiController
    {
        private IMSEntities db = new IMSEntities();

        // GET: api/ROLEs
        public IQueryable<ROLE> GetROLES()
        {
            return db.ROLES;
        }

        // GET: api/ROLEs/5
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult GetROLE(byte id)
        {
            ROLE rOLE = db.ROLES.Find(id);
            if (rOLE == null)
            {
                return NotFound();
            }

            return Ok(rOLE);
        }

        // PUT: api/ROLEs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutROLE(byte id, ROLE rOLE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rOLE.Id)
            {
                return BadRequest();
            }

            db.Entry(rOLE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ROLEExists(id))
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

        // POST: api/ROLEs
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult PostROLE(ROLE rOLE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ROLES.Add(rOLE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rOLE.Id }, rOLE);
        }

        // DELETE: api/ROLEs/5
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult DeleteROLE(byte id)
        {
            ROLE rOLE = db.ROLES.Find(id);
            if (rOLE == null)
            {
                return NotFound();
            }

            db.ROLES.Remove(rOLE);
            db.SaveChanges();

            return Ok(rOLE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ROLEExists(byte id)
        {
            return db.ROLES.Count(e => e.Id == id) > 0;
        }
    }
}