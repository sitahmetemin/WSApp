using System.Linq.Expressions;

namespace WSApp.Src.Domain.Repositories.Base
{
    public interface IBaseRepositories<TEntity>
    {
        Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken = default);

        Task Delete(string[] ids, CancellationToken cancellationToken = default);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAllWithPagination(Expression<Func<TEntity, bool>> condition, int take = 20, int skip = 20, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);
        Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);

        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);
    }
}