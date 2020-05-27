using Model.Database;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface IOfferDAO
    {
        Task<List<OfferDTO>> FindOffersByTrade(int tradeId);

        Task<OfferDTO> Insert(OfferDTO offer);

        Task<List<OfferDTO>> FindAll();
    }
}
