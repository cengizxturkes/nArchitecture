using Application.Features.OrderPayoneer.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Contexts;
using WebUi.Models.InventoryControllerModel;

namespace WebUi.Controllers
{
	public class NewShipmentFbaController : BaseController
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
			var userFirstName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            List<Product> products = _context.Products.Where(x => x.UserId == userId&&x.IsOrder==false).ToList();
            var productAmount = products.Count();
			
			double v = _context.Products.Where(x => x.UserId == userId).Select(x => x.Desi).ToList().Sum();
			
			
			double ProductTotalDimensionalWeight = _context.Products.Where(x => x.UserId == userId).Select(x => x.Weight).ToList().Sum()+1;
            double ProductTotalActualWeight = _context.Products.Where(x => x.UserId == userId).Select(x => x.Weight).ToList().Sum();
            double ProductTotalPrice = _context.Products.Where(x => x.UserId == userId&&x.IsOrder==false).Select(x => x.ExpectedTotalPrice).ToList().Sum();

            ViewBag.ProductTotalActualWeight = ProductTotalActualWeight;
			ViewBag.ProductTotalDimensionalWeight = ProductTotalDimensionalWeight;
			ViewBag.ProductTotalPrice = ProductTotalPrice;
            ViewBag.ProductAmount = productAmount;
			ViewBag.Orders=products;
            ViewBag.LastName = userLastName;
			ViewBag.name = userFirstName;
			ViewBag.v = usermail;
			return View();
		}
		public async Task<IActionResult> AddPayoneerCode(OrderPayoonerViewModel orderPayoonerView,string code)
		
			{
            orderPayoonerView.PayonerCode=code;
            var usermail = User.Identity.Name;
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            var Product = _context.Products.Where(x => x.UserId == userId&&x.IsOrder==false).ToList();
            orderPayoonerView.UserId = userId;
            var response = await _client.PostAsJsonAsync("payoneer/add", orderPayoonerView);
			var orderDate=DateTime.Now;

			var orderCode = orderDate.ToString("ddMMYYYYHHmmss");

            if (response.IsSuccessStatusCode)
            {
				foreach(var model in Product)
				{

					model.IsOrder = true;
					model.OrderCode = orderCode;
					model.OrderDate = orderDate;
                }
				_context.SaveChanges();
                var body = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index", "Inventory");

            }
			return RedirectToAction("Index", "Inventory");
        }
		//public async Task<IActionResult> SendMail(OrderPayoonerViewModel orderPayoonerView)
		//{ 
		
		//}
        }
}
