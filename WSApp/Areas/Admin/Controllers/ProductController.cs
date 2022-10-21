using Microsoft.AspNetCore.Mvc;
using WSApp.Areas.Admin.Base.Controllers.Base;
using WSApp.Src.Domain.Services.Base;

namespace WSApp.Areas.Admin.Controllers.Admin
{
    public class ProductController : BaseAdminController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Route("update-database")]
        public async Task<bool> UpdateDatabaseAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _productService.UpdateProductList(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}