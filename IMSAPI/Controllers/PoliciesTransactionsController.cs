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
    public class PoliciesTransactionsController : ApiController
    {
        private PoliciesTransactionEntities db = new PoliciesTransactionEntities();

        // GET: api/PoliciesTransactions
        public IQueryable<PoliciesTransaction> GetPoliciesTransactions()
        {
            return db.PoliciesTransactions;
        }

        // GET: api/PoliciesTransactions/5
        [ResponseType(typeof(PoliciesTransaction))]
        public IHttpActionResult GetPoliciesTransaction(int id)
        {
            PoliciesTransaction policiesTransaction = db.PoliciesTransactions.Find(id);
            if (policiesTransaction == null)
            {
                return NotFound();
            }

            return Ok(policiesTransaction);
        }

        // PUT: api/PoliciesTransactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoliciesTransaction(int id, PoliciesTransaction policiesTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policiesTransaction.Id)
            {
                return BadRequest();
            }

            db.Entry(policiesTransaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliciesTransactionExists(id))
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

        // POST: api/PoliciesTransactions
        [ResponseType(typeof(PoliciesTransaction))]
        public IHttpActionResult PostPoliciesTransaction(PoliciesTransaction policiesTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PoliciesTransactions.Add(policiesTransaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = policiesTransaction.Id }, policiesTransaction);
        }

        // DELETE: api/PoliciesTransactions/5
        [ResponseType(typeof(PoliciesTransaction))]
        public IHttpActionResult DeletePoliciesTransaction(int id)
        {
            PoliciesTransaction policiesTransaction = db.PoliciesTransactions.Find(id);
            if (policiesTransaction == null)
            {
                return NotFound();
            }

            db.PoliciesTransactions.Remove(policiesTransaction);
            db.SaveChanges();

            return Ok(policiesTransaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PoliciesTransactionExists(int id)
        {
            return db.PoliciesTransactions.Count(e => e.Id == id) > 0;
        }
    }
}