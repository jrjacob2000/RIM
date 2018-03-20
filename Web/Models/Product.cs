using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Starting Inventory")]
        public int StartingInventory { get; set; }
        [Display(Name = "ReOrder Point")]
        public int ReOrderPoint { get; set; }
        public string Unit { get; set; }
        public bool Taxable { get; set; }
        [Display(Name = "Category")]
        public Guid Category_Id { get; set; }
        [Display(Name = "Category")]
        [ForeignKey("Category_Id")]
        public Category Category { get; set; }
        public List<ProductPrice> ProductPrices { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public Guid CreatedBy { get; set; }

        [Display(Name = "Inventory on Hand")]
        public int InventoryOnHand {
            get {
                if (OrderDetails != null)
                {
                    var purchased = OrderDetails
                                    .Where(x => x.Order.ExpectedDate <= DateTime.Now && x.Order.OrderType == Web.Controllers.Helper.Constants.OrderType.PURCHASE)
                                    .Select(s => s.Quantity).Sum();
                    var sold = OrderDetails
                                .Where(x => x.Order.ExpectedDate <= DateTime.Now && x.Order.OrderType == Web.Controllers.Helper.Constants.OrderType.SALE)
                                .Select(s => s.Quantity).Sum();
                    var adjust = OrderDetails
                                .Where(x => x.Order.ExpectedDate <= DateTime.Now && x.Order.OrderType == Web.Controllers.Helper.Constants.OrderType.ADJUST)
                                .Select(s => s.Quantity).Sum();

                    var custReturn = OrderDetails
                                .Where(x => x.Order.ExpectedDate <= DateTime.Now && x.Order.OrderType == Web.Controllers.Helper.Constants.OrderType.CUSTOMER_RETURN)
                                .Select(s => s.Quantity).Sum();

                    var suppReturn = OrderDetails
                                .Where(x => x.Order.ExpectedDate <= DateTime.Now && x.Order.OrderType == Web.Controllers.Helper.Constants.OrderType.SUPPLIER_RETURN)
                                .Select(s => s.Quantity).Sum();

                    return (StartingInventory + purchased + custReturn) - (sold + adjust + suppReturn);
                }
                else
                    return 0;
            }
        }

        [Display(Name = "Inventory to Come")]
        public int InventoryToCome
        {
            get
            {
                if (OrderDetails != null)
                {
                    var purchased = OrderDetails
                                    .Where(x => x.Order.ExpectedDate > DateTime.Now && x.Order.OrderType == Web.Controllers.Helper.Constants.OrderType.PURCHASE)
                                    .Select(s => s.Quantity).Sum();                  

                    return  purchased;
                }
                else
                    return 0;
            }
        }

        [Display(Name = "Inventory To Go")]
        public int InventoryToGo
        {
            get
            {
                if (OrderDetails != null)
                {   
                    var sold = OrderDetails
                                .Where(x => x.Order.ExpectedDate > DateTime.Now && x.Order.OrderType == Web.Controllers.Helper.Constants.OrderType.SALE)
                                .Select(s => s.Quantity).Sum();
                    return sold;
                }
                else
                    return 0;
            }
        }
        [Display(Name="To Order")]
        public bool ToOrder
        {
            get {
                return InventoryOnHand <= ReOrderPoint;
            }
        }

        [Display(Name = "Inventory Value")]
        public decimal InventoryValue
        {
            get {

                if (ProductPrices != null)
                {
                    var currentPurchasePrice = ProductPrices.OrderByDescending(o => o.EffectiveFromDate).FirstOrDefault();
                    return InventoryOnHand * (currentPurchasePrice == null ? 0 : currentPurchasePrice.PurchasePrice);
                }
                else
                    return 0;
            }
        }
    }
}