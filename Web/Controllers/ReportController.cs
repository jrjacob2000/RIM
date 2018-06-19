using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;


namespace Web.Controllers
{
    [Authorize]
    public class ReportController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Report
        public ActionResult SalesAndPurchase()
        {
            var sales = GetOrderDetailList().Where(x => (x.Order.Invoices.Count() > 0 && x.Order.OrderType == Helper.Constants.OrderType.SALE) || x.Order.AdjustmentReason == "RETURN_CUSTOMER").ToList();

            var purchases = GetOrderDetailList().Where(x => (x.Order.Invoices.Count() > 0 &&  x.Order.OrderType == Helper.Constants.OrderType.PURCHASE) || x.Order.AdjustmentReason == "RETURN_SUPPLIER").ToList();

            var sp = new SalesAndPurchase();
            sp.Sales = new OrderReport(sales);
            sp.Purchase = new OrderReport(purchases);


            return View(sp);
        }

        public ActionResult CashFlow()
        {
            var list = new List<CashFlow>();


            var bills = GetPaymentDetailList().Where(x => x.Invoice.Type == Helper.Constants.InvoiceType.BILL).ToList();
            bills.ForEach(x =>
            {
                list.Add(new CashFlow()
                {
                    Particulars = x.Invoice.InvoiceNumber,
                    CashOut = x.Amount,
                    Date = x.Date
                });
            });

            var invoices = GetPaymentDetailList().Where(x => x.Invoice.Type == Helper.Constants.InvoiceType.INVOICE).ToList();
            invoices.ForEach(x =>
            {
                list.Add(new CashFlow()
                {
                    Particulars = x.Invoice.InvoiceNumber,
                    CashIn = x.Amount,
                    Date = x.Date
                });
            });


            var refunds = GetPaymentDetailList().Where(x => x.Credit.Order.AdjustmentReason == "RETURN_CUSTOMER").ToList();
            refunds.ForEach(x =>
            {
                list.Add(new CashFlow()
                {
                    Particulars = x.Credit.CreditNumber,
                    CashOut = x.Amount,
                    Date = x.Date
                });
            });

            //var orders = GetOrderList().Where(x => x.OrderType == Helper.Constants.OrderType.PURCHASE).ToList();

            //orders.ForEach(x => {
            //    list.Add(new CashFlow() { 
            //        Particulars = x.OrderNumber,
            //        CashOut = (x.OtherCharges + GetOrderDetailList()
            //                                    .Where(o => o.Order_Id == x.Id)
            //                                    .ToList().Sum(s => s.AmountPurchase)) - x.OrderDiscount ,
            //        Date = x.OrderDate
            //    });
            //});

            //cash Injection
            list.AddRange(GetCashInjectionList().Select(x => 
                new CashFlow()
                {
                    Particulars = x.Particulars,
                    CashIn = x.Amount,
                    Date = x.Date
                }
                ));

             list.AddRange(GetPaymentList().Where(x => x.Type == Helper.Constants.PaymentType.RECIEVE).ToList()
                 .Select(x =>
                   new CashFlow()
                   {
                       Particulars = string.Format("Recieve payment from {0}",x.Partner.Name),
                       CashIn = x.Amount,
                       Date = x.Date
                   }
                  ));

            ///handle refund
             list.AddRange(GetPaymentList().Where(x => x.Type == Helper.Constants.PaymentType.REFUND).ToList()
                 .Select(x =>
                   new CashFlow()
                   {
                       Particulars = string.Format("Refund to {0}", x.Partner.Name),
                       CashOut = x.Amount,
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