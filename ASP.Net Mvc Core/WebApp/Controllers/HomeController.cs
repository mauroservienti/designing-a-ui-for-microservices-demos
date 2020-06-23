using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}