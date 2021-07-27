using SmartApp.DotNetCore.Services.Requests;
using SmartApp.DotNetCore.Services.Responses;
using System.Threading.Tasks;

namespace SmartApp.DotNetCore.Services.Interfaces
{
    public interface ISmartAppartmentService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
        Task<LoginUserResponse> LoginUser(LoginUserRequest request);
        Task<GetAllUserResponse> GetAllUsers();
    }
}
