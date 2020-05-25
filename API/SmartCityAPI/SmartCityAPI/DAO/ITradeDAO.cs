using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface ITradeDAO
    {
        Task<TradeDTO> FindById(int id);

        Task<TradeDTO> Insert(TradeDTO trade);
    }
}
