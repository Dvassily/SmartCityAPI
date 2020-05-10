using Model.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class NetworkDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("author_id")]
        public int AuthorId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("subscriptions")]
        public IEnumerable<SubscriptionDTO> subscriptions { get; set; } = new List<SubscriptionDTO>();

        public static NetworkDTO FromNetwork(Network network)
            => new NetworkDTO
        {
            Id = network.Id,
            AuthorId = network.AuthorId,
            Name = network.Name,
            Description = network.Description,
            ImageUrl = network.ImageUrl,
        };
    }
}
