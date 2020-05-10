using Model.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class SubscriptionDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("networkId")]
        public int NetworkId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }


        public static SubscriptionDTO FromSubscription(Subscription subscription)
            => new SubscriptionDTO
        {
            Id = subscription.Id,
            UserId = subscription.UserId,
            NetworkId = subscription.NetworkId,
            State = subscription.State
        };
    }
}
