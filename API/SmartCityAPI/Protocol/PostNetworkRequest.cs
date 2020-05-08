using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol
{
    public class PostNetworkRequest
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
