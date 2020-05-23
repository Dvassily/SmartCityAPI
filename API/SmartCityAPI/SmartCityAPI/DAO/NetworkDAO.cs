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
        private readonly ICounterDAO _counterDAO;
        private readonly ISubscriptionDAO _subscriptionDAO;
        private readonly IUserDAO _userDAO;

        public NetworkDAO(INetworkContext context, ICounterDAO counterDAO, ISubscriptionDAO subscriptionDAO, IUserDAO userDAO)
        {
            _context = context;
            _counterDAO = counterDAO;
            _subscriptionDAO = subscriptionDAO;
            _userDAO = userDAO;
        }

        public async Task<IEnumerable<NetworkDTO>> FindAll()
        {
            IEnumerable<Network> networks = await _context.Networks.Find(_ => true).ToListAsync();
            List<NetworkDTO> result = new List<NetworkDTO>();

            foreach (Network network in networks)
            {
                NetworkDTO dto = NetworkDTO.FromNetwork(network);
                dto = await joinWithSubscriptionAsync(dto);
                result.Add(dto);
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

            NetworkDTO dto = NetworkDTO.FromNetwork(network);
            dto = await joinWithSubscriptionAsync(dto);
            return dto;
        }

        public async Task<NetworkDTO> Insert(NetworkDTO dto)
        {
            Counter counter = await _counterDAO.GetCountersAsync();
            int id = counter.Networks++;
            await _counterDAO.UpdateCountersAsync(counter);

            Network network = Network.FromDTO(dto);
            network.Id = id;

            await _context.Networks.InsertOneAsync(network);

            return NetworkDTO.FromNetwork(network);
        }

        private async Task<NetworkDTO> joinWithSubscriptionAsync(NetworkDTO dto)
        {
            IEnumerable<SubscriptionDTO> subscriptions = await _subscriptionDAO.findByNetworkIdAsync(dto.Id);

            foreach (SubscriptionDTO subscription in subscriptions)
            {
                UserDTO user = await _userDAO.FindById(subscription.UserId);
                subscription.UserName = user.FirstName + " " + user.LastName;
            }

            dto.subscriptions = subscriptions;

            return dto;
        }
    }
}
