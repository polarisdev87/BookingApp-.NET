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
    [RoutePrefix("accomodationType")]
    public class AccommodationTypesController : ApiController
    {
        private DBContext db = new DBContext();

        #region Create
        [HttpPost]
        [Route("Create")]

        public IHttpActionResult Add(AccommodationType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.AccommodationTypes.Add(type);
                db.SaveChanges();

            }
            catch (DbUpdateException E)
            {
                return Content(HttpStatusCode.Conflict, type);
            }
            return Ok();
        }

        #endregion

        #region ReadOneType
        [HttpGet]
        [Route("Read/{id}")]
        public IHttpActionResult ReadType(int id)
        {
            AccommodationType type = db.AccommodationTypes.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);

        }
        #endregion

        #region ReadAllTypes

        [HttpGet]
        [Route("ReadAll")]
        // GET: api/AccommodationTypes
        public IQueryable<AccommodationType> GetAccommodationTypes()
        {
            return db.AccommodationTypes;
        }
        #endregion

        #region Update
        [HttpGet]
        [Route("Change/{id}")]

        public IHttpActionResult Change(int id, AccommodationType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != type.AccommodationTypeId)
            {
                return BadRequest();
            }
            db.Entry(type).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IfExist(id))
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

        #region Delete
        [HttpDelete]
        [Route("Delete/{id}")]

        public IHttpActionResult Delete(int id)
        {
            AccommodationType type = db.AccommodationTypes.Find(id);

            if (type == null)
            {
                return NotFound();
            }

            db.AccommodationTypes.Remove(type);
            db.SaveChanges();
            return Ok(type);

        }

        private bool IfExist(int id)
        {
            return db.AccommodationTypes.Count(type => type.AccommodationTypeId == id) > 0;
        }
        #endregion

        //// GET: api/AccommodationTypes/5
        //[ResponseType(typeof(AccommodationType))]
        //public IHttpActionResult GetAccommodationType(int id)
        //{
        //    AccommodationType accommodationType = db.AccommodationTypes.Find(id);
        //    if (accommodationType == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(accommodationType);
        //}






        //// PUT: api/AccommodationTypes/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAccommodationType(int id, AccommodationType accommodationType)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != accommodationType.AccommodationTypeId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(accommodationType).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccommodationTypeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/AccommodationTypes
        //[ResponseType(typeof(AccommodationType))]
        //public IHttpActionResult PostAccommodationType(AccommodationType accommodationType)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AccommodationTypes.Add(accommodationType);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = accommodationType.AccommodationTypeId }, accommodationType);
        //}

        //// DELETE: api/AccommodationTypes/5
        //[ResponseType(typeof(AccommodationType))]
        //public IHttpActionResult DeleteAccommodationType(int id)
        //{
        //    AccommodationType accommodationType = db.AccommodationTypes.Find(id);
        //    if (accommodationType == null)
        //    {
        //        return NotFound();
        //    }

        //    db.AccommodationTypes.Remove(accommodationType);
        //    db.SaveChanges();

        //    return Ok(accommodationType);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool AccommodationTypeExists(int id)
        //{
        //    return db.AccommodationTypes.Count(e => e.AccommodationTypeId == id) > 0;
        //}
    }
}