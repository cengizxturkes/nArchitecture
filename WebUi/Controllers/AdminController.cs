using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Contexts;
using System.Security.Claims;
using WebUi.Models.AuthControllerModel;
using WebUi.Models.TokenModels;
using Microsoft.AspNetCore.Authorization;

namespace WebUi.Controllers
{
    public class AdminController : BaseController
    {
        private readonly BaseDbContext _context;
        public AdminController(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var usermail = loginViewModel.Email;
            var confirmstatus = _context.Users.Where(x => x.Email == usermail).Select(y => y.IsConfirmation).FirstOrDefault();
            var isAdmin = _context.Users.Where(x => x.Email == usermail).Select(y => y.IsAdmin).FirstOrDefault();
            var response = await _client.PostAsJsonAsync("Auth/Login", loginViewModel);

            if (response.IsSuccessStatusCode && isAdmin == true)
            {

                //var body =await response.Content.ReadFromJsonAsync<LoginResponse>();
                var body = await response.Content.ReadAsStringAsync();
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(body);
                var Firstname = loginViewModel.FirstName;
                var Lastname = loginViewModel.LastName;
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,loginViewModel.Email),
                       new Claim(ClaimTypes.Role,"Admin"),

                        //new Claim("refresh_token",  loginResponse.RefreshToken ),
                    };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

                return RedirectToAction("Index", "AdminDashboard");
            }
            return View("Index", loginViewModel);
        }
       
    }

}
