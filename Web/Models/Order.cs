using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Order
    {
        public Guid Id {get;set;}

        [Required]
        [Display(Name = "Order Number")]
        public string OrderNumber {get;set;}

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate {get;set;}

        [Display(Name = "Expected Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpectedDate {get;set;}

        [Required]
        [Display(Name = "Order Type")]
        public string OrderType {get;set;}

        [Display(Name = "Partner")]
        public Guid Partner_Id { get; set; }

        [ForeignKey("Partner_Id")]
        [Display(Name = "Partner")]
        public Partner Partner {get;set;}
        
        [Display(Name = "Other Charges")]
        public decimal OtherCharges {get;set;}

        [Display(Name = "Order Discount")]
        public decimal OrderDiscount {get;set;}

        [DisplayFormat(DataFormatString = "{0:P2}")]
        [Display(Name = "Tax Rate")]
        public decimal TaxRate {get;set;}

        [Display(Name = "Order Notes")]
        public string OrderNotes {get;set;}

        public List<OrderDetail> OrderDetails { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        public Guid CreatedBy { get; set; }

        public decimal Amount {
            get {
                if (OrderDetails != null)
                {
                    var sum = OrderDetails.Sum(x => x.AmountAfterTax);
                    return sum;
                }
                else
                    return 0;
            }
        }

        public List<PaymentDetail> PaymentDetails
        {
            get;
            set;
        }

        public decimal PaidAmount {
            get {

                if (PaymentDetails != null)
                {
                    var sum = PaymentDetails.Sum(x => x.Amount);
                    return sum;
                }
                else
                    return 0;
            }
        }

        public decimal Balance {
            get {
                return Amount - PaidAmount;
            }
        }
    }
}