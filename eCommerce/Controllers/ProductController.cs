using eCommerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace eCommerce.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductController(StoreContext context)
        {
            _storeContext = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            // Retrieve a list of products from the StoreContext
            var products = _storeContext.Products.ToList();

            // You can customize the response based on your requirements
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            // Retrieve a product by id from the StoreContext
            var product = _storeContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                // Return a 404 Not Found if the product is not found
                return NotFound($"Product with Id {id} not found");
            }

            // You can customize the response based on your requirements
            return Ok(product);
        }

    }

}
