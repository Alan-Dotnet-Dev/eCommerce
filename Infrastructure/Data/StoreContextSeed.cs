using Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {

            string basePath = @"C:\Users\user\source\repos\Alan-Dotnet-Dev\eCommerce\Infrastructure\Data\SeedData\";
            string filePathBrand = Path.Combine(basePath, "brands.json");
            string filePathType = Path.Combine(basePath, "types.json");
            string filePathProduct = Path.Combine(basePath, "products.json");

            if (!context.ProductBrand.Any())
            {
                var brandData = File.ReadAllText(filePathBrand);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                context.ProductBrand.AddRange(brands);
            }

            if (!context.ProductType.Any())
            {
                var typeData = File.ReadAllText(filePathType);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                context.ProductType.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var productData = File.ReadAllText(filePathProduct);
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                context.Products.AddRange(products);
            }

            if (context.ChangeTracker.HasChanges())
                await context.SaveChangesAsync();
        }
    }
}
