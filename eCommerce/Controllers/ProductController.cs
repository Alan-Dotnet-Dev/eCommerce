using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace eCommerce.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            
            var products = _repo.GetProductAsycn();
           
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            
            var product = _repo.GetProductByIdAsycn(id);

            if (product == null)
            {
               
                return NotFound($"Product with Id {id} not found");
            }

            return Ok(product);
        }

        [HttpGet("brand")]
        public IActionResult GetProductBrand()
        {

            var products = _repo.GetProductBrandAsycn();

            return Ok(products);
        }

        [HttpGet("type")]
        public IActionResult GetProductType()
        {

            var products = _repo.GetProductTypeAsycn();

            return Ok(products);
        }

    }

}
