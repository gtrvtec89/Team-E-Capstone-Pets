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
    public class TPetsController : Controller
    {
        private capstoneEntities db = new capstoneEntities();

        // GET: TPets
        public ActionResult Index()
        {
            return View(db.TPets.ToList());
        }

        // GET: TPets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPet tPet = db.TPets.Find(id);
            if (tPet == null)
            {
                return HttpNotFound();
            }
            return View(tPet);
        }

        // GET: TPets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TPets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intPetID,strPetNumber,strMicrochipID,strPetName,intPetTypeID,intGenderID,intBreedID,dtmDateofBirth,dblWeight,isBlind,isDeaf,isAggressive,isDeceased,isAllergic,strColor,strNotes,isActive,intOwnerID")] TPet tPet)
        {
            if (ModelState.IsValid)
            {
                db.TPets.Add(tPet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tPet);
        }

        // GET: TPets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPet tPet = db.TPets.Find(id);
            if (tPet == null)
            {
                return HttpNotFound();
            }
            return View(tPet);
        }

        // POST: TPets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intPetID,strPetNumber,strMicrochipID,strPetName,intPetTypeID,intGenderID,intBreedID,dtmDateofBirth,dblWeight,isBlind,isDeaf,isAggressive,isDeceased,isAllergic,strColor,strNotes,isActive,intOwnerID")] TPet tPet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tPet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tPet);
        }

        // GET: TPets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPet tPet = db.TPets.Find(id);
            if (tPet == null)
            {
                return HttpNotFound();
            }
            return View(tPet);
        }

        // POST: TPets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TPet tPet = db.TPets.Find(id);
            db.TPets.Remove(tPet);
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
