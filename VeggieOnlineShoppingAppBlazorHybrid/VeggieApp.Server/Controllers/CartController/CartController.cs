using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VeggieApp.Model.Model.Cart;
using VeggieApp.Model.Model.Product;
using VeggieApp.Server.Data;

namespace VeggieApp.Server.Controllers.CartController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart([FromQuery] string userId)
        {
            var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();

            return Ok(cartItems);
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartViewModel addToCartViewModel)
        {
            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == addToCartViewModel.Productid && c.UserId == addToCartViewModel.UserId);
            var product = await _context.Products.FirstOrDefaultAsync(c=> c.product_id == addToCartViewModel.Productid);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            else if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _context.CartItems.Update(existingCartItem);
            }
            else
            {
                var productState = new ProductState() { IsAdded = true ,UserId = addToCartViewModel.UserId, ProductId = addToCartViewModel.Productid};
                var cartitem = new CartItem() { Product = product, ProductId = addToCartViewModel.Productid, UserId = addToCartViewModel.UserId, Quantity = 1  };
                _context.CartItems.Add(cartitem);
                _context.ProductStates.Add(productState);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("RemoveCart")]
        public async Task<IActionResult> RemoveCart([FromBody] AddToCartViewModel addToCartViewModel)
        {
            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == addToCartViewModel.Productid && c.UserId == addToCartViewModel.UserId);
            var product = await _context.Products.FirstOrDefaultAsync(c => c.product_id == addToCartViewModel.Productid);
            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity > 1)
                {
                    existingCartItem.Quantity -= 1;
                    _context.CartItems.Update(existingCartItem);
                }
                else
                {
                    _context.CartItems.Remove(existingCartItem);

                    var productState = await _context.ProductStates
                        .FirstOrDefaultAsync(ps => ps.ProductId == addToCartViewModel.Productid && ps.UserId == addToCartViewModel.UserId);

                    if (productState != null)
                    {
                        _context.ProductStates.Remove(productState);
                    }
                }
            }
                await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("DecreaceQuantity")]
        public async Task<IActionResult> DecreaceQuantity([FromBody] AddToCartViewModel addToCartViewModel)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == addToCartViewModel.Productid && c.UserId == addToCartViewModel.UserId);
            if (cartItem == null)
            {
                return NotFound("Product not found.");
            }else if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpPost("IncreaceQuantity")]
        public async Task<IActionResult> IncreaceQuantity([FromBody] AddToCartViewModel addToCartViewModel)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == addToCartViewModel.Productid && c.UserId == addToCartViewModel.UserId);
            if (cartItem == null)
            {
                return NotFound("Product not found.");
            }
            else
            {
                cartItem.Quantity += 1;
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost("GetProductState")]
        public async Task<IActionResult> GetProductState([FromBody] AddToCartViewModel addToCartViewModel)
        {
            var cartItem = await _context.ProductStates.FirstOrDefaultAsync(c => c.UserId == addToCartViewModel.UserId && c.ProductId == addToCartViewModel.Productid && c.IsAdded == true );
            if (cartItem == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(cartItem);
        }
    }
}
