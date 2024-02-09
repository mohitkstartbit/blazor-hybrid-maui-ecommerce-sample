using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeggieApp.Model.Model.Cart
{
    public class CartViewModel
    {
        public int Productid { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int ListPrice { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public string UserId { get; set; }

    }
}
