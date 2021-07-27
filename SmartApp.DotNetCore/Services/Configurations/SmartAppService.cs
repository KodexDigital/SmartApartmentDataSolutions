namespace SmartApp.DotNetCore.Services.CleintConfig
{
    public class SmartAppService
    {
        public string ClientName { get; set; }
        public string BaseUrl { get; set; }
        public string RegisterUserEndpoint { get; set; }
        public string LoginEndpoint { get; set; }
        public string GetAllUsersEndpoint { get; set; }
        public string ClientTimeOut { get; set; }
    }
}
