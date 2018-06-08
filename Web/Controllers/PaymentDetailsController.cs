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
        public ActionResult Create(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invoice = GetInvoiceById(id);
            var payment = new PaymentDetail();
            payment.Date = DateTime.Now;
            payment.Invoice_Id = invoice.Id;
            payment.Invoice = invoice;           

            ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber");
            return View(payment);
        }

        // POST: PaymentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentDetail paymentDetail)
        {
            if (paymentDetail.Invoice_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var invoice = GetInvoiceById(paymentDetail.Invoice_Id.Value);
                //TODO: plan to remove payment table.
                var payment = new Payment();
                payment.Id = Guid.NewGuid();
                payment.Amount = paymentDetail.Amount;
                payment.Date = paymentDetail.Date;
                payment.DateCreated = DateTime.Now;
                payment.Partner_Id = invoice.Partner_Id;
                payment.CreatedBy = UserId;
                db.Payments.Add(payment);

                paymentDetail.Id = Guid.NewGuid();
                paymentDetail.CreatedBy = UserId;
                paymentDetail.Payment = payment;
                db.PaymentDetails.Add(paymentDetail);
                db.SaveChanges();
        
            }

            //ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber", paymentDetail.Order_Id);
            //return View(paymentDetail);
            return RedirectToAction("Details", "Invoices", new { id = paymentDetail.Invoice_Id });
        }

        // GET: PaymentDetails/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var payment = GetPaymentDetailById(id);
            var invoice = GetInvoiceById(payment.Invoice_Id.Value);
            
            payment.Date = DateTime.Now;
            payment.Invoice = invoice;

            return View(payment);
        }

        // POST: PaymentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentDetail paymentDetail)
        {
            
            var dbPaymentDetail = GetPaymentDetailById(paymentDetail.Id, true);
            paymentDetail.CreatedBy = dbPaymentDetail.CreatedBy;

            if (ModelState.IsValid)
            {
                db.Entry(paymentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Invoices", new { Id = paymentDetail.Invoice_Id });
            }

            return View();
        }

        //// GET: PaymentDetails/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PaymentDetail paymentDetail = GetPaymentDetailById(id.Value); //db.PaymentDetails.Find(id);
        //    if (paymentDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(paymentDetail);
        //}

        // POST: PaymentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaymentDetail paymentDetail = GetPaymentDetailById(id); //db.PaymentDetails.Find(id);
            var invId = paymentDetail.Invoice_Id;
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            db.PaymentDetails.Remove(paymentDetail);
            db.SaveChanges();
            return RedirectToAction("Details", "Invoices", new { Id = invId });
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
