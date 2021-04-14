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
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["intPetID"] = id;
            var petName = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetName).FirstOrDefault();
            var petID = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetNumber).FirstOrDefault();
            var ownerName = (from o in db.TOwners
                             join p in db.TPets
                             on o.intOwnerID equals p.intOwnerID
                             where p.intPetID == id
                             select new
                             {
                                 firstName = o.strFirstName,
                                 lastName = o.strLastName
                             }).FirstOrDefault();
            List<EmployeeInformation> doctorList = (from e in db.TEmployees
                                   join j in db.TJobTitles
                                   on e.intJobTitleID equals j.intJobTitleID
                                   where j.strJobTitleDesc == "Doctor"
                                   select new EmployeeInformation
                                   {
                                       intEmployeeID = e.intEmployeeID,
                                       intJobTitleID = j.intJobTitleID,
                                       strEmployeeName = "Dr. " + e.strFirstName + " " + e.strLastName
                                   }).ToList();

            if (petName == null)
            {
                return HttpNotFound();
            }

            ViewBag.PetName = petName;
            ViewBag.PetID = petID;
            ViewBag.OwnerName = ownerName.firstName + " " + ownerName.lastName;
            ViewBag.intVisitReasonID = new SelectList(db.TVisitReasons, "intVisitReasonID", "strVisitReason");
            ViewBag.intEmployeeID = new SelectList(doctorList, "intEmployeeID", "strEmployeeName");

            return View();
        }

        // POST: TVisits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVisit tVisit)
        {
            int petID = (int)Session["intPetID"];
            if (ModelState.IsValid) {
                TVisit newPetVisit = new TVisit()
                {
                    intPetID = petID,
                    dtmDateOfVist = DateTime.Now,
                    intVisitReasonID = tVisit.intVisitReasonID
                };
                db.TVisits.Add(newPetVisit);
                db.SaveChanges();


                int lastInsertedVisitID = db.TVisits.Max(v => v.intVisitID);
                Session["intVisitId"] = lastInsertedVisitID;

                TVisitEmployee newPetVisitEmployee = new TVisitEmployee()
                {
                    intVisitID = lastInsertedVisitID,
                    intEmployeeID = tVisit.intEmployeeID
                };

                db.TVisitEmployees.Add(newPetVisitEmployee);
                db.SaveChanges();

                //Remove existing data from session for pet id

                Session["isHealthExam"] = null;
                int healthExam = db.TVisitReasons.Where(x => x.strVisitReason == "Health Exam").Select(z => z.intVisitReasonID).FirstOrDefault();
                
                if(newPetVisit.intVisitReasonID == healthExam)
                {
                    int healthExamService = db.TServices.Where(x => x.strServiceDesc == "Health Exam").Select(z => z.intServiceID).FirstOrDefault();
                    TVisitService visitService = new TVisitService()
                    {
                        intVisitID = lastInsertedVisitID,
                        intServiceID = healthExamService
                    };

                    db.TVisitServices.Add(visitService);
                    db.SaveChanges();
                    int lastInsertedVisitServiceID = db.TVisitServices.Max(v => v.intVisitServiceID);

                    Session["isHealthExam"] = true;
                    Session["intVisitServiceID"] = lastInsertedVisitServiceID;
                    return RedirectToAction("Create", "THealthExam", new { id = petID, dateOfVisit = newPetVisit.dtmDateOfVist });
                }
                else
                {
                    return RedirectToAction("Index", "VisitServices");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
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

        public ActionResult PetVisits(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["intPetID"] = id;
            var petName = db.TPets.Where(x => x.intPetID == id).Select(x => x.strPetName).FirstOrDefault();
            var tVisits = db.TVisits.Where(x => x.intPetID == id);

            if (petName == null)
            {
                return HttpNotFound();
            }
            ViewBag.PetName = petName;
            
            return View(tVisits);
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
