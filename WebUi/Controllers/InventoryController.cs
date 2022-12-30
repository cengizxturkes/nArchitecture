using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
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
            //var userProductPrice= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Price.ToString()).ToList();
            //var userProductWidth= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Width.ToString()).ToList();
            //var userProductHeight= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Height.ToString()).ToList();
            //var userProductLenght = _context.Orders.Where(x => x.UserId == userId).Select(y => y.Length.ToString()).ToList();
            //var userProductWeight = _context.Orders.Where(x => x.UserId == userId).Select(y => y.Weight.ToString()).ToList();

            ViewBag.LastName = userLastName;
            List<Product> fundList = _context.Products.Where(x=>x.UserId==userId).ToList();
            ViewBag.Orders = fundList;
            ViewBag.name = userName;
            ViewBag.v = usermail;
           //ViewBag.price = userProductPrice;
           // ViewBag.width = userProductWidth;
           // ViewBag.height = userProductHeight;
           // ViewBag.lenght= userProductLenght;
           // ViewBag.weight= userProductWeight;

            return View();
		}
        public async Task<IActionResult> InventoryProduct(InventoryAddViewModel iavm)
        {
            

            var usermail = User.Identity.Name;
            
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
           
          
            ViewBag.LastName = userLastName;
            List<Product> fundList = _context.Products.Where(x => x.Id == userId).ToList();

            ViewBag.Orders = fundList;
            
            ViewBag.name = userName;
            ViewBag.v = usermail;
            ViewBag.UserId=userId;
            iavm.UserId = userId;
            iavm.Desi = iavm.Width * iavm.Height * iavm.Length/3000;

            string data = JsonConvert.SerializeObject(fundList);
            TempData["ProductList"] = data;
            ViewBag.Data = data;

            var response = await _client.PostAsJsonAsync("Product/add", iavm);
            if (response.IsSuccessStatusCode)
            {
                
                var body = await response.Content.ReadAsStringAsync();
                 InventoryResponse inventoryResponse = JsonConvert.DeserializeObject<InventoryResponse>(body);
                inventoryResponse.UserId = userId;
                return RedirectToAction("Index", "Inventory");
                
            }

            return RedirectToAction("Index", "Inventory");
        }
        public IActionResult NewShipmentFba()
        {

            var usermail = User.Identity.Name;
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            var userProductName = _context.Orders.Where(x => x.UserId == userId).Select(y => y.OrderName).ToList();
            //var userProductPrice= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Price.ToString()).ToList();
            //var userProductWidth= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Width.ToString()).ToList();
            //var userProductHeight= _context.Orders.Where(x => x.UserId == userId).Select(y => y.Height.ToString()).ToList();
            //var userProductLenght = _context.Orders.Where(x => x.UserId == userId).Select(y => y.Length.ToString()).ToList();
            //var userProductWeight = _context.Orders.Where(x => x.UserId == userId).Select(y => y.Weight.ToString()).ToList();
            
            ViewBag.LastName = userLastName;
            var data = ViewBag.Data;
            
            //List<Product> products = JsonSerializer.Deserialize(data);

            ViewBag.name = userName;
            ViewBag.v = usermail;
            ViewBag.order = userProductName;
            

            return View("Index","NewShipmentFba");
        }
    }
}
