using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Web.Models
{

    public class CashFlow
    {
        public string Particulars { get; set; }
        [Display(Name="Cash In")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal CashIn { get; set; }
        [Display(Name = "Cash Out")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal CashOut { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
    }
}