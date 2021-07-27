using System.ComponentModel.DataAnnotations;

namespace SmartApp.DotNetCore.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Email required.")]
        [Display(Name = "Enter your email", Prompt = "Enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number required.")]
        [Display(Name = "Phone number", Prompt = "Enter your phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password required.")]
        [Display(Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password required.")]
        [Display(Name = "Confirm password", Prompt = "Retype your password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
