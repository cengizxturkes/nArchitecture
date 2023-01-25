namespace WebUi.Models.AuthControllerModel
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        //public string? AuthenticatorCode { get; set; } public string FirstName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IsConfirmation { get; set; }
        public bool IsAdmin { get; set; }

    }
}
