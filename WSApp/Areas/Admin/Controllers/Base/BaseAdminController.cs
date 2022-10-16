using Microsoft.AspNetCore.Mvc;

namespace WSApp.Areas.Admin.Base.Controllers.Base
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class BaseAdminController : Controller
    {
        public async virtual Task<IActionResult> Index()
        {
            return View();
        }

        public async virtual Task<IActionResult> Create()
        {
            return View();
        }

        public async virtual Task<IActionResult> Update()
        {
            return View();
        }
    }
}
