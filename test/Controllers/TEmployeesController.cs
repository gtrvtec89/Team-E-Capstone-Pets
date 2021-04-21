using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;

namespace test.Controllers
{
    public class TEmployeesController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TEmployees
        public ActionResult Index()
        {
            var tEmployee = db.TEmployees.Include(t => t.TJobTitle);
            return View(tEmployee.ToList());
        }

        // GET: TEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEmployee tEmployee = db.TEmployees.Find(id);
            if (tEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tEmployee);
        }

        // GET: TEmployees/Create
        public ActionResult Create()
        {
            ViewBag.intJobTitleID = new SelectList(db.TJobTitles, "intJobTitleID", "strJobTitleDesc");
            return View();
        }

        // POST: TEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intEmployeeID,strFirstName,strLastName,intJobTitleID")] TEmployee tEmployee)
        {
            if (ModelState.IsValid)
            {
                //db.TEmployees.Add(tEmployee);
                //db.SaveChanges();
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter ("@strFirstName", tEmployee.strFirstName),
                    new SqlParameter ("@strLastName", tEmployee.strLastName),
                    new SqlParameter ("@intJobTitleID", tEmployee.intJobTitleID)
                };
                db.Database.ExecuteSqlCommand("uspAddUserEmployee @strFirstName, @strLastName, @intJobTitleID", param);
                return RedirectToAction("Index");
            }

            ViewBag.intJobTitleID = new SelectList(db.TJobTitles, "intJobTitleID ", "strJobTitleDesc", tEmployee.intJobTitleID);
            return View(tEmployee);
        }

        // GET: TEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEmployee tEmployee = db.TEmployees.Find(id);
            if (tEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.intJobTitleID = new SelectList(db.TJobTitles, "intJobTitleID ", "strJobTitleDesc", tEmployee.intJobTitleID);
            return View(tEmployee);
        }

        // POST: TEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intEmployeeID,strFirstName,strLastName,intJobTitleID,intUserID")] TEmployee tEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.intJobTitleID = new SelectList(db.TJobTitles, "intJobTitleID ", "strJobTitleDesc", tEmployee.intJobTitleID);
            return View(tEmployee);


        }

        // GET: TEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEmployee tEmployee = db.TEmployees.Find(id);
            if (tEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tEmployee);
        }

        // POST: TEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TEmployee tEmployee = db.TEmployees.Find(id);
            db.TEmployees.Remove(tEmployee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
