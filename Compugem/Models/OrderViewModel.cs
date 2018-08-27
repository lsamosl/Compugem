using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Compugem.Models
{
    public class OrderViewModel
    {
        [Display(Name="Order")]
        public int Id { get; set; }
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        [Display(Name = "Date")]
        public DateTime OrderDate { get; set; }
    }
}