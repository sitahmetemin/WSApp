﻿using System.Linq.Expressions;
using WSApp.Src.Application.DTOs.Base;
using WSApp.Src.Domain.Entities.Base.Abstraction;

namespace WSApp.Src.Domain.Services.Base
{
    public interface IBaseService<TEntity, TDTO>
    {
        Task<TDTO> Get(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> GetAll(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> GetAllWithPagination(Expression<Func<TEntity, bool>> condition, int take = 20, int skip = 20, CancellationToken cancellationToken = default);

        Task<TDTO> Insert(TEntity entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> Insert(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);

        Task<TDTO> Update(TEntity entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TDTO>> Update(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);

        Task<TDTO> Delete(TEntity entity, CancellationToken cancellationToken = default);

        Task Delete(string[] ids, CancellationToken cancellationToken = default);
    }
}