using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using eCommerce.Core.Entities;
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
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            _repoproduct = productRepo;
            _repoBrand = brandRepo;
            _repoType = typeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProducts()
        {
            var spec = new ProductwithTypeaAndBrands();

            var products =await _repoproduct.ListAsync(spec);
            var res = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);
           
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var spec = new ProductwithTypeaAndBrands(id);

            var product = await _repoproduct.GetEntityWithSpec(spec);

            if (product == null)
            {
               
                return NotFound($"Product with Id {id} not found");
            }

            var res = _mapper.Map<Product,ProductDTO>(product);

            return Ok(res);
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
