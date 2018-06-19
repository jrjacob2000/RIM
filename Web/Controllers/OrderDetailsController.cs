using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class OrderDetailsController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            //return View(await db.OrderDetails.Where(x => x.CreatedBy == UserId).ToListAsync());
            return View(GetOrderDetailList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            OrderDetail orderDetail = GetOrderDetailById(id.Value);
            orderDetail.Order = GetOrderById(orderDetail.Order_Id, true);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create(Guid Order_Id)
        {
            var od = new OrderDetail();
            od.Product = new Product();
            od.Order_Id = Order_Id;
            od.Order = GetOrderById(Order_Id, true);


            List<SelectListItem> productItems = new List<SelectListItem>();
            //productItems = db.ProductItems.Where(x => x.CreatedBy == UserId).ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            productItems = GetProductList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.ProductItems = productItems;

            return View(od);
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Quantity,UnitDiscount,Order_Id,Product_Id")] OrderDetail orderDetail)
        {
            bool hasErrors = false;

            ViewBag.ProductItems = GetProductList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
                        
            var price = GetPriceByProduct(orderDetail.Product_Id,true);           
            var order = GetOrderById(orderDetail.Order_Id,true);        
            var prod = GetProductById(orderDetail.Product_Id,true);           
            prod.OrderDetails = GetOrderDetailList(orderDetail.Product_Id).ToList();

            //check current inventory
            if (order.OrderType == Helper.Constants.OrderType.SALE)
            {
                if (orderDetail.Quantity > prod.InventoryOnHand)
                {
                    ModelState.AddModelError(string.Empty, string.Format("The current inventory of product {0} is only {1}, not enough to create your order of {2}", prod.Name, prod.InventoryOnHand, orderDetail.Quantity));
                    hasErrors = true;
                }
            }
            
            ////check if price is available
            var priceId = price == null  ? (Guid?)null : price.EffectiveFromDate <= order.OrderDate ? price.Id : (Guid?)null ;
            if (priceId == null)
            {
                ModelState.AddModelError(string.Empty, string.Format("No Price was found for the Product {0}. Please verify the effective date of the product price vs order date", prod.Name));
                hasErrors = true;
            }

            if (hasErrors)
            {
                orderDetail.Order = order;
                return View(orderDetail);
            }
            

            orderDetail.Id = Guid.NewGuid();
            orderDetail.ProductPrice_Id = priceId;
            orderDetail.CreatedBy = UserId;
            //db.OrderDetails.Add(orderDetail);

            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Added;
                await db.SaveChangesAsync();
                return RedirectToAction(order.OrderType == Helper.Constants.OrderType.ADJUST? "AdjustDetails" : "Details", "Orders", new { Id = orderDetail.Order_Id });
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ProductItems = GetProductList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            
            //OrderDetail orderDetail = await db.OrderDetails
            //    .Include("Order")
            //    .Include("Product")
            //    .Include("ProductPrice")
            //    .FirstOrDefaultAsync(x => x.Id == id);

             OrderDetail orderDetail = GetOrderDetailById(id.Value);
             orderDetail.Order = GetOrderById(orderDetail.Order_Id, true);   

            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Quantity,UnitDiscount,Order,ProductPrice, Product")]OrderDetail orderDetail)
        {
            //remove from validation
            ModelState.Remove("Order.OrderNumber");
            ModelState.Remove("Order.OrderType");
            ModelState.Remove("Product.Name");

            //ViewBag.ProductItems = db.ProductItems.ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList(); 
            ViewBag.ProductItems = GetProductList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var order = GetOrderById(orderDetail.Order.Id, true); 
            var prod = GetProductById(orderDetail.Product.Id, true);
            prod.OrderDetails = GetOrderDetailList(orderDetail.Product.Id).ToList();

            var dbOrderDetail = GetOrderDetailById(orderDetail.Id);
            //check current inventory
            var hasErrors = false;
            if (order.OrderType == Helper.Constants.OrderType.SALE)
            {
                if (orderDetail.Quantity > (prod.InventoryOnHand + dbOrderDetail.Quantity))
                {
                    ModelState.AddModelError(string.Empty, string.Format("The current inventory of product {0} is only {1}, not enough to create your order of {2}", prod.Name, (prod.InventoryOnHand + dbOrderDetail.Quantity), orderDetail.Quantity));
                    hasErrors = true;
                }
            }

            if (hasErrors)
            {               
                orderDetail.Product = prod;
                return View(orderDetail);
            }

            
           
            dbOrderDetail.Quantity = orderDetail.Quantity;
            dbOrderDetail.UnitDiscount = orderDetail.UnitDiscount;

            //orderDetail.Order_Id = orderDetail.Order.Id;
            //orderDetail.ProductPrice_Id = orderDetail.ProductPrice.Id;
            //orderDetail.Product_Id = orderDetail.Product.Id;
            //orderDetail.CreatedBy = dbOrderDetail.CreatedBy;
            ////orderDetail.Product = db.ProductItems.Find( orderDetail.Product.Id);
            //orderDetail.Product = GetProductById(orderDetail.Product.Id);
            if (ModelState.IsValid)
            {
                db.Entry(dbOrderDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(order.OrderType == Helper.Constants.OrderType.ADJUST ? "AdjustDetails" : "Details", "Orders", new { Id = orderDetail.Order.Id });
                //return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OrderDetail orderDetail = db.OrderDetails
            //    .Include("Order")
            //    .Include("Product")
            //    .Include("ProductPrice").FirstOrDefault(x => x.Id == id);

            OrderDetail orderDetail = GetOrderDetailById(id.Value);

            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            //OrderDetail orderDetail = db.OrderDetails.Find(id);
            OrderDetail orderDetail = GetOrderDetailById(id);
            var order = GetOrderById(orderDetail.Order_Id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction(order.OrderType == Helper.Constants.OrderType.ADJUST ? "AdjustDetails" : "Details", "Orders", new { Id = orderDetail.Order_Id });
            //return RedirectToAction("Index");
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
