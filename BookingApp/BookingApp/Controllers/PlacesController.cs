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
    [RoutePrefix("place")]

    public class PlacesController : ApiController
    {
        private DBContext db = new DBContext();

        #region CreatePlace
        // POST: api/Places
        [HttpPost]
        [Route("AddPlace")]
        public IHttpActionResult PostPlace(Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Places.Add(place);
            db.SaveChanges();

            return Ok();
            // return CreatedAtRoute("DefaultApi", new { id = place.PlaceId }, place);
        }
        #endregion

        #region ReadAllPlaces
        // GET: api/Places
        [HttpGet]
        [Route("AllPlaces")]
        public IQueryable<Place> GetPlaces()
        {
            return db.Places;
        }
        #endregion

        #region ReadOnePlace
        [HttpGet]
        [Route("GetPlace/{id}")]
        public IHttpActionResult GetPlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }
#endregion

        #region UpdatePlace
        [HttpPut]
        [Route("ChangePlace/{id}")]
        public IHttpActionResult PutPlace(int id, Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.PlaceId)
            {
                return BadRequest();
            }

            db.Entry(place).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
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
#endregion
        
        #region DeletePlace
        // DELETE: api/Places/5
        [HttpDelete]
        [Route("DeletePlace/{id}")]
        public IHttpActionResult DeletePlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            db.Places.Remove(place);
            db.SaveChanges();

            return Ok(place);
        }
#endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaceExists(int id)
        {
            return db.Places.Count(e => e.PlaceId == id) > 0;
        }
    }
}