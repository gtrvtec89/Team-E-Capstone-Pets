using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class VisitMedicationsController : Controller
    {
        private Entities1 db = new Entities1();

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
            //List<TMedication> availableMedication = (from m in db.TMedications
            //                                         where !(from tvm in db.TVisitMedications
            //                                              where tvm.intVisitID == intVisitId
            //                                              select tvm.intMedicationID).Contains(m.intMedicationID)
            //                                      select m).Distinct().ToList();

            List<TMedication> availableMedication = db.TMedications.Where(x => x.intQuantity > 0).ToList();

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
                intVisitMedicationID = -1,
                intVisitID = intVisitId,
                intMedicationID = medicationId,
                dtmDatePrescribed = DateTime.Now,
                intQuantity = 0,
                strMedicationNotes = ""
            };

            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
            return View(visitMedication);
        }

        [HttpPost]
        public ActionResult AddPetMedication([Bind(Include = "intVisitMedicationID, intVisitID, intMedicationID, dtmDatePrescribed, intQuantity, strMedicationNotes")]TVisitMedication visitMedication)
        {
            int intMedicationId = (int)Session["intMedicationID"];
            int intPetId = (int)Session["intPetID"];
            int intVisitId = (int)Session["intVisitId"];
            int updatedQuantity = 0;

            if (ModelState.IsValid)
            {
                TMedication medication = db.TMedications.Where(x => x.intMedicationID == intMedicationId).FirstOrDefault();
                int currentQuantity = db.TMedications.Where(x => x.intMedicationID == intMedicationId).Select(z => z.intQuantity).FirstOrDefault();
                
                //Precursory check for medications
                if (currentQuantity >= visitMedication.intQuantity)
                {
                    updatedQuantity = currentQuantity - visitMedication.intQuantity;
                }
                else
                {
                    visitMedication.intQuantity = 0;
                    updatedQuantity = currentQuantity;
                    ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
                    ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
                    return View(visitMedication);
                }

                // Insert Visit Medication
                var id = db.TVisitMedications.Where(x => x.intVisitID == intVisitId && x.intMedicationID == intMedicationId).Select(z => z.intVisitMedicationID).FirstOrDefault();
                if(id == 0 )
                {
                    TVisitMedication newVisitMedication = new TVisitMedication()
                    {
                        intVisitID = visitMedication.intVisitID,
                        intMedicationID = intMedicationId,
                        dtmDatePrescribed = visitMedication.dtmDatePrescribed,
                        intQuantity = visitMedication.intQuantity,
                        strMedicationNotes = visitMedication.strMedicationNotes
                    };

                    db.TVisitMedications.Add(newVisitMedication);
                    db.SaveChanges();
                }
                else
                {
                    int currentVisitMedicationQuantity = db.TVisitMedications.Where(x => x.intVisitMedicationID == id).Select(z => z.intQuantity).FirstOrDefault();
                    TVisitMedication updatedVisitMedication = new TVisitMedication()
                    {
                        intVisitMedicationID = id, 
                        intVisitID = visitMedication.intVisitID,
                        intMedicationID = intMedicationId,
                        dtmDatePrescribed = visitMedication.dtmDatePrescribed,
                        intQuantity = currentVisitMedicationQuantity + visitMedication.intQuantity,
                        strMedicationNotes = visitMedication.strMedicationNotes
                    };

                    db.Entry(updatedVisitMedication).State = EntityState.Modified;
                    db.SaveChanges();
                };

                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@intMedicationID", intMedicationId),
                    new SqlParameter("@strMedicationName", medication.strMedicationName),
                    new SqlParameter("@strMedicationDesc", medication.strMedicationDesc),
                    new SqlParameter("@dblCost", medication.dblCost),
                    new SqlParameter("@dblPrice", medication.dblPrice),
                    new SqlParameter("@strNotes", medication.strNotes),
                    new SqlParameter("@intQuantity", updatedQuantity),
                    new SqlParameter("@intMethodID", medication.intMethodID),
                };

                db.Database.ExecuteSqlCommand("uspUpdateMedication @intMedicationID, @strMedicationName, @strMedicationDesc,@dblCost, @dblPrice,@strNotes,@intQuantity, @intMethodID ", param);

                // Redirect to the visit medications page
                return RedirectToAction("Index");
            }

            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
            return View(visitMedication);
        }

        public ActionResult DeletePetMedication(int medicationId)
        {
             
            TVisitMedication visitMedication = db.TVisitMedications.Where(x => x.intVisitMedicationID == medicationId).FirstOrDefault();
            TMedication medicationInfo = db.TMedications.Where(x => x.intMedicationID == medicationId).FirstOrDefault();

            SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@intMedicationID", medicationInfo.intMedicationID),
                        new SqlParameter("@strMedicationName", medicationInfo.strMedicationName),
                        new SqlParameter("@strMedicationDesc", medicationInfo.strMedicationDesc),
                        new SqlParameter("@dblCost", medicationInfo.dblCost),
                        new SqlParameter("@dblPrice", medicationInfo.dblPrice),
                        new SqlParameter("@strNotes", medicationInfo.strNotes),
                        new SqlParameter("@intQuantity", medicationInfo.intQuantity + visitMedication.intQuantity),
                        new SqlParameter("@intMethodID", medicationInfo.intMethodID)
                    };

            db.Database.ExecuteSqlCommand("uspUpdateMedication @intMedicationID, @strMedicationName, @strMedicationDesc,@dblCost, @dblPrice,@strNotes,@intQuantity, @intMethodID ", param);
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
            int intVisitId = (int)Session["intVisitId"];
            if (ModelState.IsValid)
            {
                int oldQuantity = db.TVisitMedications.Where(x => x.intVisitMedicationID == visitMedication.intVisitMedicationID).Select(z => z.intQuantity).FirstOrDefault();
                int medicationId = db.TVisitMedications.Where(x => x.intVisitMedicationID == visitMedication.intVisitMedicationID).Select(z => z.intMedicationID).FirstOrDefault();
                TMedication medicationInfo = db.TMedications.Where(x => x.intMedicationID == medicationId).FirstOrDefault();

                if (oldQuantity > visitMedication.intQuantity)
                {
                    int medQuantity = oldQuantity - visitMedication.intQuantity;
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@intMedicationID", medicationInfo.intMedicationID),
                        new SqlParameter("@strMedicationName", medicationInfo.strMedicationName),
                        new SqlParameter("@strMedicationDesc", medicationInfo.strMedicationDesc),
                        new SqlParameter("@dblCost", medicationInfo.dblCost),
                        new SqlParameter("@dblPrice", medicationInfo.dblPrice),
                        new SqlParameter("@strNotes", medicationInfo.strNotes),
                        new SqlParameter("@intQuantity", medicationInfo.intQuantity + medQuantity),
                        new SqlParameter("@intMethodID", medicationInfo.intMethodID),
                    };

                    db.Database.ExecuteSqlCommand("uspUpdateMedication @intMedicationID, @strMedicationName, @strMedicationDesc,@dblCost, @dblPrice,@strNotes,@intQuantity, @intMethodID ", param);
                    db.uspUpdateVisitMedication(visitMedication.intVisitMedicationID, visitMedication.intQuantity, visitMedication.strMedicationNotes);
                    //db.Database.ExecuteSqlCommand("uspUpdateVisitMedication @intVisitMedicationID, @intQuantity", visitMedication.intVisitMedicationID, visitMedication.intQuantity);

                }
                else if (medicationInfo.intQuantity >= (visitMedication.intQuantity - oldQuantity) && visitMedication.intQuantity > oldQuantity)
                {
                    int subQuantity = visitMedication.intQuantity - oldQuantity;
                    
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@intMedicationID", medicationInfo.intMedicationID),
                        new SqlParameter("@strMedicationName", medicationInfo.strMedicationName),
                        new SqlParameter("@strMedicationDesc", medicationInfo.strMedicationDesc),
                        new SqlParameter("@dblCost", medicationInfo.dblCost),
                        new SqlParameter("@dblPrice", medicationInfo.dblPrice),
                        new SqlParameter("@strNotes", medicationInfo.strNotes),
                        new SqlParameter("@intQuantity", medicationInfo.intQuantity - subQuantity),
                        new SqlParameter("@intMethodID", medicationInfo.intMethodID)
                    };

                    db.Database.ExecuteSqlCommand("uspUpdateMedication @intMedicationID, @strMedicationName, @strMedicationDesc,@dblCost, @dblPrice,@strNotes,@intQuantity, @intMethodID ", param);
                    db.uspUpdateVisitMedication(visitMedication.intVisitMedicationID, visitMedication.intQuantity, visitMedication.strMedicationNotes);
                    //db.Database.ExecuteSqlCommand("uspUpdateVisitMedication @intVisitMedicationID, @intQuantity", visitMedication.intVisitMedicationID, visitMedication.intQuantity);

                }
                else
                {
                    //TO DO: Need some error handling
                    db.uspUpdateVisitMedication(visitMedication.intVisitMedicationID, oldQuantity, visitMedication.strMedicationNotes);
                    //db.Database.ExecuteSqlCommand("uspUpdateVisitMedication @intVisitMedicationID, @intQuantity", visitMedication.intVisitMedicationID, oldQuantity);

                }
                return RedirectToAction("Index");
            }

            ViewBag.Name = db.TPets.Where(x => x.intPetID == intPetId).Select(z => z.strPetName).FirstOrDefault();
            ViewBag.intMedicationID = new SelectList(db.TMedications, "intMedicationID", "strMedicationName", visitMedication.intMedicationID);
            return View(visitMedication);
        }
    }
}