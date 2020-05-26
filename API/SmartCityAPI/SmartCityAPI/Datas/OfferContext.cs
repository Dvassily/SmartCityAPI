using Microsoft.Extensions.Options;
using Model.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.Datas
{
    public class OfferContext : IOfferContext
    {
        private readonly IMongoDatabase _db;

        public OfferContext(IOptions<DbSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);

            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Offer> Offers => _db.GetCollection<Offer>("offers");
    }
}
