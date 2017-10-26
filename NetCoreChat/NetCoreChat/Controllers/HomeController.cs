using Microsoft.AspNetCore.Mvc;

namespace NetCoreChat.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
