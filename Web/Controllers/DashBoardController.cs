using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    [Authorize]
    public class DashBoardController : ControllerBase
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            Login obj = new Login();

            ViewBag.Title = "Home Page";

            var productlist = GetProductList().ToList();

            productlist.ForEach(x =>
            {
                x.OrderDetails = GetOrderDetailList().Where(o => o.Product_Id == x.Id).ToList();
                x.ProductPrices = GetPriceList().Where(p => p.Product_Id == x.Id).ToList();
            });


            ViewBag.InventoryOnHand = productlist.Sum(x => x.InventoryOnHand);
            ViewBag.InventoryToGo = productlist.Sum(x => x.InventoryToGo);
            ViewBag.InventoryToCome = productlist.Sum(x => x.InventoryToCome);

            ViewBag.ProductToReOrder = productlist.Count(x => x.ToOrder);
            ViewBag.ProductInStock = productlist.Count(x => x.InventoryOnHand > 0);
            ViewBag.InventoryValue = productlist.Sum(x => x.InventoryValue);


            return View(obj);
        }

        // GET: DashBoard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashBoard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashBoard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBoard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashBoard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBoard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashBoard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
