using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Adjust
    {
        public Guid Id {get;set;}

        [Required]
        [Display(Name = "Adjust Number")]
        [Index(IsUnique = true)]
        [MaxLength(250)]
        public string OrderNumber {get;set;}

        [Required]
        [Display(Name = "Adjust Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate {get;set;}

        [Required]
        [Display(Name = "Order Type")]
        public string OrderType {get;set;}
        
        [Display(Name = "Order Notes")]
        public string OrderNotes {get;set;}

        public List<OrderDetail> OrderDetails { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        public Guid CreatedBy { get; set; }
              

       
    }
}