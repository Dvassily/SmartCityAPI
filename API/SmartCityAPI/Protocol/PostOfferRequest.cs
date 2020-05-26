using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol
{
    public class PostOfferRequest
    {
        public int TradeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
