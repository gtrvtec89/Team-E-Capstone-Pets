using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class VisitMedicationsController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: VisitMedications
        public ActionResult Index()
        {
            VisitMedicationViewModel myModel = new VisitMedicationViewModel();
            int intVisitId = (int)Session["intVisitId"];

            var doctor = (from e in db.TEmployees
                          join ve in db.TVisitEmployees
                          on e.intEmployeeID equals ve.intEmployeeID
                          join j in db.TJobTitles
                          on e.intJobTitleID equals j.intJobTitleID
                          where ve.intVisitID == intVisitId
                          where j.strJobTitleDesc == "Doctor"
                          select new
                          {
                              doctorName = "Dr. " + e.strFirstName + " " + e.strLastName
                          }).FirstOrDefault();

            var petData = (from p in db.TPets
                           join v in db.TVisits
                           on p.intPetID equals v.intPetID
                           where v.intVisitID == intVisitId
                           select new
                           {
                               dtmDateOfVisit = v.dtmDateOfVist,
                               name = p.strPetName
                           }).FirstOrDefault();
            myModel.strPetName = petData.name;
            myModel.strDoctor = doctor.doctorName;
            myModel.dtmDateOfVisit = petData.dtmDateOfVisit;
            List<TMedication> availableMedication = (from m in db.TMedications
                                                     where !(from tvm in db.TVisitMedications
                                                          where tvm.intVisitID == intVisitId
                                                          select tvm.intMedicationID).Contains(m.intMedicationID)
                                                  select m).Distinct().ToList();

            myModel.Medications = availableMedication;
            myModel.PetVisitMedications = db.TVisitMedications.Where(x => x.intVisitID == intVisitId).ToList();
            ViewBag.Name = petData.name;
            return View(myModel);
        }
        public ActionResult AddPetMedication(int medicationId)
        {
            int intVisitId = (int)Session["intVisitId"];
            int intPetId = (int)Session["intPetID"];

            Session["intMedicationID"] = medicationId;
            TVisitMedication visitMedication = new TVisitMedication()
            {
                intVisitMedicationID = 4,
                intVisitID = intVisitId,
                intMedicationID = medicationId,
                dtmDatePrescribed = DateTime.Now,
                intQuantity = 0
            };

            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
            return View(visitMedication);
        }

        [HttpPost]
        public ActionResult AddPetMedication([Bind(Include = "intVisitMedicationID, intVisitID, intMedicationID, dtmDatePrescribed, intQuantity")]TVisitMedication visitMedication)
        {
            int intMedicationId = (int)Session["intMedicationID"];
            int intPetId = (int)Session["intPetID"];

            if (ModelState.IsValid)
            {
                TVisitMedication newVisitMedication = new TVisitMedication()
                {
                    intVisitID = visitMedication.intVisitID,
                    intMedicationID = intMedicationId,
                    dtmDatePrescribed = visitMedication.dtmDatePrescribed,
                    intQuantity = visitMedication.intQuantity
                };
                db.TVisitMedications.Add(newVisitMedication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
            return View(visitMedication);
        }

        public ActionResult DeletePetMedication(int medicationId)
        {
            TVisitMedication visitMedication = db.TVisitMedications.Where(x => x.intVisitMedicationID == medicationId).FirstOrDefault();
            db.TVisitMedications.Remove(visitMedication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EditMedication(int medicationId)
        {
            int intPetId = (int)Session["intPetID"];
            TVisitMedication visitMedication = db.TVisitMedications.Find(medicationId);
            if (visitMedication == null)
            {
                return HttpNotFound();
            }

            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
            
            return View(visitMedication);
        }

        [HttpPost]
        public ActionResult EditMedication(TVisitMedication visitMedication)

        {
            int intPetId = (int)Session["intPetID"];
            if (ModelState.IsValid)
            {
                db.Entry(visitMedication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
            return View(visitMedication);
        }
    }
}