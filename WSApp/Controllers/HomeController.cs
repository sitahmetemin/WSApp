using WSApp.Controllers.Base;

namespace WSApp.Controllers
{
    public class HomeController : ClientBaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    }
}