using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection.Metadata;
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
            List<Product> productsToAdd = new List<Product>();
            foreach (var product in entities)
            {
                var filter = Builders<Product>.Filter.Eq(p => p.ModelName, product.ModelName);

                var documentCount = _mongoCl
                    .FindAsync(filter, cancellationToken: cancellationToken)
                    .Result
                    .ToList()
                    .Count;

                if (documentCount != 0)
                    _ = await _mongoCl.ReplaceOneAsync(filter, product, cancellationToken: cancellationToken);
                else
                    productsToAdd.Add(product);
            }

            if (productsToAdd.Count != 0)
            {
                foreach (var prod in productsToAdd)
                {
                    prod.Id = ObjectId.GenerateNewId().ToString();

                    await _mongoCl.InsertOneAsync(prod, cancellationToken);
                }

            }

            return entities;
        }
    }
}