using AutoMapper;
using System.Linq.Expressions;
using WSApp.Src.Domain.Repositories.Base;
using WSApp.Src.Domain.Services.Base;

namespace WSApp.Src.Application.Services.Base
{
    public class BaseService<TEntity, TDTO> : IBaseService<TEntity, TDTO>
    {
        private readonly IBaseRepositories<TEntity> _baseRepositories;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepositories<TEntity> baseRepositories, IMapper mapper)
        {
            _baseRepositories = baseRepositories ?? throw new ArgumentNullException(nameof(baseRepositories));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TDTO> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.Delete(entity, cancellationToken);
            return _mapper.Map<TDTO>(result);
        }

        public async Task Delete(string[] ids, CancellationToken cancellationToken = default)
        {
            await _baseRepositories.Delete(ids, cancellationToken);
        }

        public async Task<TDTO> Get(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.Get(condition, cancellationToken);
            return _mapper.Map<TDTO>(result);
        }

        public async Task<IEnumerable<TDTO>> GetAll(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.Get(condition, cancellationToken);
            return _mapper.Map<IEnumerable<TDTO>>(result);
        }

        public async Task<IEnumerable<TDTO>> GetAllWithPagination(Expression<Func<TEntity, bool>> condition, int take = 20, int skip = 20, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.GetAllWithPagination(condition, take, skip, cancellationToken);
            return _mapper.Map<IEnumerable<TDTO>>(result);
        }

        public async Task<TDTO> Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.Insert(entity, cancellationToken);
            return _mapper.Map<TDTO>(result);
        }

        public async Task<IEnumerable<TDTO>> Insert(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.Insert(entity, cancellationToken);

            return _mapper.Map<IEnumerable<TDTO>>(result);
        }

        public async Task<TDTO> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.Update(entity, cancellationToken);

            return _mapper.Map<TDTO>(result);
        }

        public async Task<IEnumerable<TDTO>> Update(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            var result = await _baseRepositories.Update(entity, cancellationToken);

            return _mapper.Map<IEnumerable<TDTO>>(result);
        }
    }
}