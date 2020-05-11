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
    public class PublicationDAO : IPublicationDAO
    {
        private readonly IPublicationContext _context;
        private readonly ICounterDAO _counterDAO;

        public PublicationDAO(IPublicationContext context, ICounterDAO counterDAO)
        {
            _context = context;
            this._counterDAO = counterDAO;
        }

        public async Task<IEnumerable<PublicationDTO>> FindAll()
        {
            IEnumerable<Publication> publications = await _context.Publications.Find(_ => true).ToListAsync();
            List<PublicationDTO> result = new List<PublicationDTO>();

            foreach (Publication publication in publications)
            {
                result.Add(PublicationDTO.FromPublication(publication));
            }

            return result;
        }

        public async Task<PublicationDTO> FindById(int id)
        {
            Publication publication = await _context.Publications
                .Find(Builders<Publication>.Filter.Eq(u => u.Id, id))
                .FirstOrDefaultAsync();

            if (publication == null)
            {
                return null;
            }

            return PublicationDTO.FromPublication(publication);
        }

        public async Task<IEnumerable<PublicationDTO>> FindByNetworkId(int id)
        {
            List<Publication> publications = await _context.Publications
                .Find(Builders<Publication>.Filter.Eq(p => p.NetworkId, id))
                .ToListAsync();

            List<PublicationDTO> result = new List<PublicationDTO>();

            foreach (Publication publication in publications)
            {
                result.Add(PublicationDTO.FromPublication(publication));
            }

            return result;
        }

        public async Task<PublicationDTO> Insert(PublicationDTO dto)
        {
            Counter counter = await _counterDAO.GetCountersAsync();
            int id = counter.Publications++;
            await _counterDAO.UpdateCountersAsync(counter);

            Publication publication = Publication.FromDTO(dto);
            publication.Id = id;

            await _context.Publications.InsertOneAsync(publication);

            return FindById(publication.Id).Result;
        }
    }
}
