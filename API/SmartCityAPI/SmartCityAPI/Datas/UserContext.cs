using Microsoft.Extensions.Options;
using Model;
using Model.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.Datas
{
    public class UserContext : IUserContext
    {
        private readonly IMongoDatabase _db;

        public UserContext(IOptions<DbSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);

            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<User> Users => _db.GetCollection<User>("users");
    }
}
