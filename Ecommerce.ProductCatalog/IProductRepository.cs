﻿using Ecommerce.ProductCatalog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ProductCatalog
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProduct(Guid key);

        Task AddProduct(Product product);
    }
}
