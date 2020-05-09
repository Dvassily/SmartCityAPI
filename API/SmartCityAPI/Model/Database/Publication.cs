using Model.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class Publication
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("network_id")]
        public int NetworkId { get; set; }

        [BsonElement("author_id")]
        public int AuthorId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        public static Publication FromDTO(PublicationDTO publication)
            => new Publication
            {
                Id = publication.Id,
                NetworkId = publication.NetworkId,
                AuthorId = publication.AuthorId,
                Date = publication.Date,
                Content = publication.Content
            };
    }
}
