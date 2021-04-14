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
            myModel.Medications = db.TMedications;
            myModel.PetVisitMedications = db.TVisitMedications.Where(x => x.intVisitID == intVisitId).ToList();
            return View(myModel);
        }
    }
}