using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    
    public class Payment
    {
        public Guid Id { get; set; }
        [Display(Name="Payment Recieved")]
        public decimal Amount { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name="Customer")]
        public Guid Partner_Id { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [ForeignKey("Partner_Id")]
        public Partner Partner { get; set; }

        public List<PaymentDetail> PaymentDetails { get; set; }

        public decimal Balance {
            get {
                return 0;
            }
        }

        public bool Deleted { get; set; }

        public Guid CreatedBy { get; set; }
    }

    
}