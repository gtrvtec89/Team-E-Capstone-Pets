using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;

namespace test.Controllers
{
    public class TEmployees1Controller : Controller
    {
        private capstoneEntities db = new capstoneEntities();

        // GET: TEmployees1
        public ActionResult Index()
        {
            var tEmployees = db.TEmployees.Include(t => t.TDepartment);
            return View(tEmployees.ToList());
        }

        // GET: TEmployees1/Details/5
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

        // GET: TEmployees1/Create
        public ActionResult Create()
        {
            ViewBag.intDepartmentID = new SelectList(db.TDepartments, "intDepartmentID", "strDepartment");
            return View();
        }

        // POST: TEmployees1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intEmployeeID,strFirstName,strLastName,intJobTitleID,isActive,intUserID,intDepartmentID")] TEmployee tEmployee)
        {
            if (ModelState.IsValid)
            {
                db.TEmployees.Add(tEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.intDepartmentID = new SelectList(db.TDepartments, "intDepartmentID", "strDepartment", tEmployee.intDepartmentID);
            return View(tEmployee);
        }

        // GET: TEmployees1/Edit/5
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
            ViewBag.intDepartmentID = new SelectList(db.TDepartments, "intDepartmentID", "strDepartment", tEmployee.intDepartmentID);
            return View(tEmployee);
        }

        // POST: TEmployees1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intEmployeeID,strFirstName,strLastName,intJobTitleID,isActive,intUserID,intDepartmentID")] TEmployee tEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intDepartmentID = new SelectList(db.TDepartments, "intDepartmentID", "strDepartment", tEmployee.intDepartmentID);
            return View(tEmployee);
        }

        // GET: TEmployees1/Delete/5
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

        // POST: TEmployees1/Delete/5
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
