using System;
using System.Collections.Generic;
using System.Linq;
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
            myModel.Medications = db.TMedications;
            myModel.PetVisitMedications = db.TVisitMedications.Where(x => x.intVisitID == intVisitId).ToList();
            ViewBag.Name = petData.name;
            return View(myModel);
        }
        public ActionResult AddPetMedication(int medicationId)
        {
            int intVisitId = (int)Session["intVisitId"];
            TVisitMedication visitMedication = new TVisitMedication()
            {
                intVisitID = intVisitId,
                intMedicationID = medicationId,
                dtmDatePrescribed = DateTime.Now,
                intQuantity = 0
            };

            db.TVisitMedications.Add(visitMedication);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeletePetMedication(int medicationId)
        {
            int intVisitId = (int)Session["intVisitId"];
            TVisitMedication visitMedication = db.TVisitMedications.Where(x => x.intVisitID == intVisitId && x.intMedicationID == medicationId).FirstOrDefault();
            db.TVisitMedications.Remove(visitMedication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateMedication(int visitMedicationId)
        {
            TVisitMedication visitMedication = db.TVisitMedications.Where(x => x.intVisitMedicationID == visitMedicationId).FirstOrDefault();

            TVisitMedication model = new TVisitMedication
            {
                intVisitMedicationID = visitMedication.intVisitMedicationID,
                intVisitID = visitMedication.intVisitID,
                intMedicationID =visitMedication.intMedicationID,
                dtmDatePrescribed = visitMedication.dtmDatePrescribed,
                intQuantity = visitMedication.intQuantity
            };

            return PartialView("UpdateMedication", model);
        }
    }
}