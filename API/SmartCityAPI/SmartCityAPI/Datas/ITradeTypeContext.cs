﻿using Model.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityAPI.Datas
{
    public interface ITradeTypeContext
    {
        IMongoCollection<TradeType> TradeTypes { get; }
    }
}
