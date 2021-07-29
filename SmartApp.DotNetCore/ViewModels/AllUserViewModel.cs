using SmartApp.DotNetCore.Services.Responses;
using System.Collections.Generic;

namespace SmartApp.DotNetCore.ViewModels
{
    public class AllUserViewModel
    {
        //public GetAllUserResponse Response { get; set; }
        public string Message { get; set; }
        public List<GetAllUserResponse> Data { get; set; }
    }


    public class UserData
    {
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
    }
}
