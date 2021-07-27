using System.Text.Json.Serialization;

namespace SmartApp.DotNetCore.Services.Responses
{
    public class RegisterUserResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

    public class LoginUserResponse : RegisterUserResponse
    { }
}

