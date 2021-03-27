using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class THealthExamController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: THealthExam
        public ActionResult Index()
        {
            return View();
        }

        // GET: Create 
        public ActionResult Create()
        {
            //ViewBag.intStateID = new SelectList(db.TVisitServices, "intVisitServiceID", "strStateCode");
            return View();
        }

        // GET: Create 
        [HttpPost]
        public ActionResult Create(HealthExam healthExam )
        {
            //SqlParameter[] param = new SqlParameter[]
            //{

            //};
            //db.Database.SqlQuery("EXECUTE uspAddPetVisit", param);
            return View();
        }
    }
}