using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class ProductPrice
    {
        public Guid Id { get; set; }

        [Display(Name = "Effective From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveFromDate { get; set; }

        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Sales Price")]
        public decimal SelesPrice { get; set; }

        public Guid Product_Id { get; set; }

        [ForeignKey("Product_Id")]
        [Display(Name="Product")]
        public Product Product { get; set; }

        public Guid CreatedBy { get; set; }

    }
}