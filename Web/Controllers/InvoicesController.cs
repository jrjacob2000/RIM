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
    public class InvoicesController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices
                .Include("Partner")
                .Where(x => x.CreatedBy == UserId && x.Type == Helper.Constants.InvoiceType.INVOICE)
                .ToList()
                .Select(s => new Invoice()
                {
                    Id = s.Id,
                    Partner_Id = s.Partner_Id,
                    InvoiceNumber = s.InvoiceNumber,
                    Order_Id = s.Order_Id,
                    InvoiceDate = s.InvoiceDate,
                    DueDate = s.DueDate,
                    OtherCharges = s.OtherCharges,
                    OrderDiscount = s.OrderDiscount,
                    TaxRate = s.TaxRate,
                    CreatedBy = s.CreatedBy,
                    Order = GetOrderById(s.Order_Id),
                    Partner = s.Partner,
                    PaymentDetails = db.PaymentDetails.Include("Payment").Where(p => p.Invoice_Id == s.Id && !p.Payment.Deleted).ToList(),
                    Credits = GetCreditByPartnerId(s.Partner.Id).ToList(),
                    Status = GetStatus(s)
                });


            return View(invoices);
        }

        private string GetStatus(Invoice inv)
        {
            if(inv.Balance <= 0)
                return Helper.Constants.InvoiceStatus.PAID;
            else if (inv.DueDate < DateTime.Now)
                return Helper.Constants.InvoiceStatus.OVERDUE;
            else if (inv.PaidAmount > 0)
                return Helper.Constants.InvoiceStatus.PARTIALPAID;
            else
                return Helper.Constants.InvoiceStatus.DRAFT;           

        }
       
        // GET: Invoices/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices
                .Include("Order")
                .Include("Order.OrderDetails")
                .Include("Order.OrderDetails.Product")
                .Include("Order.OrderDetails.ProductPrice")
                .Include("Partner").
                Where(x => x.Id == id && x.CreatedBy == UserId).FirstOrDefault();

            invoice.PaymentDetails = GetPaymentDetailList().Where(x => x.Invoice_Id == id && !x.Payment.Deleted).ToList();
            invoice.Credits = GetCreditByPartnerId(invoice.Partner_Id).ToList();//GetCreditByInvoiceId(invoice.Id).ToList();
            //invoice.CreditsAvailable = GetCreditByPartnerId(invoice.Partner_Id).ToList();
            ViewBag.Total = invoice.Order.OrderDetails.Sum(x => x.AmountAfterTax);

            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create(Guid? Order_Id)
        {
            ViewBag.Orders = GetOrderList().Where(x => x.OrderType == Helper.Constants.OrderType.SALE).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.OrderNumber }).ToList();
            ViewBag.Partners = new SelectList(GetPartnerList(), "Id", "Name");

            var inv = new Invoice();


            inv.DueDate = DateTime.Now;
            inv.InvoiceDate = DateTime.Now;
            inv.Type = Helper.Constants.InvoiceType.INVOICE;

            var order = new Order();
            if (Order_Id.HasValue)
            {
                order = GetOrderById(Order_Id.Value);
                inv.Partner_Id = order.Partner_Id.Value;
            }

            var setting = GetSetting();
            if (setting != null && !string.IsNullOrEmpty(setting.InvoiceNumber))
            {
                inv.InvoiceNumber = string.Format("{0}-{1}", setting.InvoicePrefix, setting.InvoiceNumber);

            }

            return View(inv);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Partner_Id,InvoiceNumber,Order_Id,InvoiceDate,DueDate,OtherCharges,OrderDiscount,TaxRate,CreatedBy")] Invoice invoice)
        {

            if (ModelState.IsValid)
            {
                invoice.Id = Guid.NewGuid();
                invoice.CreatedBy = UserId;
                invoice.Status = Helper.Constants.InvoiceStatus.DRAFT;
                invoice.Type = Helper.Constants.InvoiceType.INVOICE;
                //regenerate the invoicenumber just incase that the number has been used already
                var setting = GetSetting();
                if (setting != null && !string.IsNullOrEmpty(setting.InvoiceNumber))
                {
                    invoice.InvoiceNumber = string.Format("{0}-{1}", setting.InvoicePrefix, setting.InvoiceNumber);

                    var length = setting.InvoiceNumber.Length;
                    var newValue = int.Parse(setting.InvoiceNumber) + 1;
                    setting.InvoiceNumber = newValue.ToString().PadLeft(length, '0');
                }

                
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Details", new {id=invoice.Id });
            }

            ViewBag.Orders = GetOrderList().Where(x => x.OrderType == Helper.Constants.OrderType.SALE).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.OrderNumber }).ToList();
            ViewBag.Partners = new SelectList(GetPartnerList(), "Id", "Name");

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices
                .Include("Order")
                //.Include("Order.OrderDetails")
                //.Include("Order.OrderDetails.Product")
                //.Include("Order.OrderDetails.ProductPrice")
                .Include("Partner").
                Where(x => x.Id == id && x.CreatedBy == UserId).FirstOrDefault();


            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_Id = new SelectList(db.Orders, "Id", "OrderNumber", invoice.Order_Id);
            ViewBag.Partner_Id = new SelectList(db.Partners, "Id", "Name", invoice.Partner_Id);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Partner_Id,InvoiceNumber,Order_Id,InvoiceDate,DueDate,OtherCharges,OrderDiscount,TaxRate,CreatedBy")] Invoice invoice)
        {
            var dbInvoice = db.Invoices.Find(invoice.Id);
            dbInvoice.Partner_Id = invoice.Partner_Id;
            dbInvoice.InvoiceNumber = invoice.InvoiceNumber;
            dbInvoice.Order_Id = invoice.Order_Id;
            dbInvoice.InvoiceDate = invoice.InvoiceDate;
            dbInvoice.DueDate = invoice.DueDate;


            if (ModelState.IsValid)
            {
                db.Entry(dbInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            var orderlist = GetOrderList().Where(x => x.OrderType == Helper.Constants.OrderType.SALE);
            ViewBag.Orders = new SelectList(orderlist, "Id", "OrderNumber", invoice.Order_Id);
            ViewBag.Partners = new SelectList(GetPartnerList(), "Id", "Name");
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Invoice invoice = db.Invoices.Find(id);

            if (invoice.PaymentDetails.Count() > 0)
            {
                
                ModelState.AddModelError(string.Empty,string.Format( "Can't delete this {0}, because its payment", invoice.Type));
                return View(invoice);
            }

            try
            {                
                db.Invoices.Remove(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(invoice);
                }

                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 547/*DELETE statement conflicted */))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete this order, because its containing details or used in payment");
                    return View(invoice);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(invoice);
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
