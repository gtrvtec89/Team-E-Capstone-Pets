using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using System.Web.Security;
using System.Data.Entity;
using System.Net;
using System.Data.SqlClient;

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


			CapstoneEntities db = new CapstoneEntities();
			int? intUserID = db.Validate_User6(user.strUserName, user.strPassword).FirstOrDefault();

			string message = string.Empty;
			switch (intUserID.Value) {
				case -1:
					message = "Username and/or password is incorrect.";
					break;
				case -2:
					message = "Account has not been activated.";
					break;
				default:
					FormsAuthentication.SetAuthCookie(user.strUserName, user.RememberMe);

					int? intOwnerID = db.uspGetOwnerID(intUserID).FirstOrDefault();
					int? intRoleID = db.uspGetRole(intUserID).FirstOrDefault();



					if (intRoleID == 1) {
						return RedirectToAction("OwnerHome", new { @id = intOwnerID });
						//	return RedirectToAction("OwnerHome", "Home");
					}
					else { return RedirectToAction("Index", "Home"); }
					return RedirectToAction("Index", "Home");
			}

			ViewBag.Message = message;
			return View(user);
		}



			//if (ModelState.IsValid) {
			//	bool IsValidUser = db.TUsers
			//   .Any(u => u.strUserName.ToLower() == user
			//   .strUserName.ToLower() && user
			//   .strPassword == user.strPassword);

			//	if (IsValidUser) {
			//		FormsAuthentication.SetAuthCookie(user.strUserName, false);

			//		//using (var ctx = new CapstoneEntities()) {
			//		//	var idParam = new SqlParameter {
			//		//		ParameterName = "@strUserName",
			//		//		Value = user.strUserName.ToLower();
			//		//}, new SqlParameter {
			//		//	ParameterName = "@strPassword",
			//		//	Value = user.strPassword; }
			//	//};
			//			//Get student name of string type
			//			//var courseList = ctx.Database.SqlQuery<Course>("exec GetCoursesByStudentId @StudentId ", idParam).ToList<Course>();

			//	SqlParameter[] param = new SqlParameter[] {
			//				new SqlParameter("@strUserName", user.strUserName),
			//				new SqlParameter("@strPassword", user.strPassword)
			//			};
			//	////SqlParameter[] param = new SqlParameter[] {
			//	////	new SqlParameter("@id", user.intUserID)
			//	////};
			//	//var un = user.strUserName.ToLower();
			//	//var pw = user.strPassword;
			//	var uid = db.Database.ExecuteSqlCommand("DECLARE @return_value int EXEC    @return_value = [db_owner].[uspGetUserID]", param);
			//	//var uid = db.Database.ExecuteSqlCommand("uspGetUserID", param);
			//	var ownid = db.Database.ExecuteSqlCommand("uspGetUserOwnerID @uid ", uid);

			//		if (user.intRoleID == 1) {
			//			return RedirectToAction("OwnerHome", new { @id = ownid });
			//			//	return RedirectToAction("OwnerHome", "Home");
			//		}
			//		else { return RedirectToAction("Index", "Home"); }
			//		return RedirectToAction("Index", "Home");
			//	}


			//}

			//ViewBag.PromptMessage = "Invalid Credentials Supplied";
			//return View();
	//	}


		public ActionResult OwnerHome(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var tPetImages = db.TPetImages
			   .Include(t => t.TPet)
			   .Include(t => t.TPet.TPetType)
			   .Include(t => t.TPet.TOwner)
			   .Include(t => t.TPet.TBreed)
			   .Include(t => t.TPet.TGender);
			   
				
			//.Include(t => t.imgContent);
			
			return View(tPetImages.ToList());


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