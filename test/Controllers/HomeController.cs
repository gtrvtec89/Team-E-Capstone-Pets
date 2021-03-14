using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using test.Models;
using System.Web.Security;

namespace test.Controllers {
	public class HomeController : Controller {

		private readonly capstoneEntities db = new capstoneEntities();


		public ActionResult Index() {

		return View();
	

		}





		public ActionResult Login() {

			return View();


		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TUser user) {
            if (ModelState.IsValid) {
                bool IsValidUser = db.TUsers
               .Any(u => u.strUserName.ToLower() == user
               .strUserName.ToLower() && user
               .strPassword == user.strPassword);

                if (IsValidUser) {
                    FormsAuthentication.SetAuthCookie(user.strUserName, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }








        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }







        //public ActionResult Register() {

        //    return View();


        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(TUser registerUser) {
        //    if (ModelState.IsValid) {
        //        db.TUsers.Add(registerUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Login");

        //    }
        //    return View();
        //}




    }
}