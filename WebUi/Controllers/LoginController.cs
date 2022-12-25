using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using WebUi.Models.AuthControllerModel;
using WebUi.Models.TokenModels;
using Microsoft.EntityFrameworkCore;

namespace WebUi.Controllers
{
    public class LoginController : BaseController
    {

        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var response = await _client.PostAsJsonAsync("Auth/Login",loginViewModel);
            if (response.IsSuccessStatusCode)
            {

                //var body =await response.Content.ReadFromJsonAsync<LoginResponse>();
                var body=await response.Content.ReadAsStringAsync();
                LoginResponse loginResponse=JsonConvert.DeserializeObject<LoginResponse>(body);
                var Firstname=loginViewModel.FirstName;
                var Lastname=loginViewModel.LastName;
                
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,loginViewModel.Email),

                       new Claim(ClaimTypes.Role,"Customer")
                        //new Claim("refresh_token",  loginResponse.RefreshToken ),
                    };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

                return RedirectToAction("Index", "Dashboard");
            }
            return View("Index",loginViewModel);

        }
        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login()
        //{
            

        //        //var body =await response.Content.ReadFromJsonAsync<LoginResponse>();

        //        var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Name,"Layloş"),
        //                new Claim(ClaimTypes.Email,  "layloşunmaili"),
        //                new Claim(ClaimTypes.Role,"Admin")
        //                //new Claim("refresh_token",  loginResponse.RefreshToken ),
        //            };


        //        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var authProperties = new AuthenticationProperties();
        //        await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(claimsIdentity),
        //        authProperties);


        //        return RedirectToAction("Index", "Dashboard");
            

        //}

      

    }
}
