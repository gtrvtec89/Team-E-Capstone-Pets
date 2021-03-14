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
    public class TJobTitlesController : Controller
    {
        private capstoneEntities db = new capstoneEntities();

        // GET: TJobTitles
        public ActionResult Index()
        {
            return View(db.TJobTitles.ToList());
        }

        // GET: TJobTitles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TJobTitle tJobTitle = db.TJobTitles.Find(id);
            if (tJobTitle == null)
            {
                return HttpNotFound();
            }
            return View(tJobTitle);
        }

        // GET: TJobTitles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TJobTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intJobTitleID,strJobTitleDesc")] TJobTitle tJobTitle)
        {
            if (ModelState.IsValid)
            {
                db.TJobTitles.Add(tJobTitle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tJobTitle);
        }

        // GET: TJobTitles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TJobTitle tJobTitle = db.TJobTitles.Find(id);
            if (tJobTitle == null)
            {
                return HttpNotFound();
            }
            return View(tJobTitle);
        }

        // POST: TJobTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intJobTitleID,strJobTitleDesc")] TJobTitle tJobTitle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tJobTitle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tJobTitle);
        }

        // GET: TJobTitles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TJobTitle tJobTitle = db.TJobTitles.Find(id);
            if (tJobTitle == null)
            {
                return HttpNotFound();
            }
            return View(tJobTitle);
        }

        // POST: TJobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TJobTitle tJobTitle = db.TJobTitles.Find(id);
            db.TJobTitles.Remove(tJobTitle);
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
