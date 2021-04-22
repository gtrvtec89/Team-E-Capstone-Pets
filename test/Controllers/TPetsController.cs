using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;

namespace test.Controllers {
    public class TPetsController : Controller {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TPets
        public ActionResult Index() {
            var tPets = db.TPets
                .Include(t => t.TPetType)
                .Include(t => t.TOwner)
                .Include(t => t.TBreed)
                .Include(t => t.TGender)
                .Include(t => t.TPetImages);

            return View(tPets.ToList());
        }

        // GET: TPets/Details/5
        public ActionResult Details(int? id){
            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType");
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender");
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strLastName");
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName");
            ViewBag.intPetImageID = new SelectList(db.TPetImages, "intPetImageID", "imgContent");

            if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
  
            Session["intPetID"] = id;
            // TPet tPet = db.TPets.Find(id);
            //TPetImage tPetImage = db.TPetImages.Find(db.);
            //
            TPet tPet = db.TPets.Include(s => s.TPetImages).SingleOrDefault(s => s.intPetID == id);

            if (tPet == null) {
				return HttpNotFound();
			}
			return View(tPet);
			//return View();
        }

        // GET: TPets/Create
        public ActionResult Create() {
            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType");
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender");
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strLastName");
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName");

            return View();
        }

        // POST: TPets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intPetID,strPetNumber,strMicrochipID,strPetName,intPetTypeID,intGenderID,intBreedID,dtmDateofBirth,dblWeight,isBlind,isDeaf,isAggressive,isDeceased,isAllergic,strColor,strNotes,isDeceased,intOwnerID")] TPet tPet, HttpPostedFileBase upload) {
            try {
                if (ModelState.IsValid) {
                    if (upload != null && upload.ContentLength > 0) {
                        var image = new TPetImage {
                            strFileName = Path.GetFileName(upload.FileName),
                            strFileType = Path.GetExtension(upload.FileName),
                            strContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream)) {
                            image.imgContent = reader.ReadBytes(upload.ContentLength);
                        }
                        tPet.TPetImages = new List<TPetImage> { image };
                    }
                    db.TPets.Add(tPet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch (RetryLimitExceededException /* dex */) {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType", tPet.intPetTypeID);
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender", tPet.intGenderID);
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strLastName", tPet.intOwnerID);
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName", tPet.intBreedID);

            return View(tPet);
        }

        // GET: TPets/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPet tPet = db.TPets.Find(id);
            if (tPet == null) {
                return HttpNotFound();
            }
            ViewBag.intPetTypeID = new SelectList(db.TPetTypes, "intPetTypeID", "strPetType", tPet.intPetTypeID);
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender", tPet.intGenderID);
            ViewBag.intOwnerID = new SelectList(db.TOwners, "intOwnerID", "strLastName", tPet.intOwnerID);
            ViewBag.intBreedID = new SelectList(db.TBreeds, "intBreedID", "strBreedName", tPet.intBreedID);
            return View(tPet);
        }


        //// POST: TPets/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(TPet tPet, int? id, HttpPostedFileBase upload) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var petToUpdate = db.TPets.Find(id);
            if (TryUpdateModel(petToUpdate, "",
               new string[] { "intPetID", "strPetNumber", "strMicrochipID", "strPetName", "intPetTypeID", "intGenderID", "intBreedID", "dtmDateofBirth", "dblWeight", "isBlind", "isDeaf", "isAggressive", "isDeceased", "isAllergic", "strColor", "strNotes", "isDeceased", "intOwnerID" })) {
                try {
                    if (upload != null && upload.ContentLength > 0) {
                        if (petToUpdate.TPetImages.Any(f => f.strFileType == ".jpg")) {
                            db.TPetImages.Remove(petToUpdate.TPetImages.First(f => f.strFileType == ".jpg"));
                        }
                        var avatar = new TPetImage {
                            strFileName = System.IO.Path.GetFileName(upload.FileName),
                            strFileType = System.IO.Path.GetExtension(upload.FileName),
                            strContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream)) {
                            avatar.imgContent = reader.ReadBytes(upload.ContentLength);
                        }
                        petToUpdate.TPetImages = new List<TPetImage> { avatar };
                    }
                    db.Entry(petToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = tPet.intPetID });
                }
                catch (RetryLimitExceededException /* dex */) {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(petToUpdate);

        }

        // GET: TPets/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TPet tPet = db.TPets.Find(id);
            if (tPet == null) {
                return HttpNotFound();
            }
            return View(tPet);
        }

        // POST: TPets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            TPet tPet = db.TPets.Find(id);
            db.TPets.Remove(tPet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // To convert the Byte Array to the author Image
        public FileContentResult getImg(int intPetID) {
            byte[] byteArray = db.TPetImages.Find(intPetID).imgContent;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        public Image byteArrayToImage(byte[] byteArrayIn) {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
