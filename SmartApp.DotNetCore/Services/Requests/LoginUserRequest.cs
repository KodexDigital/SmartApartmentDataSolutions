using Newtonsoft.Json;

namespace SmartApp.DotNetCore.Services.Requests
{
    public class LoginUserRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
