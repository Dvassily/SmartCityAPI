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
    public class TradeDAO : ITradeDAO
    {
        private readonly ITradeContext _context;
        private readonly ICounterDAO _counterDAO;

        public TradeDAO(ITradeContext context, ICounterDAO counterDAO)
        {
            _context = context;
            _counterDAO = counterDAO;
        }

        public async Task<TradeDTO> FindById(int id)
        {
            Trade trade = await _context.Trades
                .Find(Builders<Trade>.Filter.Eq(t => t.Id, id))
                .FirstOrDefaultAsync();

            if (trade == null)
            {
                return null;
            }

            TradeDTO dto = TradeDTO.FromTrade(trade);

            return dto;
        }

        public async Task<TradeDTO> Insert(TradeDTO dto)
        {
            Counter counter = await _counterDAO.GetCountersAsync();
            int id = counter.Trades++;
            await _counterDAO.UpdateCountersAsync(counter);

            Trade trade = Trade.FromDTO(dto);
            trade.Id = id;

            await _context.Trades.InsertOneAsync(trade);

            return TradeDTO.FromTrade(trade);
        }
    }
}
