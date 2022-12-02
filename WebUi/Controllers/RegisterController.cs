using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebUi.Models.AuthControllerModel;
using WebUi.Models.TokenModels;

namespace WebUi.Controllers
{
    public class RegisterController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            var response = await _client.PostAsJsonAsync("Auth/Register", loginViewModel);
            if (response.IsSuccessStatusCode)
            {
                //var body =await response.Content.ReadFromJsonAsync<LoginResponse>();
                var body = await response.Content.ReadAsStringAsync();
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(body);
                return RedirectToAction("Index", "Home");
            }
            return View("Register", loginViewModel);

        }
        public IActionResult Register()
        {
            return View();
        }


    }
}
