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
    public class PolicyCategoriesController : ApiController
    {
        private PolicyCategoriesEntities db = new PolicyCategoriesEntities();

        // GET: api/PolicyCategories
        public IQueryable<PolicyCategory> GetPolicyCategories()
        {
            return db.PolicyCategories;
        }

        // GET: api/PolicyCategories/5
        [ResponseType(typeof(PolicyCategory))]
        public IHttpActionResult GetPolicyCategory(int id)
        {
            PolicyCategory policyCategory = db.PolicyCategories.Find(id);
            if (policyCategory == null)
            {
                return NotFound();
            }

            return Ok(policyCategory);
        }

        // PUT: api/PolicyCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPolicyCategory(int id, PolicyCategory policyCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policyCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(policyCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyCategoryExists(id))
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

        // POST: api/PolicyCategories
        [ResponseType(typeof(PolicyCategory))]
        public IHttpActionResult PostPolicyCategory(PolicyCategory policyCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PolicyCategories.Add(policyCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = policyCategory.Id }, policyCategory);
        }

        // DELETE: api/PolicyCategories/5
        [ResponseType(typeof(PolicyCategory))]
        public IHttpActionResult DeletePolicyCategory(int id)
        {
            PolicyCategory policyCategory = db.PolicyCategories.Find(id);
            if (policyCategory == null)
            {
                return NotFound();
            }

            db.PolicyCategories.Remove(policyCategory);
            db.SaveChanges();

            return Ok(policyCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PolicyCategoryExists(int id)
        {
            return db.PolicyCategories.Count(e => e.Id == id) > 0;
        }
    }
}