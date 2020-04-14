using Model;
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
    public class ServiceDAO : IServiceDAO
    {
        private readonly IServiceContext _context;

        public ServiceDAO(IServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceDTO>> FindAll()
        {
            IEnumerable<Service> services = await _context.Services.Find(_ => true).ToListAsync();
            List<ServiceDTO> result = new List<ServiceDTO>();

            foreach (Service service in services)
            {
                result.Add(ServiceDTO.FromService(service));
            }

            return result;
        }

        public async Task<ServiceDTO> FindById(int id)
        {
            Service service = await _context.Services
                .Find(Builders<Service>.Filter.Eq(s => s.Id, id))
                .FirstOrDefaultAsync();

            if (service == null)
            {
                return null;
            }

            return ServiceDTO.FromService(service);
        }
    }
}
