using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface IPublicationDAO
    {

        Task<IEnumerable<PublicationDTO>> FindAll();

        Task<PublicationDTO> FindById(int id);

        Task<IEnumerable<PublicationDTO>> FindByNetworkId(int id);

        Task<PublicationDTO> Insert(PublicationDTO publication);
    }
}
