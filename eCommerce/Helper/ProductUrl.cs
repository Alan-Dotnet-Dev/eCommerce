using AutoMapper;
using Core.Entities;
using eCommerce.Core.Entities;

namespace eCommerce.API.Helper
{
    public class ProductUrl : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _config;
        public ProductUrl(IConfiguration config)
        {
            _config = config;
        }

        

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
