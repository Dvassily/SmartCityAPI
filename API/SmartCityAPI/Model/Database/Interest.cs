using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class Interest
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("children")]
        public List<int> Children { get; set; }
    }
}
