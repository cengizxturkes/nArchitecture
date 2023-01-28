using Application.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace WebUi.Controllers
{
    public class ShipmentFbaController : Controller
    {
        private readonly BaseDbContext _context;
        IProductRepository productRepository;
        public ShipmentFbaController(IProductRepository productRepository, BaseDbContext context)
        {
            this.productRepository = productRepository;
            _context = context;
        }
        public IActionResult Index()
        {

            var usermail = User.Identity.Name;
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            var v = _context.Users.Where(x => x.Email == usermail).Select(y => y.Email).FirstOrDefault();
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            var orderCode = _context.Products.Where(x => x.UserId == userId&&x.IsOrder==true).ToList();
            ViewBag.v = v;
            ViewBag.OrderCode = orderCode;
            ViewBag.LastName = userLastName;
            ViewBag.name = userName;
            return View(productRepository.GetOrderbyUser(userId));
        }
        public IActionResult Detail(string id)
        {
            var OrdersProduct = _context.Products.Where(x => x.OrderCode == id).ToList();

            var usermail = User.Identity.Name;
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            ViewBag.LastName = userLastName;
            ViewBag.name = userName;
            var code = id;
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            ViewBag.OrdersProduct= OrdersProduct;
            return View(productRepository.GetOrderbyCode(code));
        }
    }
}
