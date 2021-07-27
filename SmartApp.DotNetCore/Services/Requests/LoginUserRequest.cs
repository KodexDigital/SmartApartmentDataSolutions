using SmartApp.DotNetCore.Services.Responses;
using System.Text.Json.Serialization;

namespace SmartApp.DotNetCore.Services.Requests
{
    public class LoginUserRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
