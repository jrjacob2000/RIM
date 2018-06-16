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
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }
        [Display(Name = "Contact Number")]
        public string BusinessContactNumber { get; set; }
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

        [Display(Name = "Prefix")]
        public string AdjustPrefix { get; set; }
        [Display(Name = "Adjust Number")]
        public string AdjustNumber { get; set; }

        [Display(Name = "Prefix")]
        public string BillPrefix { get; set; }
        [Display(Name = "Bill Number")]
        public string BillNumber { get; set; }

        [Display(Name = "Prefix")]
        public string CreditNotePrefix { get; set; }
        [Display(Name = "Credit Note Number")]
        public string CreditNoteNumber { get; set; }


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

        public bool AdjustAuto
        {
            get { return !string.IsNullOrEmpty(AdjustNumber); }
        }

        public bool BillAuto
        {
            get { return !string.IsNullOrEmpty(BillNumber); }
        }

        public bool CreditNoteAuto
        {
            get { return !string.IsNullOrEmpty(CreditNoteNumber); }
        }
    }
}