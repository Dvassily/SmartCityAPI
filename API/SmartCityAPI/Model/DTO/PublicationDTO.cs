using Model.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class PublicationDTO
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("network_id")]
        public int NetworkId { get; set; }

        [JsonProperty("author_id")]
        public int AuthorId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }


        [JsonProperty("content")]
        public string Content { get; set; }

        // Only in outcoming requests
        [JsonProperty("authorName")]
        public string AuthorName { get; set; }

        public static PublicationDTO FromPublication(Publication publication) 
            => new PublicationDTO
        {
            Id = publication.Id,
            NetworkId = publication.NetworkId,
            AuthorId = publication.AuthorId,
            Date = publication.Date,
            Content = publication.Content
        };
    }
}
