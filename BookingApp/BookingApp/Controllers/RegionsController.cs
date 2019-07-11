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
    [RoutePrefix("region")]

    public class RegionsController : ApiController
    {
        private DBContext db = new DBContext();

        #region CreateRegion
        [HttpPost]
       [Route("AddRegion")]
        public IHttpActionResult PostRegion(Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Regions.Add(region);
            db.SaveChanges();

            return Ok();
            //return CreatedAtRoute("DefaultApi", new { id = region.RegionId }, region);
        }
        #endregion

        #region ReadRegions
        [HttpGet]
        [Route("AllRegions")]
        public IQueryable<Region> GetRegions()
        {
            return db.Regions;
        }
        #endregion

        #region ReadRegion
        // GET: api/Regions/5
        [HttpGet]
        [Route("GetRegion/{id}")]
        public IHttpActionResult GetRegion(int id)
        {
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }
        #endregion

        #region UpdateRegion

        [HttpPut]
        [Route("ChangeRegion/{id}")]
        public IHttpActionResult PutRegion(int id, Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != region.RegionId)
            {
                return BadRequest();
            }

            db.Entry(region).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        #region DeleteRegion
        // DELETE: api/Regions/5
        [HttpDelete]
        [Route("DeleteRegion/{id}")]
        public IHttpActionResult DeleteRegion(int id)
        {
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            db.Regions.Remove(region);
            db.SaveChanges();

            return Ok(region);
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

        private bool RegionExists(int id)
        {
            return db.Regions.Count(e => e.RegionId == id) > 0;
        }
    }
}