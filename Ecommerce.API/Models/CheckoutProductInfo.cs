using Newtonsoft.Json;
using System;

namespace Ecommerce.API.Models
{
    public class CheckoutProductInfo
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }
        [JsonProperty("productName")]
        public string Name { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
