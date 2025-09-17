using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model;
using VeggieApp.Model.Model.Cart;
using VeggieApp.Model.Model.Product;

namespace VeggieApp.DataSource.Service.CartService
{
    public interface ICartService
    {
        Task<Result> AddToCart(int ProductId, string UserId);
        Task<Result> RemoveCartItem(int ProductId, string UserId);

        Task<List<CartViewModel>> GetAllCartItems(string userId);
        // Task<List<CartItem>> GetCartItems(string UserId);
        Task<Result> IncreaseQuantity(int ProductId, string UserId);
        Task<Result> DecreaseQuantity(int ProductId, string UserId);
        Task<Dictionary<int, bool>> GetUserProductStates(int productId, string UserId);
    }
}
