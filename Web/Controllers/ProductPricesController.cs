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
    public class ProductPricesController : ControllerBase
    {
         //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PriceItems
        public ActionResult Index(string product, Guid? category)
        {
            ViewBag.CategoryItems = GetCategoryList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList(); ;
            var list = GetPriceList()
                .Where(x => (x.Product.Name.Contains(product) || string.IsNullOrEmpty(product)) && (x.Product.Category.Id == category || category == null))
                .OrderBy(o => o.Product.Name)
                .ToList(); 
            return View(list);
        }

        // GET: PriceItems/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPrice priceItem = GetPriceById(id.Value); //db.PriceItems.Find(id);
            //db.Entry(priceItem).Reference("Product").Load();

            if (priceItem == null)
            {
                return HttpNotFound();
            }
            return View(priceItem);
        }

        // GET: PriceItems/Create
        public ActionResult Create(Guid? Product_Id, string ReturnPage)
        {
            var p = new ProductPrice();

            p.Product = new Product();
            if (Product_Id.HasValue)
            {
                p.Product_Id = Product_Id.Value;
                p.Product = GetProductById(Product_Id.Value, true);
            }
            
            p.EffectiveFromDate = DateTime.Now.Date;
            p.CreatedBy = UserId;
             
            ViewBag.ProductItems = GetProductList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View(p);
        }

        // POST: PriceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EffectiveFromDate,PurchasePrice,SelesPrice,Product_Id")] ProductPrice priceItem, string ReturnPage)
        {

            ViewBag.ProductItems = GetProductList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

            if (ModelState.IsValid)
            {
                priceItem.Id = Guid.NewGuid();
                priceItem.CreatedBy = UserId;                
                
                db.PriceItems.Add(priceItem);
                db.SaveChanges();
                if(ReturnPage == "Product")
                    return RedirectToAction("Index","Products");
                else
                    return RedirectToAction("Index");
            }

            return View(priceItem);
        }

        // GET: PriceItems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPrice priceItem = GetPriceById(id.Value);//db.PriceItems.Find(id);
            //db.Entry(priceItem).Reference("Product").Load();

            //priceItem.Product = db.ProductItems.Find(priceItem.pro)
            if (priceItem == null)
            {
                return HttpNotFound();
            }
            return View(priceItem);
        }

        // POST: PriceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EffectiveFromDate,PurchasePrice,SelesPrice,Product_Id")] ProductPrice priceItem)
        {
            var dbPriceItem = GetPriceById(priceItem.Id,true);
            priceItem.CreatedBy = dbPriceItem.CreatedBy;
            if (ModelState.IsValid)
            {
                db.Entry(priceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(priceItem);
        }

        // GET: PriceItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPrice priceItem = GetPriceById(id.Value); //db.PriceItems.Find(id);
            if (priceItem == null)
            {
                return HttpNotFound();
            }
            return View(priceItem);
        }

        // POST: PriceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductPrice priceItem = GetPriceById(id);// db.PriceItems.Find(id);

            var hasChild = db.OrderDetails.Where(x => x.ProductPrice_Id == id).Count() > 0;

            if (hasChild)
            {
                ModelState.AddModelError(string.Empty,"Price cannot be deleted, its currently being used in Order items");
                return View(priceItem);
            }

            db.PriceItems.Remove(priceItem);
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
