using Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string APIUrl { get; set; }

        public string Description { get; set; }

        public static ServiceDTO FromService(Service service)
            => new ServiceDTO
            {
                Id = service.Id,
                Name = service.Name,
                ImageUrl = service.ImageUrl,
                APIUrl = service.APIUrl,
                Description = service.Description
            };
    }
}
