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
    public class TPetTypesController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TPetTypes
        public ActionResult Index()
        {
            return View(db.TPetTypes.ToList());
        }

        // GET: TPetTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPetType tPetType = db.TPetTypes.Find(id);
            if (tPetType == null)
            {
                return HttpNotFound();
            }
            return View(tPetType);
        }

        // GET: TPetTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TPetTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intPetTypeID,strPetType")] TPetType tPetType)
        {
            if (ModelState.IsValid)
            {
                db.TPetTypes.Add(tPetType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tPetType);
        }

        // GET: TPetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPetType tPetType = db.TPetTypes.Find(id);
            if (tPetType == null)
            {
                return HttpNotFound();
            }
            return View(tPetType);
        }

        // POST: TPetTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intPetTypeID,strPetType")] TPetType tPetType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tPetType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tPetType);
        }

        // GET: TPetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPetType tPetType = db.TPetTypes.Find(id);
            if (tPetType == null)
            {
                return HttpNotFound();
            }
            return View(tPetType);
        }

        // POST: TPetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TPetType tPetType = db.TPetTypes.Find(id);
            db.TPetTypes.Remove(tPetType);
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
