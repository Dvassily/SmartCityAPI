using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Model.Database;
using Model.DTO;
using MongoDB.Driver;
using Protocol;
using SmartCityAPI.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public class NetworkDAO : INetworkDAO
    {
        private readonly INetworkContext _context;

        public NetworkDAO(INetworkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NetworkDTO>> FindAll()
        {
            IEnumerable<Network> networks = await _context.Networks.Find(_ => true).ToListAsync();
            List<NetworkDTO> result = new List<NetworkDTO>();

            foreach (Network network in networks)
            {
                result.Add(NetworkDTO.FromNetwork(network));
            }

            return result;
        }

        public async Task<NetworkDTO> FindById(int id)
        {
            Network network = await _context.Networks
              .Find(Builders<Network>.Filter.Eq(u => u.Id, id))
              .FirstOrDefaultAsync();

            if (network == null)
            {
                return null;
            }

            return NetworkDTO.FromNetwork(network);
        }

        public async Task Insert(NetworkDTO dto)
        {
            Network network = Network.FromDTO(dto);
            network.Id = (await FindAll()).Count();

            await _context.Networks.InsertOneAsync(network);
        }
    }
}
