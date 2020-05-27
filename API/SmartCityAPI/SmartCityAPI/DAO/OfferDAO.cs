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
    public class OfferDAO : IOfferDAO
    {
        private readonly IOfferContext _context;
        private readonly ICounterDAO _counterDAO;

        public OfferDAO(IOfferContext context, ICounterDAO counterDAO)
        {
            _context = context;
            _counterDAO = counterDAO;
        }

        public async Task<List<OfferDTO>> FindOffersByTrade(int tradeId)
        {
            IEnumerable<Offer> offers = await _context.Offers.Find(Builders<Offer>.Filter.Eq(t => t.TradeId, tradeId)).ToListAsync();
            List<OfferDTO> result = new List<OfferDTO>();

            foreach (Offer offer in offers)
            {
                OfferDTO dto = OfferDTO.FromOffer(offer);

                result.Add(dto);
            }

            return result;
        }

        public async Task<OfferDTO> Insert(OfferDTO dto)
        {
            Counter counter = await _counterDAO.GetCountersAsync();
            int id = counter.Offers++;
            await _counterDAO.UpdateCountersAsync(counter);

            Offer offer = Offer.FromDTO(dto);
            offer.Id = id;

            await _context.Offers.InsertOneAsync(offer);

            return OfferDTO.FromOffer(offer);
        }

        public async Task<List<OfferDTO>> FindAll()
        {
            IEnumerable<Offer> offers = await _context.Offers.Find(_ => true).ToListAsync();
            List<OfferDTO> result = new List<OfferDTO>();

            foreach (Offer offer in offers)
            {
                OfferDTO dto = OfferDTO.FromOffer(offer);

                result.Add(dto);
            }

            return result;
        }
    }
}
