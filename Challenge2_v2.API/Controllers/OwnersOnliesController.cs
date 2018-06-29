using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Challenge2_v2.API.Models;

namespace Challenge2_v2.API.Controllers
{
    public class OwnersOnliesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/OwnersOnlies
        public IQueryable<OwnersOnly> GetOwnersOnlies()
        {
            return db.OwnersOnlies;
        }

        // GET: api/OwnersOnlies/5
        [ResponseType(typeof(OwnersOnly))]
        public async Task<IHttpActionResult> GetOwnersOnly(int id)
        {
            OwnersOnly ownersOnly = await db.OwnersOnlies.FirstOrDefaultAsync(o => o.OwnerID == id);
            if (ownersOnly == null)
            {
                return NotFound();
            }

            return Ok(ownersOnly);
        }

        // PUT: api/OwnersOnlies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOwnersOnly(int id, OwnersOnly ownersOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ownersOnly.OwnerID)
            {
                return BadRequest();
            }

            db.Entry(ownersOnly).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnersOnlyExists(id))
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

        // POST: api/OwnersOnlies
        [ResponseType(typeof(OwnersOnly))]
        public async Task<IHttpActionResult> PostOwnersOnly(OwnersOnly ownersOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OwnersOnlies.Add(ownersOnly);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OwnersOnlyExists(ownersOnly.OwnerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ownersOnly.OwnerID }, ownersOnly);
        }

        // DELETE: api/OwnersOnlies/5
        [ResponseType(typeof(OwnersOnly))]
        public async Task<IHttpActionResult> DeleteOwnersOnly(int id)
        {
            OwnersOnly ownersOnly = await db.OwnersOnlies.FindAsync(id);
            if (ownersOnly == null)
            {
                return NotFound();
            }

            db.OwnersOnlies.Remove(ownersOnly);
            await db.SaveChangesAsync();

            return Ok(ownersOnly);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnersOnlyExists(int id)
        {
            return db.OwnersOnlies.Count(e => e.OwnerID == id) > 0;
        }
    }
}