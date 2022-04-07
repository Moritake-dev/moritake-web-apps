using System.ComponentModel.DataAnnotations;

namespace MGAuthentication.Data.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }
        public string Username { get; set; }

        [DataType("Password")]
        public string Password { get; set; }
    }
}