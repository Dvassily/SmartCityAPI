using Model.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class TradeTypeDTO
    {

        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("children")]
        public List<int> Children { get; set; } = new List<int>();


        [JsonProperty("tradeId")]
        public int TradeId { get; set; }

        public static TradeTypeDTO FromTradeType(TradeType tradeType)
        {
            TradeTypeDTO dto = new TradeTypeDTO
            {
                Id = tradeType.Id,
                Name = tradeType.Name,
                Children = tradeType.Children,
                TradeId = tradeType.TradeId
            };

            return dto;
        }
    }
}
