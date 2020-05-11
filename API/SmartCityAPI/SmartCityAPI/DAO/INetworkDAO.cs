using Model.DTO;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface INetworkDAO
    {
        Task<IEnumerable<NetworkDTO>> FindAll();

        Task<NetworkDTO> FindById(int id);

        Task<NetworkDTO> Insert(NetworkDTO network);
    }
}
