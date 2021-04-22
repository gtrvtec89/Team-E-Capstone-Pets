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
    public class TMedicationsController : Controller
    {
        private Entities db = new Entities();

        // GET: TMedications
        public ActionResult Index()
        {
            return View(db.TMedications.ToList());
        }

        // GET: TMedications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMedication tMedication = db.TMedications.Find(id);
            if (tMedication == null)
            {
                return HttpNotFound();
            }
            return View(tMedication);
        }

        // GET: TMedications/Create
        public ActionResult Create()
        {
            ViewBag.intMethodID = new SelectList(db.TMethods, "intMethodID", "strMethod");
            return View();
        }

        // POST: TMedications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intMedicationID,strMedicationName,strMedicationDesc,dblCost,dblPrice,strNotes,intQuantity,intMethodID")] TMedication tMedication)
        {
            if (ModelState.IsValid)
            {
                db.TMedications.Add(tMedication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tMedication);
        }

        // GET: TMedications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMedication tMedication = db.TMedications.Find(id);
            if (tMedication == null)
            {
                return HttpNotFound();
            }
            ViewBag.intMethodID = new SelectList(db.TMethods, "intMethodID", "strMethod");
            return View(tMedication);
        }

        // POST: TMedications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intMedicationID,strMedicationName,strMedicationDesc,dblCost,dblPrice,strNotes,intQuantity,intMethodID")] TMedication tMedication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tMedication).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tMedication);
        }

        // GET: TMedications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMedication tMedication = db.TMedications.Find(id);
            if (tMedication == null)
            {
                return HttpNotFound();
            }
            return View(tMedication);
        }

        // POST: TMedications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TMedication tMedication = db.TMedications.Find(id);
            db.TMedications.Remove(tMedication);
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
