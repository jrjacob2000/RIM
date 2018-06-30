using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    [Authorize]
    public class CashInjectionsController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();


        // GET: CashInjections
        public ActionResult Index()
        {
            return View(db.CashInjections.Where(x => x.CreatedBy == UserId).ToList());
        }

        // GET: CashInjections/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInjection cashInjection = db.CashInjections.Where(x => x.CreatedBy == UserId && x.Id == id).FirstOrDefault();
            if (cashInjection == null)
            {
                return HttpNotFound();
            }
            return View(cashInjection);
        }

        // GET: CashInjections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CashInjections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Particulars,Amount,Date")] CashInjection cashInjection)
        {
            if (ModelState.IsValid)
            {
                cashInjection.Id = Guid.NewGuid();
                cashInjection.CreatedBy = UserId;
                db.CashInjections.Add(cashInjection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cashInjection);
        }

        // GET: CashInjections/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInjection cashInjection = GetCashInjectionById(id.Value, true); //db.CashInjections.Where(x => x.CreatedBy == UserId && x.Id == id).FirstOrDefault();
            if (cashInjection == null)
            {
                return HttpNotFound();
            }
            return View(cashInjection);
        }

        // POST: CashInjections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Particulars,Amount,Date")] CashInjection cashInjection)
        {
            var dbCashInjection = GetCashInjectionById(cashInjection.Id);

            cashInjection.CreatedBy = dbCashInjection.CreatedBy;
            if (ModelState.IsValid)
            {
                db.Entry(cashInjection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashInjection);
        }

        // GET: CashInjections/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInjection cashInjection = db.CashInjections.Where(x => x.CreatedBy == UserId && x.Id == id).FirstOrDefault();
            if (cashInjection == null)
            {
                return HttpNotFound();
            }
            return View(cashInjection);
        }

        // POST: CashInjections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CashInjection cashInjection = db.CashInjections.Where(x => x.CreatedBy == UserId && x.Id == id).FirstOrDefault();
            db.CashInjections.Remove(cashInjection);
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
