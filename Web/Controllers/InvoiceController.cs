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
    public class InvoiceController : ControllerBase
    {

        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index(string SearchString, string GenerateInvoice)
        {
           
            ViewBag.Orders = GetOrderList().Where(x => x.OrderType == Helper.Constants.OrderType.SALE).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.OrderNumber }).ToList();
            
            var order = GetOrderList().Where(x => x.OrderNumber == SearchString).FirstOrDefault();
            ViewData["currentFilter"] = SearchString;

            var list = new List<OrderDetail>();

            if (order != null)
            {
                if (!string.IsNullOrEmpty(GenerateInvoice))
                    GetInvoice(order.Id);

                list = db.OrderDetails
                    .Include("Product")
                    .Include("ProductPrice")
                    .Include("Order.Partner")
                    .Where(x => x.Order_Id == order.Id && x.CreatedBy == UserId)
                    .OrderBy(o => o.Product.Name).ToList();
            }
            ViewBag.Total = list.Sum(x => x.AmountAfterTax);

            var setting = GetSetting();
            if (setting != null)
            {
                ViewBag.BusinessName = setting.BusinessName;
                ViewBag.BillingAddress = setting.BillingAddress;
                ViewBag.ContactNumber = setting.BusinessContactNumber;
            }

            return View(list);
        }

  
        public void GetInvoice(Guid Id)
        {
            var order = GetOrderById(Id); //db.Orders.Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (order.InvoiceNumber == null)
            {
                var setting = GetSetting();
                //var invNumber = db.Orders.Max(x => x.InvoiceNumber);


                if (setting != null && !string.IsNullOrEmpty(setting.InvoiceNumber))
                {
                    //invNumber = setting.InvoiceNumber + 1;
                    order.InvoiceNumber = string.Format("{0}-{1}",setting.InvoicePrefix, setting.InvoiceNumber);

                    var length = setting.InvoiceNumber.Length;
                    var newValue = int.Parse(setting.InvoiceNumber) + 1;

                    setting.InvoiceNumber = newValue.ToString().PadLeft(length, '0'); ;

                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                    ViewBag.Message = "Please fill autogenerate invoice in Application setting";
               
            }
        }       
    }
}