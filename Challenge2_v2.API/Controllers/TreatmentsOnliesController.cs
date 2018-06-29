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
    public class TreatmentsOnliesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/TreatmentsOnlies
        public IQueryable<TreatmentsOnly> GetTreatmentsOnlies()
        {
            return db.TreatmentsOnlies;
        }

        // GET: api/TreatmentsOnlies/5
        [ResponseType(typeof(TreatmentsOnly))]
        public async Task<IHttpActionResult> GetTreatmentsOnly(int id)
        {
            TreatmentsOnly treatmentsOnly = await db.TreatmentsOnlies.FirstOrDefaultAsync(t => t.TreatmentID == id);
            if (treatmentsOnly == null)
            {
                return NotFound();
            }

            return Ok(treatmentsOnly);
        }

        // PUT: api/TreatmentsOnlies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTreatmentsOnly(int id, TreatmentsOnly treatmentsOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatmentsOnly.TreatmentID)
            {
                return BadRequest();
            }

            db.Entry(treatmentsOnly).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentsOnlyExists(id))
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

        // POST: api/TreatmentsOnlies
        [ResponseType(typeof(TreatmentsOnly))]
        public async Task<IHttpActionResult> PostTreatmentsOnly(TreatmentsOnly treatmentsOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TreatmentsOnlies.Add(treatmentsOnly);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TreatmentsOnlyExists(treatmentsOnly.TreatmentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = treatmentsOnly.TreatmentID }, treatmentsOnly);
        }

        // DELETE: api/TreatmentsOnlies/5
        [ResponseType(typeof(TreatmentsOnly))]
        public async Task<IHttpActionResult> DeleteTreatmentsOnly(int id)
        {
            TreatmentsOnly treatmentsOnly = await db.TreatmentsOnlies.FindAsync(id);
            if (treatmentsOnly == null)
            {
                return NotFound();
            }

            db.TreatmentsOnlies.Remove(treatmentsOnly);
            await db.SaveChangesAsync();

            return Ok(treatmentsOnly);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentsOnlyExists(int id)
        {
            return db.TreatmentsOnlies.Count(e => e.TreatmentID == id) > 0;
        }
    }
}