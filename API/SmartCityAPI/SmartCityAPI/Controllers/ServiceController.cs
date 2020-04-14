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

        public ServiceController(IServiceDAO serviceDAO)
        {
            _serviceDAO = serviceDAO;
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
