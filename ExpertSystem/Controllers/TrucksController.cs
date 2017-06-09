using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpertSystem;
using ExpertSystem.Models;
using ExpertSystem.ViewModels;

namespace ExpertSystem.Controllers
{
    public class TrucksController : Controller
    {
        private ESContext db = new ESContext();

        // GET: Trucks
        public ActionResult Index()
        {
            var trucks = db.Trucks.Include(t => t.Location);
            return View(trucks.ToList());
        }

        // GET: Trucks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }

            var ruleIds = truck.Rules.Select(tr => tr.RequireRuleID);

            var rules = db.Rules.Where(r => !ruleIds.Contains(r.RequireRuleID));

            return View(new TruckDetailsViewModel() { Truck = truck, Rules = rules });
        }

        // GET: Trucks/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name");
            return View();
        }

        // POST: Trucks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TruckID,Name,Capacity,Speed,CapacityUnit,Type,Status,LocationID")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                db.Trucks.Add(truck);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", truck.LocationID);
            return View(truck);
        }

        public ActionResult AddRule(int? rulesId, int? truckId)
        {
            if (rulesId == null || truckId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var truck = db.Trucks.Find(truckId);
            var rule = db.Rules.Find(rulesId);

            if (truck == null || rule == null)
            {
                return HttpNotFound();
            }

            var truckRule = new TruckRule()
            {
                Truck = truck,
                TruckRuleID = truck.TruckID,
                RequireRuleID = rule.RequireRuleID,
                Rule = rule
            };

            db.TruckRules.Add(truckRule);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = truckId });
        }

        public ActionResult DeleteRule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rule = db.TruckRules.Find(id);
            db.TruckRules.Remove(rule);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = rule.TruckID });
        }

        // GET: Trucks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", truck.LocationID);
            return View(truck);
        }

        // POST: Trucks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TruckID,Name,Capacity,Speed,CapacityUnit,Type,Status,LocationID")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", truck.LocationID);
            return View(truck);
        }

        // GET: Trucks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: Trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Truck truck = db.Trucks.Find(id);
            db.Trucks.Remove(truck);
            db.SaveChanges();
            return RedirectToAction("Index");
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
