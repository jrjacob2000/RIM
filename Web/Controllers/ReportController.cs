using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using System.Web.Mvc;


namespace Web.Controllers
{
    [Authorize]
    public class ReportController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Report
        public ActionResult SalesAndPurchase()
        {
            //var sales = db.OrderDetails
            //    .Include("ProductPrice")
            //    .Include("Product")
            //    .Include("Order").Where(x => x.Order.OrderType == Helper.Constants.OrderType.SALE).ToList();
            var sales = GetOrderDetailList().Where(x => x.Order.OrderType == Helper.Constants.OrderType.SALE).ToList();
            
            //var purchases = db.OrderDetails
            //    .Include("ProductPrice")
            //    .Include("Product")
            //    .Include("Order").Where(x => x.Order.OrderType == Helper.Constants.OrderType.PURCHASE).ToList();
            var purchases = GetOrderDetailList().Where(x => x.Order.OrderType == Helper.Constants.OrderType.PURCHASE || x.Order.OrderType == Helper.Constants.OrderType.ADJUST).ToList();

            var sp = new SalesAndPurchase();
            sp.Sales = new OrderReport(sales);
            sp.Purchase = new OrderReport(purchases);


            return View(sp);
        }

        public ActionResult CashFlow()
        {
            var list = new List<CashFlow>();

            var orders = GetOrderList().Where(x => x.OrderType == Helper.Constants.OrderType.PURCHASE).ToList();

            orders.ForEach(x => {
                list.Add(new CashFlow() { 
                    Particulars = x.OrderNumber,
                    CashOut = (x.OtherCharges + GetOrderDetailList()
                                                .Where(o => o.Order_Id == x.Id)
                                                .ToList().Sum(s => s.AmountPurchase)) - x.OrderDiscount ,
                    Date = x.OrderDate
                });
            });

            list.AddRange(GetCashInjectionList().Select(x => 
                new CashFlow()
                {
                    Particulars = x.Particulars,
                    CashIn = x.Amount,
                    Date = x.Date
                }
                ));

             list.AddRange(GetPaymentList().ToList()
                 .Select(x =>
                   new CashFlow()
                   {
                       Particulars = string.Format("Recieve payment from {0}",x.Partner.Name),
                       CashIn = x.Amount,
                       Date = x.Date
                   }
                  ));

            var totalCashIn = list.Sum(x => x.CashIn);
            var totalCashOut = list.Sum(x => x.CashOut);
            ViewBag.AvailableCashOnHand = totalCashIn - totalCashOut;

            return View(list.OrderBy(o => o.Date).ToList());
        }
    }
}