using Microsoft.Extensions.Options;
using Model.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.Datas
{
    public class CounterContext : ICounterContext
    {
        private readonly IMongoDatabase _db;

        public CounterContext(IOptions<DbSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);

            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Counter> Counters => _db.GetCollection<Counter>("counters");
    }
}
