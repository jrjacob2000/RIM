using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Guid Partner_Id { get; set; }
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        public Guid Order_Id { get; set; }
       
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [ForeignKey("Partner_Id")]
        [Display(Name = "Partner")]
        public Partner Partner { get; set; }

        [ForeignKey("Order_Id")]
        [Display(Name = "Order")]
        public Order Order { get; set; }

        [Display(Name = "Other Charges")]
        public decimal? OtherCharges { get; set; }

        [Display(Name = "Order Discount")]
        public decimal? OrderDiscount { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        [Display(Name = "Tax Rate (%)")]
        public decimal? TaxRate { get; set; }

        public Guid CreatedBy { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public List<PaymentDetail> PaymentDetails
        {
            get;
            set;
        }

        public decimal Amount
        {
            get
            {
                if (Order != null && Order.OrderDetails != null)
                {
                    var sum = Order.OrderDetails.Sum(x => x.AmountAfterTax);
                    return sum;
                }
                else
                    return 0;
            }
        }

        public decimal PaidAmount
        {
            get
            {

                if (PaymentDetails != null)
                {
                    var sum = PaymentDetails.Sum(x => x.Amount);
                    return sum;
                }
                else
                    return 0;
            }
        }

        public decimal Balance
        {
            get
            {
                return Amount - PaidAmount;
            }
        }
         
    }
}