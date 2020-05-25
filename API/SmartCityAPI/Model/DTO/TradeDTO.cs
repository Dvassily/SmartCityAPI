using Model.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class TradeDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        public static TradeDTO FromTrade(Trade trade)
            => new TradeDTO
            {
                Id = trade.Id,
                Name = trade.Name,
                Description = trade.Description,
                ImageUrl = trade.ImageUrl,
                Address = trade.Address,
                Town = trade.Town
            };
    }
}
