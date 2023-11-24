using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsycn(int id);
        Task<IReadOnlyList<Product>> GetProductAsycn();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsycn();
        Task<IReadOnlyList<ProductType>> GetProductTypeAsycn();
    }
}
