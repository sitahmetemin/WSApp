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

        public async Task<IEnumerable<Product>> Upsert(IEnumerable<Product> entities, CancellationToken cancellationToken = default)
        {
            var models = new List<WriteModel<Product>>();

            foreach (var product in entities)
            {
                var upsert = new ReplaceOneModel<Product>(
                    filter: Builders<Product>.Filter.Eq(p => p.ModelName, product.ModelName),
                    replacement: product)
                {
                    IsUpsert = true
                };

                models.Add(upsert);
            }

            var result = await _mongoCl.BulkWriteAsync(models);

            if (result.Upserts.Count == 0)
                return null;

            return entities;
        }
    }
}