using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Contexts;
using System.Text.Json.Serialization;
using WebUi.Models.AuthControllerModel;
using WebUi.Models.TokenModels;

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
                return RedirectToAction("Login", "Login");
            }
            return View("Index",loginViewModel);

        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
