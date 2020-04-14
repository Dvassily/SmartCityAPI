using Model.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Database
{
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("firstname")]
        public string FirstName { get; set; }

        [BsonElement("lastname")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("dateofbirth")]
        public string DateOfBirth { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("town")]
        public string Town { get; set; }

        [BsonElement("interests")]
        public List<int> Interests { get; set; } = new List<int>();

        [BsonElement("services")]
        public List<int> Services { get; set; } = new List<int>();

        public static User FromDTO(UserDTO dto)
        {
            return new User
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                Town = dto.Town,
                Interests = dto.Interests,
                Services = dto.Services
            };
        }
    }
}
