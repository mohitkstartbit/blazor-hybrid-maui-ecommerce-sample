using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeggieApp.Model.Model.Pagination;
using VeggieApp.Model.Model.Product;
using VeggieApp.Server.Data;

namespace VeggieApp.Server.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  --------   Api for Fet Product List  -------------------------
      
        [HttpGet("GetProducts")]
        public async Task<IActionResult> Index()
        {
            try
            {

                if (_context.Products == null)
                {
                    return NotFound();
                }
                var listofproducts = await _context.Products.ToListAsync();

                return Ok(listofproducts);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // ---- Api for Get Product Details --------
        [HttpGet("ProductDetails/{Id:int}")]
        public async Task<IActionResult> Getproductdetails(int Id)
        {
            try
            {

                var product = await _context.Products.Where(x => x.product_id == Id).FirstOrDefaultAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Products by catagory

        [HttpGet("GetProductByCatagory/{Id:int}")]
        public async Task<IActionResult> GetProductbycatagory(int Id, int page)
        {
            try
            {

                /* var frcount = _context.Products.Count(x => x.category_id == 1);

                 var juicount = _context.Products.Count(x => x.category_id == 2);

                 var vegcount = _context.Products.Count(x => x.category_id == 3);

                 var dricount = _context.Products.Count(x => x.category_id == 4);*/
                /////////

                /* var listofveg = _context.Products.Where(x => x.category_id == Id).ToList();

                 return Ok(listofveg);*/
                var listofveg = await _context.Products.Include(x => x.Categories).Where(x => x.category_id == Id).ToListAsync();
                var totalItems = listofveg.Count();

                var size = 3;
                if (size > totalItems)
                {
                    page = 1;
                }

                var skip = (page * size) - size;
                if (skip > totalItems)
                {
                    page = 1;
                    skip = (page * size) - size;
                }
                var totalPages = Convert.ToInt32((double)(totalItems / size));
                var x = Math.Ceiling((decimal)totalItems / size);
                var result = listofveg.Skip(skip).Take(size);
                return Ok(new GenericPagination<Product>() { Response = result, Page = page, TotalCount = totalItems, TotalPages = Convert.ToInt32(x) });

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

       
    }
}
