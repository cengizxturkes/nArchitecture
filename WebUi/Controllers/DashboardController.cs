using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebUi.Models.AuthControllerModel;
using WebUi.Models.TokenModels;

namespace WebUi.Controllers
{

    public class DashboardController : Controller
    { private readonly BaseDbContext _context;
        public DashboardController(BaseDbContext context)
        {
            _context = context;
        }
        [Authorize]

        public IActionResult Index(LoginViewModel loginViewModel)
        {
            var usermail = User.Identity.Name;
           var Auth= User.Identity.IsAuthenticated;

                var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            ViewBag.LastName = userLastName;
            ViewBag.name = userId;
            ViewBag.v = usermail;
            return View();


        }
     







        }
    }
