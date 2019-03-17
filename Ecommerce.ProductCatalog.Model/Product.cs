using System;

namespace Ecommerce.ProductCatalog.Model
{
    public class Product
    { 
        public Guid Id { get; set; } 

        public string Name { get; set; } 

        public double Price { get; set; } 

        public long Availability { get; set; }
    }
}
