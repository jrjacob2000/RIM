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
        public ActionResult Create(Guid id, string source)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (source == "Credits")
            {
                var credit = db.Credits
                    .Include("Order")
                    .Include("Order.Partner")
                    .Where(x => x.Id == id && x.CreatedBy == UserId).First();

                if (credit.Status == Helper.Constants.CreditNotesStatus.PAID)
                    ModelState.AddModelError("","Credit notes is already paid");

                var paymentDtl = new PaymentDetail();
                paymentDtl.Date = DateTime.Now;
                paymentDtl.Credit_Id = credit.Id;
                paymentDtl.Credit = credit;
                paymentDtl.Payment = new Payment() { Id = Guid.NewGuid() };
                return View(paymentDtl);
            }
            else
            {
                var invoice = GetInvoiceById(id);
                var paymentDtl = new PaymentDetail();
                paymentDtl.Date = DateTime.Now;
                paymentDtl.Invoice_Id = invoice.Id;
                paymentDtl.Invoice = invoice;

                ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber");
                paymentDtl.Payment = new Payment() { Id = Guid.NewGuid() };
                return View(paymentDtl);
            }
        }

        // POST: PaymentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentDetail paymentDetail, string source)
        {
            //TODO: plan to remove payment table.
            var payment = new Payment();
            payment.Id = Guid.NewGuid();
            payment.Reference = paymentDetail.Payment.Reference;
            payment.Amount = paymentDetail.Amount;
            payment.Date = paymentDetail.Date;
            payment.DateCreated = DateTime.Now;
            payment.CreatedBy = UserId;

            if(!(paymentDetail.Amount > 0))
                ModelState.AddModelError("", "Amount is required");

            if (source == "Credits") 
            {
                if (paymentDetail.Credit_Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var credit = db.Credits
                    .Include("Order")
                    .Include("Order.OrderDetails")
                    .Include("Order.OrderDetails.Product")
                    .Include("Order.OrderDetails.ProductPrice")
                    .Include("Order.Partner")
                    .Where(x => x.Id == paymentDetail.Credit_Id && x.CreatedBy == UserId)
                    .First();

                decimal dbTotalPaid = 0;
                var dbPaymentdtls = db.PaymentDetails.Where(x => x.Credit_Id == paymentDetail.Credit_Id && x.CreatedBy == UserId).ToList();
                if(dbPaymentdtls != null)
                    dbTotalPaid = dbPaymentdtls.Sum(s => s.Amount);

                if (dbTotalPaid == credit.Amount)
                    ModelState.AddModelError("", "Unable to save. Credit notes is already paid");
                
                if (paymentDetail.Amount != credit.Amount)
                    ModelState.AddModelError("", "Amount should be equal to credit notes amount. Credit notes amount is " + credit.Amount);

                if (ModelState.IsValid)
                {
                    payment.Partner_Id = credit.Partner_Id;                    
                    db.Payments.Add(payment);

                    paymentDetail.Id = Guid.NewGuid();
                    paymentDetail.CreatedBy = UserId;
                    paymentDetail.Payment = payment;
                    db.PaymentDetails.Add(paymentDetail);

                    credit.Status = Helper.Constants.CreditNotesStatus.PAID;

                    db.SaveChanges();
                    return RedirectToAction("Details", source, new { id = paymentDetail.Credit_Id });
                }
                                
                paymentDetail.Credit = credit;
                return View(paymentDetail);
            }
            else
            {
                if (paymentDetail.Invoice_Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var invoice = GetInvoiceById(paymentDetail.Invoice_Id.Value);

                var prevReceivePmnts = db.PaymentDetails.Where(x => x.Invoice_Id == invoice.Id && x.CreatedBy == UserId).ToList();
                decimal invoiceBal = 0;
                if (prevReceivePmnts != null)
                    invoiceBal = invoice.Amount - prevReceivePmnts.Sum(s => s.Amount);

                if (paymentDetail.Amount > invoiceBal)
                    ModelState.AddModelError("", "Amount should not greater than the invoice balance. Invoice balance is only " + invoiceBal);

                if (ModelState.IsValid)
                {

                    payment.Partner_Id = invoice.Partner_Id;
                    db.Payments.Add(payment);

                    paymentDetail.Id = Guid.NewGuid();
                    paymentDetail.CreatedBy = UserId;
                    paymentDetail.Payment = payment;
                    db.PaymentDetails.Add(paymentDetail);
                    db.SaveChanges();

                    //Update invoice status
                    var totalRecievedAmount = db.PaymentDetails.Where(x => x.Invoice_Id == invoice.Id && x.CreatedBy == UserId).Sum(s => s.Amount);
                    if (invoice.Amount == totalRecievedAmount)
                        invoice.Status = Helper.Constants.InvoiceStatus.PAID;
                    else if (invoice.Amount > totalRecievedAmount && totalRecievedAmount > 0)
                        invoice.Status = Helper.Constants.InvoiceStatus.PARTIALPAID;
                    else if (totalRecievedAmount == 0)
                        invoice.Status = Helper.Constants.InvoiceStatus.DRAFT;
                    db.SaveChanges();
                    return RedirectToAction("Details", source, new { id = paymentDetail.Invoice_Id });
                }

                //ViewBag.Order_Id = new SelectList(GetOrderList(), "Id", "OrderNumber", paymentDetail.Order_Id);
                paymentDetail.Invoice = invoice;
                return View(paymentDetail);
            }
           
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
            var dbPayment = GetPaymentById(paymentDetail.Payment_Id);
            dbPayment.Date = paymentDetail.Date;
            dbPayment.Reference = paymentDetail.Payment.Reference;

            var dbPaymentDetail = GetPaymentDetailById(paymentDetail.Id);

            var invoice = GetInvoiceById(paymentDetail.Invoice_Id.Value);
            
            var prevReceivePmnts = db.PaymentDetails.Where(x => x.Invoice_Id == invoice.Id 
                && x.CreatedBy == UserId
                && x.Id != paymentDetail.Id);

            decimal invoiceBal = invoice.Amount; 

            if (prevReceivePmnts.Count() > 0)
                invoiceBal = invoice.Amount - prevReceivePmnts.Sum(s => s.Amount);

            if (paymentDetail.Amount > invoiceBal && invoiceBal > 0)
                ModelState.AddModelError("", "Amount should not greater than the invoice balance. Invoice balance is only " + invoiceBal);

            if (ModelState.IsValid)
            { 
                //db.Entry(paymentDetail).State = EntityState.Modified;
                dbPaymentDetail.Amount = paymentDetail.Amount;
                dbPaymentDetail.Date = paymentDetail.Date;
                //dbPaymentDetail.Reference = paymentDetail.Reference;
                dbPaymentDetail.Notes = dbPaymentDetail.Notes;
                db.SaveChanges();

                //Update invoice status
                var totalRecievedAmount = db.PaymentDetails.Where(x => x.Invoice_Id == invoice.Id && x.CreatedBy == UserId).Sum(s => s.Amount);
                if (invoice.Amount == totalRecievedAmount)
                    invoice.Status = Helper.Constants.InvoiceStatus.PAID;
                else if (invoice.Amount > totalRecievedAmount && totalRecievedAmount > 0)
                    invoice.Status = Helper.Constants.InvoiceStatus.PARTIALPAID;
                else if (totalRecievedAmount == 0)
                    invoice.Status = Helper.Constants.InvoiceStatus.DRAFT;

                db.SaveChanges();
                return RedirectToAction("Details", "Invoices", new { Id = paymentDetail.Invoice_Id });
            }

            paymentDetail.Invoice = invoice;
            return View(paymentDetail);
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
        public ActionResult DeleteConfirmed(Guid id, string source)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaymentDetail paymentDetail = GetPaymentDetailById(id); //db.PaymentDetails.Find(id);
            var srcId = paymentDetail.Invoice_Id;

            if (source == "Invoices" || source == "Bills")
            {
                srcId = paymentDetail.Invoice_Id;
                var dbInvoice = db.Invoices.Where(x => x.Id == srcId && x.CreatedBy == UserId).First();
                dbInvoice.Status = Helper.Constants.InvoiceStatus.DRAFT;
            }
            if (source == "Credits")
            {
                srcId = paymentDetail.Credit_Id;
                var dbCredit = db.Credits.Where(x => x.Id == srcId && x.CreatedBy == UserId).First();
                dbCredit.Status = Helper.Constants.CreditNotesStatus.DRAFT;
            }

            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            paymentDetail.Payment.Amount = paymentDetail.Payment.Amount - paymentDetail.Amount;
            db.PaymentDetails.Remove(paymentDetail);
            db.SaveChanges();
            return RedirectToAction("Details", source, new { Id = srcId });
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
