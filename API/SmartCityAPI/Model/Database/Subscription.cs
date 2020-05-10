using Model.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class Subscription
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("networkId")]
        public int NetworkId { get; set; }

        [BsonElement("state")]
        public string State { get; set; }
        
        public static Subscription FromDTO(SubscriptionDTO dto)
            => new Subscription
            {
                Id = dto.Id,
                UserId = dto.UserId,
                NetworkId = dto.NetworkId,
                State = dto.State
            };
    }
}
