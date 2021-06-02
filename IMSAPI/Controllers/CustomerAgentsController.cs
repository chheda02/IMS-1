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
    public class CustomerAgentsController : ApiController
    {
        private CustomerAgentEntities db = new CustomerAgentEntities();

        // GET: api/CustomerAgents
        public IQueryable<CustomerAgent> GetCustomerAgents()
        {
            return db.CustomerAgents;
        }

        // GET: api/CustomerAgents/5
        [ResponseType(typeof(CustomerAgent))]
        public IHttpActionResult GetCustomerAgent(int id)
        {
            CustomerAgent customerAgent = db.CustomerAgents.Find(id);
            if (customerAgent == null)
            {
                return NotFound();
            }

            return Ok(customerAgent);
        }

        // PUT: api/CustomerAgents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerAgent(int id, CustomerAgent customerAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerAgent.Id)
            {
                return BadRequest();
            }

            db.Entry(customerAgent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAgentExists(id))
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

        // POST: api/CustomerAgents
        [ResponseType(typeof(CustomerAgent))]
        public IHttpActionResult PostCustomerAgent(CustomerAgent customerAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerAgents.Add(customerAgent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerAgent.Id }, customerAgent);
        }

        // DELETE: api/CustomerAgents/5
        [ResponseType(typeof(CustomerAgent))]
        public IHttpActionResult DeleteCustomerAgent(int id)
        {
            CustomerAgent customerAgent = db.CustomerAgents.Find(id);
            if (customerAgent == null)
            {
                return NotFound();
            }

            db.CustomerAgents.Remove(customerAgent);
            db.SaveChanges();

            return Ok(customerAgent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerAgentExists(int id)
        {
            return db.CustomerAgents.Count(e => e.Id == id) > 0;
        }
    }
}