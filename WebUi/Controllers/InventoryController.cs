using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
	public class InventoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
