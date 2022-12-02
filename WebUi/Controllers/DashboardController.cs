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
    {
       

        public IActionResult Index(LoginViewModel loginViewModel)
        {
            return View();


        }
     







        }
    }
