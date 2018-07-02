using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Web.Models;
using PagedList;
using MvcBreadCrumbs;

namespace Web.Controllers
{
    [Authorize]
    public class OrdersController : ControllerBase
    {

        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        [BreadCrumb(Clear =true)]
        public ActionResult Index(string orderType,string sortOrder,  int page = 1, int pageSize = 10)
        {
            switch(orderType)
            {
                case Helper.Constants.OrderType.PURCHASE:
                    BreadCrumb.SetLabel("Purchase Orders");
                    break;
                case Helper.Constants.OrderType.SALE:
                    BreadCrumb.SetLabel("Sales Orders");
                    break;
                case Helper.Constants.OrderType.ADJUST:
                    BreadCrumb.SetLabel("Adjustments");
                    break;
            }
            ViewBag.OrderType = orderType;
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 10;

            ViewBag.OrderNumberSortParam = sortOrder == "OrderNumber" ? "OrderNumber_desc" : "OrderNumber";
            ViewBag.OrderDateSortParam = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";
            ViewBag.ExpectedDateSortParam = sortOrder == "ExpectedDate" ? "ExpectedDate_desc" : "ExpectedDate";
            ViewBag.PartnerSortParam = sortOrder == "Partner" ? "Partner_desc" : "Partner";
            ViewBag.ReasonSortParam = sortOrder == "Reason" ? "Reason_desc" : "Reason";

            ViewBag.CurrentSort = sortOrder;

            var list = Helper.Constants.OrderTypeList();

            ViewBag.OrderTypes = list;
            

            var query = GetOrderList()
                .Where(x => x.OrderType == orderType || string.IsNullOrEmpty(orderType));
                //.OrderByDescending(o => o.OrderDate).ToList();

            switch (sortOrder)
            {
                case "OrderDate":
                    query = query.OrderBy(x => x.OrderDate);
                    break;
                case "OrderDate_desc":
                    query = query.OrderByDescending(x => x.OrderDate);
                    break;
                case "OrderNumber":
                    query = query.OrderBy(x => x.OrderNumber);
                    break;
                case "OrderNumber_desc":
                    query = query.OrderByDescending(x => x.OrderNumber);
                    break;
                case "ExpectedDate":
                    query = query.OrderBy(x => x.ExpectedDate);
                    break;
                case "ExpectedDate_desc":
                    query = query.OrderByDescending(x => x.ExpectedDate);
                    break;
                case "Partner":
                    query = query.OrderBy(x => x.Partner.Name);
                    break;
                case "Partner_desc":
                    query = query.OrderByDescending(x => x.Partner.Name);
                    break;
                case "Reason":
                    query = query.OrderBy(x => x.AdjustmentReason);
                    break;
                case "Reason_desc":
                    query = query.OrderByDescending(x => x.AdjustmentReason);
                    break;  
                default:
                    query = query.OrderBy(x => x.OrderDate);
                    break;
            }

            IPagedList<Order> orders = query.ToPagedList(page, pageSize);
            orders.ToList().ForEach(x =>
                    x.Invoices = GetInvoiceByOrderId(x.Id).ToList()
                );

            return View(orders);
        }

        // GET: Orders/Details/5
        [BreadCrumb]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                      
            Order order = GetOrderById(id.Value);
            BreadCrumb.SetLabel("View " + order.OrderNumber);

            order.Invoices = GetInvoiceByOrderId(order.Id).ToList();
            order.Credits = GetCreditByInvoiceId(order.Id).ToList();
                       

            if (order == null)
            {
                return HttpNotFound();
            }

                       
            return View(order);
        }

