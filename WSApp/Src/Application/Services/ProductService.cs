using AutoMapper;
using WSApp.Src.Application.DTOs;
using WSApp.Src.Application.Models;
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
        #region Props
        private readonly IProductRepository _productRepository;
        #endregion

        #region constracture
        public ProductService(IBaseRepositories<Product> baseRepositories, IMapper mapper, IProductRepository productRepository) : base(baseRepositories, mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        #endregion

        #region Privates
        private IList<ProductDTO> SetProductGroup(List<SitePropertiesModel> scrapedResult)
        {
            List<ProductDTO> productDTOs = new();

            var uniqueProducts = scrapedResult
                .Where(qr =>
                !string.IsNullOrEmpty(qr.ModelName)
                && !string.IsNullOrEmpty(qr.ModelNo)
                )
                .GroupBy(q => new
                {
                    q.ModelName,
                    q.ModelNo
                }).Select(q => q.First());

            foreach (var prod in uniqueProducts)
            {
                var mutipleSource = scrapedResult.Where(q =>
                q.Brand == prod.Brand
                && q.ModelName == prod.ModelName
                && q.ModelNo == prod.ModelNo
                && q.ProcessorBrand == prod.ProcessorBrand)
                    .ToList();

                if (mutipleSource.Count > 1)
                {
                    var subPropsList = new List<SellSourceDTO>();
                    var insertableProd = _mapper.Map<ProductDTO>(prod);

                    foreach (var item in mutipleSource)
                    {
                        var subProps = new SellSourceDTO
                        {
                            Price = item.Prices,
                            ProductUrl = item.ProductUrl,
                            Site = item.Site
                        };

                        subPropsList.Add(subProps);
                    }

                    insertableProd.SellSource = subPropsList;
                    productDTOs.Add(insertableProd);
                }
                else
                {
                    var insertableProd = _mapper.Map<ProductDTO>(prod);
                    var subProps = new List<SellSourceDTO>{
                        new SellSourceDTO
                        {
                            Price = prod.Prices,
                            ProductUrl = prod.ProductUrl,
                            Site = prod.Site
                        }
                    };
                    insertableProd.SellSource = subProps;

                    productDTOs.Add(insertableProd);
                }

            }

            return productDTOs;
        }
        #endregion


        #region publics
        public async Task<bool> UpdateProductList(CancellationToken cancellationToken)
        {
            try
            {
                WebScrapingAdapters webScraping = new WebScrapingAdapters();
                var scrapedResult = await webScraping.GetScrapingData();
                var mappedDto = SetProductGroup(scrapedResult);

                var mappedProds = _mapper.Map<List<Product>>(mappedDto);

                var updateResult = await _productRepository.Upsert(mappedProds, cancellationToken);
                if (!updateResult.Any())
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}