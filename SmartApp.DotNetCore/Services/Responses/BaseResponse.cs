using System.Text.Json.Serialization;

namespace SmartApp.DotNetCore.Services.Responses
{
    public class BaseResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
