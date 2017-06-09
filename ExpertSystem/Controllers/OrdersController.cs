using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ExpertSystem.Models;
using ExpertSystem.ViewModels;

namespace ExpertSystem.Controllers
{
    public class OrdersController : Controller
    {
        private ESContext db = new ESContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Product).Include(o => o.Truck).Include(o => o.Warehouse);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Name");
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "WarehouseID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,WarehouseID,ProductID,Capacity,DateTime,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
                ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Name", order.TruckID);
                ViewBag.WarehouseID = new SelectList(db.Warehouses, "WarehouseID", "Name", order.WarehouseID);

                var product = db.Products.Find(order.ProductID);
                db.Trucks.Include(t => t.Location);
                var trucks = db.Trucks.ToList()
                    .Where(t => t.Status == Enums.TruckStatus.NotUsed && t.Capacity >= order.Capacity);

                var productRuleIds = product.Rules.Select(r => r.RequireRuleID);
                IEnumerable<Truck> truckWithPermission = new List<Truck>();
                var tempTrucks = new List<Truck>();

                foreach (var truck in trucks)
                {
                    var toQualify = true;

                    foreach (var rule in truck.Rules)
                    {
                        if (!productRuleIds.Contains(rule.RequireRuleID))
                        {
                            toQualify = false;
                            break;
                        }
                    }

                    if (toQualify)
                    {
                        tempTrucks.Add(truck);
                    }
                }

                var warehouse = db.Warehouses.Find(order.WarehouseID);

                var hours = (order.DateTime - DateTime.Now).TotalHours;
                truckWithPermission = tempTrucks
                    .Where(t => GetKm(t.Location, warehouse.Location) < (t.Speed * GetKm(t.Location, warehouse.Location)));

                truckWithPermission = truckWithPermission.OrderBy(t => GetKm(t.Location, warehouse.Location));

                var model = new OrderViewModel()
                {
                    Order = order,
                    Trucks = truckWithPermission
                };

                return View("Create2", model);
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Name", order.TruckID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "WarehouseID", "Name", order.WarehouseID);
            return View(order);
        }

        public ActionResult CreateFinish([Bind(Include = "WarehouseID,ProductID,TruckID,Capacity,DateTime,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return View("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Name", order.TruckID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "WarehouseID", "Name", order.WarehouseID);
            return View("Create", order);
        }

            private double GetKm(Location truck, Location warehouse)
        {
            var distanse = Math.Sqrt((warehouse.Latitude - truck.Latitude) * (warehouse.Latitude - truck.Latitude) +
                (warehouse.Longitude - truck.Longitude) * (warehouse.Longitude - truck.Longitude)) * 73;

            return distanse;
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Name", order.TruckID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "WarehouseID", "Name", order.WarehouseID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,WarehouseID,ProductID,TruckID,Capacity,DateTime,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Name", order.TruckID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "WarehouseID", "Name", order.WarehouseID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
