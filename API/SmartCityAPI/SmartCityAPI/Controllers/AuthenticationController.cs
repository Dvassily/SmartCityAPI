using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using SmartCityAPI.DAO;

namespace SmartCityAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserDAO _userDAO;

        public AuthenticationController(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        // POST: api/Authentication
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostAsync([FromBody] AuthenticationData data)
        {
            UserDTO user = await _userDAO.Authentify(data.Email, data.Password);

            if (user == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(user);
        }
    }
}
