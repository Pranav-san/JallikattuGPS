using Antlr.Runtime.Misc;
using Jallikattu.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Jallikattu.Controllers
{
    public class EventsController : Controller
    {
        private JallikattuGPSEntities db = new JallikattuGPSEntities();

        // GET: Events
        //public ActionResult Index()
        //{
        //    return View(db.EventsTables.ToList());
        //}

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventsTable eventsTable = db.EventsTables.Find(id);
            if (eventsTable == null)
            {
                return HttpNotFound();
            }
            return View(eventsTable);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventName,EventDate,EventLocation,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] EventsTable eventsTable)
        {
            if (ModelState.IsValid)
            {

                string adminName = Session["UserName"] as string;
                int adminId = Session["UserID"] != null ? (int)Session["UserID"] : 0;

                //Sets Event Status To 1, Which Displays The Event On Calendar
                eventsTable.Status = 1;

                eventsTable.MobileNo = "0000000000";
                eventsTable.UserName = adminName;



                eventsTable.CreatedAt = DateTime.Now;
                eventsTable.UpdatedAt = DateTime.Now;


                eventsTable.CreatedBy = adminId;
                eventsTable.UpdatedBy = adminId;


                db.EventsTables.Add(eventsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventsTable);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventsTable eventsTable = db.EventsTables.Find(id);
            if (eventsTable == null)
            {
                return HttpNotFound();
            }
            return View(eventsTable);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventsTable model, string action)
        {
            if (!ModelState.IsValid)
                return View(model);

            var eventInDb = db.EventsTables.Find(model.EventID);

            if (eventInDb == null)
                return HttpNotFound();

            // Update only editable fields
            eventInDb.EventName = model.EventName;
            eventInDb.EventDate = model.EventDate;
            eventInDb.EventLocation = model.EventLocation;

            // Audit fields
            eventInDb.UpdatedAt = DateTime.Now;


            // Status handling
            if (action == "activate")
                eventInDb.Status = 1;
            else if (action == "deactivate")
                eventInDb.Status = 0;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventsTable eventsTable = db.EventsTables.Find(id);
            if (eventsTable == null)
            {
                return HttpNotFound();
            }
            return View(eventsTable);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventsTable eventsTable = db.EventsTables.Find(id);
            db.EventsTables.Remove(eventsTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index(int? status)
        {
            using (var db = new JallikattuGPSEntities())
            {
                var events = db.EventsTables.AsQueryable();

                // FILTER LOGIC
                if (status.HasValue)
                {
                    events = events.Where(e => e.Status == status.Value);
                }

                //Optional ordering
                events = events
                         .OrderBy(e => e.Status)
                         .ThenByDescending(e => e.EventDate);

                ViewBag.SelectedStatus = status;

                return View(events.ToList());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
