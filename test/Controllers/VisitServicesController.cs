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
        private CapstoneEntities db = new CapstoneEntities();

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
                          where j.intJobTitleID == 4
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
            myModel.Services = db.TServices;
            myModel.PetVisitServices = db.TVisitServices.Where(x => x.intVisitID == intVisitId).ToList();
            ViewBag.Name = petData.name;
            return View(myModel);
        }

        public ActionResult AddPetService(int serviceID)
        {
            TVisitService visitService = new TVisitService()
            {
                intServiceID = serviceID,
                intVisitID = (int)Session["intVisitId"]
            };

            db.TVisitServices.Add(visitService);
            db.SaveChanges();

            int lastInsertedVisitServiceID = db.TVisitServices.Max(v => v.intVisitServiceID);
            int service = db.TVisitServices.Where(x => x.intVisitServiceID == lastInsertedVisitServiceID).Select(x => x.intServiceID).FirstOrDefault();
            //If it's a health exam
            int healthExamService = db.TServices.Where(x => x.strServiceDesc == "Health Exam").Select(z => z.intServiceID).FirstOrDefault();
            if (service == healthExamService)
            {
                DateTime dateOfHealthExam = DateTime.Now;
                int intPetId = (int)Session["intPetID"];
                return RedirectToAction("Create", "THealthExam", new { id = intPetId , dateOfVisit = dateOfHealthExam });
            }

            return RedirectToAction("Index");
        }



    }
}