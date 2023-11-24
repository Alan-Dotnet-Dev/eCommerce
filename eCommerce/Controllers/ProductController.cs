using Core.Entities;
using Core.Interfaces;
using eCommerce.Core.Interfaces;
using eCommerce.Core.Spesification;
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
        private readonly IGenericRepository<Product> _repoproduct;
        private readonly IGenericRepository<ProductBrand> _repoBrand;
        private readonly IGenericRepository<ProductType> _repoType;

        public ProductController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo)
        {
            _repoproduct = productRepo;
            _repoBrand = brandRepo;
            _repoType = typeRepo;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var spec = new ProductwithTypeaAndBrands();

            var products = _repoproduct.ListAsync(spec);
           
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var spec = new ProductwithTypeaAndBrands(id);

            var product = _repoproduct.GetEntityWithSpec(spec);

            if (product == null)
            {
               
                return NotFound($"Product with Id {id} not found");
            }

            return Ok(product);
        }

        [HttpGet("brand")]
        public IActionResult GetProductBrand()
        {

            var products = _repoBrand.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("type")]
        public IActionResult GetProductType()
        {

            var products = _repoType.GetAllAsync();

            return Ok(products);
        }

    }

}
