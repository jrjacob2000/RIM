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
    public class CreditsController : ControllerBase
    {

        //Get: Bills
        public ActionResult Index(string type)
        {
            var invoices = db.Credits
                .Include("Partner")
                .Where(x => x.CreatedBy == UserId && x.Type == type)
                .ToList()
                .Select(s => new Credit()
                {
                    Id = s.Id,
                    Partner_Id = s.Partner_Id,
                    CreditNumber = s.CreditNumber,
                    Order_Id = s.Order_Id,
                    CreditDate = s.CreditDate,
                    DueDate = s.DueDate,
                    CreatedBy = s.CreatedBy,
                    Order = GetOrderById(s.Order_Id),
                    Partner = s.Partner,
                    //PaymentDetails = db.PaymentDetails.Include("Payment").Where(p => p.Invoice_Id == s.Id && !p.Payment.Deleted).ToList(),
                    Status = s.Status == Helper.Constants.InvoiceStatus.PAID? s.Status : s.DueDate < DateTime.Now ? Helper.Constants.InvoiceStatus.OVERDUE : s.Status
                });


            return View(invoices);
        }

      
        // GET: Invoices/Create
        public ActionResult Create(Guid? Order_Id, string OrderType)
        {
            var orderlist = GetOrderList().Where(x => x.OrderType == OrderType);
            ViewBag.Orders = new SelectList(orderlist, "Id", "OrderNumber");
            ViewBag.Partners = new SelectList(GetPartnerList(), "Id", "Name");


            var inv = new Credit();


            inv.DueDate = DateTime.Now;
            inv.CreditDate = DateTime.Now;
            if (OrderType == Helper.Constants.OrderType.SUPPLIER_RETURN)
                inv.Type = Helper.Constants.InvoiceType.SupplierCredit;
            else if (OrderType == Helper.Constants.OrderType.CUSTOMER_RETURN)
                inv.Type = Helper.Constants.InvoiceType.CustomerCredit;
            else
                throw new Exception(string.Format("Invalid command {0} for creating credits.", OrderType));


            var order = new Order();
            if (Order_Id.HasValue)
            {
                order = GetOrderById(Order_Id.Value);
                inv.Partner_Id = order.Partner_Id.Value;
            }

            var setting = GetSetting();
            //if (setting != null && !string.IsNullOrEmpty(setting.InvoiceNumber))
            //{
            //    inv.CreditNumber = string.Format("{0}-{1}", setting.InvoicePrefix, setting.InvoiceNumber);

            //}

            return View(inv);
        }

        // POST: Invoices/CreateBill
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Credit credit, string OrderType)
        {

            if (ModelState.IsValid)
            {
                credit.Id = Guid.NewGuid();
                credit.CreatedBy = UserId;
                credit.Status = Helper.Constants.InvoiceStatus.DRAFT;


                if (OrderType == Helper.Constants.OrderType.SUPPLIER_RETURN)
                    credit.Type = Helper.Constants.InvoiceType.SupplierCredit;
                else if (OrderType == Helper.Constants.OrderType.CUSTOMER_RETURN)
                    credit.Type = Helper.Constants.InvoiceType.CustomerCredit;
                else
                    throw new Exception(string.Format("Invalid command {0} for creating credits.", OrderType));
                
                ////regenerate the invoicenumber just incase that the number has been used already
                //var setting = GetSetting();
                //if (setting != null && !string.IsNullOrEmpty(setting.InvoiceNumber))
                //{
                //    invoice.InvoiceNumber = string.Format("{0}-{1}", setting.InvoicePrefix, setting.InvoiceNumber);

                //    var length = setting.InvoiceNumber.Length;
                //    var newValue = int.Parse(setting.InvoiceNumber) + 1;
                //    setting.InvoiceNumber = newValue.ToString().PadLeft(length, '0');
                //}


                db.Credits.Add(credit);
                db.SaveChanges();
                return RedirectToAction("Index", new { type = credit.Type });
            }

            var orderlist = GetOrderList().Where(x => x.OrderType == OrderType);
            ViewBag.Orders = new SelectList(orderlist, "Id", "OrderNumber", credit.Order_Id);
            ViewBag.Partners = new SelectList(GetPartnerList(), "Id", "Name", credit.Partner_Id);


            return View(credit);
        }


        // GET: Invoices/EditBill/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit credit = db.Credits
                .Include("Order")
                //.Include("Order.OrderDetails")
                //.Include("Order.OrderDetails.Product")
                //.Include("Order.OrderDetails.ProductPrice")
                .Include("Partner").
                Where(x => x.Id == id && x.CreatedBy == UserId).FirstOrDefault();


            if (credit == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_Id = new SelectList(db.Orders, "Id", "OrderNumber", credit.Order_Id);
            ViewBag.Partner_Id = new SelectList(db.Partners, "Id", "Name", credit.Partner_Id);
            return View(credit);
        }

        // POST: Invoices/EditBill/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Credit credit)
        {
            var dbCredit = db.Credits.Find(credit.Id);
            dbCredit.Partner_Id = credit.Partner_Id;
            dbCredit.CreditNumber = credit.CreditNumber;
            dbCredit.Order_Id = credit.Order_Id;
            dbCredit.CreditDate = credit.CreditDate;
            dbCredit.DueDate = credit.DueDate;
            dbCredit.Status = DateTime.Now > credit.DueDate ? Helper.Constants.InvoiceStatus.OVERDUE : dbCredit.Status;


            if (ModelState.IsValid)
            {
                db.Entry(dbCredit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Type = dbCredit.Type });
            }


            var orderlist = GetOrderList().Where(x => x.OrderType == Helper.Constants.OrderType.SALE);
            ViewBag.Orders = new SelectList(orderlist, "Id", "OrderNumber", credit.Order_Id);
            ViewBag.Partners = new SelectList(GetPartnerList(), "Id", "Name");
            return View(credit);
        }


        // GET: Invoices/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit credit = db.Credits
                .Include("Order")
                .Include("Order.OrderDetails")
                .Include("Order.OrderDetails.Product")
                .Include("Order.OrderDetails.ProductPrice")
                .Include("Partner").
                Where(x => x.Id == id && x.CreatedBy == UserId).FirstOrDefault();

            //invoice.PaymentDetails = GetPaymentDetailList().Where(x => x.Invoice_Id == id && !x.Payment.Deleted).ToList();
            ViewBag.Total = credit.Order.OrderDetails.Sum(x => x.AmountAfterTax);

            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }


        // GET: Invoices/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit credit = db.Credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {

            Credit credit = db.Credits.Find(id);

            
            try
            {
                db.Credits.Remove(credit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(credit);
                }

                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 547/*DELETE statement conflicted */))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete this, because its containing details or used in payment");
                    return View(credit);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(credit);
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
