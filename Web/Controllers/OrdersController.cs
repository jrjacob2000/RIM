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

namespace Web.Controllers
{
    [Authorize]
    public class OrdersController : ControllerBase
    {
        
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index(string orderType)
        {
            var list = Helper.Constants.OrderTypeList();

            ViewBag.OrderTypes = list;
            ViewData["orderType"] = orderType;
            
            //return View(await db.Orders.Include("Partner").Where(x => x.OrderType == orderType || string.IsNullOrEmpty(orderType)).ToListAsync());
            var orders = GetOrderList()
                .Where(x => x.OrderType == orderType || string.IsNullOrEmpty(orderType))
                .OrderByDescending(o => o.OrderDate).ToList();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Order order = await db.Orders
            //    .Include("OrderDetails")
            //    .Include("OrderDetails.Product")
            //    .Include("OrderDetails.ProductPrice")
            //    .Include("Partner")
            //    .FirstOrDefaultAsync(x => x.Id == id);

            Order order = GetOrderById(id.Value);
                       

            if (order == null)
            {
                return HttpNotFound();
            }

            
                       
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(string OrderType)
        {
            ViewBag.OrderTypes = Helper.Constants.OrderTypeList();
            //ViewBag.Partners = db.Partners.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var setting = GetSetting();
            var order = new Order();
            
            if(setting != null) 
            {
                if (!string.IsNullOrEmpty(setting.PurchaseNumber) && OrderType == Helper.Constants.OrderType.PURCHASE)
                    order.OrderNumber = string.Format("{0}-{1}", setting.PurchasePrefix, setting.PurchaseNumber);

                if (!string.IsNullOrEmpty(setting.SalesNumber) && OrderType == Helper.Constants.OrderType.SALE)
                    order.OrderNumber = string.Format("{0}-{1}", setting.SalesPrefix, setting.SalesNumber);
            }

            
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
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderNumber,OrderDate,ExpectedDate,OrderType,OtherCharges,OrderDiscount,TaxRate,OrderNotes,Partner_Id")] Order order)
        {
            ViewBag.OrderTypes = Helper.Constants.OrderTypeList();
            //ViewBag.Partners = db.Partners.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var setting = GetSetting();
            if (order.OrderType == Helper.Constants.OrderType.PURCHASE && !string.IsNullOrEmpty(setting.PurchaseNumber))
            {
                order.OrderNumber = string.Format("{0}-{1}", setting.PurchasePrefix, setting.PurchaseNumber);

                var length = setting.PurchaseNumber.Length;
                var newValue = int.Parse(setting.PurchaseNumber) + 1;

                setting.PurchaseNumber = newValue.ToString().PadLeft(length, '0'); ;

            }
            if (order.OrderType == Helper.Constants.OrderType.SALE && !string.IsNullOrEmpty(setting.SalesNumber))
            {
                order.OrderNumber = string.Format("{0}-{1}", setting.SalesPrefix, setting.SalesNumber);

                var length = setting.SalesNumber.Length;
                var newValue = int.Parse(setting.SalesNumber) + 1;

                setting.SalesNumber = newValue.ToString().PadLeft(length, '0');
                
            }

            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                order.CreatedBy = UserId;
                //order.Partner_Id = order.Partner.Id;
                //order.Partner = db.Partners.Find(order.Partner.Id);
                db.Orders.Add(order);
                await db.SaveChangesAsync();

                return RedirectToAction("Details", new { id = order.Id });
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(Guid? id)
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

            ViewBag.OrderTypes = Helper.Constants.OrderTypeList();
            //ViewBag.Partners = db.Partners.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderNumber,OrderDate,ExpectedDate,OrderType,OtherCharges,OrderDiscount,TaxRate,OrderNotes,Partner_Id")] Order order)
        {
            ViewBag.OrderTypes = Helper.Constants.OrderTypeList();
            //ViewBag.Partners = db.Partners.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Partners = GetPartnerList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var dbOrder = GetOrderById(order.Id, true);
            order.CreatedBy = dbOrder.CreatedBy;
            
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
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
                return RedirectToAction("Index");
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
    }
}
