using Ecommerce.API.Models;
using Ecommerce.CheckoutService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private static readonly Random rnd = new Random(DateTime.UtcNow.Second);

        [HttpGet("{userId}")]
        public async Task<CheckoutSummaryInfo> Get(string userId)
        {
            CheckoutSummary summary = await GetCheckoutService().Checkout(userId);

            return ToCheckoutSummaryInfo(summary);
        }

        [HttpGet("history/{userId}")]
        public async Task<IEnumerable<CheckoutSummaryInfo>> GetHistory(string userId)
        {
            IEnumerable<CheckoutSummary> history = await GetCheckoutService().GetOrderHistory(userId);

            return history.Select(ToCheckoutSummaryInfo);
        }

        private CheckoutSummaryInfo ToCheckoutSummaryInfo(CheckoutSummary model)
        {
            return new CheckoutSummaryInfo
            {
                Products = model.Products.Select(x => new CheckoutProductInfo()
                {
                    Name = x.Product.Name,
                    Price = x.Price,
                    ProductId = x.Product.Id,
                    Quantity = x.Quantity
                }).ToList(),
                Date = model.Date,
                TotalPrice = model.TotalPrice
            };
        }

        private ICheckoutService GetCheckoutService()
        {
            long key = LongRandom();

            return ServiceProxy.Create<ICheckoutService>(
                new Uri("fabric:/Ecommerce/Ecommerce.CheckoutService"),
                new ServicePartitionKey(key));
        }

        private long LongRandom()
        {
            byte[] buff = new byte[8];
            rnd.NextBytes(buff);
            long longRand = BitConverter.ToInt64(buff, 0);
            return longRand;
        }

    }
}
