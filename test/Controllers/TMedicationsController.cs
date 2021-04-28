using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;
using test.Models;

namespace test.Controllers
{
    public class TMedicationsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: TMedications
        public ActionResult Index()
        {
            return View(db.TMedications.ToList());
        }

        // GET: TMedications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMedication tMedication = db.TMedications.Find(id);
            if (tMedication == null)
            {
                return HttpNotFound();
            }
            return View(tMedication);
        }

        // GET: TMedications/Create
        public ActionResult Create()
        {
            ViewBag.intMethodID = new SelectList(db.TMethods, "intMethodID", "strMethod");
            return View();
        }

        // POST: TMedications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intMedicationID,strMedicationName,strMedicationDesc,dblCost,dblPrice,strNotes,intQuantity,intMethodID")] TMedication tMedication)
        {
            if (ModelState.IsValid)
            {
                db.TMedications.Add(tMedication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tMedication);
        }

        // GET: TMedications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMedication tMedication = db.TMedications.Find(id);
            if (tMedication == null)
            {
                return HttpNotFound();
            }
            ViewBag.intMethodID = new SelectList(db.TMethods, "intMethodID", "strMethod");
            return View(tMedication);
        }

        // POST: TMedications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intMedicationID,strMedicationName,strMedicationDesc,dblCost,dblPrice,strNotes,intQuantity,intMethodID")] TMedication tMedication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tMedication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tMedication);
        }

        // GET: TMedications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMedication tMedication = db.TMedications.Find(id);
            if (tMedication == null)
            {
                return HttpNotFound();
            }
            return View(tMedication);
        }

        // POST: TMedications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TMedication tMedication = db.TMedications.Find(id);
            db.TMedications.Remove(tMedication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddOrder(int id)
        {
            TMedication medication = db.TMedications.Where(x => x.intMedicationID == id).FirstOrDefault();

            OrderMedication orderMedication = new OrderMedication()
            {
                dtmDateOfOrder = DateTime.Now,
                intMedicationId = medication.intMedicationID,
                strMedicationName = medication.strMedicationName,
                dblUnitCost = medication.dblCost,
                intCurrentQuantity = medication.intQuantity,
                intOrderQuantity = 0,
                dblTotal = 0,
                strNotes = ""
            };

            ViewBag.intMedicationId = new SelectList(db.TMedications, "intMedicationID", "strMedicationName");
            return View(orderMedication);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderMedication order)
        {
            TMedication medication = db.TMedications.Where(x => x.intMedicationID == order.intMedicationId).FirstOrDefault();
            SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("@intMedicationID", medication.intMedicationID),
              new SqlParameter("@strMedicationName", medication.strMedicationName),
              new SqlParameter("@strMedicationDesc", medication.strMedicationDesc),
              new SqlParameter("@dblCost", medication.dblCost),
              new SqlParameter("@dblPrice", medication.dblPrice),
              new SqlParameter("@strNotes", medication.strNotes),
              new SqlParameter("@intQuantity", medication.intQuantity + order.intOrderQuantity),
              new SqlParameter("@intMethodID", medication.intMethodID),
            };

            db.uspAddMedicationOrder(order.dtmDateOfOrder, medication.intMedicationID, medication.strMedicationName, medication.dblCost, order.intOrderQuantity, medication.dblCost * order.intOrderQuantity, order.strNotes);
            db.Database.ExecuteSqlCommand("uspUpdateMedication @intMedicationID, @strMedicationName, @strMedicationDesc,@dblCost, @dblPrice,@strNotes,@intQuantity, @intMethodID ", param);
            return RedirectToAction("Index");
        }

        public ActionResult Inventory()
        {
            return View(db.TMedicationOrders.ToList());
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
