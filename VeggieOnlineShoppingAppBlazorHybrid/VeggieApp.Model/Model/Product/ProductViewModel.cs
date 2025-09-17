using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeggieApp.Model.Model.Product
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsAddedToCart { get; set; }
    }
}
