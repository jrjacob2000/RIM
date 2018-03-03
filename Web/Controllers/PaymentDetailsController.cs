using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class PaymentDetailsController : ControllerBase
    {
       // private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentDetails
        public ActionResult Index()
        {
            var paymentDetails = GetPaymentDetailList(); //db.PaymentDetails.Include(p => p.Order);
            return View(paymentDetails.ToList());
        }

        // GET: PaymentDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = GetPaymentDetailById(id.Value); //db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // GET: PaymentDetails/Create
        public ActionResult Create()
        {
            ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber");
            return View();
        }

        // POST: PaymentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Amount,Order_Id")] PaymentDetail paymentDetail)
        {
            if (ModelState.IsValid)
            {
                paymentDetail.Id = Guid.NewGuid();
                paymentDetail.CreatedBy = UserId;
                db.PaymentDetails.Add(paymentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber", paymentDetail.Order_Id);
            return View(paymentDetail);
        }

        // GET: PaymentDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = GetPaymentDetailById(id.Value); //db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber", paymentDetail.Order_Id);
            return View(paymentDetail);
        }

        // POST: PaymentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,Order_Id")] PaymentDetail paymentDetail)
        {
            var dbPaymentDetail = GetPaymentDetailById(paymentDetail.Id, true);
            paymentDetail.CreatedBy = dbPaymentDetail.CreatedBy;

            if (ModelState.IsValid)
            {
                db.Entry(paymentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber", paymentDetail.Order_Id);
            return View(paymentDetail);
        }

        // GET: PaymentDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = GetPaymentDetailById(id.Value); //db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // POST: PaymentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PaymentDetail paymentDetail = GetPaymentDetailById(id); //db.PaymentDetails.Find(id);
            db.PaymentDetails.Remove(paymentDetail);
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
