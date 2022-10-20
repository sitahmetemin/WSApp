using WSApp.Src.Application.DTOs;
using WSApp.Src.Domain.Entities;

namespace WSApp.Src.Domain.Services.Base
{
    public interface IProductService : IBaseService<Product, ProductDTO>
    {
        Task<bool> UpdateProductList(CancellationToken cancellationToken);
    }
}