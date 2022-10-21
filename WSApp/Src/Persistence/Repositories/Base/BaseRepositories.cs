using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using WSApp.Src.Application.Options.MongoDB;
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

        public virtual async Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.DeleteOneAsync(q => q.Id == entity.Id, cancellationToken: cancellationToken);
            return entity;
        }

        public virtual async Task Delete(string[] ids, CancellationToken cancellationToken = default)
        {
            await _mongoCl.DeleteManyAsync(q => ids.Contains(q.Id), cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            var result = await _mongoCl.FindAsync(condition, cancellationToken: cancellationToken);
            return result.FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _mongoCl.FindAsync(q => true, cancellationToken: cancellationToken);
            return result.ToList();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllWithPagination(Expression<Func<TEntity, bool>> condition, int take = 20, int skip = 20, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            var result = await _mongoCl.FindAsync(condition, cancellationToken: cancellationToken);
            return result.ToList();
        }

        public virtual async Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.InsertManyAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _mongoCl.ReplaceOneAsync(q => q.Id == entity.Id, entity, cancellationToken: cancellationToken);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}