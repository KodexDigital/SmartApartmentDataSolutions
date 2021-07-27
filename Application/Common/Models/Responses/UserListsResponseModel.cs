using Application.Common.Responses;

namespace Application.Common.Models.Responses
{
    public class UserListsResponseModel : ResponseModel
    {
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
    }
}
