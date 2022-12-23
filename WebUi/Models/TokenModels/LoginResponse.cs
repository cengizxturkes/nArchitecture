using Core.Security.Entities;
using Core.Security.JWT;

namespace WebUi.Models.TokenModels
{
	public class LoginResponse
	{
        public string Token { get; set; }
       public  RefreshToken RefreshToken { get; set; }
            public DateTime Expiration { get; set; }

    }
}
