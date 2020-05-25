using Model.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class Trade
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("owner_id")]
        public int OwnerId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("image_url")]
        public string ImageUrl { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("town")]
        public string Town { get; set; }

        public static Trade FromDTO(TradeDTO trade)
            => new Trade
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
