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
    public class InvoiceControllerBase : ControllerBase
    {
       
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

            var credits = new List<Credit>();
            if(invoice.Type == Helper.Constants.InvoiceType.INVOICE)
                credits = GetCreditByPartnerId(invoice.Partner_Id).Where(x => x.Order.AdjustmentReason == "RETURN_CUSTOMER").ToList();
            else if (invoice.Type == Helper.Constants.InvoiceType.INVOICE)
                credits = GetCreditByPartnerId(invoice.Partner_Id).Where(x => x.Order.AdjustmentReason == "RETURN_SUPPLIER").ToList();

            invoice.PaymentDetails = GetPaymentDetailList().Where(x => x.Invoice_Id == id && !x.Payment.Deleted).ToList();
            invoice.Credits = credits;
            ViewBag.Total = invoice.Order.OrderDetails.Sum(x => x.AmountAfterTax);

            if (invoice == null)
            {
                return HttpNotFound();
            }
            if (TempData.ContainsKey("ModelState"))
                ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);

            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create(Guid? Order_Id,string type)
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
            if (type.ToUpper() == "INV" && setting != null && !string.IsNullOrEmpty(setting.InvoiceNumber))
            {
                inv.InvoiceNumber = string.Format("{0}-{1}", setting.InvoicePrefix, setting.InvoiceNumber);

            }
            else if (type.ToUpper() == "BILL" && setting != null && !string.IsNullOrEmpty(setting.BillNumber))
            {
                inv.InvoiceNumber = string.Format("{0}-{1}", setting.BillPrefix, setting.BillNumber);

            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"unrecognized command");

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
                return RedirectToAction("Details", new { id = invoice.Id });
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

                ModelState.AddModelError(string.Empty, string.Format("Can't delete this {0}, because its payment", invoice.Type));
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

        public ActionResult UseCredit(Guid id)
        {
            Invoice invoice = db.Invoices
                .Include("Order")
                .Include("Order.OrderDetails")
                .Include("Order.OrderDetails.Product")
                .Include("Order.OrderDetails.ProductPrice")
                .Where(x => x.Id == id && x.CreatedBy == UserId).FirstOrDefault();
            invoice.PaymentDetails = GetPaymentDetailList().Where(x => x.Invoice_Id == id && !x.Payment.Deleted).ToList();
            invoice.Credits = GetCreditByPartnerId(invoice.Partner_Id).ToList();

            var credits = new List<Credit>();
            if (invoice.Type == Helper.Constants.InvoiceType.INVOICE)
                credits = GetCreditByPartnerId(invoice.Partner_Id).Where(x => x.Invoice_Id == null && x.Order.AdjustmentReason == "RETURN_CUSTOMER").ToList();
            else if (invoice.Type == Helper.Constants.InvoiceType.INVOICE)
                credits = GetCreditByPartnerId(invoice.Partner_Id).Where(x => x.Invoice_Id == null && x.Order.AdjustmentReason == "RETURN_SUPPLIER").ToList();

            //var credits = GetCreditByPartnerId(invoice.Partner_Id).Where(x => x.Invoice_Id == null).ToList();

            var creditsAmount = credits.Sum(x => x.Amount);

            if (invoice.Amount < creditsAmount)
            {
                ModelState.AddModelError(string.Empty, "Total credits amount is greater than the invoice amount.");
                TempData["ModelState"] = ModelState;//use tempdata To persist the model state across redirects. for error display purpose
                return RedirectToAction("Details", new { id = invoice.Id });
            }

            ///TODO:use the CreditsInvoice
            ///for now we are using 1 invoice per credits
            foreach (Credit cr in credits)
            {
                cr.Invoice_Id = id;
                cr.Status = Helper.Constants.CreditNotesStatus.PAID;
            }
            db.SaveChanges();

            return RedirectToAction("Details", new { id = invoice.Id });
        }

        public ActionResult DeleteCredit(Guid id)
        {
            var credit = db.Credits
                .Where(x => x.Id == id && x.CreatedBy == UserId).FirstOrDefault();
            if (credit == null || credit.Invoice_Id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var invoice = db.Invoices
                    .Where(x => x.Id == credit.Invoice_Id && x.CreatedBy == UserId).FirstOrDefault();
                if (invoice == null)
                    return HttpNotFound();

                if (invoice.Status == Helper.Constants.InvoiceStatus.PAID)
                {
                    ModelState.AddModelError(string.Empty, "Can't remove this credit, invoice is already paid or completed");
                    return RedirectToAction("Details", new { id = invoice.Id });
                }


                credit.Invoice_Id = null;
                credit.Status = Helper.Constants.CreditNotesStatus.DRAFT;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = invoice.Id });

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