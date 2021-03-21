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
    public class TUsersController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TUsers
        public ActionResult Index()
        {
            var tUsers = db.TUsers.Include(t => t.TRole);
            return View(tUsers.ToList());
        }

        // GET: TUsers/Details/5

        [Authorize(Roles ="Admin, Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TUser tUser = db.TUsers.Find(id);
            if (tUser == null)
            {
                return HttpNotFound();
            }
            return View(tUser);
        }

        // GET: TUsers/Create
       
        public ActionResult Create()
        {
            ViewBag.intRoleID = new SelectList(db.TRoles, "intRoleID", "strRoleName");
            return View();
        }

        // POST: TUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intUserID,strUserName,strPassword,intRoleID")] TUser tUser)
        {
            if (ModelState.IsValid)
            {
                db.TUsers.Add(tUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.intRoleID = new SelectList(db.TRoles, "intRoleID", "strRoleName", tUser.intRoleID);
            return View(tUser);
        }

        // GET: TUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TUser tUser = db.TUsers.Find(id);
            if (tUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.intRoleID = new SelectList(db.TRoles, "intRoleID", "strRoleName", tUser.intRoleID);
            return View(tUser);
        }

        // POST: TUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intUserID,strUserName,strPassword,intRoleID")] TUser tUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intRoleID = new SelectList(db.TRoles, "intRoleID", "strRoleName", tUser.intRoleID);
            return View(tUser);
        }

        // GET: TUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TUser tUser = db.TUsers.Find(id);
            if (tUser == null)
            {
                return HttpNotFound();
            }
            return View(tUser);
        }

        // POST: TUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TUser tUser = db.TUsers.Find(id);
            db.TUsers.Remove(tUser);
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
