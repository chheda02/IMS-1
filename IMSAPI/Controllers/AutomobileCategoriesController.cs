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
    public class AutomobileCategoriesController : ApiController
    {
        private AutomobileCategoriesEntities db = new AutomobileCategoriesEntities();

        // GET: api/AutomobileCategories
        public IQueryable<AutomobileCategory> GetAutomobileCategories()
        {
            return db.AutomobileCategories;
        }

        // GET: api/AutomobileCategories/5
        [ResponseType(typeof(AutomobileCategory))]
        public IHttpActionResult GetAutomobileCategory(byte id)
        {
            AutomobileCategory automobileCategory = db.AutomobileCategories.Find(id);
            if (automobileCategory == null)
            {
                return NotFound();
            }

            return Ok(automobileCategory);
        }

        // PUT: api/AutomobileCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutomobileCategory(byte id, AutomobileCategory automobileCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != automobileCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(automobileCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutomobileCategoryExists(id))
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

        // POST: api/AutomobileCategories
        [ResponseType(typeof(AutomobileCategory))]
        public IHttpActionResult PostAutomobileCategory(AutomobileCategory automobileCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AutomobileCategories.Add(automobileCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = automobileCategory.Id }, automobileCategory);
        }

        // DELETE: api/AutomobileCategories/5
        [ResponseType(typeof(AutomobileCategory))]
        public IHttpActionResult DeleteAutomobileCategory(byte id)
        {
            AutomobileCategory automobileCategory = db.AutomobileCategories.Find(id);
            if (automobileCategory == null)
            {
                return NotFound();
            }

            db.AutomobileCategories.Remove(automobileCategory);
            db.SaveChanges();

            return Ok(automobileCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutomobileCategoryExists(byte id)
        {
            return db.AutomobileCategories.Count(e => e.Id == id) > 0;
        }
    }
}