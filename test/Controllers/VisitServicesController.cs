using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class VisitServicesController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: VisitServices
        public ActionResult Index()
        {
            VisitServiceViewModel myModel = new VisitServiceViewModel();
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
            List<TService> availableServices = (from s in db.TServices
                                                  where !(from tvs in db.TVisitServices
                                                          where tvs.intVisitID == intVisitId
                                                          select tvs.intServiceID).DefaultIfEmpty().Contains(s.intServiceID)
                                                  select s).ToList();


            myModel.Services = availableServices;
            myModel.PetVisitServices = db.TVisitServices.Where(x => x.intVisitID == intVisitId).ToList();
            ViewBag.Name = petData.name;
            return View(myModel);
        }

        public ActionResult AddPetService(int serviceID)
        {

            int intPetId = (int)Session["intPetID"];
            //int lastInsertedVisitServiceID = db.TVisitServices.Max(v => v.intVisitServiceID);
            int healthExamService = db.TServices.Where(x => x.strServiceDesc == "Health Exam").Select(z => z.intServiceID).FirstOrDefault();
            if (serviceID == healthExamService)
            {
                return RedirectToAction("Create", "THealthExam", new { id = intPetId });
            }
            else
            {
                TVisitService visitService = new TVisitService()
                {
                    intServiceID = serviceID,
                    intVisitID = (int)Session["intVisitId"]
                };

                db.TVisitServices.Add(visitService);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeletePetService(int serviceID)
        {
            int intVisitId = (int)Session["intVisitId"];
            TVisitService visitService = db.TVisitServices.Where(x => x.intVisitServiceID == serviceID).FirstOrDefault();
            db.TVisitServices.Remove(visitService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}