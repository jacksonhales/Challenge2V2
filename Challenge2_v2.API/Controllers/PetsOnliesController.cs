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
    public class PetsOnliesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/PetsOnlies
        public IQueryable<PetsOnly> GetPetsOnlies()
        {
            return db.PetsOnlies;
        }

        // GET: api/PetsOnlies/5
        [ResponseType(typeof(PetsOnly))]
        public async Task<IHttpActionResult> GetPetsOnly(int id)
        {
            PetsOnly petsOnly = await db.PetsOnlies.FirstOrDefaultAsync(p => p.PetID == id);
            if (petsOnly == null)
            {
                return NotFound();
            }

            return Ok(petsOnly);
        }

        // PUT: api/PetsOnlies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPetsOnly(int id, PetsOnly petsOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petsOnly.PetID)
            {
                return BadRequest();
            }

            db.Entry(petsOnly).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetsOnlyExists(id))
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

        // POST: api/PetsOnlies
        [ResponseType(typeof(PetsOnly))]
        public async Task<IHttpActionResult> PostPetsOnly(PetsOnly petsOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PetsOnlies.Add(petsOnly);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PetsOnlyExists(petsOnly.PetID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = petsOnly.PetID }, petsOnly);
        }

        // DELETE: api/PetsOnlies/5
        [ResponseType(typeof(PetsOnly))]
        public async Task<IHttpActionResult> DeletePetsOnly(int id)
        {
            PetsOnly petsOnly = await db.PetsOnlies.FindAsync(id);
            if (petsOnly == null)
            {
                return NotFound();
            }

            db.PetsOnlies.Remove(petsOnly);
            await db.SaveChangesAsync();

            return Ok(petsOnly);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PetsOnlyExists(int id)
        {
            return db.PetsOnlies.Count(e => e.PetID == id) > 0;
        }
    }
}