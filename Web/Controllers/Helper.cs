using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
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
                new SelectListItem(){Value = OrderType.QOUTE, Text="Qoute"},
                new SelectListItem(){Value = OrderType.ADJUST, Text="Adjust"},
                new SelectListItem(){Value = OrderType.CUSTOMER_RETURN, Text="Return by Customer"},
                new SelectListItem(){Value = OrderType.SUPPLIER_RETURN, Text="Return to Supplier"}
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
                public const string CUSTOMER_RETURN = "CUSTOMER_RETURN";
                public const string SUPPLIER_RETURN = "SUPPLIER_RETURN";
            }

            public static class Unit
            {
                public const string PIECES = "PIECES";
                public const string SETS = "SETS";
                public const string BOX = "BOX";
                public const string BOTTLE = "BOTTLE";
                public const string SACKS = "BOTTLE";
            }

        }
    }
}