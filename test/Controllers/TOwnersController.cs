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
    public class TOwnersController : Controller
    {
        private capstoneEntities db = new capstoneEntities();

        // GET: TOwners
        public ActionResult Index()
        {
            var tOwners = db.TOwners.Include(t => t.TState);
            return View(tOwners.ToList());
        }

        // GET: TOwners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOwner tOwner = db.TOwners.Find(id);
            if (tOwner == null)
            {
                return HttpNotFound();
            }
            return View(tOwner);
        }

        // GET: TOwners/Create
        public ActionResult Create()
        {
            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode");
            return View();
        }

        // POST: TOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intOwnerID,strFirstName,strLastName,intGenderID,strAddress,strCity,intStateID,strZip,strPhoneNumber,strEmail,strOwner2Name,strOwner2PhoneNumber,strOwner2Email,strNotes,isActive,intUserID")] TOwner tOwner)
        {
            if (ModelState.IsValid)
            {
                db.TOwners.Add(tOwner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode", tOwner.intStateID);
            return View(tOwner);
        }

        // GET: TOwners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOwner tOwner = db.TOwners.Find(id);
            if (tOwner == null)
            {
                return HttpNotFound();
            }
            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode", tOwner.intStateID);
            return View(tOwner);
        }

        // POST: TOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intOwnerID,strFirstName,strLastName,intGenderID,strAddress,strCity,intStateID,strZip,strPhoneNumber,strEmail,strOwner2Name,strOwner2PhoneNumber,strOwner2Email,strNotes,isActive,intUserID")] TOwner tOwner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tOwner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode", tOwner.intStateID);
            return View(tOwner);
        }

        // GET: TOwners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOwner tOwner = db.TOwners.Find(id);
            if (tOwner == null)
            {
                return HttpNotFound();
            }
            return View(tOwner);
        }

        // POST: TOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TOwner tOwner = db.TOwners.Find(id);
            db.TOwners.Remove(tOwner);
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
