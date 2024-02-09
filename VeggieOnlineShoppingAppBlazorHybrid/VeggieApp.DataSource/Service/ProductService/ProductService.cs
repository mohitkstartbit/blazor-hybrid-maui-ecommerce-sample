using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.DataSource.Data;
using VeggieApp.Model.Model;
using VeggieApp.Model.Model.Cart;
using VeggieApp.Model.Model.Product;

namespace VeggieApp.DataSource.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _Client;
       // private ProductStore _productsStore;
        public ProductService(HttpClient Client)
        {
            _Client = Client;
            //_productsStore = productsStore;

        }

        // public static List<Product> products1 = new List<Product>();
        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                var response = await _Client.GetFromJsonAsync<IEnumerable<Product>>("api/Product/GetProducts");
                var hello =
                    "";
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Product>();
            }
        }

        public Task SetClient(HttpClient client)
        {
            throw new NotImplementedException();
        }

        //Get All Cart Items
        public IEnumerable<CartViewModel> GetCart()
        {
            var result = ProductStore.CartNewList.ToList();
            return result;
        }

        //Add Product to cart
        public void addcart(CartViewModel product)
        {
            /*_productsStore.CartList.Add(product);*/
            ProductStore.CartNewList.Add(product);
        }
        //Increate qunatity 
        public CartViewModel IncreaseQuantity(int ProductId, string id)
        {
            bool isfound = false;

            var CartItems = ProductStore.CartNewList.FirstOrDefault(s => s.UserId == id && s.Productid == ProductId);
            if (CartItems != null)
            {
                CartItems.Quantity += 1;
                CartItems.TotalPrice = CartItems.Quantity * CartItems.ListPrice;
                return CartItems;
            }
            return null;
            //foreach (var items in CartItems)
            //{
            //    if (items.Productid == ProductId)
            //    {
            //        isfound = true;
            //        items.Quantity += 1;
            //        items.TotalPrice = items.Quantity * items.ListPrice;
            //    }

            //}
            //return null;
        }
        public CartViewModel DecreaseQuantity(int ProductId, string id)
        {
            var CartItems = ProductStore.CartNewList.FirstOrDefault(s => s.UserId == id && s.Productid == ProductId);
            if (CartItems != null)
            {
                CartItems.Quantity -= 1;
                CartItems.TotalPrice = CartItems.Quantity * CartItems.ListPrice;
                if (CartItems.Quantity == 0)
                {
                    ProductStore.CartNewList.Remove(CartItems);
                }
                return CartItems;
            }
            return null;
            //bool isfound = false;
            //var CartItems = _productsStore.CartNewList.ToList();
            //foreach (var items in CartItems)
            //{
            //    if (items.Productid == ProductId)
            //    {
            //        isfound = true;
            //        items.Quantity -= 1;
            //        items.TotalPrice = items.Quantity * items.ListPrice;
            //    }
            //    if (items.Quantity == 0)
            //    {
            //        _productsStore.CartNewList.Remove(items);

            //        break;
            //    }
            //}
            //return null;
        }

        //clear cart
        public void ClearCart(string userId)
        {
            ProductStore.CartNewList.RemoveAll(s => s.UserId == userId);
        }



    }
}
