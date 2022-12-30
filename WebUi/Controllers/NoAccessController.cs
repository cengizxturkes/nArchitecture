using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class NoAccessController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
            await Task.Delay(2000);
            return RedirectToAction("Index","Home");

        }
    }
}
