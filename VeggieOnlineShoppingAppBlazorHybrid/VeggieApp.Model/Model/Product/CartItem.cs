using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeggieApp.Model.Model.Product
{
    public class CartItem
    {
        public int Id { get; set; }
        public required string UserId { get; set; }  // Foreign key to IdentityUser
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public required Product Product { get; set; }
    }
}
