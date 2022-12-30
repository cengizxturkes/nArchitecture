using Domain.Entities;
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

			var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
			var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            List<Product> products = _context.Products.Where(x => x.UserId == userId).ToList();
            var productAmount = products.Count();
			double v = _context.Products.Where(x => x.UserId == userId).Select(x => x.Desi).ToList().Sum();
			
			
			double ProductTotalDimensionalWeight = _context.Products.Where(x => x.UserId == userId).Select(x => x.Weight).ToList().Sum()+1;
			double ProductTotalActualWeight = _context.Products.Where(x => x.UserId == userId).Select(x => x.Weight).ToList().Sum();

            ViewBag.ProductTotalActualWeight = ProductTotalActualWeight;
			ViewBag.ProductTotalDimensionalWeight = ProductTotalDimensionalWeight;

            ViewBag.ProductAmount = productAmount;
			ViewBag.Orders=products;
            ViewBag.LastName = userLastName;
			ViewBag.name = userId;
			ViewBag.v = usermail;
			return View();
		}
	}
}
