using Microsoft.AspNetCore.Mvc;

namespace WSApp.Controllers.Base
{
    public class ClientBaseController : Controller
    {
        public virtual async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            return View();
        }

        public virtual async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            return View();
        }
        public virtual async Task<IActionResult> Update(CancellationToken cancellationToken = default)
        {
            return View();
        }
    }
}