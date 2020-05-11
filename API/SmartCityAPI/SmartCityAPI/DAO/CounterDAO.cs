using Model.Database;
using MongoDB.Driver;
using SmartCityAPI.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public class CounterDAO : ICounterDAO
    {
        private readonly ICounterContext _context;

        public CounterDAO(ICounterContext context)
        {
            _context = context;
        }

        public async Task<Counter> GetCountersAsync()
        {
            return await _context.Counters.Find(_ => true).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCountersAsync(Counter counter)
        {
            ReplaceOneResult result = await _context.Counters.ReplaceOneAsync(filter: c => true, replacement: counter);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
