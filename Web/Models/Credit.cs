﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Credit
    {
        public Guid Id { get; set; }
        public Guid Partner_Id { get; set; }

        [Display(Name = "Credit Note Number")]
        public string CreditNumber { get; set; }

        public Guid Order_Id { get; set; }
       
        [Display(Name = "Credit Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreditDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        //[ForeignKey("Partner_Id")]
        //[Display(Name = "Partner")]
        //public Partner Partner { get; set; }

        [ForeignKey("Order_Id")]
        [Display(Name = "Order")]
        public Order Order { get; set; }

        public Guid? Invoice_Id { get; set; }

        [ForeignKey("Invoice_Id")]
        public Invoice Invoice { get; set; }

        public List<PaymentDetail> PaymentDetails
        {
            get;
            set;
        }

        public Guid CreatedBy { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public decimal Amount
        {
            get {
                if (Order != null && Order.OrderDetails != null)
                    return Order.OrderDetails.Sum(x => x.AmountAfterTax);
                else
                    return 0;
            }
        }
         
    }
}