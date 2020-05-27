using Model.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class Offer
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("trade_id")]
        public int TradeId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("image_url")]
        public string ImageUrl { get; set; }

        [BsonElement("target")]
        public List<int> Target { get; set; } = new List<int>();

        public static Offer FromDTO(OfferDTO offer)
            => new Offer
            {
                Id = offer.Id,
                TradeId = offer.TradeId,
                Title = offer.Title,
                Description = offer.Description,
                ImageUrl = offer.ImageUrl,
                Target = offer.Target
            };
    }
}
