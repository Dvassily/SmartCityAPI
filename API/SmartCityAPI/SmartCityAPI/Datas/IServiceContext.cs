using Model;
using Model.Database;
using MongoDB.Driver;

namespace SmartCityAPI.Datas
{
    public interface IServiceContext
    {
        IMongoCollection<Service> Services { get; }
    }
}
