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
    public class TVisitsController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TVisits
        public ActionResult Index()
        {
            var tVisits = db.TVisits.Include(t => t.intVisitReasonID);
            return View(tVisits.ToList());
        }

        // GET: TVisits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVisit tVisit = db.TVisits.Find(id);
            if (tVisit == null)
            {
                return HttpNotFound();
            }
            return View(tVisit);
        }

        // GET: TVisits/Create
        public ActionResult Create()
        {
            ViewBag.intVisitReasonID = new SelectList(db.TVisitReasons, "intVisitReasonID", "strVisitReason");
            return View();
        }

        // POST: TVisits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intVisitID,intPetID,intVisitReasonID,dtmDateOfVist")] TVisit tVisit)
        {
            if (ModelState.IsValid) {
                db.TVisits.Add(tVisit);
                db.SaveChanges();

                int intVisitReasonID = Int16.Parse("intVisitReasonID");

                switch(intVisitReasonID) 
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;


				}

                return RedirectToAction("Index");
            }

            ViewBag.intVisitReasonID = new SelectList(db.TVisitReasons, "intVisitReasonID", "strVisitReason", tVisit.intVisitReasonID);
            return View(tVisit);
        }

        // GET: TVisits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVisit tVisit = db.TVisits.Find(id);
            if (tVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.intVisitReasonID = new SelectList(db.TVisitReasons, "intVisitReasonID", "strVisitReason", tVisit.intVisitReasonID);
            return View(tVisit);
        }

        // POST: TVisits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intVisitID,intPetID,intVisitReasonID,dtmDateOfVist")] TVisit tVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intVisitReasonID = new SelectList(db.TVisitReasons, "intVisitReasonID", "strVisitReason", tVisit.intVisitReasonID);
            return View(tVisit);
        }

        // GET: TVisits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVisit tVisit = db.TVisits.Find(id);
            if (tVisit == null)
            {
                return HttpNotFound();
            }
            return View(tVisit);
        }

        // POST: TVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TVisit tVisit = db.TVisits.Find(id);
            db.TVisits.Remove(tVisit);
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