        [BreadCrumb(Clear =false)]
        public ActionResult AdjustDetails(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.AdjustmentReasons = Helper.Constants.AdustmentReasonList();  

            var adjust = new Adjust();
            Order order = GetOrderById(id.Value);
            BreadCrumb.SetLabel("View " + order.OrderNumber);

            adjust.Id = order.Id;
            adjust.OrderNumber = order.OrderNumber;
            adjust.OrderDate = order.OrderDate;
            adjust.OrderDetails = order.OrderDetails;
            adjust.OrderNotes = order.OrderNotes;
            adjust.OrderType = order.OrderType;
            adjust.AdjustReason = Helper.Constants.AdustmentReasonList().Where( x => x.Value == order.AdjustmentReason).First().Text;
            adjust.Credits = db.Credits.Where(x => x.Order_Id == id.Value && x.CreatedBy == UserId).ToList();


            if (order == null)
            {
                return HttpNotFound();
            }


            return View(adjust);
        }

        // GET: Orders/Create
        //public ActionResult Create(Guid IdToCopy)
        //{
        //    return Create(null, IdToCopy.ToString());
        //}
        [BreadCrumb]
        public ActionResult Create(string OrderType, string IdToCopy)
        {
            switch (OrderType)
            {
                case Helper.Constants.OrderType.PURCHASE:
                    BreadCrumb.SetLabel("Create Purchase Order");
                    break;
                case Helper.Constants.OrderType.SALE:
                    BreadCrumb.SetLabel("Create Sales Order");
                    break;
                case Helper.Constants.OrderType.ADJUST:
                    BreadCrumb.SetLabel("Create Adjustment");
                    break;
            }

            ViewBag.IdToCopy = IdToCopy;
            ViewBag.OrderTypes = Helper.Constants.OrderTypeList();
            ViewBag.AdjustmentReasons = Helper.Constants.AdustmentReasonList();            
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var setting = GetSetting();
            var order = new Order();

       
            if(setting != null) 
            {
                if (!string.IsNullOrEmpty(setting.PurchaseNumber) && OrderType == Helper.Constants.OrderType.PURCHASE)
                    order.OrderNumber = string.Format("{0}-{1}", setting.PurchasePrefix, setting.PurchaseNumber);

                if (!string.IsNullOrEmpty(setting.SalesNumber) && OrderType == Helper.Constants.OrderType.SALE)
                    order.OrderNumber = string.Format("{0}-{1}", setting.SalesPrefix, setting.SalesNumber);

                //if (!string.IsNullOrEmpty(setting.CustomerReturnNumber) && OrderType == Helper.Constants.OrderType.CUSTOMER_RETURN)
                //    order.OrderNumber = string.Format("{0}-{1}", setting.CustomerReturnPrefix, setting.CustomerReturnNumber);

                //if (!string.IsNullOrEmpty(setting.SupplierReturnNumber) && OrderType == Helper.Constants.OrderType.SUPPLIER_RETURN)
                //    order.OrderNumber = string.Format("{0}-{1}", setting.SupplierReturnPrefix, setting.SupplierReturnNumber);

                if (!string.IsNullOrEmpty(setting.AdjustNumber) && OrderType == Helper.Constants.OrderType.ADJUST)
                    order.OrderNumber = string.Format("{0}-{1}", setting.AdjustPrefix, setting.AdjustNumber);
            }

            order.OrderType = OrderType;
            order.ExpectedDate = DateTime.Now;
            order.OrderDate = DateTime.Now;
            order.CreatedBy = UserId;

            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order,Guid? IdToCopy)
        {
            try
            {
                ViewBag.OrderTypes = Helper.Constants.OrderTypeList();
                ViewBag.AdjustmentReasons = Helper.Constants.AdustmentReasonList();                
                ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

                var setting = GetSetting();
                if (setting != null)
                {
                    if (order.OrderType == Helper.Constants.OrderType.PURCHASE && !string.IsNullOrEmpty(setting.PurchaseNumber))
                    {
                        order.OrderNumber = string.Format("{0}-{1}", setting.PurchasePrefix, setting.PurchaseNumber);

                        var length = setting.PurchaseNumber.Length;
                        var newValue = int.Parse(setting.PurchaseNumber) + 1;

                        setting.PurchaseNumber = newValue.ToString().PadLeft(length, '0'); 
                    }
                    if (order.OrderType == Helper.Constants.OrderType.SALE && !string.IsNullOrEmpty(setting.SalesNumber))
                    {
                        order.OrderNumber = string.Format("{0}-{1}", setting.SalesPrefix, setting.SalesNumber);

                        var length = setting.SalesNumber.Length;
                        var newValue = int.Parse(setting.SalesNumber) + 1;

                        setting.SalesNumber = newValue.ToString().PadLeft(length, '0');
                    }
                    //if (order.OrderType == Helper.Constants.OrderType.CUSTOMER_RETURN && !string.IsNullOrEmpty(setting.CustomerReturnNumber))
                    //{
                    //    order.OrderNumber = string.Format("{0}-{1}", setting.CustomerReturnPrefix, setting.CustomerReturnNumber);
                    //    setting.CustomerReturnNumber = GetNextNumber(setting.CustomerReturnNumber);
                    //}
                    //if (order.OrderType == Helper.Constants.OrderType.SUPPLIER_RETURN && !string.IsNullOrEmpty(setting.SupplierReturnNumber))
                    //{
                    //    order.OrderNumber = string.Format("{0}-{1}", setting.SupplierReturnPrefix, setting.SupplierReturnNumber);
                    //    setting.SupplierReturnNumber = GetNextNumber(setting.SupplierReturnNumber);
                    //}
                    if (order.OrderType == Helper.Constants.OrderType.ADJUST && !string.IsNullOrEmpty(setting.AdjustNumber))
                    {
                        order.OrderNumber = string.Format("{0}-{1}", setting.AdjustPrefix, setting.AdjustNumber);
                        setting.AdjustNumber = GetNextNumber(setting.AdjustNumber);
                    }
                }

                var orderDetails= new List<OrderDetail>();
                if (IdToCopy != null && IdToCopy != Guid.Empty)
                {
                    var orderToCopy = GetOrderById(IdToCopy.Value);

                    foreach (OrderDetail od in orderToCopy.OrderDetails)
                    {
                        var newOd = new OrderDetail
                        {
                            Id = Guid.NewGuid(),
                            Order = order,
                            Order_Id = order.Id,
                            Product = od.Product,
                            Product_Id = od.Product_Id,
                            ProductPrice = od.ProductPrice,
                            ProductPrice_Id = od.ProductPrice_Id,
                            Quantity = od.Quantity,
                            UnitDiscount = od.UnitDiscount,
                            CreatedBy = od.CreatedBy
                        };
                       orderDetails.Add(newOd);
                    }
                }

                order.Id = Guid.NewGuid();
                order.CreatedBy = UserId;
                if (order.TaxRate > 0)
                    order.TaxRate = order.TaxRateToDecimal;


                if (order.OrderType == Helper.Constants.OrderType.ADJUST)
                {
                    order.ExpectedDate = order.OrderDate;
                    if(order.AdjustmentReason == null)
                        ModelState.AddModelError("", "Adjustment reason is required");
                    if(order.AdjustmentReason != "DAMAGE_LOST" && order.Partner_Id == null)
                        ModelState.AddModelError("", "Partner is required");
                } 
                else
                {
                    if (order.Partner_Id == null)
                        ModelState.AddModelError("","Partner is required");
  
                }

                if (ModelState.IsValid)
                {    
                    if (orderDetails.Count > 0)
                        order.OrderDetails = orderDetails ;

                    db.Orders.Add(order);
                    
                    await db.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Saving Successful.";
                    return RedirectToAction(order.OrderType == Helper.Constants.OrderType.ADJUST ? "AdjustDetails":"Details", new { id = order.Id });
                }

                //return View(order);
            }
            catch(Exception ex)
            {
                 if(ex.InnerException == null)
                {                    
                    ModelState.AddModelError(string.Empty, ex.Message);               
                }

                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 547/*DELETE statement conflicted */))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete this order, because its containing details or used in payment");

                }
                else if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 2601 /*uniqe index constraint */)) 
                {                   
                    ModelState.AddModelError(string.Empty, "Cannot Create duplicate Order Number");
                    
                }
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        [BreadCrumb(Clear = false)]
        public ActionResult Edit(Guid? id, string returnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.AdjustmentReasons = Helper.Constants.AdustmentReasonList();  
            //Order order = await db.Orders.FindAsync(id);
            Order order = GetOrderById(id.Value);
            BreadCrumb.SetLabel("Edit " + order.OrderNumber);

            order.TaxRate = order.TaxRate * 100;

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderTypes = Helper.Constants.OrderTypeList();
            //ViewBag.Partners = db.Partners.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            //// If no return url supplied, use referrer url.
            //// Protect against endless loop by checking for empty referrer.
            //if (String.IsNullOrEmpty(returnUrl)
            //    && Request.UrlReferrer != null
            //    && Request.UrlReferrer.ToString().Length > 0)
            //{
            //    return RedirectToAction("Edit",
            //        new { returnUrl = Request.UrlReferrer.ToString() });
            //}
            TempData["UrlReferrer"] = Request.UrlReferrer;

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Order order, string returnUrl)
        {
            ViewBag.OrderTypes = Helper.Constants.OrderTypeList();            
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.AdjustmentReasons = Helper.Constants.AdustmentReasonList();  

            var dbOrder = GetOrderById(order.Id, true);
            order.CreatedBy = dbOrder.CreatedBy;
            order.TaxRate = order.TaxRateToDecimal;
           
            var invoices = GetInvoiceByOrderId(order.Id).ToList();
            
            if(invoices.Any() || order.Status == Helper.OrderStatus.Received || order.Status == Helper.OrderStatus.Closed)
            {
                ModelState.AddModelError("", string.Format("Unable to edit an order that is {0} already.", order.OrderType == Helper.Constants.OrderType.SALE ? "Closed or Invoiced" : "Received or Billed"));
            }
           
            if (order.OrderType == Helper.Constants.OrderType.ADJUST)
            {
                order.ExpectedDate = order.OrderDate;
            }
            else
            {
                if (order.Partner_Id == null)
                    ModelState.AddModelError("", "Partner is required");

            }

            if (ModelState.IsValid)
            {
                 foreach (Invoice inv in invoices)
                 { 
                    inv.Partner_Id = order.Partner_Id.Value;
                    db.Entry(inv).State = EntityState.Modified;
                 }              

                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Saving Successful.";
                // If redirect supplied, then do it, otherwise use a default
                if (!String.IsNullOrEmpty(returnUrl))
                {                     
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", new { orderType = order.OrderType });
                }
                
            }

            
            return View(order);
        }

        // GET: Orders/Delete/5
        [BreadCrumb(Clear = false, Label = "Order Delete")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Order order = await db.Orders.FindAsync(id);
            Order order = GetOrderById(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            //Order order = await db.Orders.FindAsync(id);
            Order order = GetOrderById(id);

            if (order.OrderDetails.Count() > 0)
            {
                ModelState.AddModelError(string.Empty, "Can't delete this order, because its containing items");
                return View(order);  
            }

            try
            {                 
                db.Orders.Remove(order);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Deleted Successful.";
                return RedirectToAction("Index", new { orderType = order.OrderType });
                //return RedirectToAction(order.OrderType == Helper.Constants.OrderType.ADJUST ? "AdjustDetails" : "Details", new { id = order.Id });
            }
            catch (Exception ex)
            {
                if(ex.InnerException == null)
                {                    
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(order);                   
                }

                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 547/*DELETE statement conflicted */))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete this order, because its containing details or used in payment");
                    return View(order);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(order);
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


        private string GetNextNumber(string number)
        {
            var length = number.Length;
            var newValue = int.Parse(number) + 1;

            return newValue.ToString().PadLeft(length, '0');
        }
    }
}
