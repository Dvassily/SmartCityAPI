using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class Counter
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("interests")]
        public int Interests { get; set; }

        [BsonElement("networks")]
        public int Networks { get; set; }

        [BsonElement("publications")]
        public int Publications { get; set; }

        [BsonElement("services")]
        public int Services { get; set; }

        [BsonElement("subscriptions")]
        public int Subscriptions { get; set; }

        [BsonElement("users")]
        public int Users { get; set; }

        [BsonElement("trades")]
        public int Trades { get; set; }

        [BsonElement("offers")]
        public int Offers { get; set; }
    }
}
