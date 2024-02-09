using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeggieApp.Model.Model;
using VeggieApp.Server.Data;

namespace VeggieApp.Server.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Api for Create Order
        [Route("CreateOrder")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel orders)
        {
            try
            {

                if (_context.Orders == null)
                {
                    return NotFound();
                }
                else
                {
                    var order = await _context.Orders.AddAsync(orders.Orders);
                    await _context.SaveChangesAsync();
                    foreach (var item in orders.OrderItems)
                    {
                        item.OrderId = order.Entity.OrderId;
                        await _context.OrderItems.AddAsync(item);
                    }
                    await _context.SaveChangesAsync();
                    return Ok("Order Created Succesfully");

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Api for Get Orders List
        [Route("GetOrders")]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {

                if (_context.Orders == null)
                {
                    return NotFound();
                }
                else
                {
                    var listofOrders = await _context.Orders.Include(s=>s.User).ToListAsync();
                    return Ok(listofOrders);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Api for get order details 

        [HttpGet("GetOrderDetailByOrderId/{Id:int}")]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            try
            {

                OrderViewModel ord = new OrderViewModel();

                var order = await _context.Orders.Include(x => x.User).Where(x => x.OrderId == Id).FirstOrDefaultAsync();

                if (order != null)
                {
                    var orderItems = await _context.OrderItems.Include(x => x.Products.Categories).Where(x => x.OrderId == order.OrderId).ToListAsync();
                    ord.Orders = order;
                    ord.OrderItems = orderItems;
                    return Ok(ord);
                }

                return Ok("Done");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Api for Get Order History by CustomerId
        [HttpGet("GetOrderHistoryByCustomerId/{Id}")]
        public async Task<IActionResult> GetOrderHistoryByCustomerId(string Id)
        {
            try
            {
                var test = await _context.Orders.Where(s => s.UserId == Id).Include(x => x.User).Include(s => s.Items).ToListAsync();

                return StatusCode(StatusCodes.Status200OK, test);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
