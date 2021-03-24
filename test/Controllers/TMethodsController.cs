using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;

namespace test.Controllers
{
    public class TMethodsController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TMethods
        public ActionResult Index()
        {
            return View(db.TMethods.ToList());
        }

        // GET: TMethods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMethod tMethod = db.TMethods.Find(id);
            if (tMethod == null)
            {
                return HttpNotFound();
            }
            return View(tMethod);
        }

        // GET: TMethods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intMethodID,strMethod")] TMethod tMethod)
        {
            if (ModelState.IsValid)
            {
                db.TMethods.Add(tMethod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tMethod);
        }

        // GET: TMethods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMethod tMethod = db.TMethods.Find(id);
            if (tMethod == null)
            {
                return HttpNotFound();
            }
            return View(tMethod);
        }

        // POST: TMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intMethodID,strMethod")] TMethod tMethod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tMethod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tMethod);
        }

        // GET: TMethods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMethod tMethod = db.TMethods.Find(id);
            if (tMethod == null)
            {
                return HttpNotFound();
            }
            return View(tMethod);
        }

        // POST: TMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TMethod tMethod = db.TMethods.Find(id);
            db.TMethods.Remove(tMethod);
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
