using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace WebUi.Controllers
{
    public class SettingsController : Controller
    {
        private readonly BaseDbContext _context;

        public SettingsController(BaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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

            ViewBag.name = userName;
            ViewBag.v = usermail;
            return View();
        }
    }
}
