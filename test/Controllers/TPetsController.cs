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
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TPets
        public ActionResult Index()
        {
            var tPets = db.TPets
                .Include(t => t.TPetType)
                .Include(t => t.TOwner)
                .Include(t => t.TBreed)
                .Include(t => t.TGender);
                
            return View(tPets.ToList());
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
            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType");
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender");
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strFirstName" + "strLastName");
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName");

            return View();
        }

        // POST: TPets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intPetID,strPetNumber,strMicrochipID,strPetName,intPetTypeID,intGenderID,intBreedID,dtmDateofBirth,dblWeight,isBlind,isDeaf,isAggressive,isDeceased,isAllergic,strColor,strNotes,isDeceased,intOwnerID")] TPet tPet)
        {
            if (ModelState.IsValid)
            {
                db.TPets.Add(tPet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType", tPet.intPetTypeID);
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender", tPet.intGenderID);
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strFirstName" + "strLastName", tPet.intOwnerID);
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName", tPet.intBreedID);

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
            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType", tPet.intPetTypeID);
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender", tPet.intGenderID);
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strFirstName" + "strLastName", tPet.intOwnerID);
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName", tPet.intBreedID);
            return View(tPet);
        }

        // POST: TPets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intPetID,strPetNumber,strMicrochipID,strPetName,intPetTypeID,intGenderID,intBreedID,dtmDateofBirth,dblWeight,isBlind,isDeaf,isAggressive,isDeceased,isAllergic,strColor,strNotes,isDeceased,intOwnerID")] TPet tPet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tPet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType", tPet.intPetTypeID);
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender", tPet.intGenderID);
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strFirstName" + "strLastName", tPet.intOwnerID);
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName", tPet.intBreedID);
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
