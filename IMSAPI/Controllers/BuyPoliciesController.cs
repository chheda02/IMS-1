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
    public class BuyPoliciesController : ApiController
    {
        private BuyPoliciesEntities db = new BuyPoliciesEntities();

        // GET: api/BuyPolicies
        public IQueryable<BuyPolicy> GetBuyPolicies()
        {
            return db.BuyPolicies;
        }

        // GET: api/BuyPolicies/5
        [ResponseType(typeof(BuyPolicy))]
        public IHttpActionResult GetBuyPolicy(int id)
        {
            BuyPolicy buyPolicy = db.BuyPolicies.Find(id);
            if (buyPolicy == null)
            {
                return NotFound();
            }

            return Ok(buyPolicy);
        }

        // PUT: api/BuyPolicies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuyPolicy(int id, BuyPolicy buyPolicy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buyPolicy.Id)
            {
                return BadRequest();
            }

            db.Entry(buyPolicy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyPolicyExists(id))
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

        // POST: api/BuyPolicies
        [ResponseType(typeof(BuyPolicy))]
        public IHttpActionResult PostBuyPolicy(BuyPolicy buyPolicy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BuyPolicies.Add(buyPolicy);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = buyPolicy.Id }, buyPolicy);
        }

        // DELETE: api/BuyPolicies/5
        [ResponseType(typeof(BuyPolicy))]
        public IHttpActionResult DeleteBuyPolicy(int id)
        {
            BuyPolicy buyPolicy = db.BuyPolicies.Find(id);
            if (buyPolicy == null)
            {
                return NotFound();
            }

            db.BuyPolicies.Remove(buyPolicy);
            db.SaveChanges();

            return Ok(buyPolicy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuyPolicyExists(int id)
        {
            return db.BuyPolicies.Count(e => e.Id == id) > 0;
        }
    }
}