using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Payments
        public ActionResult Index()
        {
            //return View(db.Payments.Include("Partner").Where(x => !x.Deleted).ToList());
            return View(GetPaymentList());
        }

       
        // GET: Payments/Details/5
        public ActionResult Details(Guid? id)
        {
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Payment payment = db.Payments.Include("Partner").Where(x => !x.Deleted).FirstOrDefault(x => x.Id == id);
            Payment payment = GetPaymentById(id.Value);
            //payment.PaymentDetails = db.PaymentDetails
            //                        .Include("Order")
            //                        .Where(x => x.Payment_Id == payment.Id).ToList();

            payment.PaymentDetails = GetPaymentDetailList().Where(x => x.Payment_Id == payment.Id).ToList();
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        public ActionResult Create(Guid? Partner_Id)
        {
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            var salesOrderList = new List<Order>();
            var paymentDetails = new List<PaymentDetail>();
            if (Partner_Id != null)
            {
                //salesOrderList = db.Orders
                //    .Include("OrderDetails")
                //    .Include("OrderDetails.ProductPrice")
                //    .Where(x => x.Partner_Id == Partner_Id && x.OrderType == Helper.Constants.OrderType.SALE).ToList();
                salesOrderList = GetOrderList().Where(x => x.Partner_Id == Partner_Id && x.OrderType == Helper.Constants.OrderType.SALE).ToList();
            }

            salesOrderList.ForEach(x =>
            {
                //x.PaymentDetails = db.PaymentDetails.Include("Payment").Where(p => p.Order_Id == x.Id && !p.Payment.Deleted).ToList();
                x.PaymentDetails = GetPaymentDetailList().Where(p => p.Order_Id == x.Id && !p.Payment.Deleted).ToList();
                if(x.Balance > 0)
                    paymentDetails.Add(new PaymentDetail() { Id = Guid.NewGuid(), Order = x, Order_Id = x.Id });
            });

            var payment = new Payment();
            payment.PaymentDetails = paymentDetails.OrderBy(o => o.Order.OrderDate).ToList();
            payment.Date = DateTime.Now;

            if (paymentDetails.Count == 0 && Partner_Id != null)
                ViewBag.Message = "The customer has no outstanding dues.";
 
            return View(payment);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Payment payment)
        {
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            if (payment.PaymentDetails.Sum(x => x.Amount) > payment.Amount)
                ModelState.AddModelError(string.Empty, "Payment allocated is greater than payment received.");

            if (payment.PaymentDetails.Sum(x => x.Amount) < payment.Amount)
                ModelState.AddModelError(string.Empty, "Payment allocated is less than payment received.");

            payment.PaymentDetails = payment.PaymentDetails.Where(x => x.Amount > 0).ToList();
            payment.PaymentDetails.ForEach(x =>
            {
                x.Id = Guid.NewGuid();
                
                //x.Order = db.Orders.Include("OrderDetails")
                //    .Include("OrderDetails.ProductPrice")
                //    .FirstOrDefault(o => o.Id == x.Order_Id);
                x.Order = GetOrderById(x.Order_Id);

                //x.Order.PaymentDetails = db.PaymentDetails.Include("Payment").Where(p => p.Order_Id == x.Order_Id && !p.Payment.Deleted).ToList();
                x.Order.PaymentDetails = GetPaymentDetailList().Where(p => p.Order_Id == x.Order_Id && !p.Payment.Deleted).ToList();
                
                if(x.Order.Balance < x.Amount)
                     ModelState.AddModelError(string.Empty,string.Format("Order {0} alloted amount is greater than the Outstanding",x.Order.OrderNumber));
               
                if(x.Order.InvoiceNumber == null)
                    ModelState.AddModelError(string.Empty, string.Format("You cannot allocate payment to order {0}, because is not yet invoiced", x.Order.OrderNumber));

                x.CreatedBy = UserId;
            });

            if (ModelState.IsValid)
            {
                payment.Id = Guid.NewGuid();
                payment.DateCreated = DateTime.Now;
                payment.CreatedBy = UserId;
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return Create(payment.Partner_Id);

        }

        // GET: Payments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            //edit is disable
            //reason to disable: the system will not be able to recalculate the remaining balance of the customer
            //and will prone to data discrepancy
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            
            Payment payment = GetPaymentById(id.Value);

            //////////////////////////////////////////////
            var salesOrderList = new List<Order>();
            var paymentDetails = new List<PaymentDetail>();
            
            salesOrderList = GetOrderList().Where(x => x.Partner_Id == payment.Partner_Id && x.OrderType == Helper.Constants.OrderType.SALE).ToList();

            salesOrderList.ForEach(x =>
            {
                //x.PaymentDetails = db.PaymentDetails.Include("Payment").Where(p => p.Order_Id == x.Id && !p.Payment.Deleted).ToList();
                x.PaymentDetails = GetPaymentDetailList().Where(p => p.Order_Id == x.Id && !p.Payment.Deleted).ToList();
                if (x.Balance > 0)
                    paymentDetails.Add(new PaymentDetail() { Id = Guid.NewGuid(), Order = x, Order_Id = x.Id });
            });

            //var payment = new Payment();
            payment.PaymentDetails = paymentDetails.OrderBy(o => o.Order.OrderDate).ToList();

            ////////////////////////


            
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,Date,Partner_Id,DateCreated")] Payment payment)
        {
            //edit is disable
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var dbPayment = GetPaymentById(payment.Id);
            dbPayment.Amount = payment.Amount;
            dbPayment.Date = payment.Date;
            dbPayment.Partner_Id = payment.Partner_Id;
            //payment.CreatedBy = dbPayment.CreatedBy;

            if (ModelState.IsValid)
            {
                db.Entry(dbPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = GetPaymentById(id.Value);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Payment payment = GetPaymentById(id);
            try
            {
                payment.Deleted = true;
                //db.Payments.Remove(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 547/*DELETE statement conflicted */))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete this order, because its containing childrens");
                    return View(payment);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(payment);
                }
            }
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
