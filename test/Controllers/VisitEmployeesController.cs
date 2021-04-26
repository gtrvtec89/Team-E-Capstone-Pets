using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class VisitEmployeesController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: VisitEmployees
        public ActionResult Index()
        {
            VisitEmployeeViewModel myModel = new VisitEmployeeViewModel();
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

            int doctorJobTitleId = db.TJobTitles.Where(t => t.strJobTitleDesc == "Doctor").Select(z => z.intJobTitleID).FirstOrDefault();
            List<TEmployee> availableEmployees = (from e in db.TEmployees
                                        where e.intJobTitleID != doctorJobTitleId
                                        where !(from tve in db.TVisitEmployees
                                                where tve.intVisitID == intVisitId
                                                select tve.intEmployeeID).Contains(e.intEmployeeID)
                                        select e).Distinct().ToList();

            myModel.Employees = availableEmployees;
            myModel.PetVisitEmployees = db.TVisitEmployees.Where(x => x.intVisitID == intVisitId && x.TEmployee.intJobTitleID != doctorJobTitleId).ToList();
            ViewBag.Name = petData.name;
            
            return View(myModel);
        }

        public ActionResult AddPetEmployee(int employeeID)
        {
            TVisitEmployee visitEmployee = new TVisitEmployee()
            {
                intEmployeeID = employeeID,
                intVisitID = (int)Session["intVisitId"]
            };

            db.TVisitEmployees.Add(visitEmployee);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteVisitEmployee(int employeeID)
        {
            int intVisitId = (int)Session["intVisitId"];
            TVisitEmployee visitEmployee = db.TVisitEmployees.Where(x => x.intVisitID == intVisitId && x.intEmployeeID == employeeID).FirstOrDefault();
            db.TVisitEmployees.Remove(visitEmployee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}