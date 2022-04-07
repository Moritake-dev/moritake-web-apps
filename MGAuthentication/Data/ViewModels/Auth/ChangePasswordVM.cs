using System.ComponentModel.DataAnnotations;

namespace MGAuthentication.Data.ViewModels.Auth
{
    public class ChangePasswordVM
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "Current Password")]
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}