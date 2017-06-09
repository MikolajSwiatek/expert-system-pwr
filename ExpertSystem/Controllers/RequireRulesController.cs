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

namespace ExpertSystem.Controllers
{
    public class RequireRulesController : Controller
    {
        private ESContext db = new ESContext();

        // GET: RequireRules
        public ActionResult Index()
        {
            return View(db.Rules.ToList());
        }

        // GET: RequireRules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequireRule requireRule = db.Rules.Find(id);
            if (requireRule == null)
            {
                return HttpNotFound();
            }
            return View(requireRule);
        }

        // GET: RequireRules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequireRules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequireRuleID,Name,Description")] RequireRule requireRule)
        {
            if (ModelState.IsValid)
            {
                db.Rules.Add(requireRule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requireRule);
        }

        // GET: RequireRules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequireRule requireRule = db.Rules.Find(id);
            if (requireRule == null)
            {
                return HttpNotFound();
            }
            return View(requireRule);
        }

        // POST: RequireRules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequireRuleID,Name,Description")] RequireRule requireRule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requireRule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requireRule);
        }

        // GET: RequireRules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequireRule requireRule = db.Rules.Find(id);
            if (requireRule == null)
            {
                return HttpNotFound();
            }
            return View(requireRule);
        }

        // POST: RequireRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequireRule requireRule = db.Rules.Find(id);
            db.Rules.Remove(requireRule);
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
