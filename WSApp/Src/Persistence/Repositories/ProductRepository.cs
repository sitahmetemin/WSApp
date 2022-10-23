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
            foreach (var product in entities.Where(qr => !string.IsNullOrEmpty(qr.Brand) && !string.IsNullOrEmpty(qr.ModelName) && !string.IsNullOrEmpty(qr.ModelNo)))
            {
                var filterBrand = Builders<Product>.Filter.Eq(p => p.Brand, product.Brand);
                var filterModelName = Builders<Product>.Filter.Eq(p => p.ModelName, product.ModelName);
                var filterModelNo = Builders<Product>.Filter.Eq(p => p.ModelNo, product.ModelNo);

                var document = _mongoCl
                    .FindAsync(filterBrand & filterModelName & filterModelNo, cancellationToken: cancellationToken)
                    .Result
                    .ToList();

                if (document.Count != 0)
                {
                    product.Id = document[0].Id;
                    _ = await _mongoCl.ReplaceOneAsync(filterBrand & filterModelName & filterModelNo, product, cancellationToken: cancellationToken);
                }
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