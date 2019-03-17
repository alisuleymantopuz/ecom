using Newtonsoft.Json;

namespace Ecommerce.API.Models
{
    public class BasketInfo
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("items")]
        public BasketItemInfo[] Items { get; set; }
    }


    public class BasketItemInfo
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }
    }
}
