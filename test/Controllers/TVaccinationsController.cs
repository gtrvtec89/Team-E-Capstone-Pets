using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;
using test.Models;

namespace test.Controllers
{
    public class TVaccinationsController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TVaccinations
        public ActionResult Index()
        {
            var tVaccinations = db.TVaccinations.Include(t => t.TVisitService);
            return View(tVaccinations.ToList());
        }

        // GET: TVaccinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVaccination tVaccination = db.TVaccinations.Find(id);
            if (tVaccination == null)
            {
                return HttpNotFound();
            }
            return View(tVaccination);
        }

        // GET: TVaccinations/Create
        public ActionResult Create()
        {
            ViewBag.intVisitServiceID = new SelectList(db.TVisitServices, "intVisitServiceID", "intVisitServiceID");
            return View();
        }

        // POST: TVaccinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intVaccinationID,intVisitServiceID,dtmDateOfVaccination,dtmDateOfExpiration,strVaccineDesc,strRabiesNumber")] TVaccination tVaccination)
        {
            if (ModelState.IsValid)
            {
                db.TVaccinations.Add(tVaccination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.intVisitServiceID = new SelectList(db.TVisitServices, "intVisitServiceID", "intVisitServiceID", tVaccination.intVisitServiceID);
            return View(tVaccination);
        }

        // GET: TVaccinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVaccination tVaccination = db.TVaccinations.Find(id);
            if (tVaccination == null)
            {
                return HttpNotFound();
            }
            ViewBag.intVisitServiceID = new SelectList(db.TVisitServices, "intVisitServiceID", "intVisitServiceID", tVaccination.intVisitServiceID);
            return View(tVaccination);
        }

        // POST: TVaccinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intVaccinationID,intVisitServiceID,dtmDateOfVaccination,dtmDateOfExpiration,strVaccineDesc,strRabiesNumber")] TVaccination tVaccination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tVaccination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intVisitServiceID = new SelectList(db.TVisitServices, "intVisitServiceID", "intVisitServiceID", tVaccination.intVisitServiceID);
            return View(tVaccination);
        }

        // GET: TVaccinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVaccination tVaccination = db.TVaccinations.Find(id);
            if (tVaccination == null)
            {
                return HttpNotFound();
            }
            return View(tVaccination);
        }

        public ActionResult PetVaccinations(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["intPetID"] = id;
            var petName = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetName).FirstOrDefault();
            List<Vaccination> tVaccinations = (from vc in db.TVaccinations
                           join vs in db.TVisitServices
                           on vc.intVisitServiceID equals vs.intVisitServiceID
                           join v in db.TVisits
                           on vs.intVisitID equals v.intVisitID
                           where v.intPetID == id
                           select new Vaccination
                           {
                               intVaccinationID = vc.intVaccinationID,
                               intVisitServiceID = vc.intVisitServiceID,
                               dtmDateOfVaccination = vc.dtmDateOfVaccination,
                               dtmDateOfExpiration = vc.dtmDateOfExpiration,
                               strVaccineDesc = vc.strVaccineDesc,
                               strRabiesNumber = vc.strRabiesNumber
                           }).ToList();

            if (petName == null)
            {
                return HttpNotFound();
            }
            ViewBag.PetName = petName;

            return View(tVaccinations);
        }

        // POST: TVaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TVaccination tVaccination = db.TVaccinations.Find(id);
            db.TVaccinations.Remove(tVaccination);
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
