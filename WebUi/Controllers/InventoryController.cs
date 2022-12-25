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
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
			var userOrder = _context.Orders.Where(x => x.UserId== userId).Select(y => y.OrderName).FirstOrDefault();
            var userOrderDate= _context.Orders.Where(x => x.UserId == userId).Select(y => y.OrderDate).FirstOrDefault();
            var userOrderPrice= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Price).FirstOrDefault();
            var userOrderAmount= _context.Orders.Where(x => x.UserId == userId).Select(y => y.TotalAmount).FirstOrDefault();
			ViewBag.LastName = userLastName;
            ViewBag.name = userName;
            ViewBag.v = usermail;
            ViewBag.order = userOrder;
            ViewBag.orderDate = userOrderDate;
            ViewBag.orderPrice = userOrderPrice;
            ViewBag.orderAmount = userOrderAmount;
            return View();
		}
	}
}
