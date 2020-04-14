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
    [Route("api/interests")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestDAO _interestDAO;

        public InterestsController(IInterestDAO interestDAO)
        {
            _interestDAO = interestDAO;
        }

        // GET: api/Interests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestDTO>>> GetInterests()
        {
            return new ObjectResult(await _interestDAO.FindAll());
        }

        // GET: api/Interests/5
        [HttpGet("{id}", Name = "GetInterest")]
        public async Task<ActionResult<InterestDTO>> GetInterest(int id)
        {
            InterestDTO service = await _interestDAO.FindById(id);

            if (service == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(service);
        }

        //// POST: api/Interests
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Interests/5
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
