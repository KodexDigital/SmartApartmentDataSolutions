using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models.UserModels
{
    public class LoginUserModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
