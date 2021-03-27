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
    public class TServiceTypesController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TServiceTypes
        public ActionResult Index()
        {
            return View(db.TServiceTypes.ToList());
        }

        // GET: TServiceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TServiceType tServiceType = db.TServiceTypes.Find(id);
            if (tServiceType == null)
            {
                return HttpNotFound();
            }
            return View(tServiceType);
        }

        // GET: TServiceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TServiceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intServiceTypeID,strServiceType")] TServiceType tServiceType)
        {
            if (ModelState.IsValid)
            {
                db.TServiceTypes.Add(tServiceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tServiceType);
        }

        // GET: TServiceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TServiceType tServiceType = db.TServiceTypes.Find(id);
            if (tServiceType == null)
            {
                return HttpNotFound();
            }
            return View(tServiceType);
        }

        // POST: TServiceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intServiceTypeID,strServiceType")] TServiceType tServiceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tServiceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tServiceType);
        }

        // GET: TServiceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TServiceType tServiceType = db.TServiceTypes.Find(id);
            if (tServiceType == null)
            {
                return HttpNotFound();
            }
            return View(tServiceType);
        }

        // POST: TServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TServiceType tServiceType = db.TServiceTypes.Find(id);
            db.TServiceTypes.Remove(tServiceType);
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
