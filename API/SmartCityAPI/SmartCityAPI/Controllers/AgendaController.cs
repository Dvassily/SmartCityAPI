using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace SmartCityAPI.Controllers
{
    [Route("api/agenda")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        [HttpGet("{userId}", Name = "GetUserAgenda")]
        public IEnumerable<Model.AgendaTask> Get(int UserId)
        {
            List<Model.AgendaTask> taches = new List<Model.AgendaTask>();
            taches.Add(new Model.AgendaTask
            {
                Title = "RV Jean-Pierre",
                Description = "Rendez-vous avec Jean-Pierre au bureau 215",
                StartDate = DateTime.Parse("2020-05-05 9:00:00Z"),
                EndDate = DateTime.Parse("2020-05-05 10:00:00Z")
            });

            taches.Add(new Model.AgendaTask
            {
                Title = "RV Catherine",
                Description = "Rendez-vous avec Catherine au café",
                StartDate = DateTime.Parse("2020-05-10 10:30:00Z"),
                EndDate = DateTime.Parse("2020-05-10 11:30:00Z")
            });

            return taches;
        }
            
    }
}