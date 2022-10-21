using Microsoft.AspNetCore.Mvc;
using WSApp.Controllers.Base;
using WSApp.Models.ResponseModels;
using WSApp.Src.Domain.Services.Base;

namespace WSApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : ClientBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public override async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var prodResult = await _productService.GetAll(cancellationToken);
            var response = new ProductListModel
            {
                Products = prodResult
            };
            return View(response);
        }

        public async Task<IActionResult> Search(string input, CancellationToken cancellationToken = default)
        {
            var prodResult = await _productService.GetMany(qr => qr.ModelName == input, cancellationToken);
            var response = new ProductListModel
            {
                Products = prodResult
            };
            return View(response);
        }
    }
}