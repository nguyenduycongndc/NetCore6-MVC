using Microsoft.AspNetCore.Mvc;
using ProjectTest.Attributes;
using ProjectTest.Services.Interface;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BaseAuthorize]
    public class DataEmailController : Controller
    {
        private readonly ILogger<DataEmailController> _logger;
        protected readonly IConfiguration _config;
        public DataEmailController(ILogger<DataEmailController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
