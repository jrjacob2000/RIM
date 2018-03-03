﻿using System;
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
                
                if (OrderDetails.Count() > 0)
                    return OrderDetails.Sum(s => s.Quantity);
                else
                    return 0;
            }
        }
        public decimal Amount
        {
            get {
                if (OrderDetails.Count() > 0)
                    return OrderDetails.Sum(x => x.Order.OrderType == Helper.Constants.OrderType.SALE ? x.AmountBeforeTax : x.AmountPurchase);
                else
                    return 0;
            }
        }
        public decimal Tax
        {
            get
            {
                if (OrderDetails.Count() > 0)
                    return OrderDetails.Sum(x => x.Tax);
                else
                    return 0;
            }
        }
        public decimal QuantityReturn
        {
            get
            {
                if (OrderDetails.Count() > 0)
                    return OrderDetails.Where(x => x.Quantity < 0).Sum(x => x.Quantity);
                else
                    return 0;
            }
        }
        public decimal Discount
        {
            get
            {
                if (OrderDetails.Count() > 0)
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
                if (OrderDetails.Count() > 0)
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