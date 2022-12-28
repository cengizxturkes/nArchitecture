using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Contexts;
using System;
using WebUi.Models.AuthControllerModel;
using WebUi.Models.InventoryControllerModel;
using WebUi.Models.TokenModels;

namespace WebUi.Controllers
{
	public class InventoryController : BaseController
	{
        private readonly BaseDbContext _context;
        public InventoryController(BaseDbContext context)
        {
            _context = context;
        }
        [Authorize]
		public IActionResult Index(Order order)
		{
            
            var usermail = User.Identity.Name;
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
			var userProductName = _context.Orders.Where(x => x.UserId== userId).Select(y => y.OrderName).ToList();
            //var userProductPrice= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Price.ToString()).ToList();
            //var userProductWidth= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Width.ToString()).ToList();
            //var userProductHeight= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Height.ToString()).ToList();
            //var userProductLenght = _context.Orders.Where(x => x.UserId == userId).Select(y => y.Length.ToString()).ToList();
            //var userProductWeight = _context.Orders.Where(x => x.UserId == userId).Select(y => y.Weight.ToString()).ToList();

            ViewBag.LastName = userLastName;
            List<Order> fundList = _context.Orders.Where(x=>x.UserId==userId).ToList();
            ViewBag.Orders = fundList;
            ViewBag.name = userName;
            ViewBag.v = usermail;
            ViewBag.order = userProductName;
           //ViewBag.price = userProductPrice;
           // ViewBag.width = userProductWidth;
           // ViewBag.height = userProductHeight;
           // ViewBag.lenght= userProductLenght;
           // ViewBag.weight= userProductWeight;

            return View();
		}
        public async Task<IActionResult> InventoryProduct(InventoryAddViewModel inventoryAddViewModel)
        {
            

            var usermail = User.Identity.Name;
            
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            var userOrder = _context.Orders.Where(x => x.UserId == userId).Select(y => y.OrderName).ToList();
            var userOrderDate = _context.Orders.Where(x => x.UserId == userId).Select(y => y.OrderDate).ToList();
            var userOrderPrice = _context.Orders.Where(x => x.UserId == userId).Select(y => y.Price.ToString()).ToList();
            var userOrderAmount = _context.Orders.Where(x => x.UserId == userId).Select(y => y.TotalAmount).ToList();
            ViewBag.LastName = userLastName;
            List<Order> fundList = _context.Orders.Where(x => x.UserId == userId).ToList();
            ViewBag.Orders = fundList;
            ViewBag.name = userName;
            ViewBag.v = usermail;
            ViewBag.order = userOrder;
            ViewBag.orderDate = userOrderDate;
            ViewBag.orderPrice = userOrderPrice;
            ViewBag.orderAmount = userOrderAmount;
            inventoryAddViewModel.UserId = userId;
            var response = await _client.PostAsJsonAsync("Orders/add", inventoryAddViewModel);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "Inventory");
            }
            return RedirectToAction("Index", "Inventory");
        }
    }
}
