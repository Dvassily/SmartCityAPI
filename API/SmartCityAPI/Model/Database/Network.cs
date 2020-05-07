using Model.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class Network
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("author_id")]
        public int AuthorId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("image_url")]
        public string ImageUrl { get; set; }

        public static Network FromDTO(NetworkDTO dto)
        {
            return new Network
            {
                Id = dto.Id,
                AuthorId = dto.AuthorId,
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            };
        }
    }
}
