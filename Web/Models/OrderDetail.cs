using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class OrderDetail
    {
        public Guid Id {get;set;}
        public Guid Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public Order Order {get;set;}

        [Display(Name="Product")]
        public Guid Product_Id { get; set; }

        [Display(Name = "Product")]
        [ForeignKey("Product_Id")]
        public Product Product {get;set;}

        public int Quantity {get;set;}

        [Display(Name = "Unit Discount")]
        public decimal UnitDiscount {get;set;}
        
        public Guid? ProductPrice_Id { get; set; }

        [Display(Name = "Unit Price")]
        [ForeignKey("ProductPrice_Id")]
        public ProductPrice ProductPrice { get; set; }

        public Guid CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        [Display(Name = "Amount Before Tax")]
        public decimal AmountBeforeTax { 
            get 
            {
                if (ProductPrice != null)
                    return (Quantity * ((Order.OrderType == Helper.Constants.OrderType.SALE
                                        || Order.AdjustmentReason == "RETURN_CUSTOMER")
                                        ? ProductPrice.SelesPrice : ProductPrice.PurchasePrice) - UnitDiscount);
                else
                    return 0;
            }
        }

        [DisplayFormat(DataFormatString = "{0:00.0000}")]
        [Display(Name = "Tax")]
        public decimal Tax
        {
            get
            {
                if (Product != null)
                {
                    if (Product.Taxable)
                        return AmountBeforeTax * Order.TaxRate;
                    else
                        return 0;
                }
                return 0;
            }
        }

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        [Display(Name = "Amount After Tax")]
        public decimal AmountAfterTax
        {
            get {
                return AmountBeforeTax + Tax;
            }
        }

        [Display(Name = "Amount Purchase")]
        public decimal AmountPurchase
        {
            get
            {
                if (ProductPrice != null)
                    return (Quantity * ProductPrice.PurchasePrice) - UnitDiscount;
                else
                    return 0;
            }
        }
    }
}