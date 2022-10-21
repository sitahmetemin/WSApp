using Microsoft.AspNetCore.Mvc;
using WSApp.Areas.Admin.Base.Controllers.Base;
using WSApp.Src.Application.DTOs;
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
        public async Task<bool> UpdateDatabaseAsync(CancellationToken cancellationToken = default)
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

        [Route("edit")]
        public async Task<IActionResult> Edit(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _productService.Get(q => q.Id == id);
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("update")]
        public async Task<IActionResult> Update(ProductDTO product, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = _productService.Update(product);
                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = _productService.Delete(id);
                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}