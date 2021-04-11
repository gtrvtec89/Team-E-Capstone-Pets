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
            myModel.Services = db.TServices;
            myModel.PetVisitServices = db.TVisitServices.Where(x => x.intVisitID == intVisitId).ToList();
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
            if(service == 8)
            {
                DateTime dateOfHealthExam = DateTime.Now;
                int intPetId = (int)Session["intPetID"];
                return RedirectToAction("Create", "THealthExam", new { id = intPetId , dateOfVisit = dateOfHealthExam });
            }

            return RedirectToAction("Index");
        }



    }
}