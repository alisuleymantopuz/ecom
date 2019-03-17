using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;
using Ecommerce.UserActor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<BasketInfo> Get(string userId)
        {
            IUserActor actor = GetActor(userId);

            Dictionary<Guid, int> products = await actor.GetBasket();

            return new BasketInfo
            {
                UserId = userId,
                Items = products.Select(p => new BasketItemInfo
                {
                    ProductId = p.Key.ToString(),
                    Quantity = p.Value.ToString()
                }).ToArray()
            };
        }

        [HttpPost("{userId}")]
        public async Task Add(string userId, [FromBody]BasketItemAddRequest basketItemAddRequest)
        {
            IUserActor actor = GetActor(userId);

            await actor.AddToBasket(basketItemAddRequest.ProductId, basketItemAddRequest.Quantity);
        }

        [HttpDelete("{userId}")]
        public async Task Delete(string userId)
        {
            IUserActor actor = GetActor(userId);

            await actor.ClearBasket();
        }

        private IUserActor GetActor(string userId)
        {
            return ActorProxy.Create<IUserActor>(new ActorId(userId), new Uri("fabric:/Ecommerce/UserActorService"));
        }
    }
}
