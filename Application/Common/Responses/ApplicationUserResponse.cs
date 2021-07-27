namespace Application.Common.Responses
{
    public class ApplicationUserResponse : BaseUserResponse
    {
        public string Token { get; set; }
    }
    public class BaseUserResponse
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
