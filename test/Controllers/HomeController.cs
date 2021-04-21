using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using System.Web.Security;
using System.Data.Entity;
using System.Net;

namespace test.Controllers {
	public class HomeController : Controller {

		private readonly CapstoneEntities db = new CapstoneEntities();


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
			ViewBag.PromptMessage = "Invalid Credentials Supplied";
			return View();
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



	}
}