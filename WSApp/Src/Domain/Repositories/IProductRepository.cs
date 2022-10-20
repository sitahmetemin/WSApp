using WSApp.Src.Domain.Entities;
using WSApp.Src.Domain.Repositories.Base;

namespace WSApp.Src.Domain.Repositories
{
    public interface IProductRepository : IBaseRepositories<Product>
    {
        Task<IEnumerable<Product>> Upsert(IEnumerable<Product> entities, CancellationToken cancellationToken = default);
    }
}
