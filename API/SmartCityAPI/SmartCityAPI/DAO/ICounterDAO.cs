using Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface ICounterDAO
    {
        public Task<Counter> GetCountersAsync();
        public Task<bool> UpdateCountersAsync(Counter counter);
    }
}
