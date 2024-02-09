using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeggieApp.Model.Model.Authentication
{
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [DataType(DataType.Password)]
        public string CustomerPassword { get; set; }

    }
}
