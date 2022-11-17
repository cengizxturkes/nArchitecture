using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
	public class BaseController : Controller
	{
        private readonly Uri _baseAdress = new("http://localhost:5147/api/");
        protected readonly HttpClient _client;

        public BaseController()
        {
            _client = new HttpClient
            {
                BaseAddress = _baseAdress
            };
        }
    }
}
