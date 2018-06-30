using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Partner
    {
        public Guid Id { get; set; }
        public string Name{get;set;}
        public string ShippingAddress{get;set;}
        public string BillingAddress{get;set;}
        public string Email{get;set;}
        public string Phone{get;set;}
        public string Contact{get;set;}
        public Guid CreatedBy { get; set; }

        public List<Payment> Payments { get; set; }
        public List<Invoice> UnpaidInvoices { get; set; }
        public List<Credit> UnpaidCreditNotes { get; set; }

        public decimal Recievables {
            get {
                if (UnpaidInvoices == null)
                    return 0;
                else
                {
                    return UnpaidInvoices.Where(x => x.Type == Helper.Constants.InvoiceType.INVOICE).Sum(x => x.Balance);
                }
            }
        }

        public decimal Payables
        {
            get
            {
                if (UnpaidInvoices == null)
                    return 0;
                else
                {
                    return UnpaidInvoices.Where(x => x.Type == Helper.Constants.InvoiceType.BILL).Sum(x => x.Balance);
                }
            }
        }

 
        public decimal CustomerCredits
        {
            get {
                if (UnpaidCreditNotes == null)
                    return 0;
                else {
                    return  UnpaidCreditNotes.Where(x => x.Order.AdjustmentReason == "RETURN_CUSTOMER").Sum(x => x.Amount);

                }
            }
        }

        public decimal SupplierCredits
        {
            get {
                if (UnpaidCreditNotes == null)
                    return 0;
                else {
                    return UnpaidCreditNotes.Where(x => x.Order.AdjustmentReason == "RETURN_SUPPLIER").Sum(x => x.Amount); 
                }
            }
        }

    }
}