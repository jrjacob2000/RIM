using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreatedBy { get; set; }
    }
}