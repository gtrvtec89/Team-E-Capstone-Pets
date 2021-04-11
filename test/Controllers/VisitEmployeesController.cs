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
        private CapstoneEntities db = new CapstoneEntities();

        // GET: VisitEmployees
        public ActionResult Index()
        {
            VisitEmployeeViewModel myModel = new VisitEmployeeViewModel();
            int intVisitId = (int)Session["intVisitId"];
            myModel.Employees = db.TEmployees;
            myModel.PetVisitEmployees = db.TVisitEmployees.Where(x => x.intVisitID == intVisitId).ToList();
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
    }
}