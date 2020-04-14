using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface IServiceDAO
    {
        Task<IEnumerable<ServiceDTO>> FindAll();
        Task<ServiceDTO> FindById(int id);
    }
}
