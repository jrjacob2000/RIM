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

    }
}