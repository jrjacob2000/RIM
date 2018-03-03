using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class UserSetting
    {
        [Key]
        public Guid UserId { get; set; }
        [Display(Name="Business Name")]
        public string BusinessName { get; set; }
        [Display(Name = "Prefix")]
        public string InvoicePrefix { get; set; }
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }
        [Display(Name = "Prefix")]
        public string PurchasePrefix { get; set; }
        [Display(Name = "Purchase Order Number")]
        public string PurchaseNumber { get; set; }
        [Display(Name = "Prefix")]
        public string SalesPrefix { get; set; }
        [Display(Name = "Sale Order Number")]
        public string SalesNumber { get; set; }

        public bool InvoiceAuto
        {
            get { return !string.IsNullOrEmpty(InvoiceNumber); }
        }

        public bool PurchaseAuto
        {
            get { return !string.IsNullOrEmpty(PurchaseNumber); }
        }

        public bool SalesAuto
        {
            get { return !string.IsNullOrEmpty(SalesNumber); }
        }
    }
}