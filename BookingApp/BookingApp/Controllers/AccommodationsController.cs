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
using BookingApp.Models;

namespace BookingApp.Controllers
{
    

    public class AccommodationsController : ApiController
    {
        private DBContext db = new DBContext();
    
       
        private bool IfExist(int id)
        {
            return db.Accommodation.Count(type => type.AccommodationId == id) > 0;
        }


        [HttpGet]
        [Route("ReadAll")]
        public IQueryable<Accommodation> GetAccommodations()
        {
            return db.Accommodation;
        }

        [HttpGet]
        [Route("Read/{id}")]
        public IHttpActionResult GetAccommodation(int id)
        {
            Accommodation accommodation = db.Accommodation.Find(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return Ok(accommodation);
        }

        [HttpGet]
        [Route("Change/{id}")]
        public IHttpActionResult PutAccommodation(int id, Accommodation accommodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accommodation.AccommodationId)
            {
                return BadRequest();
            }

            db.Entry(accommodation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccommodationExists(id))
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

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult PostAccommodation(Accommodation accommodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accommodation.Add(accommodation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accommodation.AccommodationId }, accommodation);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteAccommodation(int id)
        {
            Accommodation accommodation = db.Accommodation.Find(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            db.Accommodation.Remove(accommodation);
            db.SaveChanges();

            return Ok(accommodation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccommodationExists(int id)
        {
            return db.Accommodation.Count(e => e.AccommodationId == id) > 0;
        }
    }
}