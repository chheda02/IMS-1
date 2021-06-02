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
    public class PoliciesClaimsController : ApiController
    {
        private PoliciesClaimsEntities db = new PoliciesClaimsEntities();

        // GET: api/PoliciesClaims
        public IQueryable<PoliciesClaim> GetPoliciesClaims()
        {
            return db.PoliciesClaims;
        }

        // GET: api/PoliciesClaims/5
        [ResponseType(typeof(PoliciesClaim))]
        public IHttpActionResult GetPoliciesClaim(int id)
        {
            PoliciesClaim policiesClaim = db.PoliciesClaims.Find(id);
            if (policiesClaim == null)
            {
                return NotFound();
            }

            return Ok(policiesClaim);
        }

        // PUT: api/PoliciesClaims/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoliciesClaim(int id, PoliciesClaim policiesClaim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policiesClaim.Id)
            {
                return BadRequest();
            }

            db.Entry(policiesClaim).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliciesClaimExists(id))
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

        // POST: api/PoliciesClaims
        [ResponseType(typeof(PoliciesClaim))]
        public IHttpActionResult PostPoliciesClaim(PoliciesClaim policiesClaim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PoliciesClaims.Add(policiesClaim);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = policiesClaim.Id }, policiesClaim);
        }

        // DELETE: api/PoliciesClaims/5
        [ResponseType(typeof(PoliciesClaim))]
        public IHttpActionResult DeletePoliciesClaim(int id)
        {
            PoliciesClaim policiesClaim = db.PoliciesClaims.Find(id);
            if (policiesClaim == null)
            {
                return NotFound();
            }

            db.PoliciesClaims.Remove(policiesClaim);
            db.SaveChanges();

            return Ok(policiesClaim);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PoliciesClaimExists(int id)
        {
            return db.PoliciesClaims.Count(e => e.Id == id) > 0;
        }
    }
}