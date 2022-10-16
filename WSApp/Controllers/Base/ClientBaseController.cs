using Microsoft.AspNetCore.Mvc;

namespace WSApp.Controllers.Base
{
    public class ClientBaseController : Controller
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
