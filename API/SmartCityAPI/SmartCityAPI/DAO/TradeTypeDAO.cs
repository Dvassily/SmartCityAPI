using Model.Database;
using Model.DTO;
using MongoDB.Driver;
using SmartCityAPI.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public class TradeTypeDAO : ITradeTypeDAO
    {
        private readonly ITradeTypeContext _tradeTypeContext;

        public TradeTypeDAO(ITradeTypeContext tradeTypeContext)
        {
            _tradeTypeContext = tradeTypeContext;
        }

        public async Task<TradeTypeDTO> FindById(int id)
        {
            TradeType tradeType = await _tradeTypeContext.TradeTypes
                .Find(Builders<TradeType>.Filter.Eq(t => t.Id, id))
                .FirstOrDefaultAsync();

            if (tradeType == null)
            {
                return null;
            }

            return TradeTypeDTO.FromTradeType(tradeType);
        }
    }
}
