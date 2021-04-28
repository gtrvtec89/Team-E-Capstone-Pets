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
    public class TVisitMedicationsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: TVisitMedications
        public ActionResult Index()
        {
            var tVisitMedications = db.TVisitMedications.Include(t => t.TMedication).Include(t => t.TVisit);
            return View(tVisitMedications.ToList());
        }

        // GET: TVisitMedications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVisitMedication tVisitMedication = db.TVisitMedications.Find(id);
            if (tVisitMedication == null)
            {
                return HttpNotFound();
            }
            return View(tVisitMedication);
        }

        // GET: TVisitMedications/Create
        public ActionResult Create()
        {
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName");
            ViewBag.intVisitID = new SelectList(db.TVisits, "intVisitID", "intVisitID");
            return View();
        }

        // POST: TVisitMedications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intVisitMedicationID,intVisitID,intMedicationID,dtmDatePrescribed,intQuantity")] TVisitMedication tVisitMedication)
        {
            if (ModelState.IsValid)
            {
                db.TVisitMedications.Add(tVisitMedication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", tVisitMedication.intMedicationID);
            ViewBag.intVisitID = new SelectList(db.TVisits, "intVisitID", "intVisitID", tVisitMedication.intVisitID);
            return View(tVisitMedication);
        }

        // GET: TVisitMedications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVisitMedication tVisitMedication = db.TVisitMedications.Find(id);
            if (tVisitMedication == null)
            {
                return HttpNotFound();
            }
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", tVisitMedication.intMedicationID);
            ViewBag.intVisitID = new SelectList(db.TVisits, "intVisitID", "intVisitID", tVisitMedication.intVisitID);
            return View(tVisitMedication);
        }

        // POST: TVisitMedications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intVisitMedicationID,intVisitID,intMedicationID,dtmDatePrescribed,intQuantity")] TVisitMedication tVisitMedication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tVisitMedication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", tVisitMedication.intMedicationID);
            ViewBag.intVisitID = new SelectList(db.TVisits, "intVisitID", "intVisitID", tVisitMedication.intVisitID);
            return View(tVisitMedication);
        }

        // GET: TVisitMedications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVisitMedication tVisitMedication = db.TVisitMedications.Find(id);
            if (tVisitMedication == null)
            {
                return HttpNotFound();
            }
            return View(tVisitMedication);
        }

        public ActionResult PetMedications(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["intPetID"] = id;
            var petName = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetName).FirstOrDefault();

            // Get data from database with LINQ
            List<Medication> data = (from m in db.TMedications
                                     join vm in db.TVisitMedications
                                     on m.intMedicationID equals vm.intMedicationID
                                     join v in db.TVisits
                                     on vm.intVisitID equals v.intVisitID
                                     where v.intPetID == id
                                     select new Medication
                                     {
                                         intVisitMedicationID = vm.intVisitMedicationID,
                                         intVisitID = vm.intVisitID,
                                         intMedicationID = vm.intMedicationID,
                                         dtmDatePrescribed = vm.dtmDatePrescribed,
                                         intQuantity = vm.intQuantity,
                                         strMedicationName = m.strMedicationName,
                                     }).ToList();

            // Convert raw data
            List<TVisitMedication> tPetMedications = data.Select(a => new TVisitMedication
            {
                intVisitMedicationID = a.intVisitMedicationID,
                intVisitID = a.intVisitID,
                intMedicationID = a.intMedicationID,
                dtmDatePrescribed = a.dtmDatePrescribed,
                intQuantity = a.intQuantity,
                strMedicationName = a.strMedicationName,
            }).ToList();
            if (petName == null)
            {
                return HttpNotFound();
            }
            ViewBag.PetName = petName;

            return View(tPetMedications);
        }


        //TO DO: Finish the logic for this
        public ActionResult PetMedicationDetails(int id)
        {
            int intPetId = (int)Session["intPetID"];
            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            TVisitMedication visitMedication = db.TVisitMedications.Where(x => x.intVisitMedicationID == id).FirstOrDefault();
            return View(visitMedication);
        }

        // POST: TVisitMedications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TVisitMedication tVisitMedication = db.TVisitMedications.Find(id);
            db.TVisitMedications.Remove(tVisitMedication);
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


        public ActionResult PetProfileMedications(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["intPetID"] = id;
            var petName = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetName).FirstOrDefault();

            // Get data from database with LINQ
            List<Medication> data = (from m in db.TMedications
                                     join vm in db.TVisitMedications
                                     on m.intMedicationID equals vm.intMedicationID
                                     join v in db.TVisits
                                     on vm.intVisitID equals v.intVisitID
                                     where v.intPetID == id
                                     select new Medication
                                     {
                                         intVisitMedicationID = vm.intMedicationID,
                                         intVisitID = vm.intVisitID,
                                         intMedicationID = vm.intMedicationID,
                                         dtmDatePrescribed = vm.dtmDatePrescribed,
                                         intQuantity = vm.intQuantity,
                                         strMedicationName = m.strMedicationName
                                     }).ToList();

            // Convert raw data
            List<TVisitMedication> tPetMedications = data.Select(a => new TVisitMedication
            {
                intVisitMedicationID = a.intMedicationID,
                intVisitID = a.intVisitID,
                intMedicationID = a.intMedicationID,
                dtmDatePrescribed = a.dtmDatePrescribed,
                intQuantity = a.intQuantity,
                strMedicationName = a.strMedicationName
            }).ToList();
            if (petName == null)
            {
                return HttpNotFound();
            }
            ViewBag.PetName = petName;

            return View(tPetMedications);
        }
    }
}
