using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model.Product;
using VeggieApp.Server.Controllers.ProductController;
using VeggieApp.Server.Data;

namespace VeggieApp.Test.VeggieAppServer.Controller
{
    public class ProductControllerMockTest 
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductController _productController;
        public ProductControllerMockTest()
        {
            _context = TestDbContextFactory.Create("ProductTestDb");
            _productController = new ProductController(_context);
        }

        public static IEnumerable<object[]> GetIndexTestCases()
        {
            // Case 1: Products is null → NotFound
            var context1 = TestDbContextFactory.Create("Db1");
            context1.Products = null!;
            yield return new object[] { context1, typeof(NotFoundResult), null! };

            // Case 2: Products returns list → Ok
            var context2 = TestDbContextFactory.Create("Db2");
            context2.Products.AddRange(new List<Product>
            {
                new Product { product_id = 1, product_name = Faker.Name.FullName(),product_image="testimage",list_price = Faker.RandomNumber.Next() },
                new Product { product_id = 2, product_name = Faker.Name.FullName(),product_image="testimage",list_price = Faker.RandomNumber.Next() }
            });
            context2.SaveChanges();
            yield return new object[] { context2, typeof(OkObjectResult), 2 };

            // Case 3: Exception thrown → Ok with error message
            var context3 = TestDbContextFactory.Create("Db3");
            context3.Dispose(); // Simulate exception
            yield return new object[] { context3, typeof(OkObjectResult), "Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.\r\n          Object name: 'ApplicationDbContext'" };
        }
        [Theory]
        [MemberData(nameof(GetIndexTestCases))]
        public async Task Index_ReturnsExpectedResult(ApplicationDbContext context, Type expectedType, object expectedValue)
        {
            var controller = new ProductController(context);
            var result = await controller.Index();

            Assert.IsType(expectedType, result);

            if (result is OkObjectResult okResult)
            {
                if (expectedValue is int count)
                {
                    var products = Assert.IsAssignableFrom<List<Product>>(okResult.Value);
                    Assert.Equal(count, products.Count);
                }
            }
        }

        public static IEnumerable<object[]> GetAllCategoriesTestData()
        {
            // Case 1: Products is null → NotFound
            var context1 = TestDbContextFactory.Create("Db1");
            context1.Products = null!;
            yield return new object[] { context1, typeof(NotFoundResult), null! };

            // Case 2: Products returns list → Ok
            var context2 = TestDbContextFactory.Create("Db2");
            context2.Categories.AddRange(new List<Categories>
            {
                new Categories {category_name = "Fruits" },
                new Categories {category_name = "Fruits" }
            });
            context2.SaveChanges();
            yield return new object[] { context2, typeof(OkObjectResult), 2 };

            // Case 3: Exception thrown → Ok with error message
            var context3 = TestDbContextFactory.Create("Db3");
            context3.Dispose(); // Simulate exception
            yield return new object[] { context3, typeof(OkObjectResult), "Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.\r\n          Object name: 'ApplicationDbContext'" };
        }
        [Theory]
        [MemberData(nameof(GetAllCategoriesTestData))]
        public async Task GetAllCategoriesTest(ApplicationDbContext context, Type expectedType, object expectedValue)
        {
            var controller = new ProductController(context);
            var result = await controller.GetAllCategories();

            Assert.IsType(expectedType, result);

            if (result is OkObjectResult okResult)
            {
                if (expectedValue is int count)
                {
                    var category = Assert.IsAssignableFrom<List<Categories>>(okResult.Value);
                    Assert.Equal(count, category.Count);
                }
            }
        }
    }
}
