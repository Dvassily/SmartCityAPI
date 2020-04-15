using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using SmartCityAPI.DAO;
using SmartCityAPI.Datas;

namespace SmartCityAPI.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceDAO _serviceDAO;
        private readonly IUserDAO _userDAO;

        public ServiceController(IServiceDAO serviceDAO, IUserDAO userDAO)
        {
            _serviceDAO = serviceDAO;
            _userDAO = userDAO;
        }

        // GET: api/Actualite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
        {
            return new ObjectResult(await _serviceDAO.FindAll());
        }

        // GET: api/Actualite/5
        [HttpGet("{id}", Name = "GetServiceById")]
        public async Task<ActionResult<ServiceDTO>> GetServiceById(int id)
        {
            ServiceDTO service = await _serviceDAO.FindById(id);

            if (service == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(service);
        }

        // GET: api/Actualite/5
        [HttpGet("user/{id}", Name = "GetServicesByUserId")]
        public async Task<ActionResult<List<ServiceDTO>>> GetServicesByUserId(int id)
        {
            UserDTO user = await _userDAO.FindById(id);

            if (user == null)
            {
                return new NotFoundResult();
            }

            List<ServiceDTO> services = new List<ServiceDTO>();

            foreach (int serviceId in user.Services)
            {
                services.Add(await _serviceDAO.FindById(serviceId));
            }

            return new ObjectResult(services);
        }

        //// POST: api/Actualite
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Actualite/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
