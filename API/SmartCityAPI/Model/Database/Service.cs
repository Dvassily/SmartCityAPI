﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Model.Database
{
    public class Service
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("image_url")]
        public string ImageUrl { get; set; }

        [BsonElement("api_url")]
        public string APIUrl { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("service_type")]
        public string ServiceType { get; set; }
    }
}
