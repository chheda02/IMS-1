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
    public class PoliciesStatusController : ApiController
    {
        private PoliciesStatusEntities db = new PoliciesStatusEntities();

        // GET: api/PoliciesStatus
        public IQueryable<PoliciesStatu> GetPoliciesStatus()
        {
            return db.PoliciesStatus;
        }

        // GET: api/PoliciesStatus/5
        [ResponseType(typeof(PoliciesStatu))]
        public IHttpActionResult GetPoliciesStatu(int id)
        {
            PoliciesStatu policiesStatu = db.PoliciesStatus.Find(id);
            if (policiesStatu == null)
            {
                return NotFound();
            }

            return Ok(policiesStatu);
        }

        // PUT: api/PoliciesStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoliciesStatu(int id, PoliciesStatu policiesStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policiesStatu.Id)
            {
                return BadRequest();
            }

            db.Entry(policiesStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliciesStatuExists(id))
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

        // POST: api/PoliciesStatus
        [ResponseType(typeof(PoliciesStatu))]
        public IHttpActionResult PostPoliciesStatu(PoliciesStatu policiesStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PoliciesStatus.Add(policiesStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = policiesStatu.Id }, policiesStatu);
        }

        // DELETE: api/PoliciesStatus/5
        [ResponseType(typeof(PoliciesStatu))]
        public IHttpActionResult DeletePoliciesStatu(int id)
        {
            PoliciesStatu policiesStatu = db.PoliciesStatus.Find(id);
            if (policiesStatu == null)
            {
                return NotFound();
            }

            db.PoliciesStatus.Remove(policiesStatu);
            db.SaveChanges();

            return Ok(policiesStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PoliciesStatuExists(int id)
        {
            return db.PoliciesStatus.Count(e => e.Id == id) > 0;
        }
    }
}