using System.ComponentModel.DataAnnotations;

namespace MGAuthentication.Data.ViewModels.Auth
{
    public class PasswordResetVM
    {
        [Display(Name = "User Id")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "New Password")]
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}