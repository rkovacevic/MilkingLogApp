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
using MilkingLogApp.Models;

namespace MilkingLogApp.Controllers
{
    public class MilkingLogsController : ApiController
    {
        private MilkingLogAppContext db = new MilkingLogAppContext();

        // GET: api/MilkingLogs
        public IQueryable<MilkingLog> GetMilkingLogs()
        {
            return db.MilkingLogs;
        }

        // GET: api/MilkingLogs/5
        [ResponseType(typeof(MilkingLog))]
        public IHttpActionResult GetMilkingLog(int id)
        {
            MilkingLog milkingLog = db.MilkingLogs.Find(id);
            if (milkingLog == null)
            {
                return NotFound();
            }

            return Ok(milkingLog);
        }

        // PUT: api/MilkingLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMilkingLog(int id, MilkingLog milkingLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != milkingLog.id)
            {
                return BadRequest();
            }

            db.Entry(milkingLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MilkingLogExists(id))
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

        // POST: api/MilkingLogs
        [ResponseType(typeof(MilkingLog))]
        public IHttpActionResult PostMilkingLog(MilkingLog milkingLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MilkingLogs.Add(milkingLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = milkingLog.id }, milkingLog);
        }

        // DELETE: api/MilkingLogs/5
        [ResponseType(typeof(MilkingLog))]
        public IHttpActionResult DeleteMilkingLog(int id)
        {
            MilkingLog milkingLog = db.MilkingLogs.Find(id);
            if (milkingLog == null)
            {
                return NotFound();
            }

            db.MilkingLogs.Remove(milkingLog);
            db.SaveChanges();

            return Ok(milkingLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MilkingLogExists(int id)
        {
            return db.MilkingLogs.Count(e => e.id == id) > 0;
        }
    }
}