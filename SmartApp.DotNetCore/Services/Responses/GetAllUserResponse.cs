using System.Text.Json.Serialization;

namespace SmartApp.DotNetCore.Services.Responses
{
    public class GetAllUserResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public UserData UserData { get; set; }
    }

    public class UserData : BaseResponse
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}