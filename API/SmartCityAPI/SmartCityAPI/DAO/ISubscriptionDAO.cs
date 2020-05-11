using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface ISubscriptionDAO
    {
        Task<List<SubscriptionDTO>> findAllAsync();
        Task<List<SubscriptionDTO>> findByNetworkIdAsync(int networkId);
        Task<List<SubscriptionDTO>> findByUserIdAsync(int userId);
        Task<SubscriptionDTO> insertAsync(SubscriptionDTO dto);
        Task<bool> Update(SubscriptionDTO subscription);
        Task DeleteAsync(int subscriptionId);
    }
}
