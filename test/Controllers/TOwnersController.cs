using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;

namespace test.Controllers {
    public class TOwnersController : Controller {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TOwners
        public ActionResult Index()
        {
            var tOwners = db.TOwners.Include(t => t.TState);
            return View(tOwners.ToList());
        }

        // GET: TOwners/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOwner tOwner = db.TOwners.Find(id);
            if (tOwner == null) {
                return HttpNotFound();
            }
            return View(tOwner);
        }

        // GET: TOwners/Create
        public ActionResult Create() {
            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode");
            ViewBag.intUserID = new SelectList(db.TUsers, "intUserID", "strUserName");
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender");
            return View();
        }

        // POST: TOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intOwnerID,strFirstName,strLastName,intGenderID,strAddress,strCity,intStateID,strZip,strPhoneNumber,strEmail,strOwner2Name,strOwner2PhoneNumber,strOwner2Email,strNotes")] TOwner tOwner) {
            if (ModelState.IsValid) {

                //            SqlParameter[] userparam = new SqlParameter[] {
                //                    new SqlParameter("@strUserName", tOwner.strEmail),
                //                    new SqlParameter("@strPassword", tOwner.strZip),
                //                    new SqlParameter("@intRoleID", 1)
                //            };
                //            db.Database.ExecuteSqlCommand("uspAddNewUser @strUserName, @strPassword, @intRoleID", userparam);
                //var userID = db.TUsers.Max(u => u.intUserID);

                //string userName = "";
                //string password = "";
                //int roleId = 0;
                //int ownerId = 0;

                //SqlParameter[] param = new SqlParameter[] {
                //    //new ObjectParameter(SqlDbType.VarChar, 50 ) { Value = userNam, Name = "},
                //    //new ObjectParameter("@strPassword", SqlDbType.VarChar, 50) { Value = password },
                //    //new ObjectParameter("@intRoleID", SqlDbType.Int ) { Value = roleId },
                //    //new ObjectParameter("@intOwnerID", SqlDbType.Int ) { Value = ownerId },
                //    new SqlParameter("@strFirstName", SqlDbType.VarChar, 50 ) { Value = tOwner.strFirstName },
                //    new SqlParameter("@strLastName", SqlDbType.VarChar, 50 ) { Value = tOwner.strLastName },
                //    new SqlParameter("@intGenderID", SqlDbType.Int) { Value = tOwner.intGenderID },
                //    new SqlParameter("@strAddress", SqlDbType.VarChar, 50 ) { Value = tOwner.strAddress },
                //    new SqlParameter("@strCity", SqlDbType.VarChar, 50 ) { Value = tOwner.strCity },
                //    new SqlParameter("@intStateID", SqlDbType.Int) { Value = tOwner.intStateID }, 
                //    new SqlParameter("@strZip", SqlDbType.VarChar, 50 ) { Value = tOwner.strZip },
                //    new SqlParameter("@strPhoneNumber", SqlDbType.VarChar, 50 ) { Value = tOwner.strPhoneNumber },
                //    new SqlParameter("@strEmail", SqlDbType.VarChar, 50 ) { Value = tOwner.strEmail },
                //    new SqlParameter("@strOwner2Name", SqlDbType.VarChar, 50 ) { Value = tOwner.strOwner2Name },
                //    new SqlParameter("@strOwner2PhoneNumber", SqlDbType.VarChar, 50 ) { Value = tOwner.strOwner2PhoneNumber },
                //    new SqlParameter("@strOwner2Email", SqlDbType.VarChar, 50 ) { Value = tOwner.strOwner2Email },
                //    new SqlParameter("@strNotes", SqlDbType.VarChar, 200 ) { Value = tOwner.strNotes },
                //};

                ObjectParameter strUserName = new ObjectParameter("strUserName", typeof(string));
                ObjectParameter strPassword = new ObjectParameter("strPassword", typeof(string));
                ObjectParameter intRoleID = new ObjectParameter("intRoleID", typeof(Int32));
                ObjectParameter intOwnerID = new ObjectParameter("intOwnerID", typeof(Int32));
                

                //var data = db.Database.ExecuteSqlCommand("uspAddUserOwner @strUserName, @strPassword, @intRoleID, @intOwnerID, @strFirstName, @strLastName, @intGenderID, @strAddress, @strCity, @intStateID, @strZip, @strPhoneNumber, @strEmail, @strOwner2Name, @strOwner2PhoneNumber, @strOwner2Email, @strNotes", strUserName, strPassword, intRoleID, intOwnerID, param);
                var data = db.uspAddUserOwner(strUserName, strPassword, intRoleID, intOwnerID, tOwner.strFirstName, tOwner.strLastName, tOwner.intGenderID, tOwner.strAddress, tOwner.strCity, tOwner.intStateID, tOwner.strZip, tOwner.strPhoneNumber,tOwner.strEmail, tOwner.strOwner2Name, tOwner.strOwner2PhoneNumber, tOwner.strOwner2Email, tOwner.strNotes);

                string UserName = Convert.ToString(strUserName.Value);
                ViewBag.username = UserName;

                return RedirectToAction("Index");
            }

            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode", tOwner.intStateID);
            return View(tOwner);
        }

        // GET: TOwners/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOwner tOwner = db.TOwners.Find(id);
            if (tOwner == null) {
                return HttpNotFound();
            }
            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode", tOwner.intStateID);
            ViewBag.intUserID = new SelectList(db.TUsers, "intUserID", "strUserName", tOwner.intUserID);
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender", tOwner.intGenderID);
            return View(tOwner);
        }

        // POST: TOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intOwnerID,strFirstName,strLastName,intGenderID,strAddress,strCity,intStateID,strZip,strPhoneNumber,strEmail,strOwner2Name,strOwner2PhoneNumber,strOwner2Email,strNotes,intUserID")] TOwner tOwner) {
            if (ModelState.IsValid) {
                db.Entry(tOwner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intStateID = new SelectList(db.TStates, "intStateID", "strStateCode", tOwner.intStateID);
            ViewBag.intUserID = new SelectList(db.TUsers, "intUserID", "strUserName", tOwner.intUserID);
            ViewBag.intGenderID = new SelectList(db.TGenders, "intGenderID", "strGender", tOwner.intGenderID);


            return View(tOwner);
        }

        // GET: TOwners/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOwner tOwner = db.TOwners.Find(id);
            if (tOwner == null) {
                return HttpNotFound();
            }
            return View(tOwner);
        }

        // POST: TOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            TOwner tOwner = db.TOwners.Find(id);
            db.TOwners.Remove(tOwner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
