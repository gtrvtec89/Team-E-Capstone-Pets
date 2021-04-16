using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using test;

namespace test.Controllers {
    public class OwnerHomeController : Controller {
        private CapstoneEntities db = new CapstoneEntities();


        public ActionResult OwnerHome() {
            var tPetImages = db.TPetImages
               .Include(t => t.TPet)
               .Include(t => t.TPet.TPetType)
               .Include(t => t.TPet.TOwner)
               .Include(t => t.TPet.TBreed)
               .Include(t => t.TPet.TGender);
            //.Include(t => t.imgContent);

            return View(tPetImages.ToList());


        }

        public FileContentResult DisplayImagePage(int id) {
            TPetImage document = db.TPetImages.Find(id);
            return new FileContentResult(document.imgContent, document.strContentType);
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Settings() {


            return View();


        }

        public ActionResult About() {

            return View();

        }


        public ActionResult Help() {

            return View();

        }


        public ActionResult PetMedication()
        {

            return View();

        }

    }
}
