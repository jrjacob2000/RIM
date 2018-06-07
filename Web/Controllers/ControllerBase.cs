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
using Microsoft.AspNet.Identity;


namespace Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        
        public ControllerBase()
        {
           
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var setting = GetSetting(new Guid(requestContext.HttpContext.User.Identity.GetUserId()));

                if (setting != null && !string.IsNullOrEmpty(setting.BusinessName))
                    ViewBag.ApplicationName = setting.BusinessName;
                else
                    ViewBag.ApplicationName = "Your Business Name";
            }
            else
                ViewBag.ApplicationName = "JRIM";
        }

        protected UserSetting GetSetting()
        {
            return GetSetting(UserId);
        }


        protected UserSetting GetSetting(Guid userId)
        {
            
                var setting = db.UserSettings.Find(userId);
                return setting;
            
        }

        protected Guid UserId
        {
            get
            {                
                return new Guid(User.Identity.GetUserId());
            }
        }

        //Order
        public Order GetOrderById(Guid Id, bool detach = false)
        {
            var order = db.Orders
                .Include("OrderDetails")
                .Include("OrderDetails.Product")
                .Include("OrderDetails.ProductPrice")
                .Include("Partner").
                Where(x => x.Id == Id && x.CreatedBy == UserId).FirstOrDefault();

            order.OrderDetails = order.OrderDetails.OrderBy(x => x.Product.Name).ToList();

            if (order == null)
                return null;

            if (detach)
                db.Entry(order).State = EntityState.Detached;

            return order;
        }

        public IQueryable<Order> GetOrderList()
        {
            return db.Orders
                .Include("OrderDetails")
                .Include("OrderDetails.ProductPrice")
                .Include("Partner").Where(x => x.CreatedBy == UserId);
        }

        //Order Detail
        public OrderDetail GetOrderDetailById(Guid Id, bool detach = false)
        {            
            if (detach)
            {
                var orderdetail = db.OrderDetails
                .Where(x => x.Id == Id && x.CreatedBy == UserId).FirstOrDefault();
                db.Entry(orderdetail).State = EntityState.Detached;
                return orderdetail;
            }
            else
            {
                var orderdetail = db.OrderDetails
                   .Include("Order")
                   .Include("Product")
                   .Include("ProductPrice")
                   .Where(x => x.Id == Id && x.CreatedBy == UserId).FirstOrDefault();
                return orderdetail;
            }          

        }

        public IQueryable<OrderDetail> GetOrderDetailList()
        {
            return db.OrderDetails
                .Include("ProductPrice")
                .Include("Product")
                .Include("Order")
                .Where(x =>  x.CreatedBy == UserId);
        }

        public IQueryable<OrderDetail> GetOrderDetailList(Guid productId)
        {
            return db.OrderDetails
                .Include("ProductPrice")
                .Include("Product")
                .Include("Order")
                .Where(x => x.CreatedBy == UserId && x.Product_Id == productId);
        }

        //Product
        public Product GetProductById(Guid Id, bool detach = false)
        {
            var prod = db.ProductItems
                .Include("Category")
                .Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (prod == null)
                return null;

            if (detach)
                db.Entry(prod).State = EntityState.Detached;

            return prod;
        }

        public IQueryable<Product> GetProductList()
        {
            return db.ProductItems.Include("Category")
                .OrderBy(o => o.Name)
                .Where(x => x.CreatedBy == UserId);
        }



        //Pricelist
        public ProductPrice GetPriceByProduct(Guid productId, bool detach = false)
        {
            var price = db.PriceItems.Where(x => x.CreatedBy == UserId && x.Product_Id == productId).OrderByDescending(x => x.EffectiveFromDate).FirstOrDefault();

            if (price == null)
                return null;

            if (detach)
                db.Entry(price).State = EntityState.Detached;
            return price;
        }

        public ProductPrice GetPriceById(Guid Id, bool detach = false)
        {
            var price = db.PriceItems
                .Include("Product")
                .Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (price == null)
                return null;

            if (detach)
                db.Entry(price).State = EntityState.Detached;

            return price;

        }

        public IQueryable<ProductPrice> GetPriceList()
        {
            return db.PriceItems
                .Include("Product")
                .Where(x => x.CreatedBy == UserId);
        }


        //Partner
        public Partner GetPartnerById(Guid Id, bool detach = false)
        {
            var partner = db.Partners.Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (partner == null)
                return null;

            if (detach)
                db.Entry(partner).State = EntityState.Detached;
            return partner;
        }

        public IQueryable<Partner> GetPartnerList()
        {
            return db.Partners.Where(x => x.CreatedBy == UserId );
        }

        //PaymentDetail
        public PaymentDetail GetPaymentDetailById(Guid Id, bool detach = false)
        {
            var paymentDetail = db.PaymentDetails.Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (paymentDetail == null)
                return null;

            if (detach)
                db.Entry(paymentDetail).State = EntityState.Detached;

            return paymentDetail;
        }

        public IQueryable<PaymentDetail> GetPaymentDetailList()
        {
            return db.PaymentDetails
                .Include("Invoice")
                .Include("Invoice.Order")
                .Include("Payment")
                .Where(x => x.CreatedBy == UserId);
        }

        //Payment
        public Payment GetPaymentById(Guid Id, bool detach = false)
        {
            var payment = db.Payments              
                .Include("Partner").Where(x => x.CreatedBy == UserId && !x.Deleted && x.Id == Id).FirstOrDefault();

            if (payment == null)
                return null;

            if (detach)
                db.Entry(payment).State = EntityState.Detached;

            return payment;
        }

        public IQueryable<Payment> GetPaymentList()
        {
            return db.Payments
                .Include("Partner")
                .Where(x => x.CreatedBy == UserId && !x.Deleted);
        }


        public CashInjection GetCashInjectionById(Guid Id, bool detach = false)
        {
            var cashInjection = db.CashInjections.Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (cashInjection == null)
                return null;

            if (detach)
                db.Entry(cashInjection).State = EntityState.Detached;

            return cashInjection;
        }

        public IQueryable<CashInjection> GetCashInjectionList()
        {
            return db.CashInjections.Where(x => x.CreatedBy == UserId );
        }

        //category
        public Category GetCategoryById(Guid Id, bool detach = false)
        {
            var category = db.CategoryItems.Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (category == null)
                return null;

            if(detach)
                db.Entry(category).State = EntityState.Detached;
            return category;
        }

        public IQueryable<Category> GetCategoryList()
        {
            return db.CategoryItems.Where(x => x.CreatedBy == UserId ).OrderBy(o => o.Name);
        }

        //Invoice
        public Invoice GetInvoiceById(Guid Id, bool detach = false)
        {
            var invoice = db.Invoices
                   .Include("Order")
                   .Include("Order.OrderDetails")
                   .Include("Order.OrderDetails.Product")
                   .Include("Order.OrderDetails.ProductPrice")
                   .Include("Partner")
                   .Where(x => x.CreatedBy == UserId && x.Id == Id).FirstOrDefault();

            if (detach)
                db.Entry(invoice).State = EntityState.Detached;

            return invoice;
        }

        public IQueryable<Invoice> GetInvoiceByOrderId(Guid OrderId)
        {
            var invoices = db.Invoices                  
                   .Where(x => x.CreatedBy == UserId && x.Order_Id == OrderId);
                        
            return invoices;
        }


        public IQueryable<Credit> GetCreditByInvoiceId(Guid InvoiceId)
        {
            var credits = db.Credits
                   .Include("Order")
                   .Include("Order.OrderDetails")
                   .Include("Order.OrderDetails.Product")
                   .Include("Order.OrderDetails.ProductPrice")
                   .Where(x => x.CreatedBy == UserId && x.Invoice_Id == InvoiceId);

            return credits;
        }

        public IQueryable<Credit> GetCreditByPartnerId(Guid PartnerId)
        {
            var credits = db.Credits
                .Include("Order")
                .Include("Order.OrderDetails")
                .Include("Order.OrderDetails.Product")
                .Include("Order.OrderDetails.ProductPrice")
                .Where(x => x.CreatedBy == UserId 
                       && x.Partner_Id == PartnerId);

            return credits;
        }
    }
}