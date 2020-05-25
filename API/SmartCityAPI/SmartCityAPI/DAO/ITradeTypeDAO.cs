using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface ITradeTypeDAO
    {
        Task<TradeTypeDTO> FindById(int id);
    }
}
