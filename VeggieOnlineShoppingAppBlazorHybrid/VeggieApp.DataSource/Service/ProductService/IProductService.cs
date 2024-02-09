using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model.Cart;
using VeggieApp.Model.Model.Product;

namespace VeggieApp.DataSource.Service.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task SetClient(HttpClient client);
        public void addcart(CartViewModel product);
        IEnumerable<CartViewModel> GetCart();
        public void ClearCart(string cartValue);
        CartViewModel IncreaseQuantity(int ProductId,string id);
        CartViewModel DecreaseQuantity(int ProductId,string id);
    }
}
