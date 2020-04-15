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
    public class UserDAO : IUserDAO
    {
        private readonly IUserContext _context;

        public UserDAO(IUserContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> Authentify(string email, string password)
        {
            FilterDefinition<User> filters = Builders<User>.Filter.Eq(u => u.Email, email);
            filters &= Builders<User>.Filter.Eq(u => u.Password, password);

            User User = await _context.Users
                .Find(filters)
                .FirstOrDefaultAsync();

            if (User == null)
            {
                return null;
            }

            return UserDTO.FromUser(User);
        }

        public async Task<IEnumerable<UserDTO>> FindAll()
        {
            IEnumerable<User> Users = await _context.Users.Find(_ => true).ToListAsync();
            List<UserDTO> result = new List<UserDTO>();

            foreach (User User in Users)
            {
                result.Add(UserDTO.FromUser(User));
            }

            return result;
        }

        public async Task<UserDTO> FindById(int id)
        {
            User User = await _context.Users
                .Find(Builders<User>.Filter.Eq(u => u.Id, id))
                .FirstOrDefaultAsync();

            if (User == null)
            {
                return null;
            }

            return UserDTO.FromUser(User);
        }

        public async Task Insert(UserDTO dto)
        {
            User user = User.FromDTO(dto);
            user.Id = (await FindAll()).Count();


            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> Update(UserDTO dto)
        {
            User original = await _context.Users
                .Find(Builders<User>.Filter.Eq(s => s.Id, dto.Id))
                .FirstOrDefaultAsync();

            User user = User.FromDTO(dto);
            user._id = original._id;

            ReplaceOneResult result = await _context.Users.ReplaceOneAsync(filter : u => u.Id == user.Id, replacement: user);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
