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
    public class InterestDAO : IInterestDAO
    {
        private readonly IInterestContext _context;

        public InterestDAO(IInterestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InterestDTO>> FindAll()
        {
            IEnumerable<Interest> interests = await _context.Interests.Find(_ => true).ToListAsync();
            List<InterestDTO> result = new List<InterestDTO>();

            foreach (Interest interest in interests)
            {
                result.Add(InterestDTO.FromInterest(interest));
            }

            return result;
        }

        public async Task<InterestDTO> FindById(int id)
        {
            Interest Interest = await _context.Interests
                .Find(Builders<Interest>.Filter.Eq(s => s.Id, id))
                .FirstOrDefaultAsync();

            if (Interest == null)
            {
                return null;
            }

            return InterestDTO.FromInterest(Interest);
        }
    }
}
