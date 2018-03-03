using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class OrderDetailView
    {
        OrderDetail _model;
        public OrderDetailView(OrderDetail model)
        {
            _model = model;
        }

        [Key]
        public Guid Id { get { return _model.Id; } }
        public Guid Order_Id { get { return _model.Order_Id; } }
        public Order Order { get { return _model.Order; } }
        public Product Product { get { return _model.Product; } }
        public int Quantity { get { return _model.Quantity; } }
        public decimal UnitDiscount { get { return _model.UnitDiscount; } }
        public decimal UnitPrice { get; set; }
    }
}