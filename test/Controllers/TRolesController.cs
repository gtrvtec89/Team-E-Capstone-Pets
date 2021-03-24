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
    public class TRolesController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: TRoles
        public ActionResult Index()
        {
            return View(db.TRoles.ToList());
        }

        // GET: TRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRole tRole = db.TRoles.Find(id);
            if (tRole == null)
            {
                return HttpNotFound();
            }
            return View(tRole);
        }

        // GET: TRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intRoleID,strRoleName")] TRole tRole)
        {
            if (ModelState.IsValid)
            {
                db.TRoles.Add(tRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tRole);
        }

        // GET: TRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRole tRole = db.TRoles.Find(id);
            if (tRole == null)
            {
                return HttpNotFound();
            }
            return View(tRole);
        }

        // POST: TRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intRoleID,strRoleName")] TRole tRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRole);
        }

        // GET: TRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRole tRole = db.TRoles.Find(id);
            if (tRole == null)
            {
                return HttpNotFound();
            }
            return View(tRole);
        }

        // POST: TRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRole tRole = db.TRoles.Find(id);
            db.TRoles.Remove(tRole);
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
