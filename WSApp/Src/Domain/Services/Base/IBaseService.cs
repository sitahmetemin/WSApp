using System.Linq.Expressions;

namespace WSApp.Src.Domain.Services.Base
{
    public interface IBaseService<TEntity, TDTO>
    {
        Task<TDTO> Delete(TDTO entity, CancellationToken cancellationToken = default);

        Task Delete(string[] ids, CancellationToken cancellationToken = default);

        Task Delete(string id, CancellationToken cancellationToken = default);

        Task<TDTO> Get(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> GetAll(CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> GetAllWithPagination(Expression<Func<TEntity, bool>> condition, int take = 20, int skip = 20, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> GetMany(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<TDTO> Insert(TDTO entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> Insert(IEnumerable<TDTO> entity, CancellationToken cancellationToken = default);

        Task<TDTO> Update(TDTO entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> Update(IEnumerable<TDTO> entity, CancellationToken cancellationToken = default);
    }
}