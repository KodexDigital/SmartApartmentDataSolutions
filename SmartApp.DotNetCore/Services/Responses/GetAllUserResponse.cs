using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartApp.DotNetCore.Services.Responses
{
    public class GetAllUserResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public List<UserData> Data { get; set; }
    }

    public class UserData
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}