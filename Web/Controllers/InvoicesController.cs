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
    public class InvoicesController : InvoiceControllerBase
    {

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices
                .Include("Partner")
                .Where(x => x.CreatedBy == UserId && x.Type == Helper.Constants.InvoiceType.INVOICE)
                .ToList()
                .Select(s => new Invoice()
                {
                    Id = s.Id,
                    Partner_Id = s.Partner_Id,
                    InvoiceNumber = s.InvoiceNumber,
                    Order_Id = s.Order_Id,
                    InvoiceDate = s.InvoiceDate,
                    DueDate = s.DueDate,
                    OtherCharges = s.OtherCharges,
                    OrderDiscount = s.OrderDiscount,
                    TaxRate = s.TaxRate,
                    CreatedBy = s.CreatedBy,
                    Order = GetOrderById(s.Order_Id),
                    Partner = s.Partner,
                    PaymentDetails = db.PaymentDetails.Include("Payment").Where(p => p.Invoice_Id == s.Id && !p.Payment.Deleted).ToList(),
                    Credits = GetCreditByPartnerId(s.Partner.Id).ToList(),
                    Status = GetStatus(s)
                });


            return View(invoices);
        }
           
    }
}
