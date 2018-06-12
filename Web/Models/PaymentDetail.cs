using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class PaymentDetail
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        //public Guid Order_Id { get; set; }
        public Guid? Invoice_Id { get; set; }

        public Guid? Credit_Id { get; set; }

        //[ForeignKey("Order_Id")]
        //public Order Order { get; set; }

        [ForeignKey("Invoice_Id")]
        public Invoice Invoice { get; set; }

        [ForeignKey("Credit_Id")]
        public Credit Credit { get; set; }

        [Column("Payment_Id")]
        public Guid Payment_Id { get; set; }

        [ForeignKey("Payment_Id")]
        public Payment Payment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name="Reference#")]
        public string Reference { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public Guid CreatedBy { get; set; }

    }
}