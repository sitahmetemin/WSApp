using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WSApp.Src.Application.Options.MongoDB;
using WSApp.Src.Domain.Entities;
using WSApp.Src.Domain.Repositories;
using WSApp.Src.Persistence.Repositories.Base;

namespace WSApp.Src.Persistence.Repositories
{
    public class ProductRepository : BaseRepositories<Product>, IProductRepository
    {
        public ProductRepository(IMongoClient mongoClient, IOptions<MongoStoreDatabaseOption> mongoOption) : base(mongoClient, mongoOption)
        {
        }
    }
}