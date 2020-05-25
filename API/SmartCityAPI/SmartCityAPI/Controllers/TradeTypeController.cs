using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Database;
using Model.DTO;
using MongoDB.Driver;
using SmartCityAPI.DAO;

namespace SmartCityAPI.Controllers
{
    [Route("api/tradetypes")]
    [ApiController]
    public class TradeTypeController : ControllerBase
    {
        private readonly ITradeTypeDAO _tradeTypeDAO;

        public TradeTypeController(ITradeTypeDAO tradeTypeDAO)
        {
            _tradeTypeDAO = tradeTypeDAO;
        }

        // GET: api/TradeType
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/tradetypes/5
        [HttpGet("{id}", Name = "FindContent")]
        public async Task<IActionResult> GetAsync(int id)
        {
            List<TradeTypeDTO> result = new List<TradeTypeDTO>();
            System.Diagnostics.Trace.WriteLine("id : " + id);

            TradeTypeDTO category = await _tradeTypeDAO.FindById(id);
            
            if (category == null)
            {
                return new NotFoundResult();
            }

            foreach (int childrenId in category.Children)
            {
                TradeTypeDTO subCategory = await _tradeTypeDAO.FindById(childrenId);

                result.Add(subCategory);
            }

            return new ObjectResult(result);
        }
    }
}
