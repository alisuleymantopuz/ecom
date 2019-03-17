using Newtonsoft.Json;
using System;

namespace Ecommerce.API.Models
{
    public class BasketItemAddRequest
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
