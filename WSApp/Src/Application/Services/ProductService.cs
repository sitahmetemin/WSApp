using AutoMapper;
using WSApp.Src.Application.DTOs;
using WSApp.Src.Application.Services.Base;
using WSApp.Src.Domain.Entities;
using WSApp.Src.Domain.Repositories;
using WSApp.Src.Domain.Repositories.Base;
using WSApp.Src.Domain.Services.Base;
using WSApp.Src.Infrastrurcture.Adapters;

namespace WSApp.Src.Application.Services
{
    public class ProductService : BaseService<Product, ProductDTO>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IBaseRepositories<Product> baseRepositories, IMapper mapper, IProductRepository productRepository) : base(baseRepositories, mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<bool> UpdateProductList(CancellationToken cancellationToken)
        {
            try
            {
                WebScrapingAdapters webScraping = new WebScrapingAdapters();
                var result = webScraping.GetScrapingData();
                var mappedResult = _mapper.Map<List<Product>>(result);

                var updateResult = await _productRepository.Upsert(mappedResult, cancellationToken);
                if (!updateResult.Any())
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}