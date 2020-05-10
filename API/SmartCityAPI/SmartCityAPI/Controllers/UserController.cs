using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using SmartCityAPI.DAO;

namespace SmartCityAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDAO _userDAO;
        private readonly ISubscriptionDAO _subscriptionDAO;
        private readonly INetworkDAO _networkDAO;

        public UserController(IUserDAO userDAO, ISubscriptionDAO subscriptionDAO, INetworkDAO networkDAO)
        {
            _userDAO = userDAO;
            _subscriptionDAO = subscriptionDAO;
            _networkDAO = networkDAO;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return new ObjectResult(await _userDAO.FindAll());
        }

        // GET: api/users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            UserDTO user = await _userDAO.FindById(id);

            if (user == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO user)
        {
            await _userDAO.Insert(user);

            return new OkObjectResult(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserDTO user)
        {
            // TODO : Throw error if id is different of user's id
            await _userDAO.Update(user);

            return new OkObjectResult(user);
        }


        // GET: api/users/5/networks
        [HttpGet("{id}/networks", Name = "GetSubscribedNetworks")]
        public async Task<ActionResult<NetworkDTO>> GetSubscribedNetworks(int id)
        {
            List<SubscriptionDTO> subscriptions = await _subscriptionDAO.findByUserIdAsync(id);
            List<NetworkDTO> networks = new List<NetworkDTO>();

            foreach (SubscriptionDTO subscription in subscriptions)
            {
                networks.Add(await _networkDAO.FindById(subscription.Id));
            }

            return new OkObjectResult(networks);
        }

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
