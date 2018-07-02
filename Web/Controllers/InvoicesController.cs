using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using PagedList;
using Web.Models;
using MvcBreadCrumbs;

namespace Web.Controllers
{
    public class InvoicesController : InvoiceControllerBase
    {

        // GET: Invoices
        [BreadCrumb(Clear =true,Label ="Invoices")]
        public ActionResult Index(string sortOrder, int page = 1, int pageSize = 10)
        {
            var query = db.Invoices
                .Include("Partner")
                .Where(x => x.CreatedBy == UserId && x.Type == Helper.Constants.InvoiceType.INVOICE)
                .OrderByDescending(o => o.InvoiceDate);

            var list = query.ToPagedList(page, pageSize);

            list.ToList().ForEach(s =>
                {
                    s.Order = GetOrderById(s.Order_Id);
                    s.Partner = s.Partner;
                    s.PaymentDetails = db.PaymentDetails.Include("Payment").Where(p => p.Invoice_Id == s.Id && !p.Payment.Deleted).ToList();
                    s.Credits = GetCreditByPartnerId(s.Partner.Id).ToList();
                });


            return View(list);
        }
           
    }
}
