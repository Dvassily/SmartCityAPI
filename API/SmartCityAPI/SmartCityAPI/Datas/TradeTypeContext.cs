﻿using Microsoft.Extensions.Options;
using Model.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.Datas
{
    public class TradeTypeContext : ITradeTypeContext
    {
        private readonly IMongoDatabase _db;

        public TradeTypeContext(IOptions<DbSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);

            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<TradeType> TradeTypes => _db.GetCollection<TradeType>("trade_types");
    }
}
