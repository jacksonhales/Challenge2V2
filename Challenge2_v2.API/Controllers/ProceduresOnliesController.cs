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
    public class ProceduresOnliesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/ProceduresOnlies
        public IQueryable<ProceduresOnly> GetProceduresOnlies()
        {
            return db.ProceduresOnlies;
        }

        // GET: api/ProceduresOnlies/5
        [ResponseType(typeof(ProceduresOnly))]
        public async Task<IHttpActionResult> GetProceduresOnly(int id)
        {
            ProceduresOnly proceduresOnly = await db.ProceduresOnlies.FirstOrDefaultAsync(p => p.ProcedureID == id); ;
            if (proceduresOnly == null)
            {
                return NotFound();
            }

            return Ok(proceduresOnly);
        }

        // PUT: api/ProceduresOnlies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProceduresOnly(int id, ProceduresOnly proceduresOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proceduresOnly.ProcedureID)
            {
                return BadRequest();
            }

            db.Entry(proceduresOnly).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProceduresOnlyExists(id))
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

        // POST: api/ProceduresOnlies
        [ResponseType(typeof(ProceduresOnly))]
        public async Task<IHttpActionResult> PostProceduresOnly(ProceduresOnly proceduresOnly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProceduresOnlies.Add(proceduresOnly);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProceduresOnlyExists(proceduresOnly.ProcedureID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = proceduresOnly.ProcedureID }, proceduresOnly);
        }

        // DELETE: api/ProceduresOnlies/5
        [ResponseType(typeof(ProceduresOnly))]
        public async Task<IHttpActionResult> DeleteProceduresOnly(int id)
        {
            ProceduresOnly proceduresOnly = await db.ProceduresOnlies.FindAsync(id);
            if (proceduresOnly == null)
            {
                return NotFound();
            }

            db.ProceduresOnlies.Remove(proceduresOnly);
            await db.SaveChangesAsync();

            return Ok(proceduresOnly);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProceduresOnlyExists(int id)
        {
            return db.ProceduresOnlies.Count(e => e.ProcedureID == id) > 0;
        }
    }
}