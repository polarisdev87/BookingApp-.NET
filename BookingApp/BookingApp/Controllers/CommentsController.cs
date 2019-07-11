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
    [RoutePrefix("api/Comment")]

    public class CommentsController : ApiController
    {
        private DBContext db = new DBContext();



        #region Create
        [HttpPost]
        [Route("Create")]

        public IHttpActionResult Add(Comment comm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Comments.Add(comm);
                db.SaveChanges();

            }
            catch (DbUpdateException E)
            {
                return Content(HttpStatusCode.Conflict, comm);
            }
            return Ok();
        }

        #endregion

        #region ReadOneComment
        [HttpGet]
        [Route("Read/{id}")]
        public IHttpActionResult ReadComment(int id)
        {
            Comment type = db.Comments.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);

        }
        #endregion

        #region ReadAllComments

        [HttpGet]
        [Route("ReadAll")]
        // GET: api/Comments
        public IQueryable<Comment> GetAComments()
        {
            return db.Comments;
        }
        #endregion

        #region Update
        [HttpGet]
        [Route("Change/{id}")]

        public IHttpActionResult Change(int id, Comment type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != type.CommentId)
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
            Comment type = db.Comments.Find(id);

            if (type == null)
            {
                return NotFound();
            }

            db.Comments.Remove(type);
            db.SaveChanges();
            return Ok(type);

        }

        private bool IfExist(int id)
        {
            return db.Comments.Count(type => type.CommentId == id) > 0;
        }
        #endregion

        //// GET: api/Comments
        //public IQueryable<Comment> GetComments()
        //{
        //    return db.Comments;
        //}

        //// GET: api/Comments/5
        //[ResponseType(typeof(Comment))]
        //public IHttpActionResult GetComment(int id)
        //{
        //    Comment comment = db.Comments.Find(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(comment);
        //}

        //// PUT: api/Comments/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutComment(int id, Comment comment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != comment.CommentId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(comment).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CommentExists(id))
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

        //// POST: api/Comments
        //[ResponseType(typeof(Comment))]
        //public IHttpActionResult PostComment(Comment comment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Comments.Add(comment);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        //}

        //// DELETE: api/Comments/5
        //[ResponseType(typeof(Comment))]
        //public IHttpActionResult DeleteComment(int id)
        //{
        //    Comment comment = db.Comments.Find(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Comments.Remove(comment);
        //    db.SaveChanges();

        //    return Ok(comment);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool CommentExists(int id)
        //{
        //    return db.Comments.Count(e => e.CommentId == id) > 0;
        //}
    }
}