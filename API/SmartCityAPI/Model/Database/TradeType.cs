using Model.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class TradeType
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }


        [BsonElement("name")]
        public string Name { get; set; }


        [BsonElement("children")]
        public List<int> Children { get; set; } = new List<int>();


        [BsonElement("tradeId")]
        public int TradeId { get; set; }

        public static TradeType FromDTO(TradeTypeDTO dto)
        {
            return new TradeType
            {
                Id = dto.Id,
                Name = dto.Name,
                Children = dto.Children,
                TradeId = dto.TradeId
            };
        }
    }
}
