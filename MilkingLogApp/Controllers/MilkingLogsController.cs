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
        public IQueryable<DateMilkingLog> GetMilkingLogs()
        {
            var dateMilkingLogs = from m in db.MilkingLogs
                                  group m by m.date into g
                                  let amountSum = g.Sum(m => m.amount)
                                  orderby g.Key descending
                                  select new DateMilkingLog()
                                  {
                                      date = g.Key,
                                      amount = amountSum
                                  };
            return dateMilkingLogs;
        }

        // POST: api/MilkingLogs
        [ResponseType(typeof(MilkingLog))]
        public IHttpActionResult PostMilkingLog(MilkingLog milkingLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var milkingLogs = db.MilkingLogs.Where(m => m.date == milkingLog.date);

            foreach (var previousMilkingLog in milkingLogs)
            {
                if (previousMilkingLog.IsSubsetOf(milkingLog))
                {
                    db.MilkingLogs.Remove(previousMilkingLog);
                }
                else if (previousMilkingLog.IsOverlapingWith(milkingLog)) 
                {
                    return BadRequest("Selected cycles overlap with the previous entry");
                }
            }

            db.MilkingLogs.Add(milkingLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = milkingLog.id }, milkingLog);
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