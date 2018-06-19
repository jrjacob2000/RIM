using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Controllers;

namespace Web.Models
{
    public class SalesAndPurchase
    {
        public OrderReport Sales { get; set; }
        public OrderReport Purchase { get; set; }
    }
    public class OrderReport
    {
        public OrderReport(List<OrderDetail> orderDetails)
        {
            _orderDetails = orderDetails;
        }

        List<OrderDetail> _orderDetails;
        private List<OrderDetail> OrderDetails
        {
            get {
                return _orderDetails;
            }
        }


        public decimal Quantity
        {
            get {

                if (OrderDetails != null && OrderDetails.Count() > 0)
                {
                    var qty = OrderDetails.Where(x => (x.Order.OrderType == Helper.Constants.OrderType.SALE
                        || x.Order.OrderType == Helper.Constants.OrderType.PURCHASE)
                        && x.Order.Invoices.Count() > 0).Sum(s => s.Quantity);

                    return qty - QuantityReturn;
                }
                else
                    return 0;
            }
        }
        public decimal Amount
        {
            get {
                if (OrderDetails != null && OrderDetails.Count() > 0)
                {
                    var amount = OrderDetails.Sum(x => x.Order.OrderType == Helper.Constants.OrderType.SALE ? x.AmountBeforeTax : 
                        x.Order.OrderType == Helper.Constants.OrderType.PURCHASE ? x.AmountPurchase : 0);

                    var custReturn = OrderDetails.Sum(x => (x.Order.OrderType == Helper.Constants.OrderType.ADJUST && x.Order.AdjustmentReason == "RETURN_CUSTOMER") ? x.AmountBeforeTax :
                        (x.Order.OrderType == Helper.Constants.OrderType.ADJUST && x.Order.AdjustmentReason == "RETURN_SUPPLIER") ? x.AmountPurchase : 0);

                    
                    return amount - custReturn ;
                }
                else
                    return 0;
            }
        }
        public decimal Tax
        {
            get
            {
                if (OrderDetails != null && OrderDetails.Count() > 0)
                    return OrderDetails.Where(x => x.Order.OrderType == Helper.Constants.OrderType.SALE ||
                        x.Order.OrderType == Helper.Constants.OrderType.PURCHASE).Sum(x => x.Tax);
                else
                    return 0;
            }
        }
        public decimal QuantityReturn
        {
            get
            {
                if (OrderDetails != null && OrderDetails.Count() > 0)
                    return OrderDetails.Where(x => x.Order.OrderType == Helper.Constants.OrderType.ADJUST
                         && (x.Order.AdjustmentReason == "RETURN_CUSTOMER"
                         || x.Order.AdjustmentReason == "RETURN_SUPPLIER")).Sum(x => x.Quantity);
                else
                    return 0;
            }
        }
        public decimal Discount
        {
            get
            {
                if (OrderDetails != null &&  OrderDetails.Count() > 0)
                {
                    var unitDiscount = OrderDetails.Sum(x => x.UnitDiscount);
                    var orderDiscount = OrderDetails.Select(x => x.Order).Distinct().Sum(x => x.OrderDiscount);
                    return unitDiscount + orderDiscount;
                }
                else
                    return 0;
            }
        }
        public decimal OtherCharges
        {
            get
            {
                if (OrderDetails != null && OrderDetails.Count() > 0)
                {
                    var orders = OrderDetails.Select(x => x.Order).Distinct().ToList();
                    return orders.Sum(x => x.OtherCharges);
                }
                else
                    return 0;
            }
        }
    }

    public enum OrderType
    { 
         PURCHASE,
         SALE,
         ESTIMATE,
         ADJUST
    }
}