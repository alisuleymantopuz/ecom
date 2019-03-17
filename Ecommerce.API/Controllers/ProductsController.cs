using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;
using Ecommerce.ProductCatalog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCatalogService _productCatalogService;

        public ProductsController()
        {
            _productCatalogService = ServiceProxy.Create<IProductCatalogService>(new Uri("fabric:/Ecommerce/Ecommerce.ProductCatalog"), new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<ProductInfo>> Get()
        {
            IEnumerable<Product> products = await _productCatalogService.GetAllProducts();

            return products.Select(x => new ProductInfo()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                IsAvailabile = x.Availability > 0,
                Availability = x.Availability
            });
        }

        [HttpPost]
        public async Task Post([FromBody]ProductInfo productInfo)
        {
            var product = new Product()
            {
                Id = productInfo.Id,
                Name = productInfo.Name,
                Price = productInfo.Price,
                Availability = productInfo.Availability
            };

            await _productCatalogService.AddProduct(product);
        }
    }
}
