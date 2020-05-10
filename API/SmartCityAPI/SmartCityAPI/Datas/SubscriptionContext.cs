using Microsoft.Extensions.Options;
using Model.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.Datas
{
    public class SubscriptionContext : ISubscriptionContext
    {
        private readonly IMongoDatabase _db;

        public SubscriptionContext(IOptions<DbSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);

            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Subscription> Subscriptions => _db.GetCollection<Subscription>("subscriptions");
    }
}
