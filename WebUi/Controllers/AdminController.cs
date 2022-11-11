using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
