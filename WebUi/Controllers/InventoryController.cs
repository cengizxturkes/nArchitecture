using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace WebUi.Controllers
{
	public class InventoryController : Controller
	{
        private readonly BaseDbContext _context;
        public InventoryController(BaseDbContext context)
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
