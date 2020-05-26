using Model.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class OfferDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("trade_id")]
        public int TradeId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        public static OfferDTO FromOffer(Offer offer)
            => new OfferDTO
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                ImageUrl = offer.ImageUrl
            };
    }
}
