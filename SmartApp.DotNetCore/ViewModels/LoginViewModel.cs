using System.ComponentModel.DataAnnotations;

namespace SmartApp.DotNetCore.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email required.")]
        [Display(Name = "Enter your email", Prompt = "Enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required.")]
        [Display(Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
