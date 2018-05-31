using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class CreditInvoice
    {
        public Guid Id { get; set; }

        public Guid Credit_Id { get; set; }

        public Guid Invoice_Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid CreatedBy { get; set; }

        [ForeignKey("Invoice_Id")]
        public Invoice Invoice { get; set; }

        [ForeignKey("Credit_Id")]
        public Credit Credit { get; set; }
    }
}