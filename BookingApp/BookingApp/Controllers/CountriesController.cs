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
using System.Web.Http.Results;

namespace BookingApp.Controllers
{
    [RoutePrefix("country")]

    public class CountriesController : ApiController
    {
        private DBContext db = new DBContext();


        #region CreateCoutry
        // POST: api/Countries
        [HttpPost]
        [Route("AddCountry")]
        public IHttpActionResult PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.Countries.Add(country);
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)409,
                                                            new HttpError("Country already exists.")
                ));
            }

            return Ok();
        }
        #endregion

        #region ReadAllCountries
        [HttpGet]
        [Route("AllCountries")]
        // GET: api/Countries
        public IQueryable<Country> GetCountries()
        {
            return db.Countries;
        }
        #endregion

        #region ReadCountry
        [HttpGet]
        [Route("GetCountry/{id}")]
        public IHttpActionResult GetCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }
        #endregion

        #region UpdateCountry
        [HttpGet]
        [Route("ChangeCountry/{id}")]
        public IHttpActionResult ChangeCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.CountryId)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        #region DeleteCountry
        // DELETE: api/Countries/5
        [HttpDelete]
        [Route("DeleteCountry/{id}")]
        public IHttpActionResult DeleteCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            db.SaveChanges();

            return Ok(country);
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

        private bool CountryExists(int id)
        {
            return db.Countries.Count(e => e.CountryId == id) > 0;
        }
    }
}