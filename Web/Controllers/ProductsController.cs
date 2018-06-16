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
using PagedList;

namespace Web.Controllers
{
    [Authorize]
    public class ProductsController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductItems
        public ActionResult Index(string product, Guid? category, string sortOrder, int page = 1, int pageSize = 10)
        {
            ViewBag.ProductFilter = product;
            ViewBag.CategoryFilter = category;
            ViewBag.CategoryItems = GetCategoryList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList(); ;

            var query = GetProductList()
                .Where(x => (x.Name.Contains(product) || string.IsNullOrEmpty(product)) && (x.Category.Id == category || category == null));

            var list = query.ToPagedList(page, pageSize);

            list.ToList().ForEach(x =>
                {
                    x.OrderDetails = GetOrderDetailList().Where(o => o.Product_Id == x.Id).ToList();
                    x.ProductPrices = GetPriceList().Where(p => p.Product_Id == x.Id).ToList();
                });

            return View(list);
        }

        // GET: ProductItems/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product productItem = GetProductById(id.Value); 
            productItem.ProductPrices = db.ProductPrices.Where(x => x.Product_Id == id && x.CreatedBy == UserId).ToList();

            if (productItem == null)
            {
                return HttpNotFound();
            }
            //productItem.Category = db.CategoryItems.Find(productItem.Category_Id);
            return View(productItem);
        }

        // GET: ProductItems/Create
        public ActionResult Create()
        {
            var prices = new List<ProductPrice>();
            prices.Add(new ProductPrice());

            var p = new Product();
            p.Category = new Category();           
            p.ProductPrices = prices;

            ViewBag.CategoryItems = GetCategoryList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList(); ;
            ViewBag.Units = Helper.Constants.UnitList();

            return View(p);
        }

        // POST: ProductItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product productItem)
        {
            ViewBag.CategoryItems = GetCategoryList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList(); ;
            ViewBag.Units = Helper.Constants.UnitList();

            var prodExist = db.ProductItems.Where(x => x.Name == productItem.Name).Count() > 0;

            if (prodExist)
                ModelState.AddModelError(string.Empty, string.Format("Product with name {0} is already exists",productItem.Name));

            var hasError = ModelState[""] == null ? false : ModelState[""].Errors.Count() > 0;

            if (ModelState.IsValid && !hasError)
            {
                //var cat = db.CategoryItems.Find(productItem.Category.Id);
                productItem.Id = Guid.NewGuid();
                productItem.CreatedBy = UserId;
                db.ProductItems.Add(productItem);

                var price = productItem.ProductPrices.First();
                price.Id = Guid.NewGuid();
                price.Product_Id = productItem.Id;
                price.EffectiveFromDate = DateTime.Now;
                price.CreatedBy = UserId;

                db.SaveChanges();

                return RedirectToAction("Index");
                //return RedirectToAction("Create", "ProductPrices", new { Product_Id = productItem.Id, ReturnPage = "Product" });
            }

            return View(productItem);
        }

        // GET: ProductItems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product productItem = GetProductById(id.Value);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.CategoryItems =GetCategoryList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList(); 
            ViewBag.Units = Helper.Constants.UnitList();

            return View(productItem);
        }

        // POST: ProductItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartingInventory,ReOrderPoint,Unit,Taxable,Category_Id")] Product productItem)
        {
            ViewBag.CategoryItems = GetCategoryList().ToList().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Units = Helper.Constants.UnitList();

            var dbProductItem = GetProductById(productItem.Id, true);
            productItem.CreatedBy = dbProductItem.CreatedBy;

            if (ModelState.IsValid)
            {
                //productItem.Category_Id = productItem.Category.Id;
                db.Entry(productItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productItem);
        }

        // GET: ProductItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product productItem = GetProductById(id.Value);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // POST: ProductItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product productItem = GetProductById(id);
            try
            {
                db.ProductItems.Remove(productItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 547/*DELETE statement conflicted */))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete this record, its already being used by other records");
                    return View(productItem);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(productItem);
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
