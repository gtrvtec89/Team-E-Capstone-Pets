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
    public class TBreedsController : Controller
    {
        private capstoneEntities db = new capstoneEntities();

        // GET: TBreeds
        public ActionResult Index()
        {
            return View(db.TBreeds.ToList());
        }

        // GET: TBreeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBreed tBreed = db.TBreeds.Find(id);
            if (tBreed == null)
            {
                return HttpNotFound();
            }
            return View(tBreed);
        }

        // GET: TBreeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TBreeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intBreedID,strBreedName")] TBreed tBreed)
        {
            if (ModelState.IsValid)
            {
                db.TBreeds.Add(tBreed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBreed);
        }

        // GET: TBreeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBreed tBreed = db.TBreeds.Find(id);
            if (tBreed == null)
            {
                return HttpNotFound();
            }
            return View(tBreed);
        }

        // POST: TBreeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intBreedID,strBreedName")] TBreed tBreed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBreed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBreed);
        }

        // GET: TBreeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBreed tBreed = db.TBreeds.Find(id);
            if (tBreed == null)
            {
                return HttpNotFound();
            }
            return View(tBreed);
        }

        // POST: TBreeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBreed tBreed = db.TBreeds.Find(id);
            db.TBreeds.Remove(tBreed);
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
