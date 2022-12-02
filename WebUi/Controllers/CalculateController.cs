using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class CalculateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
