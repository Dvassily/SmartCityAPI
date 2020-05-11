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
    public class SubscriptionDAO : ISubscriptionDAO
    {
        private readonly ISubscriptionContext _context;
        private readonly ICounterDAO _counterDAO;

        public SubscriptionDAO(ISubscriptionContext context, ICounterDAO counterDAO)
        {
            _context = context;
            _counterDAO = counterDAO;
        }

        public async Task<List<SubscriptionDTO>> findAllAsync()
        {
            IEnumerable<Subscription> subscriptions = await _context.Subscriptions.Find(_ => true).ToListAsync();
            List<SubscriptionDTO> result = new List<SubscriptionDTO>();

            foreach (Subscription subscription in subscriptions)
            {
                result.Add(SubscriptionDTO.FromSubscription(subscription));
            }

            return result;
        }

        public async Task<List<SubscriptionDTO>> findByNetworkIdAsync(int networkId)
        {
            List<Subscription> subscriptions = await _context.Subscriptions
                .Find(Builders<Subscription>.Filter.Eq(s => s.NetworkId, networkId))
                .ToListAsync();
            List<SubscriptionDTO> result = new List<SubscriptionDTO>();
            foreach (Subscription subscription in subscriptions)
            {
                result.Add(SubscriptionDTO.FromSubscription(subscription));
            }

            return result;
        }

        public async Task<List<SubscriptionDTO>> findByUserIdAsync(int userId)
        {
            List<Subscription> subscriptions = await _context.Subscriptions
                .Find(Builders<Subscription>.Filter.Eq(s => s.UserId, userId))
                .ToListAsync();
            List<SubscriptionDTO> result = new List<SubscriptionDTO>();
            foreach (Subscription subscription in subscriptions)
            {
                result.Add(SubscriptionDTO.FromSubscription(subscription));
            }

            return result;
        }

        public async Task<SubscriptionDTO> insertAsync(SubscriptionDTO dto)
        {
            Counter counter = await _counterDAO.GetCountersAsync();
            int id = counter.Subscriptions++;
            await _counterDAO.UpdateCountersAsync(counter);

            Subscription subscription = Subscription.FromDTO(dto);
            subscription.Id = id;

            await _context.Subscriptions.InsertOneAsync(subscription);

            return SubscriptionDTO.FromSubscription(subscription);
        }

        public async Task<bool> Update(SubscriptionDTO dto)
        {
            Subscription original = await _context.Subscriptions
                .Find(Builders<Subscription>.Filter.Eq(s => s.Id, dto.Id))
                .FirstOrDefaultAsync();

            Subscription subscription = Subscription.FromDTO(dto);
            subscription._id = original._id;

            ReplaceOneResult result = await _context.Subscriptions.ReplaceOneAsync(filter: u => u.Id == subscription.Id, replacement: subscription);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }


        public async Task DeleteAsync(int subscriptionId)
        {
            await _context.Subscriptions.DeleteOneAsync(s => s.Id == subscriptionId);
        }
    }
}
