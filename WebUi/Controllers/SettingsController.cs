using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
