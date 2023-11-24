using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext context)
        {
            _storeContext = context;  
        }

        public async Task<IReadOnlyList<Product>> GetProductAsycn()
        {
            return await _storeContext.Products
             .Include(p => p.ProductBrand)
             .Include(p => p.ProductType)
             .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsycn()
        {
            return await _storeContext.ProductBrand.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsycn(int id)
        {
            return await _storeContext.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsycn()
        {
            return await _storeContext.ProductType.ToListAsync();
        }
    }
}
