﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Ecommerce.UserActor.Interfaces;

namespace Ecommerce.UserActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class UserActor : Actor, IUserActor
    {
        /// <summary>
        /// Initializes a new instance of UserActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public UserActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {

        }

        public async Task AddToBasket(Guid productId, int quantity)
        {
            await StateManager.AddOrUpdateStateAsync(productId.ToString(), quantity, (id, oldQuantity) => oldQuantity + quantity);
        }

        public async Task ClearBasket()
        {
            IEnumerable<string> productIDs = await StateManager.GetStateNamesAsync();

            foreach (string productId in productIDs)
            {
                await StateManager.RemoveStateAsync(productId);
            }
        }

        public async Task<Dictionary<Guid, int>> GetBasket()
        {
            var result = new Dictionary<Guid, int>();

            IEnumerable<string> productIDs = await StateManager.GetStateNamesAsync();

            foreach (string productId in productIDs)
            {
                int quantity = await StateManager.GetStateAsync<int>(productId);

                result[new Guid(productId)] = quantity;
            }

            return result;
        }
    }
}
