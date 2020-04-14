using Model.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("dateofbirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("interests")]
        public List<int> Interests { get; set; }

        [JsonProperty("services")]
        public List<int> Services { get; set; }

        public static UserDTO FromUser(User user)
        {
            UserDTO dto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Address = user.Address,
                Town = user.Town,
                Interests = user.Interests,
                Services = user.Services
            };

            return dto;
        }
    }
}
