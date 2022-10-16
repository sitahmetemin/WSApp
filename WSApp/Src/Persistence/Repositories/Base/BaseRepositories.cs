using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using WSApp.Src.Application.Options.MongoDB;
using WSApp.Src.Application.Options.MongoDB.Abstraction;
using WSApp.Src.Domain.Entities.Base.Abstraction;
using WSApp.Src.Domain.Repositories.Base;

namespace WSApp.Src.Persistence.Repositories.Base
{
    public class BaseRepositories<TEntity> : IBaseRepositories<TEntity>
        where TEntity : IBaseEntity
    {
        protected readonly IMongoCollection<TEntity> _mongoCl;
        private readonly IOptions<MongoStoreDatabaseOption> _mongoOption;

        public BaseRepositories(IMongoClient mongoClient, IOptions<MongoStoreDatabaseOption> mongoOption)
        {
            _mongoOption = mongoOption ?? throw new ArgumentException(nameof(mongoOption));
            var database = mongoClient.GetDatabase(_mongoOption.Value.DatabaseName) ?? throw new ArgumentNullException(nameof(mongoOption));
            _mongoCl = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower()) ?? throw new ArgumentNullException(nameof(mongoClient));
        }

        public async virtual Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.DeleteOneAsync(q => q.Id == entity.Id);
            return entity;
        }

        public async virtual Task Delete(string[] ids, CancellationToken cancellationToken = default)
        {
            await _mongoCl.DeleteManyAsync(q => ids.Contains(q.Id));
        }

        public async virtual Task<TEntity> Get(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            var result = await _mongoCl.FindAsync(condition);
            return result.FirstOrDefault();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            var result = await _mongoCl.FindAsync(q => true);
            return result.ToList();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllWithPagination(Expression<Func<TEntity, bool>> condition, int take = 20, int skip = 20, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.InsertOneAsync(entity);
            return entity;
        }

        public async virtual Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.InsertManyAsync(entity);
            return entity;
        }

        public async virtual Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.ReplaceOneAsync(q => q.Id == entity.Id, entity);
            return entity;
        }

        public async virtual Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}