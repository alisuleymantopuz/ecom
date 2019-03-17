using Newtonsoft.Json;
using System;

namespace Ecommerce.API.Models
{
    public class ProductInfo
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("isAvailabile")]
        public bool IsAvailabile { get; set; }

        [JsonProperty("availability")]
        public long Availability { get; set; }
    }
}
