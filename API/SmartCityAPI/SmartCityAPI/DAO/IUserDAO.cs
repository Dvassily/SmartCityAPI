using Model;
using Model.Database;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.DAO
{
    public interface IUserDAO
    {
        Task<IEnumerable<UserDTO>> FindAll();

        Task<UserDTO> FindById(int id);

        Task<UserDTO> Insert(UserDTO dto);

        Task<bool> Update(UserDTO dto);
        Task<UserDTO> Authentify(string email, string password);
    }
}
