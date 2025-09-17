using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.DataSource.Service.ProductService;
using VeggieApp.Model.Model;
using VeggieApp.Model.Model.Cart;
using VeggieApp.Model.Model.Product;

namespace VeggieApp.DataSource.Service.CartService
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly IProductService _productService;
        public CartService(HttpClient httpClient, IProductService productService) 
        {
            _httpClient = httpClient;
            this._productService = productService;
        }

        public async Task<List<CartViewModel>> GetAllCartItems(string userId)
        {
            try
            {
                var cartItems = await _httpClient.GetFromJsonAsync<IEnumerable<CartItem>>($"api/Cart/GetCart?userId={Uri.EscapeDataString(userId)}");
                var products = await _productService.GetAll();

                var cartData = cartItems.Select(cartItem =>
                {
                    var product = products.FirstOrDefault(p => p.product_id == cartItem.ProductId);
                    if (product != null)
                    {
                        return new CartViewModel
                        {
                            Productid = product.product_id,
                            ProductName = product.product_name,
                            ProductImage = product.product_image,
                            ListPrice = product.list_price,
                            Quantity = cartItem.Quantity,
                        };
                    }
                    return null;
                }).Where(c => c != null).ToList();

                return cartData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<CartViewModel>();
            }
        }
        //public async Task<List<CartItem>> GetCartItems(string userId)
        //{
        //    try
        //    {
        //        var cartItems = await _httpClient.GetFromJsonAsync<List<CartItem>>($"api/Cart/GetCart?userId={Uri.EscapeDataString(userId)}");
        //        return cartItems ?? new List<CartItem>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching cart items: {ex.Message}");
        //        return new List<CartItem>();
        //    }
        //}

        public async Task<Result> AddToCart(int productId,string userId)
        {
            var requiredPara = new AddToCartViewModel() { Productid = productId, UserId = userId};
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cart/AddToCart", requiredPara);
                if (response.IsSuccessStatusCode)
                {
                    return new Result { Successful = true };
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return new Result { Successful = false, Error = error };
                }
            }
            catch (Exception ex)
            {
                return new Result { Successful = false, Error = ex.Message };
            }
        }

        public async Task<Result> RemoveCartItem(int productId, string userId)
        {
            var requiredPara = new AddToCartViewModel() { Productid = productId, UserId = userId };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cart/RemoveCart", requiredPara);
                if (response.IsSuccessStatusCode)
                {
                    return new Result { Successful = true };
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return new Result { Successful = false, Error = error };
                }
            }
            catch (Exception ex)
            {
                return new Result { Successful = false, Error = ex.Message };
            }
        }

        public async Task<Result> IncreaseQuantity(int productId, string UserId)
        {
            var requiredPara = new AddToCartViewModel() { Productid = productId, UserId = UserId };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cart/IncreaceQuantity", requiredPara);
                if (response.IsSuccessStatusCode)
                {
                    return new Result { Successful = true };
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return new Result { Successful = false, Error = error };
                }
            }
            catch (Exception ex)
            {
                return new Result { Successful = false, Error = ex.Message };
            }
        }
        public async Task<Result> DecreaseQuantity(int productId, string UserId)
        {
            var requiredPara = new AddToCartViewModel() { Productid = productId, UserId = UserId };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cart/DecreaceQuantity", requiredPara);
                if (response.IsSuccessStatusCode)
                {
                    return new Result { Successful = true };
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return new Result { Successful = false, Error = error };
                }
            }
            catch (Exception ex)
            {
                return new Result { Successful = false, Error = ex.Message };
            }
        }
        public async Task<bool> ProductState(int productId, string UserId)
        {
            var requiredPara = new AddToCartViewModel() { Productid = productId, UserId = UserId };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cart/GetProductState", requiredPara);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Dictionary<int, bool>> GetUserProductStates(int productId, string UserId)
        {
            var requiredPara = new AddToCartViewModel() { Productid = productId, UserId = UserId };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cart/GetProductState", requiredPara);

                if (response.IsSuccessStatusCode)
                {
                    var productState = await response.Content.ReadFromJsonAsync<ProductStateDto>();
                    return productState != null
                        ? new Dictionary<int, bool> { { productState.ProductId, productState.IsAdded } }
                        : new Dictionary<int, bool>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    // Optionally log the error
                    return new Dictionary<int, bool>();
                }

            }
            catch (Exception ex)
            {
                return new Dictionary<int, bool>(); 
            }
        }

    }
}
