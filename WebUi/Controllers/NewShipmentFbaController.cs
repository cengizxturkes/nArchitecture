using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace WebUi.Controllers
{
	public class NewShipmentFbaController : Controller
	{
		private readonly BaseDbContext _context;
		public NewShipmentFbaController(BaseDbContext context)
		{
			_context = context;
		}
		[Authorize]
		public IActionResult Index()
		{
			var usermail = User.Identity.Name;

			var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
			var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
			ViewBag.LastName = userLastName;
			ViewBag.name = userId;
			ViewBag.v = usermail;
			return View();
		}
	}
}
