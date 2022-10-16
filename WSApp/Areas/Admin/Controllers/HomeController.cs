﻿using Microsoft.AspNetCore.Mvc;
using WSApp.Areas.Admin.Base.Controllers.Base;
using WSApp.Src.Domain.Services.Base;

namespace WSApp.Areas.Admin.Controllers.Admin
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAll(q => true);
            return View(result);
        }
    }
}