using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc.Html;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Web
{
    public static class Extensions
    {
        /// <summary>
        /// Used to determine the direction of the sort identifier used when filtering lists
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="sortOrder">the current sort order being used on the page</param>
        /// <param name="field">the field that we are attaching this sort identifier to</param>
        /// <returns>MvcHtmlString used to indicate the sort order of the field</returns>
        public static IHtmlString SortIdentifier(this HtmlHelper htmlHelper, string sortOrder, string field)
        {
            if (string.IsNullOrEmpty(sortOrder) || (sortOrder.Trim() != field && sortOrder.Replace("_desc", "").Trim() != field)) return null;

            string glyph = "glyphicon glyphicon-chevron-up";
            if (sortOrder.ToLower().Contains("desc"))
            {
                glyph = "glyphicon glyphicon-chevron-down";
            }

            var span = new TagBuilder("span");
            span.Attributes["class"] = glyph;

            return MvcHtmlString.Create(span.ToString());
        }

        /// <summary>
        /// Converts a NameValueCollection into a RouteValueDictionary containing all of the elements in the collection, and optionally appends
        /// {newKey: newValue} if they are not null
        /// </summary>
        /// <param name="collection">NameValue collection to convert into a RouteValueDictionary</param>
        /// <param name="newKey">the name of a key to add to the RouteValueDictionary</param>
        /// <param name="newValue">the value associated with newKey to add to the RouteValueDictionary</param>
        /// <returns>A RouteValueDictionary containing all of the keys in collection, as well as {newKey: newValue} if they are not null</returns>
        public static RouteValueDictionary ToRouteValueDictionary(this NameValueCollection collection, string newKey, string newValue)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.AllKeys)
            {
                if (key == null) continue;
                if (routeValueDictionary.ContainsKey(key))
                    routeValueDictionary.Remove(key);

                routeValueDictionary.Add(key, collection[key]);
            }
            if (string.IsNullOrEmpty(newValue))
            {
                routeValueDictionary.Remove(newKey);
            }
            else
            {
                if (routeValueDictionary.ContainsKey(newKey))
                    routeValueDictionary.Remove(newKey);

                routeValueDictionary.Add(newKey, newValue);
            }
            return routeValueDictionary;
        }
    }


    public static class Helper
    {
        public static class Constants
        {
            public static string DefaulApplicationName = "RIM";

            public static List<SelectListItem> OrderTypeList()
            {
                return new List<SelectListItem>(){
                new SelectListItem(){Value = OrderType.PURCHASE, Text="Purchase"},
                new SelectListItem(){Value = OrderType.SALE, Text="Sale"},
                //new SelectListItem(){Value = OrderType.QOUTE, Text="Qoute"},
                new SelectListItem(){Value = OrderType.ADJUST, Text="Adjust"},
                //new SelectListItem(){Value = OrderType.CUSTOMER_RETURN, Text="Return by Customer"}
                };
            }

            public static List<SelectListItem> AdustmentReasonList()
            {
                return new List<SelectListItem>(){
                new SelectListItem(){Value = "DAMAGE_LOST", Text="Damage and Lost"},
                new SelectListItem(){Value = "RETURN_CUSTOMER", Text="Return by Customer"},
                new SelectListItem(){Value = "RETURN_SUPPLIER", Text="Return to Supplier"}
                };
            }

            public static List<SelectListItem> UnitList()
            {
                return new List<SelectListItem>(){
                new SelectListItem(){Value = Unit.PIECES, Text="PIECES"},
                new SelectListItem(){Value = Unit.SETS, Text="SETS"},
                new SelectListItem(){Value = Unit.BOX, Text="BOX"},
                new SelectListItem(){Value = Unit.BOTTLE, Text="BOTTLE"}

                };
            }

            public static class OrderType
            {
                public const string PURCHASE = "PURCHASE";
                public const string SALE = "SALE";
                public const string QOUTE = "QOUTE";
                public const string ADJUST = "ADJUST";

            }

            public static class Unit
            {
                public const string PIECES = "PIECES";
                public const string SETS = "SETS";
                public const string BOX = "BOX";
                public const string BOTTLE = "BOTTLE";
                public const string SACKS = "BOTTLE";
            }

            public static class PaymentType
            {
                public const string REFUND = "REFUND";
                public const string RECIEVE = "RECIEVE";
                public const string BILL = "BILL";
            }

            public static class InvoiceStatus
            {
                public const string PAID = "PAID";
                public const string PARTIALPAID = "PARTIAL PAID";
                public const string DRAFT = "DRAFT";
                public const string OVERDUE = "OVERDUE";
            }
            public static class CreditNotesStatus
            {
                public const string PAID = "PAID";
                //public const string PARTIALPAID = "PARTIAL PAID";
                public const string DRAFT = "DRAFT";
                public const string OVERDUE = "OVERDUE";
            }
            public static class InvoiceType
            {
                public const string INVOICE = "INVOICE";
                public const string BILL = "BILL";
                public const string CustomerCredit = "CUST_CREDIT";
                public const string SupplierCredit = "SUPP_CREDIT";
            }


        }

        public static string StatusDisplay(Web.Models.Invoice inv)
        {
            if (inv.Balance <= 0)
                return Helper.Constants.InvoiceStatus.PAID;
            else if (inv.DueDate < DateTime.Now)
                return Helper.Constants.InvoiceStatus.OVERDUE;
            else if (inv.PaidAmount > 0)
                return Helper.Constants.InvoiceStatus.PARTIALPAID;
            else
                return Helper.Constants.InvoiceStatus.DRAFT;

        }

        public enum OrderStatus
        {            
            //PO
            Issued = 1,
            Received = 2,
            //SO
            Confirmed = 3,
            Closed = 4,
            //common
            Draft = 0,
            Cancelled = 5
        }

    }
}